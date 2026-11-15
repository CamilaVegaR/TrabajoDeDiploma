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
        public class DAL_CLIENTE
        {
            private ACCESO acceso;

            public List<CLIENTE> ListarClientes_626CV()
            {
                acceso = new ACCESO();
                acceso.Abrir();
                List<CLIENTE> clientes_626CV = new List<CLIENTE>();

                SqlDataReader lector = acceso.Leer("SP_CLIENTE_LISTAR_626CV");

                if (lector != null)
                {
                    while (lector.Read())
                    {
                        CLIENTE cliente_626CV = new CLIENTE
                        {
                            Id_626CV = int.Parse(lector["ID_626CV"].ToString()),
                            Nombre_626CV = lector["NOMBRE_626CV"].ToString(),
                            Apellido_626CV = lector["APELLIDO_626CV"].ToString(),
                            Dni_626CV = lector["DNI_626CV"].ToString(),
                            Telefono_626CV = lector["TELEFONO_626CV"].ToString(),
                            Email_626CV = lector["EMAIL_626CV"].ToString(),
                            SaldoClases_626CV = int.Parse(lector["SALDO_CLASES_626CV"].ToString()),
                            DVH = lector["DVH"].ToString()
                        };
                        clientes_626CV.Add(cliente_626CV);
                    }
                    lector.Close();
                }

                acceso.Cerrar();
                return clientes_626CV;
            }

            public int InsertarCliente_626CV(CLIENTE cliente_626CV)
            {
                acceso = new ACCESO();
                acceso.Abrir();
                List<SqlParameter> parametros_626CV = new List<SqlParameter>();

                parametros_626CV.Add(acceso.CrearParametro("@pNombre_626CV", cliente_626CV.Nombre_626CV));
                parametros_626CV.Add(acceso.CrearParametro("@pApellido_626CV", cliente_626CV.Apellido_626CV));
                parametros_626CV.Add(acceso.CrearParametro("@pDni_626CV", cliente_626CV.Dni_626CV));
                parametros_626CV.Add(acceso.CrearParametro("@pTelefono_626CV", cliente_626CV.Telefono_626CV));
                parametros_626CV.Add(acceso.CrearParametro("@pEmail_626CV", cliente_626CV.Email_626CV));

                int id_626CV = acceso.LeerEscalar("SP_CLIENTE_INS_626CV", parametros_626CV);
                acceso.Cerrar();
                return id_626CV;
            }

            public CLIENTE ObtenerPorId_626CV(int id_626CV)
            {
                acceso = new ACCESO();
                acceso.Abrir();
                CLIENTE cliente_626CV = null;
                List<SqlParameter> parametros_626CV = new List<SqlParameter>();
                parametros_626CV.Add(acceso.CrearParametro("@pId_626CV", id_626CV));
                DataTable tabla_626CV = acceso.Leer("SP_CLIENTE_OBTENER_POR_ID_626CV", parametros_626CV);
                bool existe_626CV = tabla_626CV?.Rows?.Count > 0;
                acceso.Cerrar();
                if (existe_626CV)
                {
                    var fila_626CV = tabla_626CV.Rows[0];
                    cliente_626CV = new CLIENTE
                    {
                        Id_626CV = int.Parse(fila_626CV["ID_626CV"].ToString()),
                        Nombre_626CV = fila_626CV["NOMBRE_626CV"].ToString(),
                        Apellido_626CV = fila_626CV["APELLIDO_626CV"].ToString(),
                        Dni_626CV = fila_626CV["DNI_626CV"].ToString(),
                        Telefono_626CV = fila_626CV["TELEFONO_626CV"].ToString(),
                        Email_626CV = fila_626CV["EMAIL_626CV"].ToString(),
                        SaldoClases_626CV = int.Parse(fila_626CV["SALDO_CLASES_626CV"].ToString()),
                        DVH = fila_626CV["DVH"].ToString()
                    };
                    return cliente_626CV;
                }
                return null;
            }

            public CLIENTE ObtenerPorDni_626CV(string dni_626CV)
            {
                acceso = new ACCESO();
                acceso.Abrir();
                CLIENTE cliente_626CV = null;
                List<SqlParameter> parametros_626CV = new List<SqlParameter>();
                parametros_626CV.Add(acceso.CrearParametro("@pDni_626CV", dni_626CV));
                DataTable tabla_626CV = acceso.Leer("SP_CLIENTE_OBTENER_POR_DNI_626CV", parametros_626CV);
                bool existe_626CV = tabla_626CV?.Rows?.Count > 0;
                acceso.Cerrar();
                if (existe_626CV)
                {
                    var fila_626CV = tabla_626CV.Rows[0];
                    cliente_626CV = new CLIENTE
                    {
                        Id_626CV = int.Parse(fila_626CV["ID_626CV"].ToString()),
                        Nombre_626CV = fila_626CV["NOMBRE_626CV"].ToString(),
                        Apellido_626CV = fila_626CV["APELLIDO_626CV"].ToString(),
                        Dni_626CV = fila_626CV["DNI_626CV"].ToString(),
                        Telefono_626CV = fila_626CV["TELEFONO_626CV"].ToString(),
                        Email_626CV = fila_626CV["EMAIL_626CV"].ToString(),
                        SaldoClases_626CV = int.Parse(fila_626CV["SALDO_CLASES_626CV"].ToString()),
                        DVH = fila_626CV["DVH"].ToString()
                    };
                    return cliente_626CV;
                }
                return null;
            }

            public int ActualizarSaldo_626CV(int id_626CV, int cantidad_626CV)
            {
                acceso = new ACCESO();
                acceso.Abrir();
                List<SqlParameter> parametros_626CV = new List<SqlParameter>();
                parametros_626CV.Add(acceso.CrearParametro("@pId_626CV", id_626CV));
                parametros_626CV.Add(acceso.CrearParametro("@pCantidad_626CV", cantidad_626CV));
                int filas_626CV = acceso.Escribir("SP_CLIENTE_ACTUALIZAR_SALDO_626CV", parametros_626CV);
                acceso.Cerrar();
                return filas_626CV;
            }

            public int ActualizarDVH_626CV(CLIENTE cliente_626CV)
            {
                acceso = new ACCESO();
                acceso.Abrir();
                List<SqlParameter> parametros_626CV = new List<SqlParameter>();
                parametros_626CV.Add(acceso.CrearParametro("@pId_626CV", cliente_626CV.Id_626CV));
                parametros_626CV.Add(acceso.CrearParametro("@pDVH_626CV", cliente_626CV.DVH));
                int filas_626CV = acceso.Escribir("SP_CLIENTE_UPD_DVH_626CV", parametros_626CV);
                acceso.Cerrar();
                return filas_626CV;
            }
        }
    }

