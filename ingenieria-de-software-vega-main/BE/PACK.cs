using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class PACK : IIntegrable
    {
        public PACK() { }

        public PACK(string pNombre_626CV, int pCantidadClases_626CV, decimal pPrecioLista_626CV, decimal pPorcentajeDescuento_626CV)
        {
            this.nombre_626CV = pNombre_626CV;
            this.cantidadClases_626CV = pCantidadClases_626CV;
            this.precioLista_626CV = pPrecioLista_626CV;
            this.porcentajeDescuento_626CV = pPorcentajeDescuento_626CV;
            this.activo_626CV = true;
        }

        private int id_626CV;
        public int Id_626CV
        {
            get { return id_626CV; }
            set { id_626CV = value; }
        }

        private string nombre_626CV;
        public string Nombre_626CV
        {
            get { return nombre_626CV; }
            set { nombre_626CV = value; }
        }

        private int cantidadClases_626CV;
        public int CantidadClases_626CV
        {
            get { return cantidadClases_626CV; }
            set { cantidadClases_626CV = value; }
        }

        private decimal precioLista_626CV;
        public decimal PrecioLista_626CV
        {
            get { return precioLista_626CV; }
            set { precioLista_626CV = value; }
        }

        private decimal porcentajeDescuento_626CV;
        public decimal PorcentajeDescuento_626CV
        {
            get { return porcentajeDescuento_626CV; }
            set { porcentajeDescuento_626CV = value; }
        }

        private bool activo_626CV;
        public bool Activo_626CV
        {
            get { return activo_626CV; }
            set { activo_626CV = value; }
        }

        public string DVH { get; set; }
    }

}
