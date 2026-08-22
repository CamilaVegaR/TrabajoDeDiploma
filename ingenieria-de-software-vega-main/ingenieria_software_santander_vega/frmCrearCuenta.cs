using BE;
using BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.DateTime;

namespace ingenieria_software_santander_vega
{
    public partial class frmCrearCuenta : Form
    {
        public frmCrearCuenta()
        {
            InitializeComponent();
            rellenarSexos();
        }

        BLL_USUARIO usuarioBLL;
        BLL_BITACORA bitacoraBLL;
        USUARIO usuario;

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                new frmIniciarSesion().Show();
            }

            base.OnFormClosing(e);
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            try
            {
                if (!checkErrores())
                {
                    USUARIO infoLogin = new USUARIO(txtNombreUsuario.Text, txtContrasena.Text);

                    usuarioBLL = new BLL_USUARIO();
                    if (!usuarioBLL.CheckUsuarioExistente(infoLogin.NombreUsuario))
                    {
                        usuario = new BE.USUARIO(txtNombre.Text, txtApellido.Text, (BE.SEXO_ENUM)cbSexo.SelectedValue, txtNombreUsuario.Text, txtContrasena.Text);
                        int resultado = usuarioBLL.Insertar(usuario);
                        if (resultado == -1)
                        {
                            throw new Exception("Ocurrió un error al tratar de crear el usuario");
                        }

                        BE.BITACORA bitacora = new BITACORA(usuario, DateTime.Now, BITACORA_ACTIVIDAD_ENUM.CREACION_USUARIO);
                        bitacoraBLL = new BLL_BITACORA();
                        resultado = bitacoraBLL.Insertar(bitacora);

                        if (resultado == -1)
                        {
                            throw new Exception("Ocurrió un error al tratar de registrar la actividad en la bitácora");
                        }

                        redirect(new frmIniciarSesion());
                    }
                    else
                    {
                        MessageBox.Show("El nombre de usuario ya existe. Por favor, elija otro.");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error inesperado");
            }
        }

        private bool checkErrores()
        {
            if (string.IsNullOrEmpty(txtNombre.Text) || string.IsNullOrEmpty(txtApellido.Text) ||
                string.IsNullOrEmpty(txtNombreUsuario.Text) || string.IsNullOrEmpty(txtContrasena.Text) ||
                cbSexo.SelectedIndex < 0)
            {
                MessageBox.Show("Por favor, complete todos los campos.");
                return true;
            }
            return false;
        }
        private void redirect(Form form)
        {
            form.Show();
            this.DialogResult = DialogResult.OK;
            this.Hide();
        }

        private void rellenarSexos()
        {
            cbSexo.DataSource = null;
            cbSexo.DataSource = Enum.GetValues(typeof(BE.SEXO_ENUM));
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            redirect(new frmIniciarSesion());
        }
    }
}
