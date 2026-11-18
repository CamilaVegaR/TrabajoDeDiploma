using System;
using System.Collections.Generic;
using System.Windows.Forms;
using BE;
using BLL;
using Servicios;

namespace ingenieria_software_santander_vega
{
    public partial class frmAdminUsuario : FORM_BASE
    {
        private BLL_USUARIO bllUsuario = new BLL_USUARIO();

        private BLL_USUARIO_HISTORIAL bllHistorial = new BLL_USUARIO_HISTORIAL();

        private BLL_ROLES_PERMISOS bllRolesPermisos = new BLL_ROLES_PERMISOS();

        public frmAdminUsuario()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (this.DesignMode) return;

            lblHistorial.Visible = false;
            dgvHistorialUsuario.Visible = false;
            lblAtributos.Visible = false;
            lstAtributos.Visible = false;
            btnRestaurar.Visible = false;
            this.ClientSize = new System.Drawing.Size(470, this.ClientSize.Height);
            this.CenterToScreen();

            cbSexo.DataSource = Enum.GetValues(typeof(SEXO_ENUM));

            CargarComboUsuarios();
        }

        private void CargarComboUsuarios()
        {
            cbUsuario.SelectedIndexChanged -= cbUsuario_SelectedIndexChanged;

            cbUsuario.DataSource = null;
            List<BE.USUARIO> listaUsuarios = bllUsuario.Listar(true);
            if (Servicios.SINGLETON_SESION.IsLoggedIn())
            {
                int idUsuarioActual = Servicios.SINGLETON_SESION.GetInstance().usuario.Id;

                listaUsuarios.RemoveAll(u => u.Id == idUsuarioActual);
            }

            cbUsuario.DataSource = listaUsuarios;
            cbUsuario.DisplayMember = "NombreUsuario";
            cbUsuario.ValueMember = "Id";
            cbUsuario.SelectedIndex = -1;

            LimpiarCampos();

            cbUsuario.SelectedIndexChanged += cbUsuario_SelectedIndexChanged;
        }

        private void LimpiarCampos()
        {
            txtNombre.Text = string.Empty;
            txtApellido.Text = string.Empty;
            txtNombreUsuario.Text = string.Empty;
            txtIdioma.Text = string.Empty;
            dgvHistorialUsuario.DataSource = null;
            lstAtributos.Items.Clear();
        }

