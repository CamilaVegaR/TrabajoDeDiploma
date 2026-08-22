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
    public class DAL_USUARIO
    {
        private ACCESO acceso;

        public int Insertar(USUARIO obj)
        {
            acceso = new ACCESO();
            acceso.Abrir();
            List<SqlParameter> parametros = new List<SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("@pNombre", obj.Nombre));
            parametros.Add(new System.Data.SqlClient.SqlParameter("@pApellido", obj.Apellido));
            parametros.Add(new System.Data.SqlClient.SqlParameter("@pSexo", obj.Sexo.ToString()));
            parametros.Add(new System.Data.SqlClient.SqlParameter("@pNombreUsuario", obj.NombreUsuario));
            parametros.Add(new System.Data.SqlClient.SqlParameter("@pContrasena", obj.Contrasena));

            int nuevoId = acceso.LeerEscalar("SP_USUARIO_INS", parametros);

            acceso.Cerrar();

            return nuevoId;
        }

        public USUARIO Login(USUARIO obj)
        {
            acceso = new ACCESO();
            acceso.Abrir();
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@pNombreUsuario", obj.NombreUsuario));
            parametros.Add(new SqlParameter("@pContrasena", obj.Contrasena));
            DataTable lector = acceso.Leer("SP_USUARIO_LOGIN", parametros);
            bool existe = lector?.Rows?.Count > 0;
            acceso.Cerrar();
            if (existe)
            {
                var fila = lector.Rows[0];
                BE.SEXO_ENUM sexo = (BE.SEXO_ENUM)Enum.Parse(typeof(BE.SEXO_ENUM), fila["SEXO"].ToString());
                DAL_IDIOMA dalIdioma = new DAL_IDIOMA();
                IDIOMA idioma;
                if (int.TryParse(fila["ID_IDIOMA"].ToString(), out int idIdioma))
                {
                    idioma = dalIdioma.ObtenerIdiomaPorId(idIdioma);
                }
                else
                {
                    idioma = null;
                }

                return new USUARIO
                {
                    Id = int.Parse(fila["ID"].ToString()),
                    Nombre = fila["NOMBRE"].ToString(),
                    Apellido = fila["APELLIDO"].ToString(),
                    Sexo = sexo,
                    NombreUsuario = fila["NOMBRE_USUARIO"].ToString(),
                    Contrasena = fila["CONTRASENA"].ToString(),
                    Bloqueado = bool.Parse(fila["BLOQUEADO"].ToString()),
                    Idioma = idioma
                };
            }
            return null;
        }

        public bool CheckUsuarioExistente(string nombre)
        {
            acceso = new ACCESO();
            acceso.Abrir();
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@pNombreUsuario", nombre));
            DataTable lector = acceso.Leer("SP_USUARIO_CHECK_EXISTENTE", parametros);
            bool existe = false;
            if (lector != null)
            {
                existe = lector.Rows.Count > 0 && int.Parse(lector.Rows[0]["RESULTADO"].ToString()) > 0;
            }
            acceso.Cerrar();
            return existe;
        }

        public List<USUARIO> Listar(bool listarBorrados = false)
        {
            acceso = new ACCESO();
            acceso.Abrir();
            List<USUARIO> usuarios = new List<USUARIO>();
            SqlDataReader lector = acceso.Leer("SP_USUARIO_LISTAR");
            while (lector.Read())
            {
                if (!listarBorrados && !bool.Parse(lector["ACTIVO"].ToString()))
                {
                    continue;
                }
                BE.SEXO_ENUM sexo = (BE.SEXO_ENUM)Enum.Parse(typeof(BE.SEXO_ENUM), lector["SEXO"].ToString());
                DAL_IDIOMA dalIdioma = new DAL_IDIOMA();
                BE.IDIOMA idioma = dalIdioma.ObtenerIdiomaPorId(int.Parse(lector["ID_IDIOMA"].ToString()));
                var usuario = new USUARIO
                {
                    Id = int.Parse(lector["ID"].ToString()),
                    Nombre = lector["NOMBRE"].ToString(),
                    Apellido = lector["APELLIDO"].ToString(),
                    Sexo = sexo,
                    NombreUsuario = lector["NOMBRE_USUARIO"].ToString(),
                    Contrasena = lector["CONTRASENA"].ToString(),
                    Idioma = idioma,
                    Activo = bool.Parse(lector["ACTIVO"].ToString()),
                    Bloqueado = bool.Parse(lector["BLOQUEADO"].ToString()),
                    DVH = lector["DVH"].ToString()
                };

                usuarios.Add(usuario);
            }
            acceso.Cerrar();
            return usuarios;
        }

        public USUARIO ObtenerPorId(int id)
        {
            acceso = new ACCESO();
            acceso.Abrir();
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@pIdUsuario", id));
            DataTable lector = acceso.Leer("SP_USUARIO_OBTENER_POR_ID", parametros);
            bool existe = lector?.Rows?.Count > 0;
            acceso.Cerrar();
            if (existe)
            {
                var fila = lector.Rows[0];

                BE.SEXO_ENUM sexo = (BE.SEXO_ENUM)Enum.Parse(typeof(BE.SEXO_ENUM), fila["SEXO"].ToString());
                DAL_IDIOMA dalIdioma = new DAL_IDIOMA();
                BE.IDIOMA idioma;
                if (int.TryParse(fila["ID_IDIOMA"].ToString(), out int idIdioma))
                {
                    idioma = dalIdioma.ObtenerIdiomaPorId(idIdioma);
                }
                else
                {
                    idioma = null;
                }
                USUARIO usuario = new USUARIO
                {
                    Id = int.Parse(fila["ID"].ToString()),
                    Nombre = fila["NOMBRE"].ToString(),
                    Apellido = fila["APELLIDO"].ToString(),
                    Sexo = sexo,
                    NombreUsuario = fila["NOMBRE_USUARIO"].ToString(),
                    Contrasena = fila["CONTRASENA"].ToString(),
                    Bloqueado = bool.Parse(fila["BLOQUEADO"].ToString()),
                    Idioma = idioma,
                    Activo = bool.Parse(fila["ACTIVO"].ToString()),
                    DVH = fila["DVH"].ToString()
                };
                DAL_ROLES_PERMISOS dalRolesPermisos = new DAL_ROLES_PERMISOS();
                dalRolesPermisos.CargarPermisosUsuario(usuario);
                return usuario;
            }
            return null;
        }

        public void ActualizarIdioma(int idUsuario, int idIdioma)
        {
            acceso = new ACCESO();
            acceso.Abrir();
            List<SqlParameter> parametros = new List<SqlParameter>();

            parametros.Add(acceso.CrearParametro("@pIdUsuario", idUsuario));
            parametros.Add(acceso.CrearParametro("@pIdIdioma", idIdioma));

            acceso.Escribir("SP_USUARIO_ACTUALIZAR_IDIOMA", parametros);
            acceso.Cerrar();
        }

        public void Modificar(USUARIO obj)
        {
            acceso = new ACCESO();
            acceso.Abrir();
            List<SqlParameter> parametros = new List<SqlParameter>();

            parametros.Add(acceso.CrearParametro("@pIdUsuario", obj.Id));
            parametros.Add(acceso.CrearParametro("@pNombre", obj.Nombre));
            parametros.Add(acceso.CrearParametro("@pApellido", obj.Apellido));
            parametros.Add(acceso.CrearParametro("@pSexo", obj.Sexo.ToString()));
            parametros.Add(acceso.CrearParametro("@pNombreUsuario", obj.NombreUsuario));

            int bloqueado = obj.Bloqueado ? 1 : 0;
            parametros.Add(acceso.CrearParametro("@pBloqueado", bloqueado));

            int activo = obj.Activo ? 1 : 0;
            parametros.Add(acceso.CrearParametro("@pActivo", activo));

            parametros.Add(acceso.CrearParametro("@pIdIdioma", obj.Idioma.Id));

            acceso.Escribir("SP_USUARIO_UPD", parametros);

            acceso.Cerrar();
        }

        public void BajaLogica(int idUsuario)
        {
            acceso = new ACCESO();
            acceso.Abrir();
            List<SqlParameter> parametros = new List<SqlParameter>();

            parametros.Add(acceso.CrearParametro("@pIdUsuario", idUsuario));

            acceso.Escribir("SP_USUARIO_DEL", parametros);

            acceso.Cerrar();
        }

        public void Reactivar(int idUsuario)
        {
            acceso = new ACCESO();
            acceso.Abrir();
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(acceso.CrearParametro("@pIdUsuario", idUsuario));
            acceso.Escribir("SP_USUARIO_REHABILITAR", parametros);
            acceso.Cerrar();
        }

        public int ActualizarContrasena(int idUsuario, string nuevaContrasena)
        {
            acceso = new ACCESO();
            acceso.Abrir();
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(acceso.CrearParametro("@pIdUsuario", idUsuario));
            parametros.Add(acceso.CrearParametro("@pContrasena", nuevaContrasena));
            int resultado = acceso.Escribir("SP_USUARIO_UPD_CONTRASENA", parametros);
            acceso.Cerrar();
            return resultado;
        }

        public int ActualizarDVH(USUARIO usuario)
        {
            acceso = new ACCESO();
            acceso.Abrir();
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(acceso.CrearParametro("@pId", usuario.Id));
            parametros.Add(acceso.CrearParametro("@pDVH", usuario.DVH));
            int resultado = acceso.Escribir("SP_USUARIO_UPD_DVH", parametros);
            acceso.Cerrar();
            return resultado;
        }
    }
}
