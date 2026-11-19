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

        private BLL_ROLES_PERMISOS bllRolesPermisos = new BLL_ROLES_PERMISOS();

        public frmAdminUsuario()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (this.DesignMode) return;

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
                RegistrarBitacora(BITACORA_ACTIVIDAD_ENUM.MODIFICACION_USUARIO, "Usuario: " + usuarioActual.NombreUsuario);

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
                    RegistrarBitacora(BITACORA_ACTIVIDAD_ENUM.ELIMINACION_USUARIO, "Usuario: " + usuarioAEliminar.NombreUsuario);

                    MessageBox.Show("Usuario eliminado. Debido a la baja lógica, el usuario mantiene su trazabilidad.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarComboUsuarios();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            Navegar(new frmMenu(), null, true);
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
                    RegistrarBitacora(BITACORA_ACTIVIDAD_ENUM.REACTIVACION_USUARIO, "Usuario: " + usuarioAReactivar.NombreUsuario);

                    MessageBox.Show("Usuario reactivado.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarComboUsuarios();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al reactivar");
                }
            }
        }

        private void RegistrarBitacora(BITACORA_ACTIVIDAD_ENUM actividad, string observacion)
        {
            try
            {
                if (observacion != null && observacion.Length > 50)
                    observacion = observacion.Substring(0, 50);
                BITACORA bitacora = new BITACORA(Servicios.SINGLETON_SESION.GetInstance().usuario, DateTime.Now, actividad, observacion);
                if (new BLL_BITACORA().Insertar(bitacora) == -1)
                    MessageBox.Show("El cambio se guardó, pero no se pudo registrar en la bitácora.", "Bitácora", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch
            {
                MessageBox.Show("El cambio se guardó, pero no se pudo registrar en la bitácora.", "Bitácora", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}