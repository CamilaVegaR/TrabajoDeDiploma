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
    public class DAL_COMPRA
    {
        private ACCESO acceso;

        // Devuelve 1 si se registró, -1 si el pack no existe o está desactivado,
        // -2 si el cliente no existe, -99 si hubo un error.
        // El importe, la cantidad y el descuento los toma la base del pack vigente.
        public int InsertarCompra_626CV(COMPRA compra_626CV, out int idCompraCreada_626CV)
        {
            acceso = new ACCESO();
            acceso.Abrir();
            List<SqlParameter> parametros_626CV = new List<SqlParameter>();
            parametros_626CV.Add(acceso.CrearParametro("@pIdCliente_626CV", compra_626CV.Cliente_626CV.Id_626CV));
            parametros_626CV.Add(acceso.CrearParametro("@pIdPack_626CV", compra_626CV.Pack_626CV.Id_626CV));
            parametros_626CV.Add(acceso.CrearParametro("@pCanalContacto_626CV", compra_626CV.CanalContacto_626CV.ToString()));
            DataTable tabla_626CV = acceso.Leer("SP_COMPRA_INS_626CV", parametros_626CV);
            acceso.Cerrar();

            idCompraCreada_626CV = 0;
            int resultado_626CV = -99;

            if (tabla_626CV != null && tabla_626CV.Rows.Count > 0)
            {
                DataRow fila_626CV = tabla_626CV.Rows[0];
                resultado_626CV = int.Parse(fila_626CV["Resultado"].ToString());

                if (fila_626CV["IdCompra"] != DBNull.Value)
                    idCompraCreada_626CV = int.Parse(fila_626CV["IdCompra"].ToString());
            }

            return resultado_626CV;
        }

        // CU06: historial de compras de un cliente, con el nombre del pack.
        public List<COMPRA> ListarPorCliente_626CV(int idCliente_626CV)
        {
            acceso = new ACCESO();
            acceso.Abrir();
            List<SqlParameter> parametros_626CV = new List<SqlParameter>();
            parametros_626CV.Add(acceso.CrearParametro("@pIdCliente_626CV", idCliente_626CV));
            DataTable tabla_626CV = acceso.Leer("SP_COMPRA_LISTAR_POR_CLIENTE_626CV", parametros_626CV);
            acceso.Cerrar();

            List<COMPRA> compras_626CV = new List<COMPRA>();
            if (tabla_626CV != null)
            {
                foreach (DataRow fila_626CV in tabla_626CV.Rows)
                {
                    COMPRA compra_626CV = MapearCompra_626CV(fila_626CV);
                    compra_626CV.Pack_626CV.Nombre_626CV = fila_626CV["NOMBRE_PACK_626CV"].ToString();
                    compras_626CV.Add(compra_626CV);
                }
            }
            return compras_626CV;
        }

        // Para el cálculo del DVV y el recálculo de dígitos verificadores.
        public List<COMPRA> ListarTodas_626CV()
        {
            acceso = new ACCESO();
            acceso.Abrir();
            DataTable tabla_626CV = acceso.Leer("SP_COMPRA_LISTAR_TODAS_626CV", null);
            acceso.Cerrar();

            List<COMPRA> compras_626CV = new List<COMPRA>();
            if (tabla_626CV != null)
            {
                foreach (DataRow fila_626CV in tabla_626CV.Rows)
                    compras_626CV.Add(MapearCompra_626CV(fila_626CV));
            }
            return compras_626CV;
        }

        public COMPRA ObtenerPorId_626CV(int id_626CV)
        {
            acceso = new ACCESO();
            acceso.Abrir();
            List<SqlParameter> parametros_626CV = new List<SqlParameter>();
            parametros_626CV.Add(acceso.CrearParametro("@pId_626CV", id_626CV));
            DataTable tabla_626CV = acceso.Leer("SP_COMPRA_OBTENER_POR_ID_626CV", parametros_626CV);
            acceso.Cerrar();

            if (tabla_626CV != null && tabla_626CV.Rows.Count > 0)
                return MapearCompra_626CV(tabla_626CV.Rows[0]);

            return null;
        }

        public int ActualizarDVH_626CV(COMPRA compra_626CV)
        {
            acceso = new ACCESO();
            acceso.Abrir();
            List<SqlParameter> parametros_626CV = new List<SqlParameter>();
            parametros_626CV.Add(acceso.CrearParametro("@pId_626CV", compra_626CV.Id_626CV));
            parametros_626CV.Add(acceso.CrearParametro("@pDVH_626CV", compra_626CV.DVH));
            int filas_626CV = acceso.Escribir("SP_COMPRA_UPD_DVH_626CV", parametros_626CV);
            acceso.Cerrar();
            return filas_626CV;
        }

        // Cliente y Pack se cargan solo con su Id, igual que en DAL_RESERVA,
        // para que el DVH se calcule siempre sobre los mismos datos.
        private COMPRA MapearCompra_626CV(DataRow fila_626CV)
        {
            return new COMPRA
            {
                Id_626CV = int.Parse(fila_626CV["ID_626CV"].ToString()),
                Cliente_626CV = new CLIENTE { Id_626CV = int.Parse(fila_626CV["ID_CLIENTE_626CV"].ToString()) },
                Pack_626CV = new PACK { Id_626CV = int.Parse(fila_626CV["ID_PACK_626CV"].ToString()) },
                FechaCompra_626CV = Convert.ToDateTime(fila_626CV["FECHA_COMPRA_626CV"]),
                CantidadClases_626CV = int.Parse(fila_626CV["CANTIDAD_CLASES_626CV"].ToString()),
                PorcentajeDescuento_626CV = Convert.ToDecimal(fila_626CV["PORCENTAJE_DESCUENTO_626CV"]),
                Importe_626CV = Convert.ToDecimal(fila_626CV["IMPORTE_626CV"]),
                CanalContacto_626CV = (CANAL_CONTACTO_ENUM)Enum.Parse(typeof(CANAL_CONTACTO_ENUM), fila_626CV["CANAL_CONTACTO_626CV"].ToString()),
                DVH = fila_626CV["DVH"] == DBNull.Value ? null : fila_626CV["DVH"].ToString()
            };
        }
    }

}
