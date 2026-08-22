using BE;
using BLL;
using System;
using System.Windows.Forms;

namespace ingenieria_software_santander_vega
{
    public partial class frmEtiquetas : FORM_BASE
    {
        private BLL_ETIQUETAS bllEtiquetas;

        public frmEtiquetas()
        {
            InitializeComponent();
            bllEtiquetas = new BLL_ETIQUETAS();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            CargarEtiquetas();
        }

        private void CargarEtiquetas()
        {
            dgvEtiquetas.DataSource = null;
            dgvEtiquetas.DataSource = bllEtiquetas.ListarEtiquetas();

            if (dgvEtiquetas.Columns.Count > 0)
            {
                dgvEtiquetas.Columns["Id"].Visible = false;
                dgvEtiquetas.Columns["DVH"].Visible = false;
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtFormulario.Text) && !string.IsNullOrWhiteSpace(txtClave.Text))
            {
                ETIQUETA nuevaEtiqueta = new ETIQUETA
                {
                    Formulario = txtFormulario.Text.Trim(),
                    Clave = txtClave.Text.Trim()
                };

                bllEtiquetas.InsertarEtiqueta(nuevaEtiqueta);

                txtFormulario.Clear();
                txtClave.Clear();

                MessageBox.Show("Etiqueta guardada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarEtiquetas();
            }
            else
            {
                MessageBox.Show("Debe completar el formulario y la clave.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            Navegar(new frmMenu(), null, true);
        }
    }
}