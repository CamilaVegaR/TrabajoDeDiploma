using BE;
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
    public partial class frmMenu : FORM_BASE
    {
        public frmMenu()
        {
            InitializeComponent();
            checkPermisos();
            cargarIdiomas();
        }

        BLL_USUARIO usuarioBLL;
        BLL_BITACORA bitacoraBLL;
        BLL_IDIOMA idiomaBLL;
        private bool cargandoIdiomas = false;

        private bool cerrandoSesion = false;

        private void cargarIdiomas()
        {
            cargandoIdiomas = true;
            idiomaBLL = new BLL_IDIOMA();
            List<IDIOMA> idiomas = idiomaBLL.ListarIdiomas();

            cbIdioma.DataSource = idiomas;
            cbIdioma.DisplayMember = "Nombre";
            cbIdioma.ValueMember = "Id";

            USUARIO usuarioActivo = SINGLETON_SESION.GetInstance().usuario;

            if (usuarioActivo != null && usuarioActivo.Idioma != null && usuarioActivo.Idioma.Id > 0)
            {
                foreach (IDIOMA idm in cbIdioma.Items)
                {
                    if (idm.Id == usuarioActivo.Idioma.Id)
                    {
                        cbIdioma.SelectedItem = idm;
                        break;
                    }
                }
            }
            else
            {
                foreach (IDIOMA idm in cbIdioma.Items)
                {
                    if (idm.PorDefecto)
                    {
                        cbIdioma.SelectedItem = idm;
                        break;
                    }
                }
            }

            cargandoIdiomas = false;

            if (cbIdioma.SelectedItem != null)
            {
                SESSION_IDIOMA.GetInstance().CambiarIdioma((IDIOMA)cbIdioma.SelectedItem);
            }
        }


        private void btnLogout_Click(object sender, EventArgs e)
        {
            if (!confirmarLogout()) { return; }

            try
            {
                RegistrarLogout();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            cerrandoSesion = true;
            this.Close();
            MostrarLogin();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (!cerrandoSesion &&
                !CerrandoPorNavegacion &&
                e.CloseReason == CloseReason.UserClosing)
            {
                if (!confirmarSalida())
                {
                    e.Cancel = true;
                    return;
                }

                try
                {
                    if (SINGLETON_SESION.IsLoggedIn())
                    {
                        RegistrarLogout();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Cancel = true;
                    return;
                }

                cerrandoSesion = true;
                base.OnFormClosing(e);
                Application.Exit();
                return;
            }

            base.OnFormClosing(e);
        }

        private void RegistrarLogout()
        {
            usuarioBLL = new BLL_USUARIO();

            BE.USUARIO usuario = usuarioBLL.logout();

            bitacoraBLL = new BLL_BITACORA();

            BITACORA log = new BITACORA(
                usuario,
                DateTime.Now,
                BE.BITACORA_ACTIVIDAD_ENUM.LOGOUT
            );

            int resultado = bitacoraBLL.Insertar(log);

            if (resultado <= 0)
            {
                usuarioBLL.Login(usuario);
                throw new Exception("Error al registrar el logout en la bitácora.");
            }

            GenerarBackupSiCorresponde();
        }


        private void GenerarBackupSiCorresponde()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                BLL_DIGITO_VERIFICADOR bllDV = new BLL_DIGITO_VERIFICADOR();
                if (bllDV.ValidarIntegridadBaseDatos().Count > 0)
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show(
                        "La base de datos presenta inconsistencias, por eso no se generó el backup de cierre de sesión.\n" +
                        "Se conserva el último backup correcto.",
                        "Backup", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                BLL_BACKUP backupBLL = new BLL_BACKUP();
                backupBLL.GenerarBackup();
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show(
                    "La sesión se cerró, pero no se pudo generar el backup.\n\nDetalle: " + ex.Message,
                    "Backup", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void MostrarLogin()
        {
            frmIniciarSesion login = Application.OpenForms.OfType<frmIniciarSesion>()
                .FirstOrDefault(f => !f.IsDisposed);

            if (login != null)
            {
                login.MostrarNuevamente();
            }
            else
            {
                new frmIniciarSesion().Show();
            }
        }

        private bool confirmarLogout()
        {
            var traducciones = SESSION_IDIOMA.GetInstance().Traducciones;
            string msj = traducciones != null && traducciones.ContainsKey("Msg_ConfirmarLogout") ? traducciones["Msg_ConfirmarLogout"] : "¿Está seguro que desea cerrar sesión?";
            string titulo = traducciones != null && traducciones.ContainsKey("Titulo_ConfirmarLogout") ? traducciones["Titulo_ConfirmarLogout"] : "Confirmar Logout";

            return MessageBox.Show(msj, titulo, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }

        private bool confirmarSalida()
        {
            var traducciones = SESSION_IDIOMA.GetInstance().Traducciones;
            string msj = traducciones != null && traducciones.ContainsKey("Msg_ConfirmarSalida") ? traducciones["Msg_ConfirmarSalida"] : "¿Desea salir del sistema? Se cerrará la sesión.";
            string titulo = traducciones != null && traducciones.ContainsKey("Titulo_ConfirmarSalida") ? traducciones["Titulo_ConfirmarSalida"] : "Salir";

            return MessageBox.Show(msj, titulo, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }

        private void checkPermisos()
        {
            USUARIO usuario = SINGLETON_SESION.GetInstance().usuario;
            if (usuario != null)
            {
                btnRecalcularDigitoVerificador.Visible = usuario.ValidarPermiso("RECALCULAR_DVV");

                adminToolStripMenuItem.Visible = usuario.ValidarPermiso("ADMIN");

                bitacoraToolStripMenuItem.Visible = usuario.ValidarPermiso("BITACORA");
                idiomasToolStripMenuItem.Visible = usuario.ValidarPermiso("ADMIN_IDIOMA");
                etiquetasToolStripMenuItem.Visible = usuario.ValidarPermiso("ADMIN_ETIQUETA");
                usuariosToolStripMenuItem.Visible = usuario.ValidarPermiso("ADMIN_USUARIO");

                rolesPermisosToolStripMenuItem.Visible = usuario.ValidarPermiso("ROLES_PERMISOS");
                historialToolStripMenuItem.Visible = usuario.ValidarPermiso("HISTORIAL");

                usuarioToolStripMenuItem.Visible = usuario.ValidarPermiso("USUARIO");
                miCuentaToolStripMenuItem.Visible = usuario.ValidarPermiso("MI_CUENTA");

                // RF1 - Agendar Reserva
                agendarReservaToolStripMenuItem.Visible = usuario.ValidarPermiso("AGENDAR_RESERVA_626CV");

                // RF2 - Gestión de Clases y Packs
                bool puedeRegistrarCompra = usuario.ValidarPermiso("REGISTRAR_COMPRA_626CV");
                bool puedeConfigurarPacks = usuario.ValidarPermiso("ADMIN_PACKS");
                registrarCompraToolStripMenuItem.Visible = puedeRegistrarCompra;
                configurarPacksToolStripMenuItem.Visible = puedeConfigurarPacks;
                // "Compra de Clases" solo se muestra si tiene al menos una de sus dos opciones
                registrarCompraToolStripMenuItem.OwnerItem.Visible = puedeRegistrarCompra || puedeConfigurarPacks;

                // Reportes
                reportesToolStripMenuItem.Visible = usuario.ValidarPermiso("REPORTES_626CV");

                // A03 - Serialización
                serializaciónToolStripMenuItem.Visible = usuario.ValidarPermiso("SERIALIZACION_626CV");

                // Maestros > Control de cambios de packs
                bool puedeVerCambiosPack = usuario.ValidarPermiso("CONTROL_CAMBIOS_PACK_626CV");
                controlCambiosPackToolStripMenuItem.Visible = puedeVerCambiosPack;
                maestrosToolStripMenuItem.Visible = puedeVerCambiosPack;
            }
        }

        private void bitacoraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Navegar(new frmBitacora(), null, true);
        }

        private void rolesPermisosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Navegar(new frmRolesPermisos(), null, true);
        }

        private void idiomasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Navegar(new frmIdiomas(), null, true);
        }

        private void etiquetasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Navegar(new frmEtiquetas(), null, true);
        }

        private void cbIdioma_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cargandoIdiomas) return;

            if (cbIdioma.SelectedItem != null)
            {
                IDIOMA idiomaSeleccionado = (IDIOMA)cbIdioma.SelectedItem;

                SESSION_IDIOMA.GetInstance().CambiarIdioma(idiomaSeleccionado);

                USUARIO usuarioActivo = SINGLETON_SESION.GetInstance().usuario;
                if (usuarioActivo != null)
                {
                    usuarioActivo.Idioma = idiomaSeleccionado;
                    usuarioBLL = new BLL_USUARIO();
                    usuarioBLL.ActualizarIdioma(usuarioActivo.Id, idiomaSeleccionado.Id);

                    // CU-04 paso 8: el cambio de idioma queda registrado en la bitácora
                    try
                    {
                        bitacoraBLL = new BLL_BITACORA();
                        bitacoraBLL.Insertar(new BITACORA(usuarioActivo, DateTime.Now,
                            BE.BITACORA_ACTIVIDAD_ENUM.CAMBIO_IDIOMA, "Idioma: " + idiomaSeleccionado.Nombre));
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Error al registrar la operación.", "Idioma", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        private void miCuentaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Navegar(new frmMiCuenta(), null, true);
        }

        private void historialToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Navegar(new frmAdminUsuario(), null, true);
        }

        private void btnRecalcularDigitoVerificador_Click(object sender, EventArgs e)
        {
            try
            {
                var traducciones = SESSION_IDIOMA.GetInstance().Traducciones;
                string msj = traducciones != null && traducciones.ContainsKey("Msg_ConfirmarRecalculo")
                    ? traducciones["Msg_ConfirmarRecalculo"]
                    : "Esta acción recalculará todos los sellos de seguridad (hashes) de la base de datos y establecerá un nuevo punto de integridad. ¿Desea continuar?";
                string titulo = traducciones != null && traducciones.ContainsKey("Titulo_ConfirmarRecalculo")
                    ? traducciones["Titulo_ConfirmarRecalculo"]
                    : "Recalcular Integridad";

                if (MessageBox.Show(msj, titulo, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    this.Cursor = Cursors.WaitCursor;

                    BLL_DIGITO_VERIFICADOR bllDV = new BLL_DIGITO_VERIFICADOR();
                    bllDV.RecalcularTablas();

                    bitacoraBLL = new BLL_BITACORA();
                    BITACORA log = new BITACORA(SINGLETON_SESION.GetInstance().usuario, DateTime.Now, BE.BITACORA_ACTIVIDAD_ENUM.MODIFICACION);
                    bitacoraBLL.Insertar(log);

                    this.Cursor = Cursors.Default;

                    string msjExito = traducciones != null && traducciones.ContainsKey("Msg_ExitoRecalculo")
                        ? traducciones["Msg_ExitoRecalculo"]
                        : "Dígitos verificadores recalculados y sincronizados correctamente.";
                    MessageBox.Show(msjExito, "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Error crítico al recalcular los dígitos verificadores", "Error de Integridad", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void agendarReservaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Navegar(new frmAgendarReserva(), null, true);
        }

     
        private void packsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Navegar(new frmPacks(), "ADMIN_PACKS", true);
        }

   
        private void claseSueltaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Navegar(new frmCompras(), null, true);
        }

        private void reportesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Navegar(new frmReportes(), "REPORTES_626CV", true);
        }

       
        private void serializaciónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Navegar(new frmSerializacion(), "SERIALIZACION_626CV", true);
        }

        private void maestrosToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void controlDeCambiosDePacksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Navegar(new frmControlCambiosPack(), "CONTROL_CAMBIOS_PACK_626CV", true);
        }

        private void ayudaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAyuda.Mostrar_626CV(this.Name, this);
        }
    }
}
