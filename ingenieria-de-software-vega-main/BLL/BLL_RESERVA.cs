using BE;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLL_RESERVA
    {
        private DAL_RESERVA mapperReserva_626CV = new DAL_RESERVA();
        private DAL_HORARIO_CLASE mapperHorario_626CV = new DAL_HORARIO_CLASE();
        private DAL_CLIENTE mapperCliente_626CV = new DAL_CLIENTE();

        public string AgendarReserva_626CV(CLIENTE cliente_626CV, HORARIO_CLASE horario_626CV, DateTime fechaClase_626CV, CANAL_CONTACTO_ENUM canalContacto_626CV)
        {
            if (cliente_626CV == null || horario_626CV == null)
                return "Debe seleccionar un cliente y un horario.";

            RESERVA nueva_626CV = new RESERVA(cliente_626CV, horario_626CV, fechaClase_626CV, canalContacto_626CV);

            int idReserva_626CV;
            int resultado_626CV = mapperReserva_626CV.InsertarReserva_626CV(nueva_626CV, out idReserva_626CV);

            switch (resultado_626CV)
            {
                case 1:
                    RESERVA reservaCreada_626CV = mapperReserva_626CV.ObtenerPorId_626CV(idReserva_626CV);
                    reservaCreada_626CV.DVH = Servicios.GESTOR_INTEGRIDAD.CalcularDVH(reservaCreada_626CV);
                    mapperReserva_626CV.ActualizarDVH_626CV(reservaCreada_626CV);
                    ActualizarDVVReserva_626CV();

                    ActualizarDVHYDVVCliente_626CV(cliente_626CV.Id_626CV);

                    return null;

                case -1:
                    return "El cliente no tiene clases disponibles.";
                case -2:
                    return "El horario seleccionado no tiene cupo disponible.";
                case -3:
                    return "El cliente ya tiene una reserva activa para ese horario y esa fecha.";
                default:
                    return "No se pudo agendar la reserva. Intente nuevamente.";
            }
        }
        public string ModificarReserva_626CV(int idReserva_626CV, HORARIO_CLASE horarioNuevo_626CV, DateTime fechaClaseNueva_626CV)
        {
            if (horarioNuevo_626CV == null)
                return "Debe seleccionar un horario.";

            int resultado_626CV = mapperReserva_626CV.ModificarHorarioReserva_626CV(
                idReserva_626CV, horarioNuevo_626CV.Id_626CV, fechaClaseNueva_626CV);

            switch (resultado_626CV)
            {
                case 1:
                    RESERVA reservaModificada_626CV = mapperReserva_626CV.ObtenerPorId_626CV(idReserva_626CV);
                    reservaModificada_626CV.DVH = Servicios.GESTOR_INTEGRIDAD.CalcularDVH(reservaModificada_626CV);
                    mapperReserva_626CV.ActualizarDVH_626CV(reservaModificada_626CV);
                    ActualizarDVVReserva_626CV();
                    return null;

                case -1:
                    return "La reserva no existe o ya no está activa.";
                case -2:
                    return "El nuevo horario no tiene cupo disponible.";
                case -3:
                    return "El cliente ya tiene una reserva activa para ese horario y esa fecha.";
                default:
                    return "No se pudo modificar la reserva. Intente nuevamente.";
            }
        }
        public string CancelarReserva_626CV(int idReserva_626CV)
        {
            RESERVA reserva_626CV = mapperReserva_626CV.ObtenerPorId_626CV(idReserva_626CV);

            if (reserva_626CV == null)
                return "La reserva no existe.";

            if (reserva_626CV.Estado_626CV != ESTADO_RESERVA_ENUM.ACTIVA_626CV)
                return "La reserva ya había sido cancelada.";

            HORARIO_CLASE horario_626CV = mapperHorario_626CV.ObtenerPorId_626CV(reserva_626CV.Horario_626CV.Id_626CV);
            DateTime fechaHoraClase_626CV = reserva_626CV.FechaClase_626CV.Date + horario_626CV.Hora_626CV;
            TimeSpan anticipacion_626CV = fechaHoraClase_626CV - DateTime.Now;

            int filasCanceladas_626CV = mapperReserva_626CV.CancelarReserva_626CV(idReserva_626CV);

            if (filasCanceladas_626CV == 0)
                return "La reserva ya había sido cancelada por otro usuario.";

            // El estado cambió a CANCELADA: hay que recalcular el DVH de la reserva antes del DVV
            RESERVA reservaCancelada_626CV = mapperReserva_626CV.ObtenerPorId_626CV(idReserva_626CV);
            reservaCancelada_626CV.DVH = Servicios.GESTOR_INTEGRIDAD.CalcularDVH(reservaCancelada_626CV);
            mapperReserva_626CV.ActualizarDVH_626CV(reservaCancelada_626CV);
            ActualizarDVVReserva_626CV();

            if (anticipacion_626CV.TotalHours >= 24)
            {
                mapperCliente_626CV.ActualizarSaldo_626CV(reserva_626CV.Cliente_626CV.Id_626CV, 1);
                ActualizarDVHYDVVCliente_626CV(reserva_626CV.Cliente_626CV.Id_626CV);
                return "Reserva cancelada. La clase fue devuelta al saldo del cliente.";
            }
            else
            {
                return "Reserva cancelada. Al ser con menos de 24hs de anticipación, la clase no fue devuelta al saldo.";
            }

        }

        public List<RESERVA> ListarPorClienteDetalle_626CV(int idCliente_626CV)
        {
            return mapperReserva_626CV.ListarPorClienteDetalle_626CV(idCliente_626CV);
        }

        private void ActualizarDVHYDVVCliente_626CV(int idCliente_626CV)
        {
            CLIENTE cliente_626CV = mapperCliente_626CV.ObtenerPorId_626CV(idCliente_626CV);
            cliente_626CV.DVH = Servicios.GESTOR_INTEGRIDAD.CalcularDVH(cliente_626CV);
            mapperCliente_626CV.ActualizarDVH_626CV(cliente_626CV);

            List<CLIENTE> listaClientes_626CV = mapperCliente_626CV.ListarClientes_626CV();
            string dvv_626CV = Servicios.GESTOR_INTEGRIDAD.CalcularDVV(listaClientes_626CV.ConvertAll(c => (BE.IIntegrable)c));
            new BLL_DIGITO_VERIFICADOR().ActualizarDVV(new BE.DIGITO_VERIFICADOR { Tabla = "CLIENTE", DVV = dvv_626CV });
        }

        private void ActualizarDVVReserva_626CV()
        {
            List<RESERVA> listaReservas_626CV = mapperReserva_626CV.ListarTodas_626CV();
            string dvv_626CV = Servicios.GESTOR_INTEGRIDAD.CalcularDVV(listaReservas_626CV.ConvertAll(r => (BE.IIntegrable)r));
            new BLL_DIGITO_VERIFICADOR().ActualizarDVV(new BE.DIGITO_VERIFICADOR { Tabla = "RESERVA", DVV = dvv_626CV });
        }

        public string ObtenerDescripcionHorario_626CV(RESERVA reserva_626CV)
        {
            if (reserva_626CV.Horario_626CV == null) return string.Empty;
            return $"{new BLL_HORARIO_CLASE().TraducirDia_626CV(reserva_626CV.Horario_626CV.Dia_626CV)} {reserva_626CV.Horario_626CV.Hora_626CV:hh\\:mm}";
        }

        public string ObtenerDescripcionFechaClase_626CV(RESERVA reserva_626CV)
        {
            return reserva_626CV.FechaClase_626CV.ToString("dd/MM/yyyy");
        }

        public string ObtenerDescripcionEstado_626CV(RESERVA reserva_626CV)
        {
            return reserva_626CV.Estado_626CV == ESTADO_RESERVA_ENUM.ACTIVA_626CV ? "Activa" : "Cancelada";
        }
    }
}
