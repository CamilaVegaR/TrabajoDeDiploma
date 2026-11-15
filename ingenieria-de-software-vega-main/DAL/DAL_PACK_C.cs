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

    public class DAL_PACK_C
    {
        private ACCESO acceso;

        public List<PACK_C> ListarTodos_626CV()
        {
            return Listar_626CV(null, null, null, null);
        }


        public List<PACK_C> Listar_626CV(int? idPack_626CV, string nombre_626CV, DateTime? fechaDesde_626CV, DateTime? fechaHasta_626CV)
        {
            acceso = new ACCESO();
            acceso.Abrir();
            List<SqlParameter> parametros_626CV = new List<SqlParameter>();
            parametros_626CV.Add(ParametroOpcional_626CV("@pIdPack_626CV", SqlDbType.Int, idPack_626CV));
            parametros_626CV.Add(ParametroOpcional_626CV("@pNombre_626CV", SqlDbType.VarChar, string.IsNullOrWhiteSpace(nombre_626CV) ? null : nombre_626CV.Trim()));
            parametros_626CV.Add(ParametroOpcional_626CV("@pFechaDesde_626CV", SqlDbType.Date, fechaDesde_626CV?.Date));
            parametros_626CV.Add(ParametroOpcional_626CV("@pFechaHasta_626CV", SqlDbType.Date, fechaHasta_626CV?.Date));
            DataTable tabla_626CV = acceso.Leer("SP_PACK_C_LISTAR_626CV", parametros_626CV);
            acceso.Cerrar();

            List<PACK_C> cambios_626CV = new List<PACK_C>();
            if (tabla_626CV != null)
            {
                foreach (DataRow fila_626CV in tabla_626CV.Rows)
                    cambios_626CV.Add(MapearCambio_626CV(fila_626CV));
            }
            return cambios_626CV;
        }

        public PACK_C ObtenerPorId_626CV(int idCambio_626CV)
        {
            acceso = new ACCESO();
            acceso.Abrir();
            List<SqlParameter> parametros_626CV = new List<SqlParameter>();
            parametros_626CV.Add(acceso.CrearParametro("@pIdCambio_626CV", idCambio_626CV));
            DataTable tabla_626CV = acceso.Leer("SP_PACK_C_OBTENER_POR_ID_626CV", parametros_626CV);
            acceso.Cerrar();

            if (tabla_626CV != null && tabla_626CV.Rows.Count > 0)
                return MapearCambio_626CV(tabla_626CV.Rows[0]);

            return null;
        }


        public int ActivarCambio_626CV(int idCambio_626CV, out int idPack_626CV)
        {
            acceso = new ACCESO();
            acceso.Abrir();
            List<SqlParameter> parametros_626CV = new List<SqlParameter>();
            parametros_626CV.Add(acceso.CrearParametro("@pIdCambio_626CV", idCambio_626CV));
            DataTable tabla_626CV = acceso.Leer("SP_PACK_C_ACTIVAR_626CV", parametros_626CV);
            acceso.Cerrar();

            idPack_626CV = 0;
            int resultado_626CV = -99;

            if (tabla_626CV != null && tabla_626CV.Rows.Count > 0)
            {
                DataRow fila_626CV = tabla_626CV.Rows[0];
                resultado_626CV = int.Parse(fila_626CV["Resultado"].ToString());

                if (fila_626CV["IdPack"] != DBNull.Value)
                    idPack_626CV = int.Parse(fila_626CV["IdPack"].ToString());
            }

            return resultado_626CV;
        }

        public int ActualizarDVH_626CV(PACK_C cambio_626CV)
        {
            acceso = new ACCESO();
            acceso.Abrir();
            List<SqlParameter> parametros_626CV = new List<SqlParameter>();
            parametros_626CV.Add(acceso.CrearParametro("@pIdCambio_626CV", cambio_626CV.IdCambio_626CV));
            parametros_626CV.Add(acceso.CrearParametro("@pDVH_626CV", cambio_626CV.DVH));
            int filas_626CV = acceso.Escribir("SP_PACK_C_UPD_DVH_626CV", parametros_626CV);
            acceso.Cerrar();
            return filas_626CV;
        }

        private SqlParameter ParametroOpcional_626CV(string nombre_626CV, SqlDbType tipo_626CV, object valor_626CV)
        {
            SqlParameter parametro_626CV = new SqlParameter(nombre_626CV, tipo_626CV);
            parametro_626CV.Value = valor_626CV ?? DBNull.Value;
            return parametro_626CV;
        }

        private PACK_C MapearCambio_626CV(DataRow fila_626CV)
        {
            return new PACK_C
            {
                IdCambio_626CV = int.Parse(fila_626CV["ID_CAMBIO_626CV"].ToString()),
                IdPack_626CV = int.Parse(fila_626CV["ID_PACK_626CV"].ToString()),
                Fecha_626CV = Convert.ToDateTime(fila_626CV["FECHA_626CV"]),
                Hora_626CV = (TimeSpan)fila_626CV["HORA_626CV"],
                Nombre_626CV = fila_626CV["NOMBRE_626CV"].ToString(),
                CantidadClases_626CV = int.Parse(fila_626CV["CANTIDAD_CLASES_626CV"].ToString()),
                PrecioLista_626CV = Convert.ToDecimal(fila_626CV["PRECIO_LISTA_626CV"]),
                PorcentajeDescuento_626CV = Convert.ToDecimal(fila_626CV["PORCENTAJE_DESCUENTO_626CV"]),
                Activo_626CV = Convert.ToBoolean(fila_626CV["ACTIVO_626CV"]),
                Act_626CV = Convert.ToBoolean(fila_626CV["ACT_626CV"]),
                DVH = fila_626CV["DVH"] == DBNull.Value ? null : fila_626CV["DVH"].ToString()
            };
        }
    }
}
