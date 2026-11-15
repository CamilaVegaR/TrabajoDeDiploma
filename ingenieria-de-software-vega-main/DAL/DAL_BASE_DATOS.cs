using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace DAL
{
    // Creación automática de la base de datos (requisito de instalación en la sala).
    // El script completo de la base (tablas, stored procedures y datos iniciales)
    // está embebido en esta DLL: Scripts\BaseOlimpic_626CV.sql con
    // "Acción de compilación = Recurso incrustado".
    public class DAL_BASE_DATOS
    {
        private const string NOMBRE_SCRIPT_626CV = "BaseOlimpic_626CV.sql";

        public string ObtenerServidor_626CV()
        {
            return new SqlConnectionStringBuilder(ACCESO.CADENA_CONEXION).DataSource;
        }

        public string ObtenerNombreBase_626CV()
        {
            return new SqlConnectionStringBuilder(ACCESO.CADENA_CONEXION).InitialCatalog;
        }

        // Conexión al servidor (base master), con espera corta para no colgar el programa
        private string CadenaMaster_626CV()
        {
            SqlConnectionStringBuilder builder_626CV = new SqlConnectionStringBuilder(ACCESO.CADENA_CONEXION);
            builder_626CV.InitialCatalog = "master";
            builder_626CV.ConnectTimeout = 15;
            return builder_626CV.ConnectionString;
        }

        // Lanza SqlException si no hay conexión con el servidor
        public void ProbarServidor_626CV()
        {
            using (SqlConnection conexion_626CV = new SqlConnection(CadenaMaster_626CV()))
            {
                conexion_626CV.Open();
            }
        }

        public bool ExisteBase_626CV()
        {
            using (SqlConnection conexion_626CV = new SqlConnection(CadenaMaster_626CV()))
            using (SqlCommand cmd_626CV = new SqlCommand("SELECT DB_ID(@pNombre)", conexion_626CV))
            {
                cmd_626CV.Parameters.AddWithValue("@pNombre", ObtenerNombreBase_626CV());
                conexion_626CV.Open();
                object resultado_626CV = cmd_626CV.ExecuteScalar();
                return resultado_626CV != null && resultado_626CV != DBNull.Value;
            }
        }

        // Crea la base vacía. SQL Server ubica los archivos en su carpeta predeterminada.
        public void CrearBase_626CV()
        {
            string nombre_626CV = ObtenerNombreBase_626CV().Replace("]", "]]");

            using (SqlConnection conexion_626CV = new SqlConnection(CadenaMaster_626CV()))
            using (SqlCommand cmd_626CV = new SqlCommand("CREATE DATABASE [" + nombre_626CV + "]", conexion_626CV))
            {
                cmd_626CV.CommandTimeout = 120;
                conexion_626CV.Open();
                cmd_626CV.ExecuteNonQuery();
            }

            SqlConnection.ClearAllPools();
        }

        // true si la base ya tiene tablas propias (es decir, ya fue inicializada)
        public bool BaseTieneTablas_626CV()
        {
            using (SqlConnection conexion_626CV = new SqlConnection(ACCESO.CADENA_CONEXION))
            using (SqlCommand cmd_626CV = new SqlCommand("SELECT COUNT(*) FROM sys.tables WHERE is_ms_shipped = 0", conexion_626CV))
            {
                conexion_626CV.Open();
                return Convert.ToInt32(cmd_626CV.ExecuteScalar()) > 0;
            }
        }

        // Ejecuta el script embebido dentro de la base, lote por lote (separados por GO),
        // en una sola transacción: si algo falla no queda la base a medio crear.
        public void EjecutarScriptInicial_626CV()
        {
            List<string> lotes_626CV = DividirEnLotes_626CV(LeerScriptEmbebido_626CV());

            using (SqlConnection conexion_626CV = new SqlConnection(ACCESO.CADENA_CONEXION))
            {
                conexion_626CV.Open();
                using (SqlTransaction transaccion_626CV = conexion_626CV.BeginTransaction())
                {
                    try
                    {
                        foreach (string lote_626CV in lotes_626CV)
                        {
                            using (SqlCommand cmd_626CV = new SqlCommand(lote_626CV, conexion_626CV, transaccion_626CV))
                            {
                                cmd_626CV.CommandTimeout = 300;
                                cmd_626CV.ExecuteNonQuery();
                            }
                        }
                        transaccion_626CV.Commit();
                    }
                    catch
                    {
                        transaccion_626CV.Rollback();
                        throw;
                    }
                }
            }
        }

        private string LeerScriptEmbebido_626CV()
        {
            Assembly ensamblado_626CV = Assembly.GetExecutingAssembly();
            string recurso_626CV = ensamblado_626CV.GetManifestResourceNames()
                .FirstOrDefault(n => n.EndsWith(NOMBRE_SCRIPT_626CV, StringComparison.OrdinalIgnoreCase));

            if (recurso_626CV == null)
                throw new FileNotFoundException("No se encontró el script embebido " + NOMBRE_SCRIPT_626CV +
                    ". Verifique que en Visual Studio tenga 'Acción de compilación = Recurso incrustado'.");

            using (Stream flujo_626CV = ensamblado_626CV.GetManifestResourceStream(recurso_626CV))
            using (StreamReader lector_626CV = new StreamReader(flujo_626CV, Encoding.UTF8, true))
            {
                return lector_626CV.ReadToEnd();
            }
        }

        // Separa el script por las líneas "GO" y descarta lo que no corresponde ejecutar:
        //  - los "USE [base]": el script siempre corre sobre la base de la cadena de conexión;
        //  - los lotes que trabajan sobre la base entera o sobre usuarios de Windows
        //    (ver EsLoteDeBaseOUsuario_626CV). Así funciona aunque el script se haya generado
        //    en SSMS con "CREATE DATABASE" incluido.
        private List<string> DividirEnLotes_626CV(string script_626CV)
        {
            string[] partes_626CV = Regex.Split(script_626CV, @"^\s*GO\s*$", RegexOptions.Multiline | RegexOptions.IgnoreCase);
            List<string> lotes_626CV = new List<string>();

            foreach (string parte_626CV in partes_626CV)
            {
                string lote_626CV = Regex.Replace(parte_626CV, @"^\s*USE\s+\[[^\]]+\]\s*;?\s*$", "", RegexOptions.Multiline | RegexOptions.IgnoreCase).Trim();
                if (lote_626CV.Length == 0 || EsLoteDeBaseOUsuario_626CV(lote_626CV))
                    continue;

                lotes_626CV.Add(lote_626CV);
            }

            return lotes_626CV;
        }

        // true para los lotes que no deben ejecutarse dentro de la base:
        //  - CREATE DATABASE / ALTER DATABASE (la base ya la creó CrearBase_626CV
        //    y SQL Server no los permite dentro de una transacción);
        //  - opciones de la base que SSMS escribe con su nombre original
        //    (full-text y vardecimal);
        //  - logins, usuarios y roles de la PC donde se generó el script
        //    (en otra PC ese usuario de Windows no existe y el script fallaría).
        private bool EsLoteDeBaseOUsuario_626CV(string lote_626CV)
        {
            // Se quitan los comentarios del principio (/****** Object: ... ******/ y --)
            string inicio_626CV = Regex.Replace(lote_626CV, @"\A(\s*(/\*.*?\*/|--[^\n]*))*\s*", "", RegexOptions.Singleline);

            if (Regex.IsMatch(inicio_626CV,
                    @"\A(CREATE\s+DATABASE|ALTER\s+DATABASE|CREATE\s+LOGIN|CREATE\s+USER|ALTER\s+USER|ALTER\s+ROLE|ALTER\s+AUTHORIZATION|EXEC(UTE)?\s+(\[?sys\]?\.)?\[?sp_addrolemember\]?)\b",
                    RegexOptions.IgnoreCase))
                return true;

            return lote_626CV.IndexOf("sp_fulltext_database", StringComparison.OrdinalIgnoreCase) >= 0
                || lote_626CV.IndexOf("sp_db_vardecimal_storage_format", StringComparison.OrdinalIgnoreCase) >= 0;
        }
    }
}
