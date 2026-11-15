using BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL_HORARIO_CLASE
    {
        private ACCESO acceso;

        public List<HORARIO_CLASE> ListarHorarios_626CV()
        {
            acceso = new ACCESO();
            acceso.Abrir();
            List<HORARIO_CLASE> horarios_626CV = new List<HORARIO_CLASE>();

            SqlDataReader lector = acceso.Leer("SP_HORARIO_CLASE_LISTAR_626CV");

            if (lector != null)
            {
                while (lector.Read())
                {
                    HORARIO_CLASE horario_626CV = new HORARIO_CLASE
                    {
                        Id_626CV = int.Parse(lector["ID_626CV"].ToString()),
                        Dia_626CV = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), lector["DIA_626CV"].ToString()),
                        Hora_626CV = TimeSpan.Parse(lector["HORA_626CV"].ToString()),
                        CupoMaximo_626CV = int.Parse(lector["CUPO_MAXIMO_626CV"].ToString()),
                        DVH = lector["DVH"].ToString()
                    };
                    horarios_626CV.Add(horario_626CV);
                }
                lector.Close();
            }

            acceso.Cerrar();
            return horarios_626CV;
        }

        public int InsertarHorario_626CV(HORARIO_CLASE horario_626CV)
        {
            acceso = new ACCESO();
            acceso.Abrir();
            List<SqlParameter> parametros_626CV = new List<SqlParameter>();

            parametros_626CV.Add(acceso.CrearParametro("@pDia_626CV", horario_626CV.Dia_626CV.ToString()));
            parametros_626CV.Add(acceso.CrearParametro("@pHora_626CV", horario_626CV.Hora_626CV));
            parametros_626CV.Add(acceso.CrearParametro("@pCupoMaximo_626CV", horario_626CV.CupoMaximo_626CV));

            int id_626CV = acceso.LeerEscalar("SP_HORARIO_CLASE_INS_626CV", parametros_626CV);
            acceso.Cerrar();
            return id_626CV;
        }

        public HORARIO_CLASE ObtenerPorId_626CV(int id_626CV)
        {
            acceso = new ACCESO();
            acceso.Abrir();
            HORARIO_CLASE horario_626CV = null;
            List<SqlParameter> parametros_626CV = new List<SqlParameter>();
            parametros_626CV.Add(acceso.CrearParametro("@pId_626CV", id_626CV));
            DataTable tabla_626CV = acceso.Leer("SP_HORARIO_CLASE_OBTENER_POR_ID_626CV", parametros_626CV);
            bool existe_626CV = tabla_626CV?.Rows?.Count > 0;
            acceso.Cerrar();
            if (existe_626CV)
            {
                var fila_626CV = tabla_626CV.Rows[0];
                horario_626CV = new HORARIO_CLASE
                {
                    Id_626CV = int.Parse(fila_626CV["ID_626CV"].ToString()),
                    Dia_626CV = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), fila_626CV["DIA_626CV"].ToString()),
                    Hora_626CV = TimeSpan.Parse(fila_626CV["HORA_626CV"].ToString()),
                    CupoMaximo_626CV = int.Parse(fila_626CV["CUPO_MAXIMO_626CV"].ToString()),
                    DVH = fila_626CV["DVH"].ToString()
                };
                return horario_626CV;
            }
            return null;
        }

        public bool ConsultarCupoDisponible_626CV(int idHorario_626CV, DateTime fechaClase_626CV, out int cupoMaximo_626CV, out int cupoOcupado_626CV)
        {
            acceso = new ACCESO();
            acceso.Abrir();
            List<SqlParameter> parametros_626CV = new List<SqlParameter>();
            parametros_626CV.Add(acceso.CrearParametro("@pIdHorario_626CV", idHorario_626CV));
            parametros_626CV.Add(acceso.CrearParametro("@pFechaClase_626CV", fechaClase_626CV));

            DataTable tabla_626CV = acceso.Leer("SP_HORARIO_CLASE_CONSULTAR_CUPO_626CV", parametros_626CV);
            acceso.Cerrar();

            cupoMaximo_626CV = 0;
            cupoOcupado_626CV = 0;

            if (tabla_626CV?.Rows?.Count > 0)
            {
                var fila_626CV = tabla_626CV.Rows[0];
                cupoMaximo_626CV = int.Parse(fila_626CV["CUPO_MAXIMO_626CV"].ToString());
                cupoOcupado_626CV = int.Parse(fila_626CV["CUPO_OCUPADO_626CV"].ToString());
            }

            return cupoOcupado_626CV < cupoMaximo_626CV;
        }

        public int ActualizarDVH_626CV(HORARIO_CLASE horario_626CV)
        {
            acceso = new ACCESO();
            acceso.Abrir();
            List<SqlParameter> parametros_626CV = new List<SqlParameter>();
            parametros_626CV.Add(acceso.CrearParametro("@pId_626CV", horario_626CV.Id_626CV));
            parametros_626CV.Add(acceso.CrearParametro("@pDVH_626CV", horario_626CV.DVH));
            int filas_626CV = acceso.Escribir("SP_HORARIO_CLASE_UPD_DVH_626CV", parametros_626CV);
            acceso.Cerrar();
            return filas_626CV;
        }
    }
}

