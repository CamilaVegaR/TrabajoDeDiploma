using BLL;
using Servicios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ingenieria_software_santander_vega
{
    public partial class frmIniciarSesion : Form
    {
        public frmIniciarSesion()
        {
            InitializeComponent();
            validarIntegridadDB();
        }

        private bool navegandoInternamente = false;
        private bool cerrandoAplicacion = false;

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (!navegandoInternamente &&
                !cerrandoAplicacion &&
                e.CloseReason == CloseReason.UserClosing)
            {
                cerrandoAplicacion = true;
                Application.Exit();
            }

            base.OnFormClosing(e);
        }

        BLL.BLL_USUARIO usuarioBLL;
        BLL_BITACORA bitacoraBLL;

        private void validarIntegridadDB()
        {
            BLL_DIGITO_VERIFICADOR bllDV = new BLL_DIGITO_VERIFICADOR();
            List<string> errores = bllDV.ValidarIntegridadBaseDatos();

            if (errores.Count > 0)
            {
                string msj = "¡ALERTA CRÍTICA DE SEGURIDAD!\nSe ha detectado una alteración en la integridad de la base de datos:\n\n";

                foreach (var err in errores.Take(7))
                {
                    msj += "• " + err + "\n";
                }
                if (errores.Count > 7) msj += $"\n... y {errores.Count - 7} errores más ocultos.";

                msj += "\n\nEl sistema se bloqueará por seguridad. Contacte al administrador.";

                MessageBox.Show(msj, "Fallo de Integridad de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {

                if (!checkErrores())
                {
                    usuarioBLL = new BLL.BLL_USUARIO();
                    BE.USUARIO login = new BE.USUARIO(txtUsuario.Text, txtContrasena.Text);
                    usuarioBLL.Login(login);

                    if (SINGLETON_SESION.IsLoggedIn())
                    {
                        BE.BITACORA bitacora = new BE.BITACORA(0, SINGLETON_SESION.GetInstance().usuario, DateTime.Now, BE.BITACORA_ACTIVIDAD_ENUM.LOGIN);
                        bitacoraBLL = new BLL_BITACORA();
                        int resultado = bitacoraBLL.Insertar(bitacora);
                        if (resultado == -1)
                        {
                            usuarioBLL.logout();
                            MessageBox.Show("Hubo un error al tratar de registrar el login");
                        }

                        redirect(new frmMenu());
                    }
                    else
                    {
                        MessageBox.Show("Usuario o contraseña incorrectos");
                    }
                }
            }
            catch (Exception)
            {
                if (SINGLETON_SESION.IsLoggedIn())
                {
                    usuarioBLL.logout();
                }
                MessageBox.Show("Ocurrió un error inesperado");
            }
        }
        private void btnCrear_Click(object sender, EventArgs e)
        {
            redirect(new frmCrearCuenta());
        }

        private bool checkErrores()
        {
            if (string.IsNullOrEmpty(txtUsuario.Text) || string.IsNullOrEmpty(txtContrasena.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos.");
                return true;
            }
            return false;
        }

        private void redirect(Form form)
        {
            navegandoInternamente = true;

            form.Show();
            this.DialogResult = DialogResult.OK;
            this.Hide();

            navegandoInternamente = false;
        }
    }
}
