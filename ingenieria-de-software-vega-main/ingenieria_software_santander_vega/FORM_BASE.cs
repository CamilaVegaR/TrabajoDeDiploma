using BE;
using ingenieria_software_santander_vega;
using ingenieria_software_santander_vega.helpers;
using Servicios;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ingenieria_software_santander_vega
{
    public partial class FORM_BASE : Form, IOBSERVER_IDIOMA
    {
        private bool cerrandoPorNavegacion = false;

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (!cerrandoPorNavegacion &&
                e.CloseReason == CloseReason.UserClosing)
            {
                new frmMenu().Show();
            }

            base.OnFormClosing(e);
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
        protected void Navegar(Form formDestino, string permisoNecesario = null, bool cerrarActual = false)
        {
            try
            {
                if (cerrarActual)
                {
                    cerrandoPorNavegacion = true;
                }

                NAVEGACION_HELPER.Redirigir(this, formDestino, permisoNecesario, cerrarActual);
            }
            catch (UnauthorizedAccessException ex)
            {
                cerrandoPorNavegacion = false;

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
            TraducirControles(this);
        }

        private void TraducirControles(Control controlPadre)
        {
            var traducciones = SESSION_IDIOMA.GetInstance().Traducciones;

            if (traducciones == null) return;

            if (controlPadre == this && !string.IsNullOrWhiteSpace(this.Name) && traducciones.ContainsKey(this.Name))
            {
                this.Text = traducciones[this.Name];
            }

            if (controlPadre is ToolStrip toolStrip)
            {
                foreach (ToolStripItem item in toolStrip.Items)
                {
                    TraducirMenu(item, traducciones);
                }
            }

            foreach (Control control in controlPadre.Controls)
            {
                if (!string.IsNullOrWhiteSpace(control.Name) && traducciones.ContainsKey(control.Name))
                {
                    control.Text = traducciones[control.Name];
                }

                if (control.HasChildren || control is ToolStrip)
                {
                    TraducirControles(control);
                }
            }
        }

        private void TraducirMenu(ToolStripItem item, Dictionary<string, string> traducciones)
        {
            if (!string.IsNullOrWhiteSpace(item.Name) && traducciones.ContainsKey(item.Name))
            {
                item.Text = traducciones[item.Name];
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