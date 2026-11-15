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
    public class DAL_PACK
    {

        private ACCESO acceso;

        public List<PACK> ListarTodos_626CV()
        {
            return Listar_626CV("SP_PACK_LISTAR_626CV");
        }

        public List<PACK> ListarActivos_626CV()
        {
            return Listar_626CV("SP_PACK_LISTAR_ACTIVOS_626CV");
        }

        public PACK ObtenerPorId_626CV(int id_626CV)
        {
            acceso = new ACCESO();
            acceso.Abrir();
            List<SqlParameter> parametros_626CV = new List<SqlParameter>();
            parametros_626CV.Add(acceso.CrearParametro("@pId_626CV", id_626CV));
            DataTable tabla_626CV = acceso.Leer("SP_PACK_OBTENER_POR_ID_626CV", parametros_626CV);
            acceso.Cerrar();

            if (tabla_626CV != null && tabla_626CV.Rows.Count > 0)
                return MapearPack_626CV(tabla_626CV.Rows[0]);

            return null;
        }

        // Devuelve 1 si se creó, -1 si el nombre ya existe, -99 si hubo un error.
        public int InsertarPack_626CV(PACK pack_626CV, out int idPackCreado_626CV)
        {
            acceso = new ACCESO();
            acceso.Abrir();
            List<SqlParameter> parametros_626CV = new List<SqlParameter>();
            parametros_626CV.Add(acceso.CrearParametro("@pNombre_626CV", pack_626CV.Nombre_626CV));
            parametros_626CV.Add(acceso.CrearParametro("@pCantidadClases_626CV", pack_626CV.CantidadClases_626CV));
            parametros_626CV.Add(acceso.CrearParametro("@pPrecioLista_626CV", pack_626CV.PrecioLista_626CV));
            parametros_626CV.Add(acceso.CrearParametro("@pPorcentajeDescuento_626CV", pack_626CV.PorcentajeDescuento_626CV));
            DataTable tabla_626CV = acceso.Leer("SP_PACK_INS_626CV", parametros_626CV);
            acceso.Cerrar();

            idPackCreado_626CV = 0;
            int resultado_626CV = -99;

            if (tabla_626CV != null && tabla_626CV.Rows.Count > 0)
            {
                DataRow fila_626CV = tabla_626CV.Rows[0];
                resultado_626CV = int.Parse(fila_626CV["Resultado"].ToString());

                if (fila_626CV["IdPack"] != DBNull.Value)
                    idPackCreado_626CV = int.Parse(fila_626CV["IdPack"].ToString());
            }

            return resultado_626CV;
        }

        // Devuelve 1 si se modificó, -1 si el nombre lo usa otro pack,
        // -2 si el pack no existe o está desactivado, -99 si hubo un error.
        public int ModificarPack_626CV(PACK pack_626CV)
        {
            acceso = new ACCESO();
            acceso.Abrir();
            List<SqlParameter> parametros_626CV = new List<SqlParameter>();
            parametros_626CV.Add(acceso.CrearParametro("@pId_626CV", pack_626CV.Id_626CV));
            parametros_626CV.Add(acceso.CrearParametro("@pNombre_626CV", pack_626CV.Nombre_626CV));
            parametros_626CV.Add(acceso.CrearParametro("@pCantidadClases_626CV", pack_626CV.CantidadClases_626CV));
            parametros_626CV.Add(acceso.CrearParametro("@pPrecioLista_626CV", pack_626CV.PrecioLista_626CV));
            parametros_626CV.Add(acceso.CrearParametro("@pPorcentajeDescuento_626CV", pack_626CV.PorcentajeDescuento_626CV));
            DataTable tabla_626CV = acceso.Leer("SP_PACK_UPD_626CV", parametros_626CV);
            acceso.Cerrar();

            return LeerResultado_626CV(tabla_626CV);
        }

        // Devuelve 1 si se desactivó, -2 si no existe o ya estaba desactivado, -99 si hubo un error.
        public int DesactivarPack_626CV(int id_626CV)
        {
            acceso = new ACCESO();
            acceso.Abrir();
            List<SqlParameter> parametros_626CV = new List<SqlParameter>();
            parametros_626CV.Add(acceso.CrearParametro("@pId_626CV", id_626CV));
            DataTable tabla_626CV = acceso.Leer("SP_PACK_DESACTIVAR_626CV", parametros_626CV);
            acceso.Cerrar();

            return LeerResultado_626CV(tabla_626CV);
        }

        public int ActualizarDVH_626CV(PACK pack_626CV)
        {
            acceso = new ACCESO();
            acceso.Abrir();
            List<SqlParameter> parametros_626CV = new List<SqlParameter>();
            parametros_626CV.Add(acceso.CrearParametro("@pId_626CV", pack_626CV.Id_626CV));
            parametros_626CV.Add(acceso.CrearParametro("@pDVH_626CV", pack_626CV.DVH));
            int filas_626CV = acceso.Escribir("SP_PACK_UPD_DVH_626CV", parametros_626CV);
            acceso.Cerrar();
            return filas_626CV;
        }

        private List<PACK> Listar_626CV(string sp_626CV)
        {
            acceso = new ACCESO();
            acceso.Abrir();
            DataTable tabla_626CV = acceso.Leer(sp_626CV, null);
            acceso.Cerrar();

            List<PACK> packs_626CV = new List<PACK>();
            if (tabla_626CV != null)
            {
                foreach (DataRow fila_626CV in tabla_626CV.Rows)
                    packs_626CV.Add(MapearPack_626CV(fila_626CV));
            }
            return packs_626CV;
        }

        private PACK MapearPack_626CV(DataRow fila_626CV)
        {
            return new PACK
            {
                Id_626CV = int.Parse(fila_626CV["ID_626CV"].ToString()),
                Nombre_626CV = fila_626CV["NOMBRE_626CV"].ToString(),
                CantidadClases_626CV = int.Parse(fila_626CV["CANTIDAD_CLASES_626CV"].ToString()),
                PrecioLista_626CV = Convert.ToDecimal(fila_626CV["PRECIO_LISTA_626CV"]),
                PorcentajeDescuento_626CV = Convert.ToDecimal(fila_626CV["PORCENTAJE_DESCUENTO_626CV"]),
                Activo_626CV = Convert.ToBoolean(fila_626CV["ACTIVO_626CV"]),
                DVH = fila_626CV["DVH"] == DBNull.Value ? null : fila_626CV["DVH"].ToString()
            };
        }

        private int LeerResultado_626CV(DataTable tabla_626CV)
        {
            if (tabla_626CV != null && tabla_626CV.Rows.Count > 0)
                return int.Parse(tabla_626CV.Rows[0]["Resultado"].ToString());
            return -99;
        }
    }

}
