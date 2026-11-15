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

namespace ingenieria_software_santander_vega
{
    public partial class frmIniciarSesion : Form
    {
        public frmIniciarSesion()
        {
            InitializeComponent();
            // El control de integridad ya no se hace al abrir: se hace después del login,
            // cuando se sabe quién ingresa y si tiene permiso para resolver el problema.
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

        // Se llama desde el menú al cerrar sesión: vuelve a mostrar esta misma ventana limpia.
        // El control de integridad se repite en el próximo login.
        public void MostrarNuevamente()
        {
            txtUsuario.Text = string.Empty;
            txtContrasena.Text = string.Empty;

            this.Show();
            this.Activate();
            txtUsuario.Focus();
        }

        // Control de integridad después del login.
        // Devuelve true si el usuario puede seguir al menú y false si tiene que quedarse en el login.
        // - Base íntegra: sigue.
        // - Base con errores y usuario administrador (permiso RECALCULAR_DVV): puede volver atrás
        //   restaurando el último backup o seguir al menú para recalcular los dígitos verificadores.
        // - Base con errores y usuario sin ese permiso: no puede ingresar; debe avisar al administrador.
        private bool validarIntegridadDB()
        {
            BLL_DIGITO_VERIFICADOR bllDV = new BLL_DIGITO_VERIFICADOR();
            List<string> errores = bllDV.ValidarIntegridadBaseDatos();

            if (errores.Count == 0) return true;

            string msj = "¡ALERTA CRÍTICA DE SEGURIDAD!\nSe ha detectado una alteración en la integridad de la base de datos:\n\n";

            foreach (var err in errores.Take(7))
            {
                msj += "• " + err + "\n";
            }
            if (errores.Count > 7) msj += $"\n... y {errores.Count - 7} errores más ocultos.";

            bool esAdministrador = SINGLETON_SESION.GetInstance().usuario.ValidarPermiso("RECALCULAR_DVV");

            if (!esAdministrador)
            {
                msj += "\n\nEl sistema no está disponible hasta que el administrador revise la base de datos." +
                       "\nContacte al administrador.";
                MessageBox.Show(msj, "Fallo de Integridad de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            BLL_BACKUP bllBackup = new BLL_BACKUP();
            DateTime? fechaBackup = null;
            try
            {
                fechaBackup = bllBackup.ObtenerFechaUltimoBackup();
            }
            catch
            {
                fechaBackup = null;
            }

            if (fechaBackup == null)
            {
                msj += "\n\nNo hay backups disponibles para volver atrás." +
                       "\nPuede ingresar y recalcular los dígitos verificadores desde el menú.";
                MessageBox.Show(msj, "Fallo de Integridad de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true;
            }

            msj += $"\n\n¿Desea VOLVER ATRÁS restaurando el último backup ({fechaBackup.Value:dd/MM/yyyy HH:mm})?" +
                   "\nSe perderán los cambios realizados después de esa fecha." +
                   "\n\nSí: restaurar el backup." +
                   "\nNo: ingresar al sistema y recalcular los dígitos verificadores desde el menú.";

            if (MessageBox.Show(msj, "Fallo de Integridad de Datos", MessageBoxButtons.YesNo, MessageBoxIcon.Error) != DialogResult.Yes)
                return true;

            try
            {
                this.Cursor = Cursors.WaitCursor;
                bllBackup.RestaurarUltimoBackup();
                this.Cursor = Cursors.Default;

                if (bllDV.ValidarIntegridadBaseDatos().Count == 0)
                {
                    MessageBox.Show($"La base de datos se restauró correctamente al backup del {fechaBackup.Value:dd/MM/yyyy HH:mm}.\nVuelva a iniciar sesión.",
                        "Volver atrás", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("La base de datos se restauró, pero el backup también presenta inconsistencias.\nVuelva a iniciar sesión y recalcule los dígitos verificadores.",
                        "Volver atrás", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("No se pudo restaurar el backup.\n" + ex.Message, "Volver atrás", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Después de restaurar, la base cambió: se pide iniciar sesión de nuevo.
            return false;
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
                        // Control de integridad: si no se puede seguir, se cierra la sesión y queda en el login.
                        if (!validarIntegridadDB())
                        {
                            usuarioBLL.logout();
                            txtContrasena.Text = string.Empty;
                            return;
                        }

                        BE.BITACORA bitacora = new BE.BITACORA(0, SINGLETON_SESION.GetInstance().usuario, DateTime.Now, BE.BITACORA_ACTIVIDAD_ENUM.LOGIN);
                        bitacoraBLL = new BLL_BITACORA();
                        int resultado = bitacoraBLL.Insertar(bitacora);
                        if (resultado == -1)
                        {
                            usuarioBLL.logout();
                            MessageBox.Show("Hubo un error al tratar de registrar el login");
                            return;
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