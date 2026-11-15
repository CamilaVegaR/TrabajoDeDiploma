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
    }
}
