using BE;
using DAL;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    public class BLL_DIGITO_VERIFICADOR
    {
        private DAL_DIGITO_VERIFICADOR dalDV = new DAL_DIGITO_VERIFICADOR();

        public List<DIGITO_VERIFICADOR> ObtenerTodos()
        {
            return dalDV.ObtenerTodos();
        }

        public DIGITO_VERIFICADOR ObtenerPorTabla(string tabla)
        {
            return dalDV.ObtenerPorTabla(tabla);
        }

        public void ActualizarDVV(DIGITO_VERIFICADOR dv)
        {
            dalDV.ActualizarDVV(dv);
        }

        public void RecalcularTablas()
        {
            BLL_USUARIO bllUsuario = new BLL_USUARIO();

            List<USUARIO> usuarios = bllUsuario.Listar(true);

            if (usuarios != null && usuarios.Any())
            {
                foreach (var u in usuarios)
                {
                    u.DVH = GESTOR_INTEGRIDAD.CalcularDVH(u);

                    bllUsuario.actualizarDVH(u);
                }

                string dvvUsuario = GESTOR_INTEGRIDAD.CalcularDVV(usuarios);

                ActualizarDVV(new DIGITO_VERIFICADOR { Tabla = "USUARIO", DVV = dvvUsuario });
            }

            BLL_BITACORA bllBitacora = new BLL_BITACORA();
            List<BITACORA> bitacoras = bllBitacora.Listar();

            if (bitacoras != null && bitacoras.Any())
            {
                foreach (var b in bitacoras)
                {
                    b.DVH = GESTOR_INTEGRIDAD.CalcularDVH(b);
                    bllBitacora.ActualizarDVH(b);
                }
                string dvvBitacora = GESTOR_INTEGRIDAD.CalcularDVV(bitacoras);
                ActualizarDVV(new DIGITO_VERIFICADOR { Tabla = "BITACORA", DVV = dvvBitacora });
            }

            BLL_ETIQUETAS bllEtiquetas = new BLL_ETIQUETAS();
            List<ETIQUETA> etiquetas = bllEtiquetas.ListarEtiquetas();

            if (etiquetas != null && etiquetas.Any())
            {
                foreach (var e in etiquetas)
                {
                    e.DVH = GESTOR_INTEGRIDAD.CalcularDVH(e);
                    bllEtiquetas.ActualizarDVH(e);
                }
                string dvvEtiqueta = GESTOR_INTEGRIDAD.CalcularDVV(etiquetas);
                ActualizarDVV(new DIGITO_VERIFICADOR { Tabla = "ETIQUETA", DVV = dvvEtiqueta });
            }

            BLL_IDIOMA bllIdioma = new BLL_IDIOMA();
            List<IDIOMA> idiomas = bllIdioma.ListarIdiomas();

            if (idiomas != null && idiomas.Any())
            {
                foreach (var i in idiomas)
                {
                    i.DVH = GESTOR_INTEGRIDAD.CalcularDVH(i);
                    bllIdioma.ActualizarDVH(i);
                }
                string dvvIdioma = GESTOR_INTEGRIDAD.CalcularDVV(idiomas);
                ActualizarDVV(new DIGITO_VERIFICADOR { Tabla = "IDIOMA", DVV = dvvIdioma });
            }

            BLL_ROLES_PERMISOS bllRolesPermisos = new BLL_ROLES_PERMISOS();
            List<Componente> componentes = bllRolesPermisos.ListarComponentes();

            if (componentes != null && componentes.Any())
            {
                foreach (var c in componentes)
                {
                    c.DVH = GESTOR_INTEGRIDAD.CalcularDVH(c);
                    bllRolesPermisos.ActualizarDVHComponente(c);
                }
                string dvvComponente = GESTOR_INTEGRIDAD.CalcularDVV(componentes);
                ActualizarDVV(new DIGITO_VERIFICADOR { Tabla = "COMPONENTE", DVV = dvvComponente });
            }

            BLL_USUARIO_HISTORIAL bllUsuarioHistorial = new BLL_USUARIO_HISTORIAL();
            List<USUARIO_HISTORIAL> historiales = bllUsuarioHistorial.ListarHistorialCompleto();

            if (historiales != null && historiales.Any())
            {
                foreach (var h in historiales)
                {
                    h.DVH = GESTOR_INTEGRIDAD.CalcularDVH(h);
                    bllUsuarioHistorial.ActualizarDVH(h);
                }
                string dvvUsuarioHistorial = GESTOR_INTEGRIDAD.CalcularDVV(historiales);
                ActualizarDVV(new DIGITO_VERIFICADOR { Tabla = "USUARIO_HISTORIAL", DVV = dvvUsuarioHistorial });
            }

            BLL_TRADUCCION bllTraduccion = new BLL_TRADUCCION();
            List<TRADUCCION> traducciones = bllTraduccion.listar();

            if (traducciones != null && traducciones.Any())
            {
                foreach (var t in traducciones)
                {
                    t.DVH = GESTOR_INTEGRIDAD.CalcularDVH(t);
                    bllTraduccion.ActualizarDVH(t);
                }
                string dvvTraduccion = GESTOR_INTEGRIDAD.CalcularDVV(traducciones);
                ActualizarDVV(new DIGITO_VERIFICADOR { Tabla = "TRADUCCION", DVV = dvvTraduccion });
            }
        }

        public List<string> ValidarIntegridadBaseDatos()
        {
            List<string> errores = new List<string>();

            VerificarTabla("USUARIO", new BLL_USUARIO().Listar(true).Cast<IIntegrable>(), errores);

            VerificarTabla("BITACORA", new BLL_BITACORA().Listar().Cast<IIntegrable>(), errores);

            VerificarTabla("ETIQUETA", new BLL_ETIQUETAS().ListarEtiquetas().Cast<IIntegrable>(), errores);

            VerificarTabla("IDIOMA", new BLL_IDIOMA().ListarIdiomas().Cast<IIntegrable>(), errores);

            VerificarTabla("COMPONENTE", new BLL_ROLES_PERMISOS().ListarComponentes().Cast<IIntegrable>(), errores);

            VerificarTabla("USUARIO_HISTORIAL", new BLL_USUARIO_HISTORIAL().ListarHistorialCompleto().Cast<IIntegrable>(), errores);

            VerificarTabla("TRADUCCION", new BLL_TRADUCCION().listar().Cast<IIntegrable>(), errores);

            return errores;
        }

        private void VerificarTabla(string nombreTabla, IEnumerable<IIntegrable> registros, List<string> errores)
        {
            if (registros == null) registros = new List<IIntegrable>();

            string dvvCalculado = GESTOR_INTEGRIDAD.CalcularDVV(registros);
            DIGITO_VERIFICADOR dvvAlmacenado = ObtenerPorTabla(nombreTabla);

            if (dvvAlmacenado == null || dvvCalculado != dvvAlmacenado.DVV)
            {
                errores.Add($"La tabla '{nombreTabla}' fue modificada por fuera del sistema (Fallo en Hash Vertical - DVV). Posible inserción o eliminación fraudulenta.");
            }

            foreach (var reg in registros)
            {
                string dvhCalculado = GESTOR_INTEGRIDAD.CalcularDVH(reg);
                if (reg.DVH != dvhCalculado)
                {
                    var propId = reg.GetType().GetProperty("Id") ?? reg.GetType().GetProperty("IdUsuario") ?? reg.GetType().GetProperty("IdHistorial");
                    string idStr = propId != null ? propId.GetValue(reg).ToString() : "Desconocido";

                    errores.Add($"El registro ID [{idStr}] de la tabla '{nombreTabla}' fue alterado (Fallo en Hash Horizontal - DVH).");
                }
            }
        }
    }
}