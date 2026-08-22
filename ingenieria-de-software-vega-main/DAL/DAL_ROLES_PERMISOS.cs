using BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class DAL_ROLES_PERMISOS
    {
        private ACCESO acceso;

        public List<Componente> ListarComponentes()
        {
            acceso = new ACCESO();
            acceso.Abrir();
            List<Componente> componentes = new List<Componente>();

            DataTable tabla = acceso.Leer("SP_COMPONENTE_LISTAR", null);
            acceso.Cerrar();

            if (tabla != null)
            {
                foreach (DataRow fila in tabla.Rows)
                {
                    int id = int.Parse(fila["Id"].ToString());
                    string nombre = fila["Nombre"].ToString();
                    string tipo = fila["Tipo"].ToString();
                    string dvh = fila["DVH"].ToString();

                    Componente comp;
                    if (tipo.Equals("Familia", StringComparison.OrdinalIgnoreCase))
                    {
                        comp = new Familia { Id = id, Nombre = nombre, DVH = dvh };
                        CargarHijos(comp);
                    }
                    else
                    {
                        comp = new Patente { Id = id, Nombre = nombre, DVH = dvh };
                    }
                    componentes.Add(comp);
                }
            }
            return componentes;
        }

        public Componente ObtenerPorId(int id)
        {
            acceso = new ACCESO();
            acceso.Abrir();
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@pId", id));
            DataTable tabla = acceso.Leer("SP_COMPONENTE_OBTENER_POR_ID", parametros);
            acceso.Cerrar();
            if (tabla != null && tabla.Rows.Count > 0)
            {
                DataRow fila = tabla.Rows[0];
                int idComp = int.Parse(fila["ID"].ToString());
                string nombreComp = fila["NOMBRE"].ToString();
                string tipoComp = fila["TIPO"].ToString();
                string dvh = fila["DVH"].ToString();
                Componente comp;
                if (tipoComp.Equals("Familia", StringComparison.OrdinalIgnoreCase))
                {
                    comp = new Familia { Id = idComp, Nombre = nombreComp, DVH = dvh };
                    CargarHijos(comp);
                }
                else
                {
                    comp = new Patente { Id = idComp, Nombre = nombreComp, DVH = dvh };
                }
                return comp;
            }
            return null;
        }

        private void CargarHijos(Componente padre)
        {
            acceso = new ACCESO();
            acceso.Abrir();

            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@pIdPadre", padre.Id));

            DataTable tablaHijos = acceso.Leer("SP_FAMILIA_OBTENER_HIJOS", parametros);
            acceso.Cerrar();

            if (tablaHijos != null)
            {
                foreach (DataRow fila in tablaHijos.Rows)
                {
                    int idHijo = int.Parse(fila["IdHijo"].ToString());
                    string nombreHijo = fila["Nombre"].ToString();
                    string tipoHijo = fila["Tipo"].ToString();

                    Componente hijo;
                    if (tipoHijo.Equals("Familia", StringComparison.OrdinalIgnoreCase))
                    {
                        hijo = new Familia { Id = idHijo, Nombre = nombreHijo };
                        CargarHijos(hijo);
                    }
                    else
                    {
                        hijo = new Patente { Id = idHijo, Nombre = nombreHijo };
                    }

                    padre.Agregar(hijo);
                }
            }
        }

        public void CargarPermisosUsuario(USUARIO usuario)
        {
            acceso = new ACCESO();
            acceso.Abrir();

            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@pIdUsuario", usuario.Id));

            DataTable tablaPermisos = acceso.Leer("SP_USUARIO_OBTENER_COMPONENTES", parametros);
            acceso.Cerrar();

            if (usuario.Permisos == null)
            {
                usuario.Permisos = new List<Componente>();
            }
            else
            {
                usuario.Permisos.Clear();
            }

            if (tablaPermisos != null)
            {
                List<Componente> componentesAgregados = new List<Componente>();
                foreach (DataRow fila in tablaPermisos.Rows)
                {
                    int idComp = int.Parse(fila["IdComponente"].ToString());
                    string nombreComp = fila["Nombre"].ToString();
                    string tipoComp = fila["Tipo"].ToString();
                    string dvh = fila["DVH"].ToString();

                    Componente comp;
                    if (tipoComp.Equals("Familia", StringComparison.OrdinalIgnoreCase))
                    {
                        comp = new Familia { Id = idComp, Nombre = nombreComp, DVH = dvh };
                        CargarHijos(comp);
                    }
                    else
                    {
                        comp = new Patente { Id = idComp, Nombre = nombreComp, DVH = dvh };
                    }
                    componentesAgregados.Add(comp);
                }
                usuario.Permisos = componentesAgregados;
            }
        }

        public bool GuardarFamilia(Familia familia)
        {
            acceso = new ACCESO();
            acceso.Abrir();

            List<SqlParameter> paramDelete = new List<SqlParameter>();
            paramDelete.Add(new SqlParameter("@pIdPadre", familia.Id));
            acceso.Escribir("SP_FAMILIA_ELIMINAR_HIJOS", paramDelete);

            foreach (var hijo in familia.ObtenerHijos())
            {
                List<SqlParameter> paramInsert = new List<SqlParameter>();
                paramInsert.Add(new SqlParameter("@pIdPadre", familia.Id));
                paramInsert.Add(new SqlParameter("@pIdHijo", hijo.Id));

                acceso.Escribir("SP_FAMILIA_INSERTAR_HIJO", paramInsert);
            }

            acceso.Cerrar();
            return true;
        }

        public bool GuardarPermisosUsuario(USUARIO usuario)
        {
            acceso = new ACCESO();
            acceso.Abrir();

            List<SqlParameter> paramDelete = new List<SqlParameter>();
            paramDelete.Add(new SqlParameter("@pIdUsuario", usuario.Id));
            acceso.Escribir("SP_USUARIO_ELIMINAR_COMPONENTES", paramDelete);

            foreach (var componente in usuario.Permisos)
            {
                List<SqlParameter> paramInsert = new List<SqlParameter>();
                paramInsert.Add(new SqlParameter("@pIdUsuario", usuario.Id));
                paramInsert.Add(new SqlParameter("@pIdComponente", componente.Id));

                acceso.Escribir("SP_USUARIO_INSERTAR_COMPONENTE", paramInsert);
            }

            acceso.Cerrar();
            return true;
        }

        public int InsertarComponente(Componente componente, bool esFamilia)
        {
            acceso = new ACCESO();
            acceso.Abrir();

            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@pNombre", componente.Nombre));
            parametros.Add(new SqlParameter("@pTipo", esFamilia ? "Familia" : "Patente"));

            int id = acceso.LeerEscalar("SP_COMPONENTE_INS", parametros);
            acceso.Cerrar();

            return id;
        }

        public bool EliminarComponente(int idComponente)
        {
            acceso = new ACCESO();
            acceso.Abrir();

            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@pIdComponente", idComponente));

            int filas = acceso.Escribir("SP_COMPONENTE_DEL", parametros);
            acceso.Cerrar();

            return filas > 0;
        }

        public List<Componente> ObtenerComponentesDisponiblesUsuario(int idUsuario)
        {
            acceso = new ACCESO();
            acceso.Abrir();

            List<Componente> componentes = new List<Componente>();

            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@pIdUsuario", idUsuario));

            DataTable tabla = acceso.Leer("SP_USUARIO_COMPONENTES_DISPONIBLES", parametros);

            acceso.Cerrar();

            if (tabla != null)
            {
                foreach (DataRow fila in tabla.Rows)
                {
                    int id = int.Parse(fila["ID"].ToString());
                    string nombre = fila["NOMBRE"].ToString();
                    string tipo = fila["TIPO"].ToString();
                    string dvh = fila["DVH"].ToString();

                    Componente comp;

                    if (tipo.Equals("Familia", StringComparison.OrdinalIgnoreCase))
                    {
                        comp = new Familia { Id = id, Nombre = nombre, DVH = dvh };
                        CargarHijos(comp);
                    }
                    else
                    {
                        comp = new Patente { Id = id, Nombre = nombre, DVH = dvh };
                    }

                    componentes.Add(comp);
                }
            }

            return componentes;
        }

        public int ActualizarDVH(Componente componente)
        {
            acceso = new ACCESO();
            acceso.Abrir();
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@pId", componente.Id));
            parametros.Add(new SqlParameter("@pDVH", componente.DVH));
            int filas = acceso.Escribir("SP_COMPONENTE_UPD_DVH", parametros);
            acceso.Cerrar();
            return filas;
        }
    }
}