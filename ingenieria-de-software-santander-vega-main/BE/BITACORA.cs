using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BE
{
    public class BITACORA : IIntegrable
    {
        public BITACORA()
        {
        }
        public BITACORA(USUARIO pUsuario, DateTime pFechaHora, BITACORA_ACTIVIDAD_ENUM pAccion, string pObservacion = null)
        {
            this.Usuario = pUsuario;
            this.FechaHora = pFechaHora;
            this.Accion = pAccion;
            this.Observacion = pObservacion;
        }
        public BITACORA(int pId, USUARIO pUsuario, DateTime pFechaHora, BITACORA_ACTIVIDAD_ENUM pAccion, string pObservacion = null)
        {
            this.Id = pId;
            this.Usuario = pUsuario;
            this.FechaHora = pFechaHora;
            this.Accion = pAccion;
            this.Observacion = pObservacion;
        }

        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private USUARIO usuario;

        public USUARIO Usuario
        {
            get { return usuario; }
            set { usuario = value; }
        }

        private DateTime fechaHora;
        public DateTime FechaHora
        {
            get { return fechaHora; }
            set { fechaHora = value; }
        }

        private BITACORA_ACTIVIDAD_ENUM accion;
        public BITACORA_ACTIVIDAD_ENUM Accion
        {
            get { return accion; }
            set { accion = value; }
        }

        private string observacion;

        public string Observacion
        {
            get { return observacion; }
            set { observacion = value; }
        }

        public string DVH { get; set; }
    }
}