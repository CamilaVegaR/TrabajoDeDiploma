using BE;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL_BITACORA
    {
        private ACCESO acceso;

        public int Insertar(BITACORA obj)
        {
            acceso = new ACCESO();
            acceso.Abrir();
            List<SqlParameter> parametros = new List<SqlParameter>();

            DAL_USUARIO mpUsuario = new DAL_USUARIO();

            int idUsuario = obj.Usuario?.Id != 0 ? obj.Usuario.Id : mpUsuario.Login(obj.Usuario).Id;

            parametros.Add(acceso.CrearParametro("@pIdUsuario", idUsuario));
            parametros.Add(acceso.CrearParametro("@pFechaHora", obj.FechaHora));
            parametros.Add(acceso.CrearParametro("@pActividad", obj.Accion.ToString()));
            parametros.Add(acceso.CrearParametro("@pObservacion", obj.Observacion));

            int nuevoId = acceso.LeerEscalar("SP_BITACORA_INS", parametros);

            acceso.Cerrar();
            return nuevoId;
        }

        public List<BITACORA> Listar()
        {
            acceso = new ACCESO();
            acceso.Abrir();
            List<BITACORA> bitacoras = new List<BITACORA>();
            SqlDataReader reader = acceso.Leer("SP_BITACORA_LISTAR");
            while (reader.Read())
            {
                BITACORA bitacora = new BITACORA
                {
                    Id =Convert.ToInt32(reader["ID"]),
                    Usuario = new DAL_USUARIO().ObtenerPorId(Convert.ToInt32(reader["ID_USUARIO"])),
                    FechaHora = Convert.ToDateTime(reader["FECHA_HORA"]),
                    Accion = (BITACORA_ACTIVIDAD_ENUM)Enum.Parse(typeof(BITACORA_ACTIVIDAD_ENUM), reader["ACTIVIDAD"].ToString()),
                    Observacion = reader["OBSERVACION"].ToString(),
                    DVH = reader["DVH"].ToString()
                };
                bitacoras.Add(bitacora);
            }
            acceso.Cerrar();
            return bitacoras;
        }

        public int ActualizarDVH(BITACORA obj)
        {
            acceso = new ACCESO();
            acceso.Abrir();
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(acceso.CrearParametro("@pId", obj.Id));
            parametros.Add(acceso.CrearParametro("@pDVH", obj.DVH));
            int filas = acceso.Escribir("SP_BITACORA_ACTUALIZAR_DVH", parametros);
            acceso.Cerrar();
            return filas;
        }
    }
}