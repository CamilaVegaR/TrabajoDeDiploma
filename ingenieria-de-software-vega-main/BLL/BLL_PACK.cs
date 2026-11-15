using BE;
using DAL;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{

    public class BLL_PACK
    {
        private DAL_PACK mapperPack_626CV = new DAL_PACK();

        public List<PACK> ListarTodos_626CV()
        {
            return mapperPack_626CV.ListarTodos_626CV();
        }

        public List<PACK> ListarActivos_626CV()
        {
            return mapperPack_626CV.ListarActivos_626CV();
        }

        public PACK ObtenerPorId_626CV(int id_626CV)
        {
            return mapperPack_626CV.ObtenerPorId_626CV(id_626CV);
        }

        // CU07
        public string CrearPack_626CV(PACK pack_626CV)
        {
            string error_626CV = ValidarPack_626CV(pack_626CV);
            if (error_626CV != null)
                return error_626CV;

            pack_626CV.Nombre_626CV = pack_626CV.Nombre_626CV.Trim();

            int idPack_626CV;
            int resultado_626CV = mapperPack_626CV.InsertarPack_626CV(pack_626CV, out idPack_626CV);

            switch (resultado_626CV)
            {
                case 1:
                    ActualizarDVHYDVVPack_626CV(idPack_626CV);
                    RegistrarBitacora_626CV(BITACORA_ACTIVIDAD_ENUM.PACK_CREADO, "Pack: " + pack_626CV.Nombre_626CV);
                    return null;
                case -1:
                    return "Ya existe un pack con el nombre ingresado.";
                default:
                    return "No se pudo crear el pack. Intente nuevamente.";
            }
        }

        // CU08
        public string ModificarPack_626CV(PACK pack_626CV)
        {
            if (pack_626CV == null || pack_626CV.Id_626CV <= 0)
                return "Debe seleccionar un pack de la grilla.";

            string error_626CV = ValidarPack_626CV(pack_626CV);
            if (error_626CV != null)
                return error_626CV;

            pack_626CV.Nombre_626CV = pack_626CV.Nombre_626CV.Trim();

            PACK packActual_626CV = mapperPack_626CV.ObtenerPorId_626CV(pack_626CV.Id_626CV);
            if (packActual_626CV == null || !packActual_626CV.Activo_626CV)
                return "No se puede modificar un pack desactivado.";

            if (packActual_626CV.Nombre_626CV == pack_626CV.Nombre_626CV
                && packActual_626CV.CantidadClases_626CV == pack_626CV.CantidadClases_626CV
                && packActual_626CV.PrecioLista_626CV == pack_626CV.PrecioLista_626CV
                && packActual_626CV.PorcentajeDescuento_626CV == pack_626CV.PorcentajeDescuento_626CV)
                return "No se realizaron cambios en el pack.";

            int resultado_626CV = mapperPack_626CV.ModificarPack_626CV(pack_626CV);

            switch (resultado_626CV)
            {
                case 1:
                    ActualizarDVHYDVVPack_626CV(pack_626CV.Id_626CV);
                    RegistrarBitacora_626CV(BITACORA_ACTIVIDAD_ENUM.PACK_MODIFICADO, "Pack: " + pack_626CV.Nombre_626CV);
                    return null;
                case -1:
                    return "Ya existe un pack con el nombre ingresado.";
                case -2:
                    return "No se puede modificar un pack desactivado.";
                default:
                    return "No se pudo modificar el pack. Intente nuevamente.";
            }
        }

        // CU09
        public string DesactivarPack_626CV(PACK pack_626CV)
        {
            if (pack_626CV == null || pack_626CV.Id_626CV <= 0)
                return "Debe seleccionar un pack de la grilla.";

            if (!pack_626CV.Activo_626CV)
                return "El pack seleccionado ya se encuentra desactivado.";

            int resultado_626CV = mapperPack_626CV.DesactivarPack_626CV(pack_626CV.Id_626CV);

            switch (resultado_626CV)
            {
                case 1:
                    ActualizarDVHYDVVPack_626CV(pack_626CV.Id_626CV);
                    RegistrarBitacora_626CV(BITACORA_ACTIVIDAD_ENUM.PACK_DESACTIVADO, "Pack: " + pack_626CV.Nombre_626CV);
                    return null;
                case -2:
                    return "El pack seleccionado ya se encuentra desactivado.";
                default:
                    return "No se pudo desactivar el pack. Intente nuevamente.";
            }
        }


        public List<PACK> ListarOrdenadosPorId_626CV()
        {
            return mapperPack_626CV.ListarTodos_626CV().OrderBy(p => p.Id_626CV).ToList();
        }

     
        public int ActualizarDVH_626CV(PACK pack_626CV)
        {
            return mapperPack_626CV.ActualizarDVH_626CV(pack_626CV);
        }

        private string ValidarPack_626CV(PACK pack_626CV)
        {
            if (pack_626CV == null || string.IsNullOrWhiteSpace(pack_626CV.Nombre_626CV))
                return "Debe completar todos los campos.";

            if (pack_626CV.Nombre_626CV.Trim().Length > 50)
                return "El nombre del pack no puede superar los 50 caracteres.";

            if (pack_626CV.CantidadClases_626CV <= 0
                || pack_626CV.PrecioLista_626CV <= 0
                || pack_626CV.PorcentajeDescuento_626CV < 0
                || pack_626CV.PorcentajeDescuento_626CV > 100)
                return "La cantidad de clases y el precio de lista deben ser mayores a cero, y el descuento debe estar entre 0 y 100.";

            return null;
        }



        public void ActualizarDVHYDVVPack_626CV(int idPack_626CV)
        {
            PACK pack_626CV = mapperPack_626CV.ObtenerPorId_626CV(idPack_626CV);
            if (pack_626CV != null)
            {
                pack_626CV.DVH = GESTOR_INTEGRIDAD.CalcularDVH(pack_626CV);
                mapperPack_626CV.ActualizarDVH_626CV(pack_626CV);
            }

            List<PACK> listaPacks_626CV = ListarOrdenadosPorId_626CV();
            string dvv_626CV = GESTOR_INTEGRIDAD.CalcularDVV(listaPacks_626CV.ConvertAll(p => (IIntegrable)p));
            new BLL_DIGITO_VERIFICADOR().ActualizarDVV(new DIGITO_VERIFICADOR { Tabla = "PACK", DVV = dvv_626CV });

            new BLL_PACK_C().ActualizarDVHYDVV_626CV(idPack_626CV);
        }

        private void RegistrarBitacora_626CV(BITACORA_ACTIVIDAD_ENUM actividad_626CV, string observacion_626CV)
        {
  
            if (observacion_626CV != null && observacion_626CV.Length > 50)
                observacion_626CV = observacion_626CV.Substring(0, 50);

            BITACORA bitacora_626CV = new BITACORA(SINGLETON_SESION.GetInstance().usuario, DateTime.Now, actividad_626CV, observacion_626CV);
            new BLL_BITACORA().Insertar(bitacora_626CV);
        }
    }

}
