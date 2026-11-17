using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BE
{
    public class RESERVA : IIntegrable
    {
        public RESERVA() { }

        public RESERVA(CLIENTE pCliente_626CV, HORARIO_CLASE pHorario_626CV, DateTime pFechaClase_626CV, CANAL_CONTACTO_ENUM pCanalContacto_626CV)
        {
            this.cliente_626CV = pCliente_626CV;
            this.horario_626CV = pHorario_626CV;
            this.fechaClase_626CV = pFechaClase_626CV;
            this.fechaReserva_626CV = DateTime.Now;
            this.estado_626CV = ESTADO_RESERVA_ENUM.ACTIVA_626CV;
            this.canalContacto_626CV = pCanalContacto_626CV;
        }

        private CANAL_CONTACTO_ENUM canalContacto_626CV;
        public CANAL_CONTACTO_ENUM CanalContacto_626CV
        {
            get { return canalContacto_626CV; }
            set { canalContacto_626CV = value; }
        }

        private int id_626CV;
        public int Id_626CV
        {
            get { return id_626CV; }
            set { id_626CV = value; }
        }

        private CLIENTE cliente_626CV;
        public CLIENTE Cliente_626CV
        {
            get { return cliente_626CV; }
            set { cliente_626CV = value; }
        }

        private HORARIO_CLASE horario_626CV;
        public HORARIO_CLASE Horario_626CV
        {
            get { return horario_626CV; }
            set { horario_626CV = value; }
        }

        private DateTime fechaClase_626CV;
        public DateTime FechaClase_626CV
        {
            get { return fechaClase_626CV; }
            set { fechaClase_626CV = value; }
        }

        private DateTime fechaReserva_626CV;
        public DateTime FechaReserva_626CV
        {
            get { return fechaReserva_626CV; }
            set { fechaReserva_626CV = value; }
        }

        private ESTADO_RESERVA_ENUM estado_626CV;
        public ESTADO_RESERVA_ENUM Estado_626CV
        {
            get { return estado_626CV; }
            set { estado_626CV = value; }
        }

        public string DVH { get; set; }
    }
}