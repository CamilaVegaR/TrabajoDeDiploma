using BE;
using DAL;
using System.Collections.Generic;

namespace BLL
{
    public class BLL_ETIQUETAS
    {
        private DAL_ETIQUETAS mapperEtiquetas = new DAL_ETIQUETAS();

        public List<BE.ETIQUETA> ListarEtiquetas()
        {
            return mapperEtiquetas.ListarEtiquetas();
        }

        public void InsertarEtiqueta(BE.ETIQUETA etiqueta)
        {
            if (etiqueta != null && !string.IsNullOrWhiteSpace(etiqueta.Formulario) && !string.IsNullOrWhiteSpace(etiqueta.Clave))
            {
                int id = mapperEtiquetas.InsertarEtiqueta(etiqueta);
                BE.ETIQUETA etiquetaInsertada = mapperEtiquetas.ObtenerPorId(id);
                etiquetaInsertada.DVH = Servicios.GESTOR_INTEGRIDAD.CalcularDVH(etiquetaInsertada);
                mapperEtiquetas.ActualizarDHV(etiquetaInsertada);
                ActualizarDVVTabla();

                BLL_TRADUCCION bLL_TRADUCCION = new BLL_TRADUCCION();
                bLL_TRADUCCION.ActualizarDVHNuevasTraduccionesEtiqueta(etiquetaInsertada);
            }
        }

        public void ActualizarDVH(BE.ETIQUETA etiqueta)
        {
            if (etiqueta != null && etiqueta.Id != 0)
            {
                mapperEtiquetas.ActualizarDHV(etiqueta);
            }
        }

        private void ActualizarDVVTabla()
        {
            List<BE.ETIQUETA> lista = ListarEtiquetas();
            string dvv = Servicios.GESTOR_INTEGRIDAD.CalcularDVV(lista);
            new BLL_DIGITO_VERIFICADOR().ActualizarDVV(new BE.DIGITO_VERIFICADOR { Tabla = "ETIQUETA", DVV = dvv });
        }
    }
}