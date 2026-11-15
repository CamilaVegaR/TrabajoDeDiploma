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
    public class DAL_RESERVA
    {
        private ACCESO acceso;

        public int InsertarReserva_626CV(RESERVA reserva_626CV, out int idReservaCreada_626CV)
        {
            acceso = new ACCESO();
            acceso.Abrir();
            List<SqlParameter> parametros_626CV = new List<SqlParameter>();

            parametros_626CV.Add(acceso.CrearParametro("@pIdCliente_626CV", reserva_626CV.Cliente_626CV.Id_626CV));
            parametros_626CV.Add(acceso.CrearParametro("@pIdHorario_626CV", reserva_626CV.Horario_626CV.Id_626CV));
            parametros_626CV.Add(acceso.CrearParametro("@pFechaClase_626CV", reserva_626CV.FechaClase_626CV));
            parametros_626CV.Add(acceso.CrearParametro("@pCanalContacto_626CV", reserva_626CV.CanalContacto_626CV.ToString()));
            DataTable tabla_626CV = acceso.Leer("SP_RESERVA_INS_626CV", parametros_626CV);
            acceso.Cerrar();

            idReservaCreada_626CV = 0;
            int resultado_626CV = -99;

            if (tabla_626CV != null && tabla_626CV.Rows.Count > 0)
            {
                var fila_626CV = tabla_626CV.Rows[0];
                resultado_626CV = int.Parse(fila_626CV["Resultado"].ToString());

                if (fila_626CV["IdReserva"] != DBNull.Value)
                    idReservaCreada_626CV = int.Parse(fila_626CV["IdReserva"].ToString());
            }

            return resultado_626CV;
        }
        public List<RESERVA> ListarPorClienteDetalle_626CV(int idCliente_626CV)
        {
            acceso = new ACCESO();
            acceso.Abrir();
            List<RESERVA> reservas_626CV = new List<RESERVA>();
            List<SqlParameter> parametros_626CV = new List<SqlParameter>();
            parametros_626CV.Add(acceso.CrearParametro("@pIdCliente_626CV", idCliente_626CV));

            DataTable tabla_626CV = acceso.Leer("SP_RESERVA_LISTAR_POR_CLIENTE_DETALLE_626CV", parametros_626CV);
            acceso.Cerrar();

            if (tabla_626CV != null)
            {
                foreach (DataRow fila_626CV in tabla_626CV.Rows)
                {
                    RESERVA reserva_626CV = new RESERVA
                    {
                        Id_626CV = int.Parse(fila_626CV["ID_626CV"].ToString()),
                        Cliente_626CV = new CLIENTE { Id_626CV = int.Parse(fila_626CV["ID_CLIENTE_626CV"].ToString()) },
                        Horario_626CV = new HORARIO_CLASE
                        {
                            Id_626CV = int.Parse(fila_626CV["ID_HORARIO_626CV"].ToString()),
                            Dia_626CV = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), fila_626CV["DIA_626CV"].ToString()),
                            Hora_626CV = TimeSpan.Parse(fila_626CV["HORA_626CV"].ToString())
                        },
                        FechaClase_626CV = DateTime.Parse(fila_626CV["FECHA_CLASE_626CV"].ToString()),
                        FechaReserva_626CV = DateTime.Parse(fila_626CV["FECHA_RESERVA_626CV"].ToString()),
                        Estado_626CV = (ESTADO_RESERVA_ENUM)Enum.Parse(typeof(ESTADO_RESERVA_ENUM), fila_626CV["ESTADO_626CV"].ToString())
                    };
                    reservas_626CV.Add(reserva_626CV);
                }
            }

            return reservas_626CV;
        }
        

        public List<RESERVA> ListarTodas_626CV()
        {
            acceso = new ACCESO();
            acceso.Abrir();
            List<RESERVA> reservas_626CV = new List<RESERVA>();

            SqlDataReader lector = acceso.Leer("SP_RESERVA_LISTAR_TODAS_626CV");

            if (lector != null)
            {
                while (lector.Read())
                {
                    RESERVA reserva_626CV = new RESERVA
                    {
                        Id_626CV = int.Parse(lector["ID_626CV"].ToString()),
                        Cliente_626CV = new CLIENTE { Id_626CV = int.Parse(lector["ID_CLIENTE_626CV"].ToString()) },
                        Horario_626CV = new HORARIO_CLASE { Id_626CV = int.Parse(lector["ID_HORARIO_626CV"].ToString()) },
                        FechaClase_626CV = DateTime.Parse(lector["FECHA_CLASE_626CV"].ToString()),
                        FechaReserva_626CV = DateTime.Parse(lector["FECHA_RESERVA_626CV"].ToString()),
                        Estado_626CV = (ESTADO_RESERVA_ENUM)Enum.Parse(typeof(ESTADO_RESERVA_ENUM), lector["ESTADO_626CV"].ToString()),
                        CanalContacto_626CV = (CANAL_CONTACTO_ENUM)Enum.Parse(typeof(CANAL_CONTACTO_ENUM), lector["CANAL_CONTACTO_626CV"].ToString()),   // NUEVO
                        DVH = lector["DVH"].ToString()
                    };
                    reservas_626CV.Add(reserva_626CV);
                }
                lector.Close();
            }

            acceso.Cerrar();
            return reservas_626CV;
        }
        public RESERVA ObtenerPorId_626CV(int id_626CV)
        {
            acceso = new ACCESO();
            acceso.Abrir();
            RESERVA reserva_626CV = null;
            List<SqlParameter> parametros_626CV = new List<SqlParameter>();
            parametros_626CV.Add(acceso.CrearParametro("@pId_626CV", id_626CV));
            DataTable tabla_626CV = acceso.Leer("SP_RESERVA_OBTENER_POR_ID_626CV", parametros_626CV);
            bool existe_626CV = tabla_626CV?.Rows?.Count > 0;
            acceso.Cerrar();
            if (existe_626CV)
            {
                var fila_626CV = tabla_626CV.Rows[0];
                reserva_626CV = new RESERVA
                {
                    Id_626CV = int.Parse(fila_626CV["ID_626CV"].ToString()),
                    Cliente_626CV = new CLIENTE { Id_626CV = int.Parse(fila_626CV["ID_CLIENTE_626CV"].ToString()) },
                    Horario_626CV = new HORARIO_CLASE { Id_626CV = int.Parse(fila_626CV["ID_HORARIO_626CV"].ToString()) },
                    FechaClase_626CV = DateTime.Parse(fila_626CV["FECHA_CLASE_626CV"].ToString()),
                    FechaReserva_626CV = DateTime.Parse(fila_626CV["FECHA_RESERVA_626CV"].ToString()),
                    Estado_626CV = (ESTADO_RESERVA_ENUM)Enum.Parse(typeof(ESTADO_RESERVA_ENUM), fila_626CV["ESTADO_626CV"].ToString()),
                    CanalContacto_626CV = (CANAL_CONTACTO_ENUM)Enum.Parse(typeof(CANAL_CONTACTO_ENUM), fila_626CV["CANAL_CONTACTO_626CV"].ToString()),   // NUEVO
                    DVH = fila_626CV["DVH"].ToString()
                };
                return reserva_626CV;
            }
            return null;
        }

        public int CancelarReserva_626CV(int id_626CV)
        {
            acceso = new ACCESO();
            acceso.Abrir();
            List<SqlParameter> parametros_626CV = new List<SqlParameter>();
            parametros_626CV.Add(acceso.CrearParametro("@pId_626CV", id_626CV));
            int filasCanceladas_626CV = acceso.Escribir("SP_RESERVA_CANCELAR_626CV", parametros_626CV);
            acceso.Cerrar();
            return filasCanceladas_626CV;
        }

        public int ActualizarDVH_626CV(RESERVA reserva_626CV)
        {
            acceso = new ACCESO();
            acceso.Abrir();
            List<SqlParameter> parametros_626CV = new List<SqlParameter>();
            parametros_626CV.Add(acceso.CrearParametro("@pId_626CV", reserva_626CV.Id_626CV));
            parametros_626CV.Add(acceso.CrearParametro("@pDVH_626CV", reserva_626CV.DVH));
            int filas_626CV = acceso.Escribir("SP_RESERVA_UPD_DVH_626CV", parametros_626CV);
            acceso.Cerrar();
            return filas_626CV;
        }

        public int ModificarHorarioReserva_626CV(int idReserva_626CV, int idHorarioNuevo_626CV, DateTime fechaClaseNueva_626CV)
        {
            acceso = new ACCESO();
            acceso.Abrir();
            List<SqlParameter> parametros_626CV = new List<SqlParameter>();

            parametros_626CV.Add(acceso.CrearParametro("@pIdReserva_626CV", idReserva_626CV));
            parametros_626CV.Add(acceso.CrearParametro("@pIdHorarioNuevo_626CV", idHorarioNuevo_626CV));
            parametros_626CV.Add(acceso.CrearParametro("@pFechaClaseNueva_626CV", fechaClaseNueva_626CV));

            DataTable tabla_626CV = acceso.Leer("SP_RESERVA_MODIFICAR_HORARIO_626CV", parametros_626CV);
            acceso.Cerrar();

            int resultado_626CV = -99;
            if (tabla_626CV != null && tabla_626CV.Rows.Count > 0)
                resultado_626CV = int.Parse(tabla_626CV.Rows[0]["Resultado"].ToString());

            return resultado_626CV;
        }
    }
}

