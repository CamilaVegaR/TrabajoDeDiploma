using System;
using System.Collections.Generic;

namespace BE
{
    public abstract class Componente : IIntegrable
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public abstract bool TienePermiso(string nombrePermiso);

        public virtual void Agregar(Componente c)
        {
            throw new InvalidOperationException("No se pueden agregar elementos a una hoja.");
        }

        public virtual void Remover(Componente c)
        {
            throw new InvalidOperationException("No se pueden remover elementos de una hoja.");
        }

        public virtual IList<Componente> ObtenerHijos()
        {
            return new List<Componente>();
        }
        public string DVH { get; set; }
    }
}