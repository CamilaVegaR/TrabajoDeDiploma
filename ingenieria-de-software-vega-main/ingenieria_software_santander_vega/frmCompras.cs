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

namespace ingenieria_software_santander_vega
{

    public partial class frmCompras : FORM_BASE
    {
        private CLIENTE clienteActual_626CV;
        private BLL_CLIENTE bllCliente_626CV;
        private BLL_PACK bllPack_626CV;
        private BLL_COMPRA bllCompra_626CV;

        public frmCompras()
        {
            InitializeComponent();
            bllCliente_626CV = new BLL_CLIENTE();
            bllPack_626CV = new BLL_PACK();
            bllCompra_626CV = new BLL_COMPRA();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            ConfigurarColumnasGrilla_626CV();
            CargarCanalesContacto_626CV();
            CargarPacks_626CV();
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
                pnlClienteNuevo_626CV.Visible = false;
                CargarHistorial_626CV();
            }
            else
            {
                
                lblClienteEncontrado_626CV.Text = "Cliente no encontrado. Complete los datos para registrarlo.";
                pnlClienteNuevo_626CV.Visible = true;
                dgvCompras_626CV.Rows.Clear();
                lblHistorial_626CV.Text = "Historial de compras";
            }
        }

        private void btnRegistrarCliente_626CV_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombreNuevo_626CV.Text) ||
                string.IsNullOrWhiteSpace(txtApellidoNuevo_626CV.Text))
            {
                MessageBox.Show("Nombre y apellido son obligatorios.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            CLIENTE nuevo_626CV = new CLIENTE(
                txtNombreNuevo_626CV.Text.Trim(),
                txtApellidoNuevo_626CV.Text.Trim(),
                txtDni_626CV.Text.Trim(),
                txtTelefonoNuevo_626CV.Text.Trim(),
                txtEmailNuevo_626CV.Text.Trim());

            clienteActual_626CV = bllCliente_626CV.RegistrarCliente_626CV(nuevo_626CV);
            MostrarCliente_626CV();
            pnlClienteNuevo_626CV.Visible = false;

            MessageBox.Show("Cliente registrado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            CargarHistorial_626CV();
        }


        private void cmbPack_626CV_SelectedIndexChanged(object sender, EventArgs e)
        {
            MostrarImporte_626CV();
        }

        private void btnRegistrarCompra_626CV_Click(object sender, EventArgs e)
        {
            if (clienteActual_626CV == null)
            {
                MessageBox.Show("Debe buscar o registrar un cliente primero.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            PACK packSeleccionado_626CV = cmbPack_626CV.SelectedItem as PACK;
            if (packSeleccionado_626CV == null)
            {
                MessageBox.Show("Debe seleccionar un pack.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            COMPRA compraRegistrada_626CV;
            string resultado_626CV = bllCompra_626CV.RegistrarCompra_626CV(
                clienteActual_626CV, packSeleccionado_626CV, ObtenerCanalContactoSeleccionado_626CV(), out compraRegistrada_626CV);

            if (resultado_626CV == null)
            {
                
                clienteActual_626CV = bllCliente_626CV.ObtenerPorId_626CV(clienteActual_626CV.Id_626CV);
                MostrarCliente_626CV();

                decimal importe_626CV = compraRegistrada_626CV != null
                    ? compraRegistrada_626CV.Importe_626CV
                    : bllPack_626CV.CalcularPrecioFinal_626CV(packSeleccionado_626CV);

                MessageBox.Show(
                    $"Compra registrada correctamente.\nImporte a cobrar: $ {importe_626CV:N2}\nSaldo actual: {clienteActual_626CV.SaldoClases_626CV} clase(s).",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                CargarHistorial_626CV();
            }
            else
            {
                MessageBox.Show(resultado_626CV, "No se pudo registrar la compra", MessageBoxButtons.OK, MessageBoxIcon.Warning);
               
                CargarPacks_626CV();
            }
        }

        private void btnVolver_626CV_Click(object sender, EventArgs e)
        {
            Navegar(new frmMenu(), null, true);
        }

  
        private void CargarPacks_626CV()
        {
            List<PACK> packs_626CV = bllPack_626CV.ListarActivos_626CV();
            cmbPack_626CV.DataSource = null;
            cmbPack_626CV.DataSource = packs_626CV;

            if (packs_626CV.Count == 0)
            {
                
                lblImporte_626CV.Text = "No hay packs disponibles para la venta.";
                lblImporte_626CV.ForeColor = Color.DarkRed;
                btnRegistrarCompra_626CV.Enabled = false;
            }
            else
            {
                btnRegistrarCompra_626CV.Enabled = true;
                MostrarImporte_626CV();
            }
        }

        private void cmbPack_626CV_Format(object sender, ListControlConvertEventArgs e)
        {
            PACK pack_626CV = e.ListItem as PACK;
            if (pack_626CV != null)
            {
                e.Value = $"{pack_626CV.Nombre_626CV} - {pack_626CV.CantidadClases_626CV} clase(s) - " +
                          $"{pack_626CV.PorcentajeDescuento_626CV:0.##}% desc. - $ {bllPack_626CV.CalcularPrecioFinal_626CV(pack_626CV):N2}";
            }
        }

        private void MostrarImporte_626CV()
        {
            PACK pack_626CV = cmbPack_626CV.SelectedItem as PACK;
            if (pack_626CV == null)
            {
                lblImporte_626CV.Text = "";
                return;
            }

            lblImporte_626CV.Text = $"Importe a cobrar: $ {bllPack_626CV.CalcularPrecioFinal_626CV(pack_626CV):N2}";
            lblImporte_626CV.ForeColor = Color.DarkGreen;
        }

        private void MostrarCliente_626CV()
        {
            lblClienteEncontrado_626CV.Text =
                $"Cliente: {clienteActual_626CV.Nombre_626CV} {clienteActual_626CV.Apellido_626CV} — Saldo: {clienteActual_626CV.SaldoClases_626CV} clase(s)";
        }

        private void LimpiarCliente_626CV()
        {
            clienteActual_626CV = null;
            lblClienteEncontrado_626CV.Text = "";
            pnlClienteNuevo_626CV.Visible = false;
            dgvCompras_626CV.Rows.Clear();
            lblHistorial_626CV.Text = "Historial de compras";
        }

        // CU06 - Consultar historial de compras
        private void CargarHistorial_626CV()
        {
            dgvCompras_626CV.Rows.Clear();
            if (clienteActual_626CV == null) return;

            List<COMPRA> compras_626CV = bllCompra_626CV.ListarPorCliente_626CV(clienteActual_626CV.Id_626CV);

            if (compras_626CV.Count == 0)
            {
                
                lblHistorial_626CV.Text = "Historial de compras: el cliente no registra compras.";
                return;
            }

            lblHistorial_626CV.Text = "Historial de compras";
            foreach (COMPRA compra_626CV in compras_626CV)
            {
                dgvCompras_626CV.Rows.Add(
                    compra_626CV.FechaCompra_626CV.ToString("dd/MM/yyyy HH:mm"),
                    compra_626CV.Pack_626CV.Nombre_626CV,
                    compra_626CV.CantidadClases_626CV,
                    $"{compra_626CV.PorcentajeDescuento_626CV:0.##}%",
                    $"$ {compra_626CV.Importe_626CV:N2}",
                    ObtenerDescripcionCanal_626CV(compra_626CV.CanalContacto_626CV));
            }
        }

        private void ConfigurarColumnasGrilla_626CV()
        {
            dgvCompras_626CV.AutoGenerateColumns = false;
            dgvCompras_626CV.AllowUserToAddRows = false;
            dgvCompras_626CV.Columns.Clear();

            dgvCompras_626CV.Columns.Add(new DataGridViewTextBoxColumn { Name = "colFecha", HeaderText = "Fecha", Width = 130 });
            dgvCompras_626CV.Columns.Add(new DataGridViewTextBoxColumn { Name = "colPack", HeaderText = "Pack", Width = 150 });
            dgvCompras_626CV.Columns.Add(new DataGridViewTextBoxColumn { Name = "colClases", HeaderText = "Clases", Width = 70 });
            dgvCompras_626CV.Columns.Add(new DataGridViewTextBoxColumn { Name = "colDescuento", HeaderText = "Descuento", Width = 90 });
            dgvCompras_626CV.Columns.Add(new DataGridViewTextBoxColumn { Name = "colImporte", HeaderText = "Importe", Width = 110 });
            dgvCompras_626CV.Columns.Add(new DataGridViewTextBoxColumn { Name = "colCanal", HeaderText = "Canal", Width = 110 });
        }

        private void CargarCanalesContacto_626CV()
        {
            cmbCanalContacto_626CV.Items.Clear();
            cmbCanalContacto_626CV.Items.Add("Presencial");
            cmbCanalContacto_626CV.Items.Add("Telefónico");
            cmbCanalContacto_626CV.Items.Add("Redes sociales");
            cmbCanalContacto_626CV.SelectedIndex = 0;
        }

        private CANAL_CONTACTO_ENUM ObtenerCanalContactoSeleccionado_626CV()
        {
            switch (cmbCanalContacto_626CV.SelectedIndex)
            {
                case 1: return CANAL_CONTACTO_ENUM.TELEFONICO_626CV;
                case 2: return CANAL_CONTACTO_ENUM.REDES_SOCIALES_626CV;
                default: return CANAL_CONTACTO_ENUM.PRESENCIAL_626CV;
            }
        }

        private string ObtenerDescripcionCanal_626CV(CANAL_CONTACTO_ENUM canal_626CV)
        {
            switch (canal_626CV)
            {
                case CANAL_CONTACTO_ENUM.TELEFONICO_626CV: return "Telefónico";
                case CANAL_CONTACTO_ENUM.REDES_SOCIALES_626CV: return "Redes sociales";
                default: return "Presencial";
            }
        }
    }

}
