using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class COMPRA : IIntegrable
    {
        public COMPRA() { }

        public COMPRA(CLIENTE pCliente_626CV, PACK pPack_626CV, CANAL_CONTACTO_ENUM pCanalContacto_626CV)
        {
            this.cliente_626CV = pCliente_626CV;
            this.pack_626CV = pPack_626CV;
            this.canalContacto_626CV = pCanalContacto_626CV;
            this.fechaCompra_626CV = DateTime.Now;
            this.cantidadClases_626CV = pPack_626CV.CantidadClases_626CV;
            this.porcentajeDescuento_626CV = pPack_626CV.PorcentajeDescuento_626CV;
            this.importe_626CV = pPack_626CV.CalcularPrecioFinal_626CV();
        }

        private int id_626CV;
        public int Id_626CV
        {
            get { return id_626CV; }
            set { id_626CV = value; }
        }

        private CLIENTE cliente_626CV;
        public CLIENTE Cliente_626CV
        {
            get { return cliente_626CV; }
            set { cliente_626CV = value; }
        }

        private PACK pack_626CV;
        public PACK Pack_626CV
        {
            get { return pack_626CV; }
            set { pack_626CV = value; }
        }

        private DateTime fechaCompra_626CV;
        public DateTime FechaCompra_626CV
        {
            get { return fechaCompra_626CV; }
            set { fechaCompra_626CV = value; }
        }
        private int cantidadClases_626CV;
        public int CantidadClases_626CV
        {
            get { return cantidadClases_626CV; }
            set { cantidadClases_626CV = value; }
        }

        private decimal porcentajeDescuento_626CV;
        public decimal PorcentajeDescuento_626CV
        {
            get { return porcentajeDescuento_626CV; }
            set { porcentajeDescuento_626CV = value; }
        }

        private decimal importe_626CV;
        public decimal Importe_626CV
        {
            get { return importe_626CV; }
            set { importe_626CV = value; }
        }

        private CANAL_CONTACTO_ENUM canalContacto_626CV;
        public CANAL_CONTACTO_ENUM CanalContacto_626CV
        {
            get { return canalContacto_626CV; }
            set { canalContacto_626CV = value; }
        }

        public string DVH { get; set; }
    }

}
