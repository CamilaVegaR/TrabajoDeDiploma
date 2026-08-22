using BE;
using System.Collections.Generic;

namespace BLL
{
    public class BLL_IDIOMA
    {
        private DAL.DAL_IDIOMA mapperIdioma = new DAL.DAL_IDIOMA();

        public List<BE.IDIOMA> ListarIdiomas()
        {
            return mapperIdioma.ListarIdiomas();
        }

        public List<BE.TRADUCCION> ObtenerTraduccionesPorIdioma(int idIdioma)
        {
            return mapperIdioma.ObtenerTraduccionesPorIdioma(idIdioma);
        }

        public void CrearIdioma(string nombre)
        {
            if (!string.IsNullOrWhiteSpace(nombre))
            {
                int id = mapperIdioma.CrearIdioma(nombre);
                IDIOMA nuevo = mapperIdioma.ObtenerIdiomaPorId(id);
                nuevo.DVH = Servicios.GESTOR_INTEGRIDAD.CalcularDVH(nuevo);
                mapperIdioma.ActualizarDVH(nuevo);
                ActualizarDVVTabla();

                BLL_TRADUCCION bllTraduccion = new BLL_TRADUCCION();
                bllTraduccion.ActualizarDVHNuevasTraduccionesIdioma(nuevo);
            }
        }

        public BE.IDIOMA ObtenerIdiomaPorId(int idIdioma)
        {
            return mapperIdioma.ObtenerIdiomaPorId(idIdioma);
        }

        public void ActualizarDVH(BE.IDIOMA idioma)
        {
            if (idioma != null)
            {
                mapperIdioma.ActualizarDVH(idioma);
            }
        }

        private void ActualizarDVVTabla()
        {
            List<BE.IDIOMA> lista = ListarIdiomas();
            string dvv = Servicios.GESTOR_INTEGRIDAD.CalcularDVV(lista);
            new BLL_DIGITO_VERIFICADOR().ActualizarDVV(new BE.DIGITO_VERIFICADOR { Tabla = "IDIOMA", DVV = dvv });
        }
    }
}