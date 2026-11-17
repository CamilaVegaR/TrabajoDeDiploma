using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BE
{
    public class HORARIO_CLASE : IIntegrable
    {
        public HORARIO_CLASE() { }

        public HORARIO_CLASE(DayOfWeek pDia_626CV, TimeSpan pHora_626CV, int pCupoMaximo_626CV)
        {
            this.dia_626CV = pDia_626CV;
            this.hora_626CV = pHora_626CV;
            this.cupoMaximo_626CV = pCupoMaximo_626CV;
        }

        private int id_626CV;
        public int Id_626CV
        {
            get { return id_626CV; }
            set { id_626CV = value; }
        }

        private DayOfWeek dia_626CV;
        public DayOfWeek Dia_626CV
        {
            get { return dia_626CV; }
            set { dia_626CV = value; }
        }

        private TimeSpan hora_626CV;
        public TimeSpan Hora_626CV
        {
            get { return hora_626CV; }
            set { hora_626CV = value; }
        }

        private int cupoMaximo_626CV;
        public int CupoMaximo_626CV
        {
            get { return cupoMaximo_626CV; }
            set { cupoMaximo_626CV = value; }
        }

        public string DVH { get; set; }

    }
}