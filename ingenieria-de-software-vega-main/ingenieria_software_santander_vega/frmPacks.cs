using BE;
using BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ingenieria_software_santander_vega
{
    // CU07 - Crear Pack_626CV / CU08 - Modificar Pack_626CV / CU09 - Desactivar Pack_626CV
    public partial class frmPacks : FORM_BASE
    {
        private BLL_PACK bllPack_626CV;
        private List<PACK> packs_626CV;

        // null = se está creando un pack nuevo (CU07); distinto de null = se modifica ese pack (CU08)
        private PACK packSeleccionado_626CV;

        public frmPacks()
        {
            InitializeComponent();
            bllPack_626CV = new BLL_PACK();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            ConfigurarColumnasGrilla_626CV();
            CargarPacks_626CV();
            PrepararNuevoPack_626CV();
        }

        // ----------------------------------------------------------------
        // CU07 - Crear Pack (botón "Nuevo" deja los campos vacíos)
        // ----------------------------------------------------------------
        private void btnNuevo_626CV_Click(object sender, EventArgs e)
        {
            PrepararNuevoPack_626CV();
            txtNombre_626CV.Focus();
        }

        // ----------------------------------------------------------------
        // CU08 - Modificar Pack (al hacer clic en una fila se cargan sus valores)
        // ----------------------------------------------------------------
        private void dgvPacks_626CV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            int idPack_626CV = Convert.ToInt32(dgvPacks_626CV.Rows[e.RowIndex].Cells["colIdPack"].Value);
            packSeleccionado_626CV = packs_626CV.FirstOrDefault(p => p.Id_626CV == idPack_626CV);
            if (packSeleccionado_626CV == null) return;

            txtNombre_626CV.Text = packSeleccionado_626CV.Nombre_626CV;
            txtCantidadClases_626CV.Text = packSeleccionado_626CV.CantidadClases_626CV.ToString();
            txtPrecioLista_626CV.Text = packSeleccionado_626CV.PrecioLista_626CV.ToString("0.##");
            txtDescuento_626CV.Text = packSeleccionado_626CV.PorcentajeDescuento_626CV.ToString("0.##");

            lblModo_626CV.Text = packSeleccionado_626CV.Activo_626CV
                ? $"Modificando: {packSeleccionado_626CV.Nombre_626CV}"
                : $"{packSeleccionado_626CV.Nombre_626CV} (desactivado)";
            lblModo_626CV.ForeColor = packSeleccionado_626CV.Activo_626CV ? Color.DarkBlue : Color.DarkRed;
        }

        // Guarda un pack nuevo (CU07) o los cambios del pack seleccionado (CU08)
        private void btnGuardar_626CV_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre_626CV.Text)
                || string.IsNullOrWhiteSpace(txtCantidadClases_626CV.Text)
                || string.IsNullOrWhiteSpace(txtPrecioLista_626CV.Text)
                || string.IsNullOrWhiteSpace(txtDescuento_626CV.Text))
            {
                MessageBox.Show("Debe completar todos los campos.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int cantidad_626CV;
            decimal precioLista_626CV, descuento_626CV;
            if (!int.TryParse(txtCantidadClases_626CV.Text.Trim(), out cantidad_626CV)
                || !TryParseDecimal_626CV(txtPrecioLista_626CV.Text, out precioLista_626CV)
                || !TryParseDecimal_626CV(txtDescuento_626CV.Text, out descuento_626CV))
            {
                MessageBox.Show("La cantidad de clases y el precio de lista deben ser mayores a cero, y el descuento debe estar entre 0 y 100.",
                    "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool esNuevo_626CV = packSeleccionado_626CV == null;
            PACK pack_626CV = new PACK(txtNombre_626CV.Text, cantidad_626CV, precioLista_626CV, descuento_626CV);

            string resultado_626CV;
            if (esNuevo_626CV)
            {
                resultado_626CV = bllPack_626CV.CrearPack_626CV(pack_626CV);
            }
            else
            {
                pack_626CV.Id_626CV = packSeleccionado_626CV.Id_626CV;
                resultado_626CV = bllPack_626CV.ModificarPack_626CV(pack_626CV);
            }

            if (resultado_626CV == null)
            {
                MessageBox.Show(esNuevo_626CV ? "Pack creado correctamente." : "Pack modificado correctamente.",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarPacks_626CV();
                PrepararNuevoPack_626CV();
            }
            else
            {
                MessageBox.Show(resultado_626CV, "Packs", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // ----------------------------------------------------------------
        // CU09 - Desactivar Pack
        // ----------------------------------------------------------------
        private void btnDesactivar_626CV_Click(object sender, EventArgs e)
        {
            if (packSeleccionado_626CV == null)
            {
                MessageBox.Show("Debe seleccionar un pack de la grilla.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!packSeleccionado_626CV.Activo_626CV)
            {
                MessageBox.Show("El pack seleccionado ya se encuentra desactivado.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirmacion_626CV = MessageBox.Show(
                "¿Está seguro que desea desactivar el pack seleccionado? Dejará de estar disponible para la venta, pero las compras ya registradas no se modificarán.",
                "Desactivar pack", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmacion_626CV != DialogResult.Yes) return;

            string resultado_626CV = bllPack_626CV.DesactivarPack_626CV(packSeleccionado_626CV);

            if (resultado_626CV == null)
            {
                MessageBox.Show("Pack desactivado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarPacks_626CV();
                PrepararNuevoPack_626CV();
            }
            else
            {
                MessageBox.Show(resultado_626CV, "No se pudo desactivar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnVolver_626CV_Click(object sender, EventArgs e)
        {
            Navegar(new frmMenu(), null, true);
        }

        // Recalcula el precio final mientras se escribe el precio o el descuento
        private void txtPrecioODescuento_626CV_TextChanged(object sender, EventArgs e)
        {
            decimal precioLista_626CV, descuento_626CV;
            if (TryParseDecimal_626CV(txtPrecioLista_626CV.Text, out precioLista_626CV)
                && TryParseDecimal_626CV(txtDescuento_626CV.Text, out descuento_626CV)
                && precioLista_626CV > 0 && descuento_626CV >= 0 && descuento_626CV <= 100)
            {
                PACK packTemporal_626CV = new PACK { PrecioLista_626CV = precioLista_626CV, PorcentajeDescuento_626CV = descuento_626CV };
                lblPrecioFinal_626CV.Text = $"Precio final: $ {packTemporal_626CV.CalcularPrecioFinal_626CV():N2}";
            }
            else
            {
                lblPrecioFinal_626CV.Text = "Precio final: -";
            }
        }

        // ----------------------------------------------------------------
        // Métodos auxiliares
        // ----------------------------------------------------------------
        private void CargarPacks_626CV()
        {
            packs_626CV = bllPack_626CV.ListarTodos_626CV();
            dgvPacks_626CV.Rows.Clear();

            foreach (PACK pack_626CV in packs_626CV)
            {
                int fila_626CV = dgvPacks_626CV.Rows.Add(
                    pack_626CV.Id_626CV,
                    pack_626CV.Nombre_626CV,
                    pack_626CV.CantidadClases_626CV,
                    $"$ {pack_626CV.PrecioLista_626CV:N2}",
                    $"{pack_626CV.PorcentajeDescuento_626CV:0.##}%",
                    $"$ {pack_626CV.CalcularPrecioFinal_626CV():N2}",
                    pack_626CV.ObtenerDescripcionEstado_626CV());

                if (!pack_626CV.Activo_626CV)
                    dgvPacks_626CV.Rows[fila_626CV].DefaultCellStyle.ForeColor = Color.Gray;
            }

            dgvPacks_626CV.ClearSelection();
        }

        private void PrepararNuevoPack_626CV()
        {
            packSeleccionado_626CV = null;
            txtNombre_626CV.Text = "";
            txtCantidadClases_626CV.Text = "";
            txtPrecioLista_626CV.Text = "";
            txtDescuento_626CV.Text = "0";
            lblModo_626CV.Text = "Nuevo pack";
            lblModo_626CV.ForeColor = Color.DarkGreen;
            dgvPacks_626CV.ClearSelection();
        }

        private void ConfigurarColumnasGrilla_626CV()
        {
            dgvPacks_626CV.AutoGenerateColumns = false;
            dgvPacks_626CV.AllowUserToAddRows = false;
            dgvPacks_626CV.Columns.Clear();

            dgvPacks_626CV.Columns.Add(new DataGridViewTextBoxColumn { Name = "colIdPack", HeaderText = "Id", Visible = false });
            dgvPacks_626CV.Columns.Add(new DataGridViewTextBoxColumn { Name = "colNombre", HeaderText = "Nombre", Width = 160 });
            dgvPacks_626CV.Columns.Add(new DataGridViewTextBoxColumn { Name = "colClases", HeaderText = "Clases", Width = 70 });
            dgvPacks_626CV.Columns.Add(new DataGridViewTextBoxColumn { Name = "colPrecioLista", HeaderText = "Precio lista", Width = 110 });
            dgvPacks_626CV.Columns.Add(new DataGridViewTextBoxColumn { Name = "colDescuento", HeaderText = "Descuento", Width = 90 });
            dgvPacks_626CV.Columns.Add(new DataGridViewTextBoxColumn { Name = "colPrecioFinal", HeaderText = "Precio final", Width = 110 });
            dgvPacks_626CV.Columns.Add(new DataGridViewTextBoxColumn { Name = "colEstado", HeaderText = "Estado", Width = 100 });
        }

        // Acepta coma o punto como separador decimal
        private bool TryParseDecimal_626CV(string texto_626CV, out decimal valor_626CV)
        {
            texto_626CV = (texto_626CV ?? "").Trim().Replace("$", "").Trim();
            if (decimal.TryParse(texto_626CV, NumberStyles.Number, CultureInfo.CurrentCulture, out valor_626CV))
                return true;
            return decimal.TryParse(texto_626CV.Replace(",", "."), NumberStyles.Number, CultureInfo.InvariantCulture, out valor_626CV);
        }
    }


}
