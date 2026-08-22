using System;
using System.Collections.Generic;

namespace BE
{
    public class Familia : Componente
    {
        private IList<Componente> _hijos;

        public Familia()
        {
            _hijos = new List<Componente>();
        }

        public override bool TienePermiso(string nombrePermiso)
        {
            if (this.Nombre.Equals(nombrePermiso, StringComparison.OrdinalIgnoreCase))
                return true;

            foreach (var hijo in _hijos)
            {
                if (hijo.TienePermiso(nombrePermiso)) return true;
            }
            return false;
        }

        public override void Agregar(Componente c)
        {
            if (_hijos.Contains(c)) return;

            if (GeneraReferenciaCircular(c, this.Id))
            {
                throw new Exception($"No se puede agregar el componente '{c.Nombre}' a '{this.Nombre}' porque generaría una referencia circular infinita.");
            }

            _hijos.Add(c);
        }

        public override void Remover(Componente c)
        {
            if (_hijos.Contains(c))
            {
                _hijos.Remove(c);
            }
        }

        public override IList<Componente> ObtenerHijos()
        {
            return _hijos;
        }

        private bool GeneraReferenciaCircular(Componente componenteAAgregar, int idFamiliaDestino)
        {
            if (componenteAAgregar.Id == idFamiliaDestino) return true;

            foreach (var hijo in componenteAAgregar.ObtenerHijos())
            {
                if (GeneraReferenciaCircular(hijo, idFamiliaDestino))
                {
                    return true;
                }
            }
            return false;
        }

        private bool GeneraReferenciaCircular(Componente componenteAAgregar, int idFamiliaDestino, List<int> visitados = null)
        {
            if (visitados == null) visitados = new List<int>();

            if (componenteAAgregar.Id == idFamiliaDestino) return true;

            if (visitados.Contains(componenteAAgregar.Id)) return false;

            visitados.Add(componenteAAgregar.Id);

            foreach (var hijo in componenteAAgregar.ObtenerHijos())
            {
                if (GeneraReferenciaCircular(hijo, idFamiliaDestino, visitados))
                {
                    return true;
                }
            }
            return false;
        }
    }
}