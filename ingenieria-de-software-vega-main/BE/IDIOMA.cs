using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BE
{
    public class IDIOMA : IIntegrable
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool PorDefecto { get; set; }

        public string DVH { get; set; }
    }
}