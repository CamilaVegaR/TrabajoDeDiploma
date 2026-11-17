using BE;
using BLL;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ingenieria_software_santander_vega
{
    // Ajustar Saldo de Clases_626CV (RF2)
    public partial class frmAjusteSaldo : FORM_BASE
    {
        private BLL_CLIENTE bllCliente_626CV;
        private CLIENTE clienteActual_626CV;

        public frmAjusteSaldo()
        {
            InitializeComponent();
            bllCliente_626CV = new BLL_CLIENTE();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            LimpiarCliente_626CV();
        }

        private void btnBuscarCliente_626CV_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDni_626CV.Text))
            {
                MessageBox.Show("Debe ingresar el DNI del cliente.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            clienteActual_626CV = bllCliente_626CV.ObtenerPorDni_626CV(txtDni_626CV.Text.Trim());

            if (clienteActual_626CV != null)
            {
                MostrarCliente_626CV();
            }
            else
            {
                LimpiarCliente_626CV();
                lblCliente_626CV.Text = "Cliente no encontrado.";
                lblCliente_626CV.ForeColor = Color.DarkRed;
            }
        }

        private void btnAjustar_626CV_Click(object sender, EventArgs e)
        {
            if (clienteActual_626CV == null)
            {
                MessageBox.Show("Debe buscar un cliente.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtMotivo_626CV.Text))
            {
                MessageBox.Show("Debe ingresar el motivo del ajuste.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int cantidad_626CV = (int)nudCantidad_626CV.Value;
            if (rdbRestar_626CV.Checked)
                cantidad_626CV = -cantidad_626CV;

            string operacion_626CV = rdbRestar_626CV.Checked ? "restar" : "sumar";
            DialogResult confirmacion_626CV = MessageBox.Show(
                "¿Está seguro que desea " + operacion_626CV + " " + (int)nudCantidad_626CV.Value + " clase(s) al saldo de "
                + clienteActual_626CV.Nombre_626CV + " " + clienteActual_626CV.Apellido_626CV + "?",
                "Ajuste de saldo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmacion_626CV != DialogResult.Yes) return;

            string resultado_626CV = bllCliente_626CV.AjustarSaldo_626CV(clienteActual_626CV, cantidad_626CV, txtMotivo_626CV.Text);

            if (resultado_626CV != null)
            {
                MessageBox.Show(resultado_626CV, "Ajuste de saldo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            clienteActual_626CV = bllCliente_626CV.ObtenerPorId_626CV(clienteActual_626CV.Id_626CV);
            MostrarCliente_626CV();
            nudCantidad_626CV.Value = 1;
            rdbSumar_626CV.Checked = true;
            txtMotivo_626CV.Clear();

            MessageBox.Show("Saldo ajustado correctamente.", "Ajuste de saldo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnVolver_626CV_Click(object sender, EventArgs e)
        {
            Navegar(new frmMenu(), null, true);
        }

        private void MostrarCliente_626CV()
        {
            lblCliente_626CV.Text = "Cliente: " + clienteActual_626CV.Nombre_626CV + " " + clienteActual_626CV.Apellido_626CV
                + " — Saldo: " + clienteActual_626CV.SaldoClases_626CV + " clase(s)";
            lblCliente_626CV.ForeColor = Color.DarkBlue;
            btnAjustar_626CV.Enabled = true;
        }

        private void LimpiarCliente_626CV()
        {
            clienteActual_626CV = null;
            lblCliente_626CV.Text = "Busque un cliente por DNI.";
            lblCliente_626CV.ForeColor = SystemColors.ControlText;
            btnAjustar_626CV.Enabled = false;
        }
    }
}