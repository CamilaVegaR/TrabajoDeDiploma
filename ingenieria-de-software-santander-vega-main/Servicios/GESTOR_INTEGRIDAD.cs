using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using BE;

namespace Servicios
{
    public class GESTOR_INTEGRIDAD
    {
        public static string CalcularDVH(IIntegrable entidad)
        {
            Type tipo = entidad.GetType();

            var propiedades = tipo.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                  .Where(p => p.Name != "DVH")
                                  .OrderBy(p => p.Name);

            StringBuilder stringBase = new StringBuilder();

            foreach (var prop in propiedades)
            {
                var valor = prop.GetValue(entidad);

                stringBase.Append(valor != null ? valor.ToString() : "");
            }

            return ENCRIPTADOR.Encriptar(stringBase.ToString());
        }

        public static string CalcularDVV(IEnumerable<IIntegrable> listaRegistros)
        {
            if (listaRegistros == null || !listaRegistros.Any())
                return ENCRIPTADOR.Encriptar("");

            StringBuilder stringBase = new StringBuilder();

            foreach (var registro in listaRegistros)
            {
                stringBase.Append(registro.DVH);
            }

            return ENCRIPTADOR.Encriptar(stringBase.ToString());
        }
    }
}