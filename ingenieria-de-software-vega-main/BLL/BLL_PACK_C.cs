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

    public class BLL_PACK_C
    {
        private DAL_PACK_C mapperPackC_626CV = new DAL_PACK_C();

        public List<PACK_C> ListarTodos_626CV()
        {
            return mapperPackC_626CV.ListarTodos_626CV();
        }

        public List<PACK_C> Filtrar_626CV(int? idPack_626CV, string nombre_626CV, DateTime? fechaDesde_626CV, DateTime? fechaHasta_626CV, out string error_626CV)
        {
            error_626CV = null;

            if (fechaDesde_626CV.HasValue && fechaHasta_626CV.HasValue && fechaDesde_626CV.Value.Date > fechaHasta_626CV.Value.Date)
            {
                error_626CV = "La fecha inicial no puede ser posterior a la fecha final.";
                return new List<PACK_C>();
            }

            return mapperPackC_626CV.Listar_626CV(idPack_626CV, nombre_626CV, fechaDesde_626CV, fechaHasta_626CV);
        }


        public string ActivarCambio_626CV(PACK_C cambio_626CV)
        {
            if (cambio_626CV == null || cambio_626CV.IdCambio_626CV <= 0)
                return "Debe seleccionar un registro de la grilla.";

            if (cambio_626CV.Act_626CV)
                return "El registro seleccionado ya es la versión activa del pack.";

            int idPack_626CV;
            int resultado_626CV = mapperPackC_626CV.ActivarCambio_626CV(cambio_626CV.IdCambio_626CV, out idPack_626CV);

            switch (resultado_626CV)
            {
                case 1:

                    new BLL_PACK().ActualizarDVHYDVVPack_626CV(idPack_626CV);
                    RegistrarBitacora_626CV("Pack " + idPack_626CV + " - versión " + cambio_626CV.IdCambio_626CV);
                    return null;
                case -1:
                    return "No se puede activar esta versión: otro pack ya usa el nombre \"" + cambio_626CV.Nombre_626CV + "\".";
                case -2:
                    return "El registro seleccionado ya no existe.";
                case -3:
                    return "El registro seleccionado ya es la versión activa del pack.";
                default:
                    return "No se pudo activar la versión seleccionada. Intente nuevamente.";
            }
        }


        public void ActualizarDVHYDVV_626CV(int idPack_626CV)
        {
            List<PACK_C> cambiosDelPack_626CV = mapperPackC_626CV.Listar_626CV(idPack_626CV, null, null, null);
            foreach (PACK_C cambio_626CV in cambiosDelPack_626CV)
            {
                string dvh_626CV = GESTOR_INTEGRIDAD.CalcularDVH(cambio_626CV);
                if (cambio_626CV.DVH != dvh_626CV)
                {
                    cambio_626CV.DVH = dvh_626CV;
                    mapperPackC_626CV.ActualizarDVH_626CV(cambio_626CV);
                }
            }

            ActualizarDVV_626CV();
        }


        public int ActualizarDVH_626CV(PACK_C cambio_626CV)
        {
            return mapperPackC_626CV.ActualizarDVH_626CV(cambio_626CV);
        }

        private void ActualizarDVV_626CV()
        {
            List<PACK_C> todos_626CV = ListarTodos_626CV();
            string dvv_626CV = GESTOR_INTEGRIDAD.CalcularDVV(todos_626CV.ConvertAll(c => (IIntegrable)c));
            new BLL_DIGITO_VERIFICADOR().ActualizarDVV(new DIGITO_VERIFICADOR { Tabla = "PACK_C", DVV = dvv_626CV });
        }

        private void RegistrarBitacora_626CV(string observacion_626CV)
        {

            if (observacion_626CV != null && observacion_626CV.Length > 50)
                observacion_626CV = observacion_626CV.Substring(0, 50);

            BITACORA bitacora_626CV = new BITACORA(SINGLETON_SESION.GetInstance().usuario, DateTime.Now,
                BITACORA_ACTIVIDAD_ENUM.PACK_VERSION_ACTIVADA, observacion_626CV);
            new BLL_BITACORA().Insertar(bitacora_626CV);
        }
    }
}