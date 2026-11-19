using BE;
using DAL;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    public class BLL_ROLES_PERMISOS
    {
        DAL.DAL_ROLES_PERMISOS mapperRoles = new DAL.DAL_ROLES_PERMISOS();
        public BLL_ROLES_PERMISOS()
        {
        }

        public List<Componente> ListarComponentes()
        {
            return mapperRoles.ListarComponentes();
        }

        public void CargarPermisosUsuario(BE.USUARIO usuario)
        {
            if (usuario == null)
                throw new ArgumentNullException("El usuario no puede ser nulo para cargar sus permisos.");

            mapperRoles.CargarPermisosUsuario(usuario);
        }

        public bool GuardarFamilia(Familia familia)
        {
            if (familia == null)
                throw new ArgumentNullException("La familia no puede ser nula.");

            if (string.IsNullOrWhiteSpace(familia.Nombre))
                throw new Exception("La familia debe tener un nombre válido.");

            bool guardadoExitoso = mapperRoles.GuardarFamilia(familia);

            if (guardadoExitoso)
            {
                ActualizarDVVTabla();
                if (SINGLETON_SESION.IsLoggedIn())
                {
                    CargarPermisosUsuario(SINGLETON_SESION.GetInstance().usuario);
                }
            }

            return guardadoExitoso;
        }

        public bool GuardarPermisosUsuario(BE.USUARIO usuario)
        {
            if (usuario == null)
                throw new ArgumentNullException("El usuario no puede ser nulo.");

            bool guardadoExitoso = mapperRoles.GuardarPermisosUsuario(usuario);

            if (guardadoExitoso)
            {
                if (SINGLETON_SESION.IsLoggedIn())
                {
                    var usuarioSesion = SINGLETON_SESION.GetInstance().usuario;
                    if (usuarioSesion.Id == usuario.Id)
                    {
                        CargarPermisosUsuario(usuarioSesion);
                    }
                }
            }

            return guardadoExitoso;
        }

        public bool TienePermisoEnSesion(string nombrePermiso)
        {
            if (!SINGLETON_SESION.IsLoggedIn()) return false;

            USUARIO usuarioLogueado = SINGLETON_SESION.GetInstance().usuario;

            if (usuarioLogueado.Permisos == null || usuarioLogueado.Permisos.Count == 0)
            {
                CargarPermisosUsuario(usuarioLogueado);
            }

            return usuarioLogueado.ValidarPermiso(nombrePermiso);
        }

        public bool GuardarComponenteBase(Componente componente, bool esFamilia)
        {
            if (componente == null || string.IsNullOrWhiteSpace(componente.Nombre))
                throw new Exception("El nombre del componente no puede estar vacío.");

            var existentes = ListarComponentes();
            if (existentes.Any(c => c.Nombre.Equals(componente.Nombre, StringComparison.OrdinalIgnoreCase)))
            {
                throw new Exception($"Ya existe un rol o permiso con el nombre '{componente.Nombre}'.");
            }

            int id = mapperRoles.InsertarComponente(componente, esFamilia);

            if (id <= 0)
            {
                throw new Exception("Hubo un error al insertar el componente.");
            }
            ;
            Componente componenteInsertado = mapperRoles.ObtenerPorId(id);

            componenteInsertado.DVH = GESTOR_INTEGRIDAD.CalcularDVH(componenteInsertado);
            ActualizarDVHComponente(componenteInsertado);
            ActualizarDVVTabla();
            return id > 0;
        }

        public bool EliminarComponenteBase(int idComponente)
        {
            if (idComponente <= 0)
                throw new Exception("ID de componente inválido.");

            bool eliminadoExitoso = mapperRoles.EliminarComponente(idComponente);

            if (eliminadoExitoso)
            {
                ActualizarDVVTabla();
                if (SINGLETON_SESION.IsLoggedIn())
                {
                    CargarPermisosUsuario(SINGLETON_SESION.GetInstance().usuario);
                }
            }

            return eliminadoExitoso;
        }

        public List<Componente> ObtenerComponentesDisponiblesParaUsuario(BE.USUARIO usuario)
        {
            if (usuario == null) return new List<Componente>();
            return mapperRoles.ObtenerComponentesDisponiblesUsuario(usuario.Id);
        }

        public List<Componente> ObtenerComponentesDisponiblesParaFamilia(Familia familiaDestino)
        {
            var todosLosComponentes = ListarComponentes();
            var hijosActuales = familiaDestino.ObtenerHijos();

            return todosLosComponentes.Where(c =>
                c.Id != familiaDestino.Id &&
                !hijosActuales.Any(h => h.Id == c.Id) &&
                !GeneraBucleCircular(c, familiaDestino.Id, new List<int>())
            ).ToList();
        }

        private bool GeneraBucleCircular(Componente compAAgregar, int idFamiliaDestino, List<int> visitados)
        {
            if (compAAgregar.Id == idFamiliaDestino) return true;
            if (visitados.Contains(compAAgregar.Id)) return false;

            visitados.Add(compAAgregar.Id);

            foreach (var hijo in compAAgregar.ObtenerHijos())
            {
                if (GeneraBucleCircular(hijo, idFamiliaDestino, visitados)) return true;
            }
            return false;
        }

        public void ActualizarDVHComponente(Componente componente)
        {
            if (componente != null)
            {
                mapperRoles.ActualizarDVH(componente);
            }
        }

        private void ActualizarDVVTabla()
        {
            List<Componente> lista = ListarComponentes();
            string dvv = GESTOR_INTEGRIDAD.CalcularDVV(lista);
            new BLL_DIGITO_VERIFICADOR().ActualizarDVV(new BE.DIGITO_VERIFICADOR { Tabla = "COMPONENTE", DVV = dvv });
        }
    }
}