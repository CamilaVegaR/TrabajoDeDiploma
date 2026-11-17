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

        public string AjustarSaldo_626CV(CLIENTE cliente_626CV, int cantidad_626CV, string motivo_626CV)
        {
            if (cliente_626CV == null || cliente_626CV.Id_626CV <= 0)
                return "Debe buscar un cliente.";

            if (cantidad_626CV == 0)
                return "La cantidad de clases debe ser distinta de cero.";

            if (string.IsNullOrWhiteSpace(motivo_626CV))
                return "Debe ingresar el motivo del ajuste.";

            motivo_626CV = motivo_626CV.Trim();
            if (motivo_626CV.Length > 30)
                return "El motivo no puede superar los 30 caracteres.";

            CLIENTE actual_626CV = mapperCliente_626CV.ObtenerPorId_626CV(cliente_626CV.Id_626CV);
            if (actual_626CV == null)
                return "El cliente no existe.";

            if (actual_626CV.SaldoClases_626CV + cantidad_626CV < 0)
                return "El cliente no tiene clases suficientes para restar. Saldo actual: " + actual_626CV.SaldoClases_626CV + " clase(s).";

            int filas_626CV = mapperCliente_626CV.ActualizarSaldo_626CV(actual_626CV.Id_626CV, cantidad_626CV);
            if (filas_626CV <= 0)
                return "No se pudo ajustar el saldo. Intente nuevamente.";

            ActualizarDVHYDVVCliente_626CV(actual_626CV.Id_626CV);

            string signo_626CV = cantidad_626CV > 0 ? "+" : "";
            RegistrarBitacora_626CV("DNI " + actual_626CV.Dni_626CV + " " + signo_626CV + cantidad_626CV + ": " + motivo_626CV);

            return null;
        }

        private void ActualizarDVHYDVVCliente_626CV(int idCliente_626CV)
        {
            CLIENTE cliente_626CV = mapperCliente_626CV.ObtenerPorId_626CV(idCliente_626CV);
            cliente_626CV.DVH = Servicios.GESTOR_INTEGRIDAD.CalcularDVH(cliente_626CV);
            mapperCliente_626CV.ActualizarDVH_626CV(cliente_626CV);

            List<CLIENTE> listaClientes_626CV = mapperCliente_626CV.ListarClientes_626CV();
            string dvv_626CV = Servicios.GESTOR_INTEGRIDAD.CalcularDVV(listaClientes_626CV.ConvertAll(c => (IIntegrable)c));
            new BLL_DIGITO_VERIFICADOR().ActualizarDVV(new DIGITO_VERIFICADOR { Tabla = "CLIENTE", DVV = dvv_626CV });
        }

        private void RegistrarBitacora_626CV(string observacion_626CV)
        {
            if (observacion_626CV != null && observacion_626CV.Length > 50)
                observacion_626CV = observacion_626CV.Substring(0, 50);

            BITACORA bitacora_626CV = new BITACORA(Servicios.SINGLETON_SESION.GetInstance().usuario, DateTime.Now, BITACORA_ACTIVIDAD_ENUM.SALDO_AJUSTADO, observacion_626CV);
            new BLL_BITACORA().Insertar(bitacora_626CV);
        }
    }
}