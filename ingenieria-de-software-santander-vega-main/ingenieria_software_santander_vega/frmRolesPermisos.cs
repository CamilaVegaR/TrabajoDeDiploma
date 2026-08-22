using BE;
using BLL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ingenieria_software_santander_vega
{
    public partial class frmRolesPermisos : FORM_BASE
    {
        private BLL_ROLES_PERMISOS bllRolesPermisos;
        private BLL_USUARIO bllUsuario;

        private List<Componente> todosLosComponentes;
        private List<USUARIO> todosLosUsuarios;

        private bool cargandoDatos = false;

        public frmRolesPermisos()
        {
            InitializeComponent();
            bllRolesPermisos = new BLL_ROLES_PERMISOS();
            bllUsuario = new BLL_USUARIO();
        }

        private void frmRolesPermisos_Load(object sender, EventArgs e)
        {
            ConfigurarIconosTreeView();
            ConfigurarFormatoListas();
            CargarDatosIniciales();
        }


        private void ConfigurarIconosTreeView()
        {
            ImageList imagenesPermisos = new ImageList();

            imagenesPermisos.Images.Add("familia", SystemIcons.Shield.ToBitmap());
            imagenesPermisos.Images.Add("patente", SystemIcons.Information.ToBitmap());

            treeRolesPermisos.ImageList = imagenesPermisos;
            treeUsuariosRolesPermisos.ImageList = imagenesPermisos;
        }

        private void ConfigurarFormatoListas()
        {
            lstPermisos.Format += FormatearNombresListas;
            lstPatentesFamiliasDisponibles.Format += FormatearNombresListas;
            lstPatentesFamiliasSeleccionados.Format += FormatearNombresListas;
            lstRoles.Format += FormatearNombresListas;
        }

        private void ActualizarListaRolesDisponiblesUsuario(USUARIO usuario)
        {
            if (usuario == null)
            {
                lstRoles.DataSource = null;
                return;
            }

            var rolesDisponibles = bllRolesPermisos.ObtenerComponentesDisponiblesParaUsuario(usuario);

            lstRoles.DataSource = null;
            lstRoles.DataSource = rolesDisponibles;
        }

        private void FormatearNombresListas(object sender, ListControlConvertEventArgs e)
        {
            Componente comp = e.ListItem as Componente;
            if (comp != null)
            {
                string tipo = comp is Familia ? "ROL" : "PERMISO";
                e.Value = $"{tipo} - {comp.Nombre}";
            }
        }


        private void CargarDatosIniciales()
        {
            try
            {
                cargandoDatos = true;

                todosLosComponentes = bllRolesPermisos.ListarComponentes();

                lstPermisos.DataSource = null;
                lstPermisos.DataSource = todosLosComponentes;

                lstPatentesFamiliasDisponibles.DataSource = null;
                lstPatentesFamiliasDisponibles.DataSource = new List<Componente>(todosLosComponentes);

                ArmarTreeRoles();

                todosLosUsuarios = bllUsuario.Listar();

                cbUsuarios.DataSource = null;
                cbUsuarios.DisplayMember = "NombreUsuario";
                cbUsuarios.ValueMember = "Id";
                cbUsuarios.DataSource = todosLosUsuarios;

                cbUsuarios.SelectedIndex = -1;

                treeUsuariosRolesPermisos.Nodes.Clear();
                lstRoles.DataSource = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al cargar datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cargandoDatos = false;
            }
        }

        private void ArmarTreeRoles()
        {
            treeRolesPermisos.Nodes.Clear();

            foreach (var comp in todosLosComponentes.Where(c => c is Familia))
            {
                TreeNode nodoRaiz = new TreeNode(comp.Nombre);
                nodoRaiz.Tag = comp;

                nodoRaiz.ImageKey = "familia";
                nodoRaiz.SelectedImageKey = "familia";

                MostrarHijosEnTree(nodoRaiz, comp);

                treeRolesPermisos.Nodes.Add(nodoRaiz);
            }
            treeRolesPermisos.ExpandAll();
        }

        private void MostrarHijosEnTree(TreeNode nodoPadre, Componente componentePadre)
        {
            foreach (var hijo in componentePadre.ObtenerHijos())
            {
                TreeNode nodoHijo = new TreeNode(hijo.Nombre);
                nodoHijo.Tag = hijo;

                string iconKey = hijo is Familia ? "familia" : "patente";
                nodoHijo.ImageKey = iconKey;
                nodoHijo.SelectedImageKey = iconKey;

                nodoPadre.Nodes.Add(nodoHijo);

                if (hijo is Familia)
                {
                    MostrarHijosEnTree(nodoHijo, hijo);
                }
            }
        }

        private void ArmarTreeUsuario(USUARIO usuario)
        {
            treeUsuariosRolesPermisos.Nodes.Clear();

            foreach (var comp in usuario.Permisos)
            {
                TreeNode nodoRaiz = new TreeNode(comp.Nombre);
                nodoRaiz.Tag = comp;

                string iconKey = comp is Familia ? "familia" : "patente";
                nodoRaiz.ImageKey = iconKey;
                nodoRaiz.SelectedImageKey = iconKey;

                MostrarHijosEnTree(nodoRaiz, comp);

                treeUsuariosRolesPermisos.Nodes.Add(nodoRaiz);
            }
            treeUsuariosRolesPermisos.ExpandAll();
        }

        private void treeRolesPermisos_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (treeRolesPermisos.SelectedNode != null)
            {
                Componente compSeleccionado = (Componente)treeRolesPermisos.SelectedNode.Tag;

                if (compSeleccionado is Familia familiaDestino)
                {
                    lstPatentesFamiliasDisponibles.Enabled = true;
                    lstPatentesFamiliasSeleccionados.Enabled = true;
                    btnAgregar.Enabled = true;
                    btnQuitar.Enabled = true;

                    lstPatentesFamiliasSeleccionados.DataSource = null;
                    lstPatentesFamiliasSeleccionados.DataSource = familiaDestino.ObtenerHijos();

                    var componentesDisponibles = bllRolesPermisos.ObtenerComponentesDisponiblesParaFamilia(familiaDestino);

                    lstPatentesFamiliasDisponibles.DataSource = null;
                    lstPatentesFamiliasDisponibles.DataSource = componentesDisponibles;
                }
                else
                {
                    lstPatentesFamiliasDisponibles.Enabled = false;
                    lstPatentesFamiliasSeleccionados.Enabled = false;
                    btnAgregar.Enabled = false;
                    btnQuitar.Enabled = false;

                    lstPatentesFamiliasDisponibles.DataSource = null;
                    lstPatentesFamiliasSeleccionados.DataSource = null;
                }
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (treeRolesPermisos.SelectedNode == null)
            {
                MessageBox.Show("Seleccione una Familia (Rol) en el árbol central para agregarle permisos.");
                return;
            }

            Familia familiaDestino = treeRolesPermisos.SelectedNode.Tag as Familia;
            Componente compAAgregar = lstPatentesFamiliasDisponibles.SelectedItem as Componente;

            if (familiaDestino == null || compAAgregar == null) return;

            try
            {
                int idFamiliaSeleccionada = familiaDestino.Id;
                int idUsuarioSeleccionado = (cbUsuarios.SelectedItem as USUARIO)?.Id ?? -1;

                familiaDestino.Agregar(compAAgregar);
                bllRolesPermisos.GuardarFamilia(familiaDestino);

                CargarDatosIniciales();

                if (idUsuarioSeleccionado != -1)
                {
                    cbUsuarios.SelectedItem = todosLosUsuarios.FirstOrDefault(u => u.Id == idUsuarioSeleccionado);
                }
                treeRolesPermisos.SelectedNode = BuscarNodoEnTree(treeRolesPermisos.Nodes, idFamiliaSeleccionada);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al agregar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnQuitar_Click(object sender, EventArgs e)
        {
            if (treeRolesPermisos.SelectedNode == null) return;

            Familia familiaDestino = treeRolesPermisos.SelectedNode.Tag as Familia;
            Componente compAQuitar = lstPatentesFamiliasSeleccionados.SelectedItem as Componente;

            if (familiaDestino != null && compAQuitar != null)
            {
                try
                {
                    int idFamiliaSeleccionada = familiaDestino.Id;
                    int idUsuarioSeleccionado = (cbUsuarios.SelectedItem as USUARIO)?.Id ?? -1;

                    familiaDestino.Remover(compAQuitar);
                    bllRolesPermisos.GuardarFamilia(familiaDestino);

                    CargarDatosIniciales();

                    if (idUsuarioSeleccionado != -1)
                    {
                        cbUsuarios.SelectedItem = todosLosUsuarios.FirstOrDefault(u => u.Id == idUsuarioSeleccionado);
                    }
                    treeRolesPermisos.SelectedNode = BuscarNodoEnTree(treeRolesPermisos.Nodes, idFamiliaSeleccionada);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error al quitar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private TreeNode BuscarNodoEnTree(TreeNodeCollection nodos, int idComponente)
        {
            foreach (TreeNode n in nodos)
            {
                if (((Componente)n.Tag).Id == idComponente) return n;
                TreeNode nodoHijo = BuscarNodoEnTree(n.Nodes, idComponente);
                if (nodoHijo != null) return nodoHijo;
            }
            return null;
        }

        private void cbUsuarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cargandoDatos)
                return;

            USUARIO usrSel = cbUsuarios.SelectedItem as USUARIO;

            if (usrSel == null)
            {
                treeUsuariosRolesPermisos.Nodes.Clear();
                lstRoles.DataSource = null;
                return;
            }

            bllRolesPermisos.CargarPermisosUsuario(usrSel);
            ArmarTreeUsuario(usrSel);
            ActualizarListaRolesDisponiblesUsuario(usrSel);
        }

        private void btnAsignarRolUsuario_Click(object sender, EventArgs e)
        {
            USUARIO usrSel = cbUsuarios.SelectedItem as USUARIO;
            Componente rolAAgregar = lstRoles.SelectedItem as Componente;

            if (usrSel == null || rolAAgregar == null)
            {
                MessageBox.Show("Por favor, seleccione un usuario y un rol para asignar.");
                return;
            }

            if (!usrSel.Permisos.Any(p => p.Id == rolAAgregar.Id))
            {
                usrSel.Permisos.Add(rolAAgregar);

                bllRolesPermisos.GuardarPermisosUsuario(usrSel);

                bllRolesPermisos.CargarPermisosUsuario(usrSel);

                ArmarTreeUsuario(usrSel);
                ActualizarListaRolesDisponiblesUsuario(usrSel);
            }
            else
            {
                MessageBox.Show("El usuario ya tiene este componente asignado directamente.");
            }
        }

        private void btnDesasignarRol_Click(object sender, EventArgs e)
        {
            USUARIO usrSel = cbUsuarios.SelectedItem as USUARIO;

            if (usrSel != null && treeUsuariosRolesPermisos.SelectedNode != null && treeUsuariosRolesPermisos.SelectedNode.Parent == null)
            {
                Componente compAQuitar = treeUsuariosRolesPermisos.SelectedNode.Tag as Componente;

                var itemToRemove = usrSel.Permisos.FirstOrDefault(p => p.Id == compAQuitar.Id);

                if (itemToRemove != null)
                {
                    usrSel.Permisos.Remove(itemToRemove);

                    bllRolesPermisos.GuardarPermisosUsuario(usrSel);

                    bllRolesPermisos.CargarPermisosUsuario(usrSel);

                    ArmarTreeUsuario(usrSel);
                    ActualizarListaRolesDisponiblesUsuario(usrSel);
                }
            }
            else
            {
                MessageBox.Show("Seleccione un Rol o Permiso principal del árbol del usuario para desasignarlo.");
            }
        }

        private string MostrarDialogoInput(string titulo, string texto)
        {
            Form prompt = new Form()
            {
                Width = 400,
                Height = 160,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = titulo,
                StartPosition = FormStartPosition.CenterScreen,
                MaximizeBox = false
            };
            Label textLabel = new Label() { Left = 20, Top = 20, Text = texto, Width = 340 };
            TextBox textBox = new TextBox() { Left = 20, Top = 50, Width = 340 };
            Button confirmation = new Button() { Text = "Aceptar", Left = 260, Width = 100, Top = 80, DialogResult = DialogResult.OK };

            prompt.Controls.Add(textBox);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(textLabel);
            prompt.AcceptButton = confirmation;

            return prompt.ShowDialog() == DialogResult.OK ? textBox.Text.Trim() : "";
        }

        private void btnNuevaFamilia_Click(object sender, EventArgs e)
        {
            string nombreRol = MostrarDialogoInput("Nuevo Rol", "Ingrese el nombre del nuevo Rol (Familia):");

            if (!string.IsNullOrWhiteSpace(nombreRol))
            {
                try
                {
                    Componente nuevoRol = new Familia() { Nombre = nombreRol };
                    bllRolesPermisos.GuardarComponenteBase(nuevoRol, esFamilia: true);

                    CargarDatosIniciales();
                    MessageBox.Show("Rol creado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnNuevaPatente_Click(object sender, EventArgs e)
        {
            string nombrePatente = MostrarDialogoInput("Nuevo Permiso", "Ingrese el nombre del nuevo Permiso (Patente):");

            if (!string.IsNullOrWhiteSpace(nombrePatente))
            {
                try
                {
                    Componente nuevaPatente = new Patente() { Nombre = nombrePatente };
                    bllRolesPermisos.GuardarComponenteBase(nuevaPatente, esFamilia: false);

                    CargarDatosIniciales();
                    MessageBox.Show("Permiso creado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Componente compSeleccionado = lstPermisos.SelectedItem as Componente;

            if (compSeleccionado != null)
            {
                var confirmacion = MessageBox.Show(
                    $"¿Está seguro que desea eliminar '{compSeleccionado.Nombre}'?\nEsto lo quitará de todos los roles y usuarios que lo tengan asignado.",
                    "Confirmar Eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirmacion == DialogResult.Yes)
                {
                    try
                    {
                        bllRolesPermisos.EliminarComponenteBase(compSeleccionado.Id);

                        CargarDatosIniciales();
                        MessageBox.Show("Componente eliminado.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error al eliminar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar un componente de la lista del Explorador (Izquierda) para eliminar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            Navegar(new frmMenu(), null, true);
        }
    }
}