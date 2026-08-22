using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BE;
using BLL;
using Servicios;

namespace ingenieria_software_santander_vega
{
    public partial class frmMiCuenta : FORM_BASE
    {
        public frmMiCuenta()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (this.DesignMode) return;

            CargarDatosUsuario();
        }

        private void CargarDatosUsuario()
        {
            try
            {
                cbSexo.DataSource = Enum.GetValues(typeof(SEXO_ENUM));

                if (SINGLETON_SESION.IsLoggedIn())
                {
                    USUARIO usuarioActual = SINGLETON_SESION.GetInstance().usuario;

                    txtNombre.Text = usuarioActual.Nombre;
                    txtApellido.Text = usuarioActual.Apellido;
                    txtNombreUsuario.Text = usuarioActual.NombreUsuario;
                    cbSexo.SelectedItem = usuarioActual.Sexo;
                }
                else
                {
                    MessageBox.Show("No hay sesión iniciada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Navegar(new frmMenu(), null, true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtApellido.Text) || string.IsNullOrWhiteSpace(txtNombreUsuario.Text))
                {
                    MessageBox.Show("Por favor, complete todos los campos obligatorios.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (SINGLETON_SESION.IsLoggedIn())
                {
                    USUARIO usuarioActual = SINGLETON_SESION.GetInstance().usuario;
                    BLL_USUARIO bllUsuario = new BLL_USUARIO();

                    string nuevoNombreUsuario = txtNombreUsuario.Text.Trim();

                    if (nuevoNombreUsuario != usuarioActual.NombreUsuario)
                    {
                        if (bllUsuario.CheckUsuarioExistente(nuevoNombreUsuario))
                        {
                            MessageBox.Show("El nombre de usuario ingresado ya está en uso. Por favor, elija otro.", "Usuario Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    usuarioActual.Nombre = txtNombre.Text;
                    usuarioActual.Apellido = txtApellido.Text;
                    usuarioActual.NombreUsuario = nuevoNombreUsuario;
                    usuarioActual.Sexo = (SEXO_ENUM)cbSexo.SelectedItem;


                    bllUsuario.ModificarUsuario(usuarioActual, usuarioActual);

                    MessageBox.Show("Tus datos han sido actualizados correctamente. Han quedado registrados en el historial de auditoría.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error al intentar actualizar la cuenta", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            Navegar(new frmMenu(), null, true);
        }

        private string MostrarDialogoPassword(string mensaje, string titulo)
        {
            Form prompt = new Form()
            {
                Width = 400,
                Height = 180,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = titulo,
                StartPosition = FormStartPosition.CenterScreen,
                MaximizeBox = false,
                MinimizeBox = false
            };

            Label textLabel = new Label() { Left = 20, Top = 20, Text = mensaje, Width = 340 };
            TextBox textBox = new TextBox() { Left = 20, Top = 50, Width = 340, UseSystemPasswordChar = true };
            Button confirmation = new Button() { Text = "Aceptar", Left = 260, Width = 100, Top = 90, DialogResult = DialogResult.OK };

            prompt.Controls.Add(textLabel);
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(confirmation);
            prompt.AcceptButton = confirmation;

            return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : string.Empty;
        }

        private void btnActualizarContrasena_Click(object sender, EventArgs e)
        {
            try
            {
                if (!SINGLETON_SESION.IsLoggedIn()) return;

                USUARIO usuarioActual = SINGLETON_SESION.GetInstance().usuario;
                BLL_USUARIO bllUsuario = new BLL_USUARIO();

                string contrasenaActual = MostrarDialogoPassword("Por seguridad, ingrese su contraseña ACTUAL:", "Verificación de Identidad");

                if (string.IsNullOrEmpty(contrasenaActual)) return;

                if (!bllUsuario.CheckContrasena(usuarioActual.Id, contrasenaActual))
                {
                    MessageBox.Show("La contraseña actual ingresada es incorrecta. Operación cancelada.", "Fallo de Seguridad", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string nuevaContrasena = MostrarDialogoPassword("Ingrese su NUEVA contraseña:", "Nueva Contraseña");
                if (string.IsNullOrEmpty(nuevaContrasena)) return;

                string confirmarContrasena = MostrarDialogoPassword("Vuelva a escribir su NUEVA contraseña para confirmar:", "Confirmación");
                if (nuevaContrasena != confirmarContrasena)
                {
                    MessageBox.Show("Las nuevas contraseñas no coinciden. Vuelva a intentarlo.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                bllUsuario.CambiarContrasena(usuarioActual.Id, nuevaContrasena, usuarioActual);

                MessageBox.Show("Tu contraseña ha sido actualizada con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al intentar cambiar la contraseña: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}