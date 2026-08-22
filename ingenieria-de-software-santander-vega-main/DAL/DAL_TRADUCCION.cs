using BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class DAL_TRADUCCION
    {
        private ACCESO acceso;

        public List<TRADUCCION> listar()
        {
            acceso = new ACCESO();
            acceso.Abrir();

            List<TRADUCCION> traducciones = new List<TRADUCCION>();

            DataTable tabla = acceso.Leer("SP_TRADUCCION_LISTAR", null);

            acceso.Cerrar();

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
                            Formulario = fila["FORMULARIO"].ToString(),
                            DVH = fila["DVH_ETIQUETA"].ToString()
                        },
                        Texto = fila["TEXTO"].ToString(),
                        Idioma = new IDIOMA
                        {
                            Id = int.Parse(fila["ID_IDIOMA"].ToString()),
                            Nombre = fila["NOMBRE"].ToString(),
                            PorDefecto = bool.Parse(fila["POR_DEFECTO"].ToString()),
                            DVH = fila["DVH_IDIOMA"].ToString()
                        },
                        DVH = fila["DVH"].ToString()
                    };

                    traducciones.Add(traduccion);
                }
            }

            return traducciones;
        }

        public TRADUCCION ObtenerTraduccion(int idIdioma, int idEtiqueta)
        {
            acceso = new ACCESO();
            acceso.Abrir();
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(acceso.CrearParametro("@pIdIdioma", idIdioma));
            parametros.Add(acceso.CrearParametro("@pIdEtiqueta", idEtiqueta));
            DataTable tabla = acceso.Leer("SP_TRADUCCION_OBTENER_POR_IDIOMA_ETIQUETA", parametros);
            acceso.Cerrar();
            if (tabla != null && tabla.Rows.Count > 0)
            {
                DataRow fila = tabla.Rows[0];
                DAL_ETIQUETAS dalEtiquetas = new DAL_ETIQUETAS();
                DAL_IDIOMA dalIdioma = new DAL_IDIOMA();
                TRADUCCION traduccion = new TRADUCCION
                {
                    Etiqueta = dalEtiquetas.ObtenerPorId(int.Parse(fila["ID_ETIQUETA"].ToString())),
                    Texto = fila["TEXTO"].ToString(),
                    Idioma = dalIdioma.ObtenerIdiomaPorId(int.Parse(fila["ID_IDIOMA"].ToString())),
                    DVH = fila["DVH"].ToString()
                };
                return traduccion;
            }
            return null;
        }

        public List<TRADUCCION> ObtenerTraduccionesPorIdioma(int idIdioma)
        {
            acceso = new ACCESO();
            acceso.Abrir();

            List<TRADUCCION> traducciones = new List<TRADUCCION>();

            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(acceso.CrearParametro("@pIdIdioma", idIdioma));

            DataTable tabla = acceso.Leer("SP_TRADUCCION_OBTENER_POR_IDIOMA", parametros);

            acceso.Cerrar();

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
                            Formulario = fila["FORMULARIO"].ToString(),
                            DVH = fila["DVH_ETIQUETA"].ToString()
                        },
                        Texto = fila["TEXTO"].ToString(),
                        Idioma = new IDIOMA
                        {
                            Id = int.Parse(fila["ID_IDIOMA"].ToString()),
                            Nombre = fila["NOMBRE"].ToString(),
                            PorDefecto = bool.Parse(fila["POR_DEFECTO"].ToString()),
                            DVH = fila["DVH_IDIOMA"].ToString()
                        },
                        DVH = fila["DVH"].ToString()
                    };

                    traducciones.Add(traduccion);
                }
            }

            return traducciones;
        }

        public List<TRADUCCION> ObtenerTraduccionesPorEtiqueta(int idEtiqueta)
        {
            acceso = new ACCESO();
            acceso.Abrir();
            List<TRADUCCION> traducciones = new List<TRADUCCION>();
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(acceso.CrearParametro("@pIdEtiqueta", idEtiqueta));
            DataTable tabla = acceso.Leer("SP_TRADUCCION_OBTENER_POR_ETIQUETA", parametros);
            if (tabla != null)
            {
                foreach (DataRow fila in tabla.Rows)
                {
                    DAL_ETIQUETAS dalEtiqueta = new DAL_ETIQUETAS();
                    ETIQUETA etiqueta = dalEtiqueta.ObtenerPorId(idEtiqueta);
                    TRADUCCION traduccion = new TRADUCCION
                    {
                        Etiqueta = etiqueta,
                        Texto = fila["TEXTO"].ToString(),
                        Idioma = new IDIOMA
                        {
                            Id = int.Parse(fila["ID_IDIOMA"].ToString()),
                            Nombre = fila["NOMBRE"].ToString(),
                            PorDefecto = bool.Parse(fila["POR_DEFECTO"].ToString())
                        }
                    };
                    traducciones.Add(traduccion);
                }
            }
            acceso.Cerrar();
            return traducciones;
        }

        public void ActualizarTraduccion(TRADUCCION traduccion)
        {
            acceso = new ACCESO();
            acceso.Abrir();
            List<SqlParameter> parametros = new List<SqlParameter>();

            parametros.Add(acceso.CrearParametro("@pIdIdioma", traduccion.Idioma.Id));
            parametros.Add(acceso.CrearParametro("@pIdEtiqueta", traduccion.Etiqueta.Id));
            parametros.Add(acceso.CrearParametro("@pTexto", traduccion.Texto));

            acceso.Escribir("SP_TRADUCCION_UPD", parametros);
            acceso.Cerrar();
        }

        public void InsertarTraduccion(TRADUCCION traduccion)
        {
            acceso = new ACCESO();
            acceso.Abrir();
            List<SqlParameter> parametros = new List<SqlParameter>();

            parametros.Add(acceso.CrearParametro("@pIdIdioma", traduccion.Idioma.Id));
            parametros.Add(acceso.CrearParametro("@pIdEtiqueta", traduccion.Etiqueta.Id));
            parametros.Add(acceso.CrearParametro("@pTexto", traduccion.Texto));

            acceso.Escribir("SP_TRADUCCION_INS", parametros);
            acceso.Cerrar();
        }

        public void GuardarTraduccionesModificadas(List<TRADUCCION> traducciones)
        {
            if (traducciones == null || traducciones.Count == 0)
                return;

            acceso = new ACCESO();
            acceso.Abrir();

            try
            {
                foreach (TRADUCCION traduccion in traducciones)
                {
                    List<SqlParameter> parametros = new List<SqlParameter>();

                    parametros.Add(acceso.CrearParametro("@pIdIdioma", traduccion.Idioma.Id));
                    parametros.Add(acceso.CrearParametro("@pIdEtiqueta", traduccion.Etiqueta.Id));
                    parametros.Add(acceso.CrearParametro("@pTexto", traduccion.Texto));
                    parametros.Add(acceso.CrearParametro("@pDVH", traduccion.DVH));

                    acceso.Escribir("SP_TRADUCCION_GUARDAR", parametros);
                }
            }
            finally
            {
                acceso.Cerrar();
            }
        }

        public int ActualizarDVH(TRADUCCION traduccion)
        {
            acceso = new ACCESO();
            acceso.Abrir();
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(acceso.CrearParametro("@pIdIdioma", traduccion.Idioma.Id));
            parametros.Add(acceso.CrearParametro("@pIdEtiqueta", traduccion.Etiqueta.Id));
            parametros.Add(acceso.CrearParametro("@pDVH", traduccion.DVH));
            int filasAfectadas = acceso.Escribir("SP_TRADUCCION_ACTUALIZAR_DVH", parametros);
            acceso.Cerrar();
            return filasAfectadas;
        }
    }
}