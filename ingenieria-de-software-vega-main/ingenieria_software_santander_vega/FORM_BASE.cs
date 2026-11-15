using BE;
using ingenieria_software_santander_vega;
using ingenieria_software_santander_vega.helpers;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ingenieria_software_santander_vega
{
    public partial class FORM_BASE : Form, IOBSERVER_IDIOMA
    {

        protected bool CerrandoPorNavegacion { get; private set; } = false;

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (e.Cancel) return;
            if (!CerrandoPorNavegacion &&
                e.CloseReason == CloseReason.UserClosing &&
                !(this is frmMenu) &&
                SINGLETON_SESION.IsLoggedIn())
            {
                VolverAlMenu();
            }
        }

        private void VolverAlMenu()
        {

            frmMenu menu = Application.OpenForms.OfType<frmMenu>()
                .FirstOrDefault(f => !f.IsDisposed);

            if (menu == null)
            {
                menu = new frmMenu();
            }

            menu.Show();
            menu.Activate();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            SESSION_IDIOMA.GetInstance().Desuscribir(this);
            base.OnFormClosed(e);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            SESSION_IDIOMA.GetInstance().Suscribir(this);

            if (SESSION_IDIOMA.GetInstance().IdiomaActual != null)
            {
                if (SESSION_IDIOMA.GetInstance().IdiomaActual.PorDefecto)
                {
                    SINCRONIZADOR_IDIOMA sync = new SINCRONIZADOR_IDIOMA();
                    sync.SincronizarFormulario(this);
                }

                ActualizarIdioma(SESSION_IDIOMA.GetInstance().IdiomaActual);
            }
        }

   
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F1)
            {
                frmAyuda.Mostrar_626CV(this.Name, this);
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        protected void Navegar(Form formDestino, string permisoNecesario = null, bool cerrarActual = false)
        {
            try
            {
                if (cerrarActual)
                {
                    CerrandoPorNavegacion = true;
                }

                NAVEGACION_HELPER.Redirigir(this, formDestino, permisoNecesario, cerrarActual);
            }
            catch (UnauthorizedAccessException ex)
            {
                CerrandoPorNavegacion = false;

                formDestino.Dispose();

                string mensaje = SESSION_IDIOMA.GetInstance().Traducciones != null &&
                                 SESSION_IDIOMA.GetInstance().Traducciones.ContainsKey("Msg_AccesoDenegado")
                    ? SESSION_IDIOMA.GetInstance().Traducciones["Msg_AccesoDenegado"]
                    : ex.Message;

                MessageBox.Show(mensaje, "Seguridad", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public virtual void ActualizarIdioma(IDIOMA idioma)
        {
            var traducciones = SESSION_IDIOMA.GetInstance().Traducciones;
            if (traducciones == null) return;

            if (!string.IsNullOrWhiteSpace(this.Name))
                this.Text = TextoTraducido(this, this.Name, this.Text, traducciones);

            TraducirControles(this, traducciones);
        }

        private readonly Dictionary<object, string> textosOriginales = new Dictionary<object, string>();

        private static bool EsTextoPendiente(string texto)
        {
            return string.IsNullOrWhiteSpace(texto)
                || texto == "[Cambiar traducción]"
                || texto == "[Nueva Etiqueta - Traducir en BD]";
        }

        private string TextoTraducido(object elemento, string clave, string textoActual, Dictionary<string, string> traducciones)
        {
            if (!textosOriginales.ContainsKey(elemento))
                textosOriginales[elemento] = textoActual;

            string traduccion;
            if (traducciones.TryGetValue(clave, out traduccion) && !EsTextoPendiente(traduccion))
                return traduccion;

            return textosOriginales[elemento];
        }


        private void TraducirControles(Control controlPadre, Dictionary<string, string> traducciones)
        {
            foreach (Control control in controlPadre.Controls)
            {
                if (EsTraducible(control) && !string.IsNullOrWhiteSpace(control.Name))
                {
                    control.Text = TextoTraducido(control, control.Name, control.Text, traducciones);
                }

                if (control is ToolStrip toolStrip)
                {
                    foreach (ToolStripItem item in toolStrip.Items)
                    {
                        TraducirMenu(item, traducciones);
                    }
                }

                if (control.HasChildren && !(control is DataGridView))
                {
                    TraducirControles(control, traducciones);
                }
            }
        }

        private bool EsTraducible(Control control)
        {
            return control is Label
                || control is Button
                || control is GroupBox
                || control is CheckBox
                || control is RadioButton
                || control is TabPage
                || control is LinkLabel;
        }

        private void TraducirMenu(ToolStripItem item, Dictionary<string, string> traducciones)
        {
            if (!string.IsNullOrWhiteSpace(item.Name))
            {
                item.Text = TextoTraducido(item, item.Name, item.Text, traducciones);
            }

            if (item is ToolStripMenuItem menuItem)
            {
                foreach (ToolStripItem subItem in menuItem.DropDownItems)
                {
                    TraducirMenu(subItem, traducciones);
                }
            }
        }
    }
}
