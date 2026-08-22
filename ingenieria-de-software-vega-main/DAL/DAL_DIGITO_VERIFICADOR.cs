using BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class DAL_DIGITO_VERIFICADOR
    {
        private ACCESO acceso;

        public List<DIGITO_VERIFICADOR> ObtenerTodos()
        {
            acceso = new ACCESO();
            acceso.Abrir();
            List<DIGITO_VERIFICADOR> lista = new List<DIGITO_VERIFICADOR>();

            SqlDataReader reader = acceso.Leer("SP_DIGITO_VERIFICADOR_OBTENER_TODOS");
            while (reader.Read())
            {
                DIGITO_VERIFICADOR dv = new DIGITO_VERIFICADOR
                {
                    Id = int.Parse(reader["Id"].ToString()),
                    Tabla = reader["Tabla"].ToString(),
                    DVV = reader["DVV"] != DBNull.Value ? reader["DVV"].ToString() : null
                };
                lista.Add(dv);
            }

            acceso.Cerrar();
            return lista;
        }

        public DIGITO_VERIFICADOR ObtenerPorTabla(string nombreTabla)
        {
            acceso = new ACCESO();
            acceso.Abrir();
            DIGITO_VERIFICADOR dv = null;
            List<SqlParameter> parametros = new List<SqlParameter>();

            parametros.Add(acceso.CrearParametro("@pTabla", nombreTabla));

            DataTable tabla = acceso.Leer("SP_DIGITO_VERIFICADOR_OBTENER_POR_TABLA", parametros);

            if (tabla != null && tabla.Rows.Count > 0)
            {
                DataRow fila = tabla.Rows[0];
                dv = new DIGITO_VERIFICADOR
                {
                    Id = int.Parse(fila["Id"].ToString()),
                    Tabla = fila["Tabla"].ToString(),
                    DVV = fila["DVV"] != DBNull.Value ? fila["DVV"].ToString() : null
                };
            }

            acceso.Cerrar();
            return dv;
        }

        public void ActualizarDVV(DIGITO_VERIFICADOR dv)
        {
            acceso = new ACCESO();
            acceso.Abrir();
            List<SqlParameter> parametros = new List<SqlParameter>();

            parametros.Add(acceso.CrearParametro("@pTabla", dv.Tabla));
            parametros.Add(acceso.CrearParametro("@pDVV", dv.DVV != null ? dv.DVV : ""));

            acceso.Escribir("SP_DIGITO_VERIFICADOR_UPD", parametros);
            acceso.Cerrar();
        }
    }
}