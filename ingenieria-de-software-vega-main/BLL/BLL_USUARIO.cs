using BE;
using DAL;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLL_USUARIO
    {
        DAL.DAL_USUARIO mapperUsuario = new DAL.DAL_USUARIO();

        public int Insertar(BE.USUARIO obj)
        {
            obj.Activo = true;
            obj.Bloqueado = false;
            obj.Contrasena = ENCRIPTADOR.Encriptar(obj.Contrasena);

            int resultado = mapperUsuario.Insertar(obj);

            if (resultado > 0)
            {
                USUARIO usuarioInsertado = mapperUsuario.ObtenerPorId(resultado);

                usuarioInsertado.DVH = Servicios.GESTOR_INTEGRIDAD.CalcularDVH(usuarioInsertado);
                actualizarDVH(usuarioInsertado);
                ActualizarDVVTabla();
            }

            return resultado;
        }

        public void Login(BE.USUARIO obj)
        {
            if (SINGLETON_SESION.IsLoggedIn())
            {
                throw new Exception("Ya hay un usuario logueado. Por favor, cierre sesión antes de intentar iniciar sesión con otro usuario.");
            }

            obj.Contrasena = ENCRIPTADOR.Encriptar(obj.Contrasena);
            USUARIO usuario = mapperUsuario.Login(obj);

            if (usuario != null)
            {
                BLL_ROLES_PERMISOS bllRoles = new BLL_ROLES_PERMISOS();
                bllRoles.CargarPermisosUsuario(usuario);

                SINGLETON_SESION.login(usuario);
            }
        }

        public USUARIO logout()
        {
            if (!SINGLETON_SESION.IsLoggedIn())
            {
                throw new Exception("No hay ningún usuario logueado.");
            }
            USUARIO usuario = SINGLETON_SESION.GetInstance().usuario;
            SINGLETON_SESION.logout();
            return usuario;
        }

        public List<BE.USUARIO> Listar(bool listarBorrados = false)
        {
            return mapperUsuario.Listar(listarBorrados);
        }

        public bool CheckUsuarioExistente(string nombre)
        {
            return mapperUsuario.CheckUsuarioExistente(nombre);
        }

        public void ActualizarIdioma(int idUsuario, int idIdioma)
        {
            mapperUsuario.ActualizarIdioma(idUsuario, idIdioma);
            ActualizarDVVTabla();
        }

        public void ModificarUsuario(USUARIO usuarioModificado, USUARIO responsable)
        {
            USUARIO usuarioAnterior = mapperUsuario.ObtenerPorId(usuarioModificado.Id);

            if (usuarioAnterior != null && usuarioAnterior.NombreUsuario != usuarioModificado.NombreUsuario)
            {
                if (mapperUsuario.CheckUsuarioExistente(usuarioModificado.NombreUsuario))
                {
                    throw new Exception("El nombre de usuario ingresado ya se encuentra en uso. Por favor, elija otro.");
                }
            }

            mapperUsuario.Modificar(usuarioModificado);
            usuarioModificado = mapperUsuario.ObtenerPorId(usuarioModificado.Id);
            usuarioModificado.DVH = Servicios.GESTOR_INTEGRIDAD.CalcularDVH(usuarioModificado);
            actualizarDVH(usuarioModificado);
            ActualizarDVVTabla();

            if (usuarioAnterior != null && usuarioAnterior.NombreUsuario != usuarioModificado.NombreUsuario)
            {
                new BLL_BITACORA().ActualizarDVHUsuario(usuarioModificado.Id);
            }

            if (usuarioModificado.Id == SINGLETON_SESION.GetInstance().usuario.Id)
            {
                DAL_ROLES_PERMISOS dalRolesPermisos = new DAL_ROLES_PERMISOS();
                dalRolesPermisos.CargarPermisosUsuario(usuarioModificado);
                SINGLETON_SESION.GetInstance().usuario = usuarioModificado;
            }
        }

        public void ReactivarUsuario(USUARIO usuarioAReactivar, USUARIO usuarioAdminLogueado)
        {
            mapperUsuario.Reactivar(usuarioAReactivar.Id);
            usuarioAReactivar = mapperUsuario.ObtenerPorId(usuarioAReactivar.Id);
            usuarioAReactivar.DVH = Servicios.GESTOR_INTEGRIDAD.CalcularDVH(usuarioAReactivar);
            actualizarDVH(usuarioAReactivar);
            ActualizarDVVTabla();
        }

        public void EliminarUsuario(USUARIO usuarioAEliminar, USUARIO usuarioAdminLogueado)
        {
            mapperUsuario.BajaLogica(usuarioAEliminar.Id);
            usuarioAEliminar = mapperUsuario.ObtenerPorId(usuarioAEliminar.Id);
            usuarioAEliminar.DVH = Servicios.GESTOR_INTEGRIDAD.CalcularDVH(usuarioAEliminar);
            actualizarDVH(usuarioAEliminar);
            ActualizarDVVTabla();
        }

        public bool CheckContrasena(int idUsuario, string contrasenaIngresada)
        {
            string contrasenaEncriptada = ENCRIPTADOR.Encriptar(contrasenaIngresada);
            USUARIO usuario = mapperUsuario.ObtenerPorId(idUsuario);
            return contrasenaEncriptada == usuario.Contrasena;
        }

        public int CambiarContrasena(int idUsuario, string nuevaContrasena, USUARIO usuarioResponsable)
        {
            string contrasenaEncriptada = ENCRIPTADOR.Encriptar(nuevaContrasena);
            USUARIO usuario = mapperUsuario.ObtenerPorId(idUsuario);
            int resultado = mapperUsuario.ActualizarContrasena(idUsuario, contrasenaEncriptada);
            if (resultado > 0)
            {
                usuario.Contrasena = contrasenaEncriptada;
                usuario.DVH = Servicios.GESTOR_INTEGRIDAD.CalcularDVH(usuario);
                actualizarDVH(usuario);
                ActualizarDVVTabla();
            }
            return resultado;
        }

        public void actualizarDVH(USUARIO usuario)
        {
            mapperUsuario.ActualizarDVH(usuario);
        }

        private void ActualizarDVVTabla()
        {
            List<BE.USUARIO> lista = Listar(true);
            string dvv = Servicios.GESTOR_INTEGRIDAD.CalcularDVV(lista);
            new BLL_DIGITO_VERIFICADOR().ActualizarDVV(new BE.DIGITO_VERIFICADOR { Tabla = "USUARIO", DVV = dvv });
        }
    }
}