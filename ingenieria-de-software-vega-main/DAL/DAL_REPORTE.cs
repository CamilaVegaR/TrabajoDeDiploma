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
    public class DAL_REPORTE
    {
        private ACCESO acceso;

        public List<RESERVA> ListarReservasPeriodo_626CV(DateTime desde_626CV, DateTime hasta_626CV)
        {
            DataTable tabla_626CV = LeerPeriodo_626CV("SP_REPORTE_RESERVAS_626CV", desde_626CV, hasta_626CV);

            List<RESERVA> reservas_626CV = new List<RESERVA>();
            if (tabla_626CV == null) return reservas_626CV;

            foreach (DataRow fila_626CV in tabla_626CV.Rows)
            {
                reservas_626CV.Add(new RESERVA
                {
                    Id_626CV = Convert.ToInt32(fila_626CV["ID_626CV"]),
                    FechaClase_626CV = Convert.ToDateTime(fila_626CV["FECHA_CLASE_626CV"]),
                    FechaReserva_626CV = Convert.ToDateTime(fila_626CV["FECHA_RESERVA_626CV"]),
                    Estado_626CV = (ESTADO_RESERVA_ENUM)Enum.Parse(typeof(ESTADO_RESERVA_ENUM), fila_626CV["ESTADO_626CV"].ToString()),
                    CanalContacto_626CV = (CANAL_CONTACTO_ENUM)Enum.Parse(typeof(CANAL_CONTACTO_ENUM), fila_626CV["CANAL_CONTACTO_626CV"].ToString()),
                    Horario_626CV = new HORARIO_CLASE
                    {
                        Id_626CV = Convert.ToInt32(fila_626CV["ID_HORARIO_626CV"]),
                        Dia_626CV = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), fila_626CV["DIA_626CV"].ToString()),
                        Hora_626CV = TimeSpan.Parse(fila_626CV["HORA_626CV"].ToString()),
                        CupoMaximo_626CV = Convert.ToInt32(fila_626CV["CUPO_MAXIMO_626CV"])
                    },
                    Cliente_626CV = MapearCliente_626CV(fila_626CV)
                });
            }
            return reservas_626CV;
        }
        public List<COMPRA> ListarVentasPeriodo_626CV(DateTime desde_626CV, DateTime hasta_626CV)
        {
            DataTable tabla_626CV = LeerPeriodo_626CV("SP_REPORTE_VENTAS_626CV", desde_626CV, hasta_626CV);

            List<COMPRA> compras_626CV = new List<COMPRA>();
            if (tabla_626CV == null) return compras_626CV;

            foreach (DataRow fila_626CV in tabla_626CV.Rows)
            {
                compras_626CV.Add(new COMPRA
                {
                    Id_626CV = Convert.ToInt32(fila_626CV["ID_626CV"]),
                    FechaCompra_626CV = Convert.ToDateTime(fila_626CV["FECHA_COMPRA_626CV"]),
                    CantidadClases_626CV = Convert.ToInt32(fila_626CV["CANTIDAD_CLASES_626CV"]),
                    PorcentajeDescuento_626CV = Convert.ToDecimal(fila_626CV["PORCENTAJE_DESCUENTO_626CV"]),
                    Importe_626CV = Convert.ToDecimal(fila_626CV["IMPORTE_626CV"]),
                    CanalContacto_626CV = (CANAL_CONTACTO_ENUM)Enum.Parse(typeof(CANAL_CONTACTO_ENUM), fila_626CV["CANAL_CONTACTO_626CV"].ToString()),
                    Pack_626CV = new PACK
                    {
                        Id_626CV = Convert.ToInt32(fila_626CV["ID_PACK_626CV"]),
                        Nombre_626CV = fila_626CV["NOMBRE_PACK_626CV"].ToString()
                    },
                    Cliente_626CV = MapearCliente_626CV(fila_626CV)
                });
            }
            return compras_626CV;
        }

        public List<CLIENTE> ListarClientesConSaldo_626CV()
        {
            acceso = new ACCESO();
            acceso.Abrir();
            DataTable tabla_626CV = acceso.Leer("SP_REPORTE_CLIENTES_CON_SALDO_626CV", null);
            acceso.Cerrar();

            List<CLIENTE> clientes_626CV = new List<CLIENTE>();
            if (tabla_626CV == null) return clientes_626CV;

            foreach (DataRow fila_626CV in tabla_626CV.Rows)
            {
                CLIENTE cliente_626CV = MapearCliente_626CV(fila_626CV);
                cliente_626CV.Telefono_626CV = fila_626CV["TELEFONO_626CV"].ToString();
                cliente_626CV.Email_626CV = fila_626CV["EMAIL_626CV"].ToString();
                cliente_626CV.SaldoClases_626CV = Convert.ToInt32(fila_626CV["SALDO_CLASES_626CV"]);
                clientes_626CV.Add(cliente_626CV);
            }
            return clientes_626CV;
        }

        private DataTable LeerPeriodo_626CV(string sp_626CV, DateTime desde_626CV, DateTime hasta_626CV)
        {
            acceso = new ACCESO();
            acceso.Abrir();
            List<SqlParameter> parametros_626CV = new List<SqlParameter>();
            parametros_626CV.Add(acceso.CrearParametro("@pDesde_626CV", desde_626CV.Date));
            parametros_626CV.Add(acceso.CrearParametro("@pHasta_626CV", hasta_626CV.Date));
            DataTable tabla_626CV = acceso.Leer(sp_626CV, parametros_626CV);
            acceso.Cerrar();
            return tabla_626CV;
        }

        private CLIENTE MapearCliente_626CV(DataRow fila_626CV)
        {
            string columnaId_626CV = fila_626CV.Table.Columns.Contains("ID_CLIENTE_626CV") ? "ID_CLIENTE_626CV" : "ID_626CV";
            return new CLIENTE
            {
                Id_626CV = Convert.ToInt32(fila_626CV[columnaId_626CV]),
                Nombre_626CV = fila_626CV["NOMBRE_626CV"].ToString(),
                Apellido_626CV = fila_626CV["APELLIDO_626CV"].ToString(),
                Dni_626CV = fila_626CV["DNI_626CV"].ToString()
            };
        }
    }
}

