using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BE
{
    public class USUARIO : IIntegrable
    {
        public USUARIO() {
            Permisos = new List<Componente>();
            Activo = true;
        }
        public USUARIO(string pNombreUsuario, string pNombreContrasena)
        {
            this.nombreUsuario = pNombreUsuario;
            this.contrasena = pNombreContrasena;
        }

        public USUARIO(string pNombre, string pApellido, SEXO_ENUM pSexo, string pNombreUsuario, string pContrasena, bool pBloqueado = false)
        {
            this.nombre = pNombre;
            this.contrasena = pContrasena;
            this.apellido = pApellido;
            this.sexo = pSexo;
            this.nombreUsuario = pNombreUsuario;
            this.bloqueado = pBloqueado;
        }

        public USUARIO(int pId, string pNombre, string pApellido, SEXO_ENUM pSexo, string pNombreUsuario, string pContrasena, bool pBloqueado = false) {
			this.id = pId;
			this.nombre = pNombre;
			this.contrasena = pContrasena;
            this.apellido = pApellido;
            this.sexo = pSexo;
            this.nombreUsuario = pNombreUsuario;
            this.bloqueado = pBloqueado;
        }

        public USUARIO(int pId, string pNombre, string pApellido, SEXO_ENUM pSexo, string pNombreUsuario, string pContrasena, IDIOMA pIdioma, bool pActivo = true, bool pBloqueado = false)
        {
            this.id = pId;
            this.nombre = pNombre;
            this.contrasena = pContrasena;
            this.apellido = pApellido;
            this.sexo = pSexo;
            this.nombreUsuario = pNombreUsuario;
            this.bloqueado = pBloqueado;
            this.idioma = pIdioma;
        }

        private int id;

		public int Id
		{
			get { return id; }
			set { id = value; }
		}

		private string nombre;

		public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        private string apellido;

        public string Apellido
        {
            get { return apellido; }
            set { apellido = value; }
        }

        private SEXO_ENUM sexo;

        public SEXO_ENUM Sexo
        {
            get { return sexo; }
            set { sexo = value; }
        }

        private string nombreUsuario;

        public string NombreUsuario
        {
            get { return nombreUsuario; }
            set { nombreUsuario = value; }
        }

        private string contrasena;

        public string Contrasena
        {
            get { return contrasena; }
            set { contrasena = value; }
        }

        private bool bloqueado;

        public bool Bloqueado
        {
            get { return bloqueado; }
            set { bloqueado = value; }
        }

        private IList<Componente> permisos;

        public IList<Componente> Permisos
        {
            get
            {
                if (permisos == null) permisos = new List<Componente>();
                return permisos;
            }
            set { permisos = value; }
        }

        public bool ValidarPermiso(string permisoNombre)   
        {
            foreach (var componente in Permisos)
            {
                if (componente.TienePermiso(permisoNombre))
                {
                    return true;
                }
            }
            return false;
        }

        private IDIOMA idioma;

        public IDIOMA Idioma
        {
            get { return idioma; }
            set { idioma = value; }
        }

        private bool activo;

        public bool Activo
        {
            get { return activo; }
            set { activo = value; }
        }

        public override string ToString()
        {
            return NombreUsuario;
        }

        public string DVH { get; set; }
    }
}