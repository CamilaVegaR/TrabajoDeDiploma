using BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class DAL_IDIOMA
    {
        private ACCESO acceso;

        public List<IDIOMA> ListarIdiomas()
        {
            acceso = new ACCESO();
            acceso.Abrir();
            List<IDIOMA> idiomas = new List<IDIOMA>();
            SqlDataReader lector = acceso.Leer("SP_IDIOMA_LISTAR");

            if (lector != null)
            {
                while (lector.Read())
                {
                    IDIOMA idioma = new IDIOMA
                    {
                        Id = int.Parse(lector["ID"].ToString()),
                        Nombre = lector["NOMBRE"].ToString(),
                        PorDefecto = bool.Parse(lector["POR_DEFECTO"].ToString()),
                        DVH = lector["DVH"].ToString()
                    };
                    idiomas.Add(idioma);
                }
                lector.Close();
            }

            acceso.Cerrar();
            return idiomas;
        }

        public List<TRADUCCION> ObtenerTraduccionesPorIdioma(int idIdioma)
        {
            acceso = new ACCESO();
            acceso.Abrir();
            List<TRADUCCION> traducciones = new List<TRADUCCION>();
            List<SqlParameter> parametros = new List<SqlParameter>();

            parametros.Add(acceso.CrearParametro("@pIdIdioma", idIdioma));

            DataTable tabla = acceso.Leer("SP_TRADUCCION_OBTENER_POR_IDIOMA", parametros);

            if (tabla != null)
            {
                foreach (DataRow fila in tabla.Rows)
                {
                    TRADUCCION traduccion = new TRADUCCION
                    {
                        Etiqueta = new ETIQUETA
                        {
                            Id = int.Parse(fila["ID_ETIQUETA"].ToString()),
                            Clave = fila["CLAVE"].ToString(),
                            Formulario = fila["FORMULARIO"].ToString()
                        },
                        Texto = fila["TEXTO"].ToString(),
                        Idioma = new IDIOMA { Id = idIdioma }
                    };
                    traducciones.Add(traduccion);
                }
            }

            acceso.Cerrar();
            return traducciones;
        }

        public int CrearIdioma(string nombre)
        {
            acceso = new ACCESO();
            acceso.Abrir();
            List<SqlParameter> parametros = new List<SqlParameter>();

            parametros.Add(acceso.CrearParametro("@pNombre", nombre));

            int id = acceso.LeerEscalar("SP_IDIOMA_CREAR_NUEVO", parametros);
            acceso.Cerrar();

            return id;
        }

        public IDIOMA ObtenerIdiomaPorId(int id)
        {
            acceso = new ACCESO();
            acceso.Abrir();
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(acceso.CrearParametro("@pIdIdioma", id));
            DataTable tabla = acceso.Leer("SP_IDIOMA_OBTENER_POR_ID", parametros);
            IDIOMA idioma = null;
            if (tabla != null && tabla.Rows.Count > 0)
            {
                DataRow fila = tabla.Rows[0];
                idioma = new IDIOMA
                {
                    Id = int.Parse(fila["ID"].ToString()),
                    Nombre = fila["NOMBRE"].ToString(),
                    PorDefecto = bool.Parse(fila["POR_DEFECTO"].ToString()),
                    DVH = fila["DVH"].ToString()
                };
            }
            acceso.Cerrar();
            return idioma;
        }

        public int ActualizarDVH(IDIOMA idioma)
        {
            acceso = new ACCESO();
            acceso.Abrir();
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(acceso.CrearParametro("@pId", idioma.Id));
            parametros.Add(acceso.CrearParametro("@pDVH", idioma.DVH));
            int filas = acceso.Escribir("SP_IDIOMA_UPD_DVH", parametros);
            acceso.Cerrar();
            return filas;
        }
    }
}