using BE;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLL_CLIENTE
    {
        private DAL_CLIENTE mapperCliente_626CV = new DAL_CLIENTE();

        public CLIENTE ObtenerPorId_626CV(int id_626CV)
        {
            return mapperCliente_626CV.ObtenerPorId_626CV(id_626CV);
        }

        public CLIENTE ObtenerPorDni_626CV(string dni_626CV)
        {
            return mapperCliente_626CV.ObtenerPorDni_626CV(dni_626CV);
        }

        public CLIENTE RegistrarCliente_626CV(CLIENTE nuevo_626CV)
        {
            int id_626CV = mapperCliente_626CV.InsertarCliente_626CV(nuevo_626CV);
            nuevo_626CV.Id_626CV = id_626CV;

            CLIENTE guardado_626CV = mapperCliente_626CV.ObtenerPorId_626CV(id_626CV) ?? nuevo_626CV;
            guardado_626CV.DVH = Servicios.GESTOR_INTEGRIDAD.CalcularDVH(guardado_626CV);
            mapperCliente_626CV.ActualizarDVH_626CV(guardado_626CV);

            List<CLIENTE> clientes_626CV = mapperCliente_626CV.ListarClientes_626CV();
            string dvv_626CV = Servicios.GESTOR_INTEGRIDAD.CalcularDVV(clientes_626CV.ConvertAll(cl => (IIntegrable)cl));
            new BLL_DIGITO_VERIFICADOR().ActualizarDVV(new DIGITO_VERIFICADOR { Tabla = "CLIENTE", DVV = dvv_626CV });

            return guardado_626CV;
        }

    }
}
