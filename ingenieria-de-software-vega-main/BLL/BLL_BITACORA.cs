using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLL_BITACORA
    {
        DAL.DAL_BITACORA mapperLogins = new DAL.DAL_BITACORA();

        public int Insertar(BE.BITACORA obj)
        {
            obj.FechaHora = new DateTime(obj.FechaHora.Ticks - (obj.FechaHora.Ticks % TimeSpan.TicksPerSecond), obj.FechaHora.Kind);

            int nuevoId = mapperLogins.Insertar(obj);

            if (nuevoId > 0)
            {
                obj.Id = nuevoId;
                obj.DVH = Servicios.GESTOR_INTEGRIDAD.CalcularDVH(obj);
                ActualizarDVH(obj);
                ActualizarDVVTabla();
            }

            return nuevoId;

        }

        public List<BE.BITACORA> Listar()
        {
            return mapperLogins.Listar();
        }

        public int ActualizarDVH(BE.BITACORA obj)
        {
            int resultado = mapperLogins.ActualizarDVH(obj);
            return resultado;
        }


        public void ActualizarDVHUsuario(int idUsuario)
        {
            List<BE.BITACORA> lista = Listar();
            foreach (BE.BITACORA registro in lista)
            {
                if (registro.Usuario == null || registro.Usuario.Id != idUsuario) continue;

                string dvh = Servicios.GESTOR_INTEGRIDAD.CalcularDVH(registro);
                if (registro.DVH != dvh)
                {
                    registro.DVH = dvh;
                    ActualizarDVH(registro);
                }
            }
            ActualizarDVVTabla();
        }

        private void ActualizarDVVTabla()
        {
            List<BE.BITACORA> lista = Listar();
            string dvv = Servicios.GESTOR_INTEGRIDAD.CalcularDVV(lista);
            new BLL_DIGITO_VERIFICADOR().ActualizarDVV(new BE.DIGITO_VERIFICADOR { Tabla = "BITACORA", DVV = dvv });
        }
    }
}