using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BE
{
    public class USUARIO_HISTORIAL : IIntegrable
    {

        private int idHistorial;

        public int IdHistorial
        {
            get { return idHistorial; }
            set { idHistorial = value; }
        }

        private USUARIO usuarioAuditado;

        public USUARIO UsuarioAuditado
        {
            get { return usuarioAuditado; }
            set { usuarioAuditado = value; }
        }

        private int version;

        public int Version
        {
            get { return version; }
            set { version = value; }
        }

        private DateTime fechaHora;

        public DateTime FechaHora
        {
            get { return fechaHora; }
            set { fechaHora = value; }
        }

        private USUARIO usuarioResponsable;

        public USUARIO UsuarioResponsable
        {
            get { return usuarioResponsable; }
            set { usuarioResponsable = value; }
        }

        private TIPO_OPERACION operacion;

        public TIPO_OPERACION Operacion
        {
            get { return operacion; }
            set { operacion = value; }
        }

        private string snapshotData;

        public string SnapshotData
        {
            get { return snapshotData; }
            set { snapshotData = value; }
        }

        public string DVH { get; set; }
    }
}