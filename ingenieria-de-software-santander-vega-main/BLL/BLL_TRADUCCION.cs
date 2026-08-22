using System;
using System.Collections.Generic;
using BE;
using DAL;

namespace BLL
{
    public class BLL_TRADUCCION
    {
        private DAL_TRADUCCION mapperTraduccion = new DAL_TRADUCCION();

        public List<TRADUCCION> listar()
        {
            return mapperTraduccion.listar();
        }

        public List<TRADUCCION> ObtenerTraduccionesPorIdioma(int idIdioma)
        {
            return mapperTraduccion.ObtenerTraduccionesPorIdioma(idIdioma);
        }

        public void ActualizarTraduccion(TRADUCCION traduccion)
        {
            if (traduccion != null && !string.IsNullOrWhiteSpace(traduccion.Etiqueta.Clave) && !string.IsNullOrWhiteSpace(traduccion.Texto))
            {
                mapperTraduccion.ActualizarTraduccion(traduccion);
                TRADUCCION traduccionModificada = mapperTraduccion.ObtenerTraduccion(traduccion.Idioma.Id, traduccion.Etiqueta.Id);
                traduccionModificada.DVH = Servicios.GESTOR_INTEGRIDAD.CalcularDVH(traduccionModificada);
                ActualizarDVH(traduccionModificada);
                ActualizarDVVTabla();
            }
        }

        public void InsertarTraduccion(TRADUCCION traduccion)
        {
            if (traduccion != null && traduccion.Idioma != null && !string.IsNullOrWhiteSpace(traduccion.Etiqueta.Clave) && !string.IsNullOrWhiteSpace(traduccion.Texto))
            {
                mapperTraduccion.InsertarTraduccion(traduccion);

                TRADUCCION traduccionInsertada = mapperTraduccion.ObtenerTraduccion(traduccion.Idioma.Id, traduccion.Etiqueta.Id);
                traduccionInsertada.DVH = Servicios.GESTOR_INTEGRIDAD.CalcularDVH(traduccionInsertada);
                ActualizarDVH(traduccionInsertada);
                ActualizarDVVTabla();
            }
        }

        public void ActualizarDVHNuevasTraduccionesIdioma(IDIOMA idioma)
        {
            List<BE.TRADUCCION> traducciones = new List<BE.TRADUCCION>();
            traducciones = ObtenerTraduccionesPorIdioma(idioma.Id);
            if (traducciones != null)
            {
                foreach (var traduccion in traducciones)
                {
                    traduccion.DVH = Servicios.GESTOR_INTEGRIDAD.CalcularDVH(traduccion);
                    mapperTraduccion.ActualizarDVH(traduccion);
                }
                ActualizarDVVTabla();
            }
        }

        public List<TRADUCCION> ObtenerTraduccionesPorEtiqueta(int idEtiqueta)
        {
            return mapperTraduccion.ObtenerTraduccionesPorEtiqueta(idEtiqueta);
        }

        public void ActualizarDVHNuevasTraduccionesEtiqueta(ETIQUETA etiqueta)
        {
            List<BE.TRADUCCION> traducciones = new List<BE.TRADUCCION>();
            traducciones = ObtenerTraduccionesPorEtiqueta(etiqueta.Id);
            if (traducciones != null)
            {
                foreach (var traduccion in traducciones)
                {
                    traduccion.DVH = Servicios.GESTOR_INTEGRIDAD.CalcularDVH(traduccion);
                    mapperTraduccion.ActualizarDVH(traduccion);
                }
                ActualizarDVVTabla();
            }
        }

        public int GuardarTraduccionesModificadas(List<TRADUCCION> traduccionesModificadas)
        {
            if (traduccionesModificadas == null || traduccionesModificadas.Count == 0)
                return 0;

            foreach (TRADUCCION traduccion in traduccionesModificadas)
            {
                if (traduccion == null)
                    throw new Exception("La traducción no puede ser nula.");

                if (traduccion.Idioma == null)
                    throw new Exception("La traducción no tiene idioma.");

                if (traduccion.Etiqueta == null)
                    throw new Exception("La traducción no tiene etiqueta.");

                if (string.IsNullOrWhiteSpace(traduccion.Etiqueta.Clave))
                    throw new Exception("La traducción no tiene clave.");

                if (traduccion.Texto == null)
                    traduccion.Texto = string.Empty;

                traduccion.DVH = Servicios.GESTOR_INTEGRIDAD.CalcularDVH(traduccion);
            }

            mapperTraduccion.GuardarTraduccionesModificadas(traduccionesModificadas);

            ActualizarDVVTabla();

            return traduccionesModificadas.Count;
        }

        public void ActualizarDVH(TRADUCCION traduccion)
        {
            if (traduccion != null)
            {
                mapperTraduccion.ActualizarDVH(traduccion);
            }
        }

        private void ActualizarDVVTabla()
        {
            List<TRADUCCION> lista = listar();
            string dvv = Servicios.GESTOR_INTEGRIDAD.CalcularDVV(lista);
            new BLL_DIGITO_VERIFICADOR().ActualizarDVV(new BE.DIGITO_VERIFICADOR { Tabla = "TRADUCCION", DVV = dvv });
        }
    }
}