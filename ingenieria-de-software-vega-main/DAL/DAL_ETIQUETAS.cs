using BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class DAL_ETIQUETAS
    {
        private ACCESO acceso;

        public List<ETIQUETA> ListarEtiquetas()
        {
            acceso = new ACCESO();
            acceso.Abrir();
            List<ETIQUETA> etiquetas = new List<ETIQUETA>();

            SqlDataReader lector = acceso.Leer("SP_ETIQUETA_LISTAR");

            if (lector != null)
            {
                while (lector.Read())
                {
                    ETIQUETA etiqueta = new ETIQUETA
                    {
                        Id = int.Parse(lector["ID"].ToString()),
                        Formulario = lector["FORMULARIO"].ToString(),
                        Clave = lector["CLAVE"].ToString(),
                        DVH = lector["DVH"].ToString()
                    };
                    etiquetas.Add(etiqueta);
                }
                lector.Close();
            }

            acceso.Cerrar();
            return etiquetas;
        }

        public int InsertarEtiqueta(ETIQUETA etiqueta)
        {
            acceso = new ACCESO();
            acceso.Abrir();
            List<SqlParameter> parametros = new List<SqlParameter>();

            parametros.Add(acceso.CrearParametro("@pFormulario", etiqueta.Formulario));
            parametros.Add(acceso.CrearParametro("@pClave", etiqueta.Clave));

            int id = acceso.LeerEscalar("SP_ETIQUETA_INS", parametros);
            acceso.Cerrar();
            return id;
        }

        public ETIQUETA ObtenerPorId(int id)
        {
            acceso = new ACCESO();
            acceso.Abrir();
            ETIQUETA etiqueta = null;
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(acceso.CrearParametro("@pId", id));
            DataTable lector = acceso.Leer("SP_ETIQUETA_OBTENER_POR_ID", parametros);
            bool existe = lector?.Rows?.Count > 0;
            acceso.Cerrar();
            if (existe)
            {
                var fila = lector.Rows[0];
                etiqueta = new ETIQUETA
                {
                    Id = int.Parse(fila["ID"].ToString()),
                    Formulario = fila["FORMULARIO"].ToString(),
                    Clave = fila["CLAVE"].ToString(),
                    DVH = fila["DVH"].ToString()
                };
                return etiqueta;
            }
            return null;
        }

        public int ActualizarDHV(ETIQUETA etiqueta)
        {
            acceso = new ACCESO();
            acceso.Abrir();
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(acceso.CrearParametro("@pId", etiqueta.Id));
            parametros.Add(acceso.CrearParametro("@pDVH", etiqueta.DVH));
            int filas = acceso.Escribir("SP_ETIQUETA_UPD_DVH", parametros);
            acceso.Cerrar();
            return filas;
        }
    }
}