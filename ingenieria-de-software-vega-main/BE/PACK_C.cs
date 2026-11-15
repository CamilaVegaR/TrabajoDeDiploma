using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{

    public class PACK_C : IIntegrable
    {
        public PACK_C() { }

        private int idCambio_626CV;
        public int IdCambio_626CV
        {
            get { return idCambio_626CV; }
            set { idCambio_626CV = value; }
        }

        private int idPack_626CV;
        public int IdPack_626CV
        {
            get { return idPack_626CV; }
            set { idPack_626CV = value; }
        }

        private DateTime fecha_626CV;
        public DateTime Fecha_626CV
        {
            get { return fecha_626CV; }
            set { fecha_626CV = value; }
        }

        private TimeSpan hora_626CV;
        public TimeSpan Hora_626CV
        {
            get { return hora_626CV; }
            set { hora_626CV = value; }
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


        private bool act_626CV;
        public bool Act_626CV
        {
            get { return act_626CV; }
            set { act_626CV = value; }
        }

        public string DVH { get; set; }

        public string ObtenerDescripcionEstado_626CV()
        {
            return Activo_626CV ? "Activo" : "Desactivado";
        }
    }
}
