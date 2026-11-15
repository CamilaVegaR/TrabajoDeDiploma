using BE;
using DAL;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    // CU05 - Registrar Compra_626CV / CU06 - Consultar Historial de Compras_626CV
    public class BLL_COMPRA
    {
        private DAL_COMPRA mapperCompra_626CV = new DAL_COMPRA();
        private DAL_CLIENTE mapperCliente_626CV = new DAL_CLIENTE();

        // CU05. Devuelve null si salió bien, o el mensaje de error para mostrar en pantalla.
        // En compraRegistrada_626CV devuelve la compra guardada (con el importe que se cobró).
        public string RegistrarCompra_626CV(CLIENTE cliente_626CV, PACK pack_626CV, CANAL_CONTACTO_ENUM canalContacto_626CV, out COMPRA compraRegistrada_626CV)
        {
            compraRegistrada_626CV = null;

            if (cliente_626CV == null || cliente_626CV.Id_626CV <= 0)
                return "Debe buscar un cliente.";

            if (pack_626CV == null)
                return "Debe seleccionar un pack.";

            COMPRA nueva_626CV = new COMPRA(cliente_626CV, pack_626CV, canalContacto_626CV);

            int idCompra_626CV;
            int resultado_626CV = mapperCompra_626CV.InsertarCompra_626CV(nueva_626CV, out idCompra_626CV);

            switch (resultado_626CV)
            {
                case 1:
                    compraRegistrada_626CV = mapperCompra_626CV.ObtenerPorId_626CV(idCompra_626CV);
                    if (compraRegistrada_626CV != null)
                    {
                        compraRegistrada_626CV.DVH = GESTOR_INTEGRIDAD.CalcularDVH(compraRegistrada_626CV);
                        mapperCompra_626CV.ActualizarDVH_626CV(compraRegistrada_626CV);
                    }
                    ActualizarDVVCompra_626CV();

                    // La compra sumó clases al saldo: se actualizan DVH y DVV de CLIENTE
                    ActualizarDVHYDVVCliente_626CV(cliente_626CV.Id_626CV);

                    RegistrarBitacora_626CV("DNI " + cliente_626CV.Dni_626CV + " - " + pack_626CV.Nombre_626CV);
                    return null;

                case -1:
                    return "El pack seleccionado ya no está disponible.";
                case -2:
                    return "El cliente no existe.";
                default:
                    return "No se pudo registrar la compra. Intente nuevamente.";
            }
        }

        // CU06
        public List<COMPRA> ListarPorCliente_626CV(int idCliente_626CV)
        {
            return mapperCompra_626CV.ListarPorCliente_626CV(idCliente_626CV);
        }

        // Para BLL_DIGITO_VERIFICADOR
        public List<COMPRA> ListarTodas_626CV()
        {
            return mapperCompra_626CV.ListarTodas_626CV();
        }

        public int ActualizarDVH_626CV(COMPRA compra_626CV)
        {
            return mapperCompra_626CV.ActualizarDVH_626CV(compra_626CV);
        }

        private void ActualizarDVVCompra_626CV()
        {
            List<COMPRA> listaCompras_626CV = mapperCompra_626CV.ListarTodas_626CV();
            string dvv_626CV = GESTOR_INTEGRIDAD.CalcularDVV(listaCompras_626CV.ConvertAll(c => (IIntegrable)c));
            new BLL_DIGITO_VERIFICADOR().ActualizarDVV(new DIGITO_VERIFICADOR { Tabla = "COMPRA", DVV = dvv_626CV });
        }

        private void ActualizarDVHYDVVCliente_626CV(int idCliente_626CV)
        {
            CLIENTE cliente_626CV = mapperCliente_626CV.ObtenerPorId_626CV(idCliente_626CV);
            cliente_626CV.DVH = GESTOR_INTEGRIDAD.CalcularDVH(cliente_626CV);
            mapperCliente_626CV.ActualizarDVH_626CV(cliente_626CV);

            List<CLIENTE> listaClientes_626CV = mapperCliente_626CV.ListarClientes_626CV();
            string dvv_626CV = GESTOR_INTEGRIDAD.CalcularDVV(listaClientes_626CV.ConvertAll(c => (IIntegrable)c));
            new BLL_DIGITO_VERIFICADOR().ActualizarDVV(new DIGITO_VERIFICADOR { Tabla = "CLIENTE", DVV = dvv_626CV });
        }

        private void RegistrarBitacora_626CV(string observacion_626CV)
        {
            // La columna OBSERVACION de BITACORA admite hasta 50 caracteres
            if (observacion_626CV != null && observacion_626CV.Length > 50)
                observacion_626CV = observacion_626CV.Substring(0, 50);

            BITACORA bitacora_626CV = new BITACORA(SINGLETON_SESION.GetInstance().usuario, DateTime.Now, BITACORA_ACTIVIDAD_ENUM.COMPRA_REGISTRADA, observacion_626CV);
            new BLL_BITACORA().Insertar(bitacora_626CV);
        }
    }

}
