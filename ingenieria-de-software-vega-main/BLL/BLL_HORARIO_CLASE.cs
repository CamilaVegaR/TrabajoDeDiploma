using BE;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLL_HORARIO_CLASE
    {
        private DAL_HORARIO_CLASE mapperHorario_626CV = new DAL_HORARIO_CLASE();

        public List<HORARIO_CLASE> ListarHorarios_626CV()
        {
            return mapperHorario_626CV.ListarHorarios_626CV();
        }

        public bool ConsultarCupoDisponible_626CV(int idHorario_626CV, DateTime fecha_626CV, out int cupoMaximo_626CV, out int cupoOcupado_626CV)
        {
            return mapperHorario_626CV.ConsultarCupoDisponible_626CV(idHorario_626CV, fecha_626CV, out cupoMaximo_626CV, out cupoOcupado_626CV);
        }

        public string TraducirDia_626CV(DayOfWeek dia_626CV)
        {
            switch (dia_626CV)
            {
                case DayOfWeek.Monday: return "Lunes";
                case DayOfWeek.Tuesday: return "Martes";
                case DayOfWeek.Wednesday: return "Miércoles";
                case DayOfWeek.Thursday: return "Jueves";
                case DayOfWeek.Friday: return "Viernes";
                case DayOfWeek.Saturday: return "Sábado";
                case DayOfWeek.Sunday: return "Domingo";
                default: return dia_626CV.ToString();
            }
        }


        public string ObtenerDescripcion_626CV(HORARIO_CLASE horario_626CV)
        {
            if (horario_626CV == null) return string.Empty;
            return $"{TraducirDia_626CV(horario_626CV.Dia_626CV)} {horario_626CV.Hora_626CV:hh\\:mm} (cupo {horario_626CV.CupoMaximo_626CV})";
        }
    }
}
