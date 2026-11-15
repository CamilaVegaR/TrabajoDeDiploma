using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL_BACKUP
    {
        private ACCESO acceso;

        public bool GenerarBackup()
        {
            acceso = new ACCESO();

            try
            {
                acceso.Abrir();

                int resultado = acceso.EjecutarProcedimiento(
                    "SP_BACKUP_GENERAR",
                    null,
                    300
                );

                acceso.Cerrar();

                return resultado >= -1;
            }
            catch
            {
                if (acceso != null)
                {
                    try
                    {
                        acceso.Cerrar();
                    }
                    catch
                    {
                    }
                }

                throw;
            }
        }

        // Busca en el historial de SQL Server (msdb) el último backup completo de la base del sistema.
        // Devuelve null si nunca se generó un backup.
        public DateTime? ObtenerUltimoBackup(out string rutaArchivo)
        {
            rutaArchivo = null;
            string nombreBase = ObtenerNombreBase();

            const string sql =
                "SELECT TOP 1 m.physical_device_name, b.backup_finish_date " +
                "FROM msdb.dbo.backupset b " +
                "INNER JOIN msdb.dbo.backupmediafamily m ON b.media_set_id = m.media_set_id " +
                "WHERE b.database_name = @pBase AND b.type = 'D' " +
                "ORDER BY b.backup_finish_date DESC";

            using (SqlConnection conexion = new SqlConnection(ObtenerCadenaMaster()))
            using (SqlCommand cmd = new SqlCommand(sql, conexion))
            {
                cmd.Parameters.AddWithValue("@pBase", nombreBase);
                conexion.Open();

                using (SqlDataReader lector = cmd.ExecuteReader())
                {
                    if (lector.Read())
                    {
                        rutaArchivo = lector["physical_device_name"].ToString();
                        return Convert.ToDateTime(lector["backup_finish_date"]);
                    }
                }
            }
            return null;
        }

        // Restaura la base del sistema desde el archivo indicado.
        // Se ejecuta desde la base master porque no se puede restaurar una base mientras se está conectado a ella.
        public void RestaurarBackup(string rutaArchivo)
        {
            string nombreBase = ObtenerNombreBase().Replace("]", "]]");

            // Se cierran las conexiones que la aplicación tiene abiertas en el pool contra la base
            SqlConnection.ClearAllPools();

            string sqlRestaurar =
                "ALTER DATABASE [" + nombreBase + "] SET SINGLE_USER WITH ROLLBACK IMMEDIATE; " +
                "RESTORE DATABASE [" + nombreBase + "] FROM DISK = @pRuta WITH REPLACE; ";

            string sqlMultiUsuario =
                "IF EXISTS (SELECT 1 FROM sys.databases WHERE name = @pBase AND user_access_desc = 'SINGLE_USER') " +
                "ALTER DATABASE [" + nombreBase + "] SET MULTI_USER;";

            using (SqlConnection conexion = new SqlConnection(ObtenerCadenaMaster()))
            {
                conexion.Open();
                try
                {
                    using (SqlCommand cmd = new SqlCommand(sqlRestaurar, conexion))
                    {
                        cmd.CommandTimeout = 300;
                        cmd.Parameters.AddWithValue("@pRuta", rutaArchivo);
                        cmd.ExecuteNonQuery();
                    }
                }
                finally
                {
                    // Pase lo que pase, la base vuelve a quedar disponible para todos
                    using (SqlCommand cmd = new SqlCommand(sqlMultiUsuario, conexion))
                    {
                        cmd.Parameters.AddWithValue("@pBase", ObtenerNombreBase());
                        cmd.ExecuteNonQuery();
                    }
                }
            }

            SqlConnection.ClearAllPools();
        }

        private string ObtenerNombreBase()
        {
            return new SqlConnectionStringBuilder(ACCESO.CADENA_CONEXION).InitialCatalog;
        }

        private string ObtenerCadenaMaster()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(ACCESO.CADENA_CONEXION);
            builder.InitialCatalog = "master";
            return builder.ConnectionString;
        }
    }
}
