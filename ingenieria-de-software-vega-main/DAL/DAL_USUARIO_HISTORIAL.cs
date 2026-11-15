using BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL_USUARIO_HISTORIAL
    {
        private ACCESO acceso;

        public DAL_USUARIO_HISTORIAL()
        {
            acceso = new ACCESO();
        }

        public int Insertar(USUARIO_HISTORIAL historial)
        {
            int id = 0;
            try
            {
                acceso.Abrir();

                int idResponsable = historial.UsuarioResponsable != null ? historial.UsuarioResponsable.Id : historial.UsuarioAuditado.Id;

                List<SqlParameter> parametros = new List<SqlParameter>
                {
                    acceso.CrearParametro("@pIdUsuarioAuditado", historial.UsuarioAuditado.Id),
                    acceso.CrearParametro("@pFechaHora", historial.FechaHora),
                    acceso.CrearParametro("@pIdUsuarioResponsable", idResponsable),
                    acceso.CrearParametro("@pOperacion", historial.Operacion.ToString()),
                    acceso.CrearParametro("@pSnapshotData", historial.SnapshotData)
                };

                id= acceso.LeerEscalar("SP_USUARIO_HISTORIAL_INS", parametros);
            }
            finally
            {
                acceso.Cerrar();
            }
            return id;
        }

        public List<USUARIO_HISTORIAL> Listar()
        {
            List<USUARIO_HISTORIAL> lista = new List<USUARIO_HISTORIAL>();
            try
            {
                acceso.Abrir();
                DataTable dt = acceso.Leer("SP_USUARIO_HISTORIAL_LISTAR_TODOS", null);
                if (dt != null && dt.Rows.Count > 0)
                {
                    DAL_USUARIO mpUsuario = new DAL_USUARIO();
                    foreach (DataRow row in dt.Rows)
                    {
                        USUARIO_HISTORIAL hist = new USUARIO_HISTORIAL();
                        hist.IdHistorial = Convert.ToInt32(row["IdHistorial"]);
                        hist.UsuarioAuditado = mpUsuario.ObtenerPorId(Convert.ToInt32(row["IdUsuarioAuditado"]));
                        hist.Version = Convert.ToInt32(row["Version"]);
                        hist.FechaHora = Convert.ToDateTime(row["FechaHora"]);
                        hist.UsuarioResponsable = mpUsuario.ObtenerPorId(Convert.ToInt32(row["IdUsuarioResponsable"]));
                        hist.Operacion = (TIPO_OPERACION)Enum.Parse(typeof(TIPO_OPERACION), row["Operacion"].ToString());
                        hist.SnapshotData = row["Snapshot_Data"].ToString();
                      
                        hist.DVH = row["DVH"].ToString();
                        lista.Add(hist);
                    }
                }
            }
            finally
            {
                acceso.Cerrar();
            }
            return lista;
        }

        public USUARIO_HISTORIAL ObtenerPorId(int idHistorial)
        {
            USUARIO_HISTORIAL historial = null;
            try
            {
                acceso.Abrir();
                List<SqlParameter> parametros = new List<SqlParameter>
                {
                    acceso.CrearParametro("@pId", idHistorial)
                };
                DataTable dt = acceso.Leer("SP_USUARIO_HISTORIAL_OBTENER_POR_ID", parametros);
                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    DAL_USUARIO mpUsuario = new DAL_USUARIO();
                    historial = new USUARIO_HISTORIAL
                    {
                        IdHistorial = Convert.ToInt32(row["IdHistorial"]),
                        UsuarioAuditado = mpUsuario.ObtenerPorId(Convert.ToInt32(row["IdUsuarioAuditado"])),
                        Version = Convert.ToInt32(row["Version"]),
                        FechaHora = Convert.ToDateTime(row["FechaHora"]),
                        UsuarioResponsable = mpUsuario.ObtenerPorId(Convert.ToInt32(row["IdUsuarioResponsable"])),
                        Operacion = (TIPO_OPERACION)Enum.Parse(typeof(TIPO_OPERACION), row["Operacion"].ToString()),
                        SnapshotData = row["Snapshot_Data"].ToString(),
                        DVH = row["DVH"].ToString()
                    };
                }
            }
            finally
            {
                acceso.Cerrar();
            }
            return historial;
        }

        public List<USUARIO_HISTORIAL> ListarPorUsuario(int idUsuario)
        {
            List<USUARIO_HISTORIAL> lista = new List<USUARIO_HISTORIAL>();

            try
            {
                acceso.Abrir();

                List<SqlParameter> parametros = new List<SqlParameter>
                {
                    acceso.CrearParametro("@pIdUsuario", idUsuario)
                };

                DataTable dt = acceso.Leer("SP_USUARIO_HISTORIAL_LISTAR", parametros);

                if (dt != null && dt.Rows.Count > 0)
                {
                    DAL_USUARIO mpUsuario = new DAL_USUARIO();
                    foreach (DataRow row in dt.Rows)
                    {
                        USUARIO_HISTORIAL hist = new USUARIO_HISTORIAL();
                        hist.IdHistorial = Convert.ToInt32(row["IdHistorial"]);
                        hist.UsuarioAuditado = mpUsuario.ObtenerPorId(Convert.ToInt32(row["IdUsuarioAuditado"]));
                        hist.Version = Convert.ToInt32(row["Version"]);
                        hist.FechaHora = Convert.ToDateTime(row["FechaHora"]);

                        hist.UsuarioResponsable = mpUsuario.ObtenerPorId(Convert.ToInt32(row["IdUsuarioResponsable"]));

                        hist.Operacion = (TIPO_OPERACION)Enum.Parse(typeof(TIPO_OPERACION), row["Operacion"].ToString());

                        hist.SnapshotData = row["Snapshot_Data"].ToString();
                        hist.DVH = row["DVH"].ToString();

                        lista.Add(hist);
                    }
                }
            }
            finally
            {
                acceso.Cerrar();
            }

            return lista;
        }

        public int ObtenerUltimaVersion(int idUsuario)
        {
            try
            {
                acceso.Abrir();
                string sql = "SELECT ISNULL(MAX(Version), 0) FROM USUARIO_HISTORIAL WHERE IdUsuarioAuditado = @pIdUsuario";
                List<SqlParameter> parametros = new List<SqlParameter>
                {
                    acceso.CrearParametro("@pIdUsuario", idUsuario)
                };

                return acceso.LeerEscalar(sql, parametros);
            }
            finally
            {
                acceso.Cerrar();
            }
        }

        public int ActualizarDVH(USUARIO_HISTORIAL historial)
        {
            try
            {
                acceso.Abrir();
                List<SqlParameter> parametros = new List<SqlParameter>
                {
                    acceso.CrearParametro("@pId", historial.IdHistorial),
                    acceso.CrearParametro("@pDVH", historial.DVH)
                };
                return acceso.Escribir("SP_USUARIO_HISTORIAL_UPD_DVH", parametros);
            }
            finally
            {
                acceso.Cerrar();
            }
        }
    }
}
