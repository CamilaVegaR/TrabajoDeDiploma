using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BE
{
    public class TRADUCCION : IIntegrable
    {
        public IDIOMA Idioma { get; set; }
        public ETIQUETA Etiqueta { get; set; }
        public string Texto { get; set; }

        public string Formulario
        {
            get { return Etiqueta != null ? Etiqueta.Formulario : string.Empty; }
        }

        public string Clave
        {
            get { return Etiqueta != null ? Etiqueta.Clave : string.Empty; }
        }

        public string DVH { get; set; }
    }
}