        private void cbUsuario_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbUsuario.SelectedItem != null)
            {
                USUARIO usuarioSeleccionado = (USUARIO)cbUsuario.SelectedItem;

                txtNombre.Text = usuarioSeleccionado.Nombre;
                txtApellido.Text = usuarioSeleccionado.Apellido;
                txtNombreUsuario.Text = usuarioSeleccionado.NombreUsuario;
                cbSexo.SelectedItem = usuarioSeleccionado.Sexo;

                txtIdioma.Text = usuarioSeleccionado.Idioma != null ? usuarioSeleccionado.Idioma.Nombre : "Sin Asignar";

                bool esActivo = usuarioSeleccionado.Activo;

                btnEliminar.Enabled = esActivo;
                btnEliminar.Visible = esActivo;

                btnReactivar.Enabled = !esActivo;
                btnReactivar.Visible = !esActivo;

                CargarHistorialGrilla(usuarioSeleccionado.Id);
            }
        }
        private void CargarHistorialGrilla(int idUsuario)
        {
            try
            {
                var historial = bllHistorial.ListarHistorialDeUsuario(idUsuario);
                dgvHistorialUsuario.DataSource = null;
                dgvHistorialUsuario.DataSource = historial;

                if (dgvHistorialUsuario.Columns["SnapshotData"] != null) dgvHistorialUsuario.Columns["SnapshotData"].Visible = false;
                if (dgvHistorialUsuario.Columns["UsuarioAuditado"] != null) dgvHistorialUsuario.Columns["UsuarioAuditado"].Visible = false;

                dgvHistorialUsuario.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                MostrarDetallesSnapshot();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar historial");
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (cbUsuario.SelectedItem == null) return;

            try
            {
                USUARIO usuarioActual = (USUARIO)cbUsuario.SelectedItem;

                usuarioActual.Nombre = txtNombre.Text;
                usuarioActual.Apellido = txtApellido.Text;
                usuarioActual.NombreUsuario = txtNombreUsuario.Text;
                usuarioActual.Sexo = (SEXO_ENUM)cbSexo.SelectedItem;
                usuarioActual.Activo = true;

                USUARIO adminResponsable = SINGLETON_SESION.GetInstance().usuario;

                bllUsuario.ModificarUsuario(usuarioActual, adminResponsable);

                MessageBox.Show("Usuario editado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                CargarComboUsuarios();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al editar");
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (cbUsuario.SelectedItem == null) return;

            DialogResult dr = MessageBox.Show("¿Está seguro que desea eliminar este usuario?", "Confirmar Baja", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (dr == DialogResult.Yes)
            {
                try
                {
                    USUARIO usuarioAEliminar = (USUARIO)cbUsuario.SelectedItem;
                    USUARIO adminResponsable = SINGLETON_SESION.GetInstance().usuario;

                    bllUsuario.EliminarUsuario(usuarioAEliminar, adminResponsable);

                    MessageBox.Show("Usuario eliminado. Debido a la baja lógica, el usuario mantiene su trazabilidad.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarComboUsuarios();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnRestaurar_Click(object sender, EventArgs e)
        {
            if (dgvHistorialUsuario.CurrentRow == null)
            {
                MessageBox.Show("Debe seleccionar una versión del historial en la grilla para restaurar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult dr = MessageBox.Show("¿Está seguro que desea sobreescribir el estado actual del usuario con la versión seleccionada del historial?", "Confirmar Restauración", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr == DialogResult.Yes)
            {
                try
                {
                    USUARIO_HISTORIAL snapshotSeleccionado = (USUARIO_HISTORIAL)dgvHistorialUsuario.CurrentRow.DataBoundItem;

                    USUARIO usuarioRecompuesto = bllHistorial.RecomponerEstado(snapshotSeleccionado);

                    USUARIO adminResponsable = SINGLETON_SESION.GetInstance().usuario;
                    bllUsuario.ModificarUsuario(usuarioRecompuesto, adminResponsable, TIPO_OPERACION.RESTAURACION);

                    MessageBox.Show($"Restauración exitosa. El usuario volvió a la Versión {snapshotSeleccionado.Version}.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    CargarComboUsuarios();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al intentar restaurar el Snapshot", "Error Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void MostrarDetallesSnapshot()
        {
            lstAtributos.Items.Clear();

            if (dgvHistorialUsuario.CurrentRow != null)
            {
                try
                {
                    USUARIO_HISTORIAL snapshotSeleccionado = (USUARIO_HISTORIAL)dgvHistorialUsuario.CurrentRow.DataBoundItem;

                    USUARIO usuarioRecompuesto = bllHistorial.RecomponerEstado(snapshotSeleccionado);

                    lstAtributos.Items.Add($"--- VERSIÓN {snapshotSeleccionado.Version} ---");
                    lstAtributos.Items.Add($"Fecha: {snapshotSeleccionado.FechaHora}");
                    lstAtributos.Items.Add($"Operación: {snapshotSeleccionado.Operacion}");
                    lstAtributos.Items.Add("");

                    lstAtributos.Items.Add($"[ DATOS DEL USUARIO ]");
                    lstAtributos.Items.Add($"Nombre: {usuarioRecompuesto.Nombre}");
                    lstAtributos.Items.Add($"Apellido: {usuarioRecompuesto.Apellido}");
                    lstAtributos.Items.Add($"Usuario: {usuarioRecompuesto.NombreUsuario}");
                    lstAtributos.Items.Add($"Sexo: {usuarioRecompuesto.Sexo}");

                    string estadoActivo = usuarioRecompuesto.Activo ? "Sí (Activo)" : "No (Eliminado)";
                    lstAtributos.Items.Add($"Activo: {estadoActivo}");

                    string estadoBloqueado = usuarioRecompuesto.Bloqueado ? "Sí" : "No";
                    lstAtributos.Items.Add($"Bloqueado: {estadoBloqueado}");

                    string idiomaNombre = usuarioRecompuesto.Idioma != null ? usuarioRecompuesto.Idioma.Nombre : "Sin Asignar";
                    lstAtributos.Items.Add($"Idioma Seleccionado: {idiomaNombre}");
                }
                catch (Exception ex)
                {
                    lstAtributos.Items.Add("Error al leer los detalles.");
                }
            }
        }

        private void dgvHistorialUsuario_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            Navegar(new frmMenu(), null, true);
        }

        private void dgvHistorialUsuario_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvHistorialUsuario.SelectedRows.Count > 0)
            {
                MostrarDetallesSnapshot();
            }
        }

        private void btnReactivar_Click(object sender, EventArgs e)
        {
            if (cbUsuario.SelectedItem == null) return;

            DialogResult dr = MessageBox.Show("¿Está seguro que desea reactivar este usuario?", "Confirmar Reactivación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (dr == DialogResult.Yes)
            {
                try
                {
                    USUARIO usuarioAReactivar = (USUARIO)cbUsuario.SelectedItem;
                    USUARIO adminResponsable = SINGLETON_SESION.GetInstance().usuario;

                    bllUsuario.ReactivarUsuario(usuarioAReactivar, adminResponsable);

                    MessageBox.Show("Usuario reactivado.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarComboUsuarios();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al reactivar");
                }
            }
        }
    }
}