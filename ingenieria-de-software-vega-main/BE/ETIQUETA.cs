using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BE
{
    public class ETIQUETA : IIntegrable
    {
        public int Id { get; set; }
        public string Formulario { get; set; }
        public string Clave { get; set; }
        public string DVH { get; set; }
    }
}