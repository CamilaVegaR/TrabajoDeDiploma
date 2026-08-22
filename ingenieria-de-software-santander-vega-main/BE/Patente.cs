using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BE
{
    public class Patente : Componente
    {
        public override bool TienePermiso(string nombrePermiso)
        {
            return this.Nombre.Equals(nombrePermiso, StringComparison.OrdinalIgnoreCase);
        }
    }
}