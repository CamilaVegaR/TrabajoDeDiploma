using BE;
using DAL;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace BLL
{
    public class BLL_USUARIO_HISTORIAL
    {
        private DAL_USUARIO_HISTORIAL historialDAL;

        private JsonSerializerSettings configuracionJson;

        public BLL_USUARIO_HISTORIAL()
        {
            historialDAL = new DAL_USUARIO_HISTORIAL();

            configuracionJson = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto,

                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,

                Formatting = Formatting.Indented
            };
        }

        public List<BE.USUARIO_HISTORIAL> ListarHistorialCompleto()
        {
            return historialDAL.Listar();
        }

        public void RegistrarCambio(USUARIO estadoUsuario, USUARIO usuarioResponsable, TIPO_OPERACION operacion)
        {
            if (estadoUsuario?.Permisos == null)
            {
                DAL_ROLES_PERMISOS dalRolesPermisos = new DAL_ROLES_PERMISOS();
                dalRolesPermisos.CargarPermisosUsuario(estadoUsuario);
            }

            string jsonSnapshot = JsonConvert.SerializeObject(estadoUsuario, configuracionJson);

            USUARIO_HISTORIAL nuevoHistorial = new USUARIO_HISTORIAL
            {
                UsuarioAuditado = estadoUsuario,
                FechaHora = DateTime.Now,
                UsuarioResponsable = usuarioResponsable,
                Operacion = operacion,
                SnapshotData = jsonSnapshot
            };

            int id = historialDAL.Insertar(nuevoHistorial);
            if(id == 0) { 
                throw new Exception("Error al registrar el historial del usuario.");
            }
            USUARIO_HISTORIAL historialInsertado = historialDAL.ObtenerPorId(id);
            historialInsertado.DVH = Servicios.GESTOR_INTEGRIDAD.CalcularDVH(historialInsertado);
            historialDAL.ActualizarDVH(historialInsertado);
            ActualizarDVVTabla();
        }

        public List<USUARIO_HISTORIAL> ListarHistorialDeUsuario(int idUsuario)
        {
            return historialDAL.ListarPorUsuario(idUsuario);
        }

        public USUARIO RecomponerEstado(USUARIO_HISTORIAL historial)
        {
            USUARIO estadoRestaurado = JsonConvert.DeserializeObject<USUARIO>(historial.SnapshotData, configuracionJson);
            return estadoRestaurado;
        }

        public void ActualizarDVH(USUARIO_HISTORIAL historial)
        {
            if (historial != null)
            {
                historialDAL.ActualizarDVH(historial);
            }
        }

        private void ActualizarDVVTabla()
        {
            List<BE.USUARIO_HISTORIAL> lista = ListarHistorialCompleto();
            string dvv = Servicios.GESTOR_INTEGRIDAD.CalcularDVV(lista);
            new BLL_DIGITO_VERIFICADOR().ActualizarDVV(new BE.DIGITO_VERIFICADOR { Tabla = "USUARIO_HISTORIAL", DVV = dvv });
        }
    }
}