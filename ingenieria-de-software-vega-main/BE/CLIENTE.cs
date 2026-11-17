using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BE
{
    public class CLIENTE : IIntegrable
    {
        public CLIENTE() { }

        public CLIENTE(string pNombre_626CV, string pApellido_626CV, string pDni_626CV, string pTelefono_626CV, string pEmail_626CV)
        {
            this.nombre_626CV = pNombre_626CV;
            this.apellido_626CV = pApellido_626CV;
            this.dni_626CV = pDni_626CV;
            this.telefono_626CV = pTelefono_626CV;
            this.email_626CV = pEmail_626CV;
            this.saldoClases_626CV = 0;
        }

        private int id_626CV;
        public int Id_626CV
        {
            get { return id_626CV; }
            set { id_626CV = value; }
        }

        private string nombre_626CV;
        public string Nombre_626CV
        {
            get { return nombre_626CV; }
            set { nombre_626CV = value; }
        }

        private string apellido_626CV;
        public string Apellido_626CV
        {
            get { return apellido_626CV; }
            set { apellido_626CV = value; }
        }

        private string dni_626CV;
        public string Dni_626CV
        {
            get { return dni_626CV; }
            set { dni_626CV = value; }
        }

        private string telefono_626CV;
        public string Telefono_626CV
        {
            get { return telefono_626CV; }
            set { telefono_626CV = value; }
        }

        private string email_626CV;
        public string Email_626CV
        {
            get { return email_626CV; }
            set { email_626CV = value; }
        }

        private int saldoClases_626CV;
        public int SaldoClases_626CV
        {
            get { return saldoClases_626CV; }
            set { saldoClases_626CV = value; }
        }

        public string DVH { get; set; }
    }
}
