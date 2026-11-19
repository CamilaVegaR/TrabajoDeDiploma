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
                try
                {
                    ETIQUETA nuevaEtiqueta = new ETIQUETA
                    {
                        Formulario = txtFormulario.Text.Trim(),
                        Clave = txtClave.Text.Trim()
                    };

                    bllEtiquetas.InsertarEtiqueta(nuevaEtiqueta);
                    RegistrarBitacora(BITACORA_ACTIVIDAD_ENUM.CREACION_ETIQUETA, nuevaEtiqueta.Formulario + "." + nuevaEtiqueta.Clave);

                    txtFormulario.Clear();
                    txtClave.Clear();

                    MessageBox.Show("Etiqueta guardada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarEtiquetas();
                }
                catch (Exception)
                {
                    MessageBox.Show("Error al guardar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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