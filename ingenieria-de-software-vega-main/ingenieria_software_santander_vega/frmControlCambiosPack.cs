using System;
using System.Collections.Generic;
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

    public partial class frmControlCambiosPack : FORM_BASE
    {
        private BLL_PACK_C bllPackC_626CV;


        private List<PACK_C> cambios_626CV;

        public frmControlCambiosPack()
        {
            InitializeComponent();
            bllPackC_626CV = new BLL_PACK_C();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            ConfigurarColumnasGrilla_626CV();
            LimpiarFiltros_626CV();
            CargarCombos_626CV();
            CargarGrilla_626CV(bllPackC_626CV.ListarTodos_626CV());
        }

        private void btnAplicar_626CV_Click(object sender, EventArgs e)
        {
            AplicarFiltros_626CV();
        }

        private void btnLimpiar_626CV_Click(object sender, EventArgs e)
        {
            LimpiarFiltros_626CV();
            CargarGrilla_626CV(bllPackC_626CV.ListarTodos_626CV());
        }

        private void btnActivar_626CV_Click(object sender, EventArgs e)
        {
            PACK_C cambio_626CV = ObtenerCambioSeleccionado_626CV();
            if (cambio_626CV == null)
            {
                MessageBox.Show("Debe seleccionar un registro de la grilla.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cambio_626CV.Act_626CV)
            {
                MessageBox.Show("El registro seleccionado ya es la versión activa del pack.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string mensaje_626CV =
                $"¿Desea activar la versión del {cambio_626CV.Fecha_626CV:dd/MM/yyyy} {cambio_626CV.Hora_626CV:hh\\:mm} del pack {cambio_626CV.IdPack_626CV}?\n\n" +
                $"El pack quedará con estos datos:\n" +
                $"Nombre: {cambio_626CV.Nombre_626CV}\n" +
                $"Clases: {cambio_626CV.CantidadClases_626CV}\n" +
                $"Precio de lista: $ {cambio_626CV.PrecioLista_626CV:N2}\n" +
                $"Descuento: {cambio_626CV.PorcentajeDescuento_626CV:0.##}%\n" +
                $"Estado: {bllPackC_626CV.ObtenerDescripcionEstado_626CV(cambio_626CV)}";

            if (MessageBox.Show(mensaje_626CV, "Activar versión", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            string resultado_626CV = bllPackC_626CV.ActivarCambio_626CV(cambio_626CV);

            if (resultado_626CV == null)
            {
                MessageBox.Show("Versión activada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarCombos_626CV();
                AplicarFiltros_626CV();
            }
            else
            {
                MessageBox.Show(resultado_626CV, "No se pudo activar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSalir_626CV_Click(object sender, EventArgs e)
        {
            Navegar(new frmMenu(), null, true);
        }


        private void AplicarFiltros_626CV()
        {
            int? idPack_626CV = null;
            if (cbCodPack_626CV.SelectedValue != null && (int)cbCodPack_626CV.SelectedValue > 0)
                idPack_626CV = (int)cbCodPack_626CV.SelectedValue;

            string nombre_626CV = string.IsNullOrWhiteSpace(cbNombre_626CV.Text) ? null : cbNombre_626CV.Text.Trim();
            DateTime? fechaDesde_626CV = dtpFechaIni_626CV.Checked ? dtpFechaIni_626CV.Value.Date : (DateTime?)null;
            DateTime? fechaHasta_626CV = dtpFechaFin_626CV.Checked ? dtpFechaFin_626CV.Value.Date : (DateTime?)null;

            string error_626CV;
            List<PACK_C> filtrados_626CV = bllPackC_626CV.Filtrar_626CV(idPack_626CV, nombre_626CV, fechaDesde_626CV, fechaHasta_626CV, out error_626CV);

            if (error_626CV != null)
            {
                MessageBox.Show(error_626CV, "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            CargarGrilla_626CV(filtrados_626CV);
        }

        private void CargarGrilla_626CV(List<PACK_C> lista_626CV)
        {
            cambios_626CV = lista_626CV;
            dgvCambios_626CV.Rows.Clear();

            foreach (PACK_C cambio_626CV in cambios_626CV)
            {
                int fila_626CV = dgvCambios_626CV.Rows.Add(
                    cambio_626CV.IdCambio_626CV,
                    cambio_626CV.IdPack_626CV,
                    cambio_626CV.Fecha_626CV.ToString("dd/MM/yyyy"),
                    cambio_626CV.Hora_626CV.ToString(@"hh\:mm"),
                    cambio_626CV.Nombre_626CV,
                    cambio_626CV.CantidadClases_626CV,
                    $"$ {cambio_626CV.PrecioLista_626CV:N2}",
                    $"{cambio_626CV.PorcentajeDescuento_626CV:0.##}%",
                    bllPackC_626CV.ObtenerDescripcionEstado_626CV(cambio_626CV),
                    cambio_626CV.Act_626CV ? "1" : "0");

              
                if (cambio_626CV.Act_626CV)
                    dgvCambios_626CV.Rows[fila_626CV].DefaultCellStyle.Font = new Font(dgvCambios_626CV.Font, FontStyle.Bold);
            }

            dgvCambios_626CV.ClearSelection();
        }


        private void CargarCombos_626CV()
        {
            List<PACK_C> todos_626CV = bllPackC_626CV.ListarTodos_626CV();

            List<KeyValuePair<int, string>> packs_626CV = new List<KeyValuePair<int, string>>();
            packs_626CV.Add(new KeyValuePair<int, string>(0, "(Todos)"));
            packs_626CV.AddRange(todos_626CV
                .Where(c => c.Act_626CV)
                .OrderBy(c => c.IdPack_626CV)
                .Select(c => new KeyValuePair<int, string>(c.IdPack_626CV, c.IdPack_626CV + " - " + c.Nombre_626CV)));

            int seleccionado_626CV = cbCodPack_626CV.SelectedValue is int ? (int)cbCodPack_626CV.SelectedValue : 0;
            cbCodPack_626CV.DataSource = null;
            cbCodPack_626CV.DisplayMember = "Value";
            cbCodPack_626CV.ValueMember = "Key";
            cbCodPack_626CV.DataSource = packs_626CV;
            cbCodPack_626CV.SelectedValue = packs_626CV.Any(p => p.Key == seleccionado_626CV) ? seleccionado_626CV : 0;

            string nombreEscrito_626CV = cbNombre_626CV.Text;
            cbNombre_626CV.Items.Clear();
            foreach (string nombre_626CV in todos_626CV.Select(c => c.Nombre_626CV).Distinct().OrderBy(n => n))
                cbNombre_626CV.Items.Add(nombre_626CV);
            cbNombre_626CV.Text = nombreEscrito_626CV;
        }

        private void LimpiarFiltros_626CV()
        {
            if (cbCodPack_626CV.Items.Count > 0)
                cbCodPack_626CV.SelectedIndex = 0;
            cbNombre_626CV.Text = "";
            dtpFechaIni_626CV.Value = DateTime.Today;
            dtpFechaFin_626CV.Value = DateTime.Today;
            dtpFechaIni_626CV.Checked = false;
            dtpFechaFin_626CV.Checked = false;
        }

        private PACK_C ObtenerCambioSeleccionado_626CV()
        {
            if (dgvCambios_626CV.SelectedRows.Count == 0 || cambios_626CV == null)
                return null;

            int idCambio_626CV = Convert.ToInt32(dgvCambios_626CV.SelectedRows[0].Cells["colIdCambio"].Value);
            return cambios_626CV.FirstOrDefault(c => c.IdCambio_626CV == idCambio_626CV);
        }

        private void ConfigurarColumnasGrilla_626CV()
        {
            dgvCambios_626CV.AutoGenerateColumns = false;
            dgvCambios_626CV.AllowUserToAddRows = false;
            dgvCambios_626CV.Columns.Clear();

            dgvCambios_626CV.Columns.Add(new DataGridViewTextBoxColumn { Name = "colIdCambio", HeaderText = "Id cambio", Visible = false });
            dgvCambios_626CV.Columns.Add(new DataGridViewTextBoxColumn { Name = "colCodPack", HeaderText = "Cod. Pack", Width = 90 });
            dgvCambios_626CV.Columns.Add(new DataGridViewTextBoxColumn { Name = "colFecha", HeaderText = "Fecha", Width = 105 });
            dgvCambios_626CV.Columns.Add(new DataGridViewTextBoxColumn { Name = "colHora", HeaderText = "Hora", Width = 70 });
            dgvCambios_626CV.Columns.Add(new DataGridViewTextBoxColumn { Name = "colNombre", HeaderText = "Nombre", Width = 190 });
            dgvCambios_626CV.Columns.Add(new DataGridViewTextBoxColumn { Name = "colClases", HeaderText = "Clases", Width = 70 });
            dgvCambios_626CV.Columns.Add(new DataGridViewTextBoxColumn { Name = "colPrecioLista", HeaderText = "Precio lista", Width = 120 });
            dgvCambios_626CV.Columns.Add(new DataGridViewTextBoxColumn { Name = "colDescuento", HeaderText = "Descuento", Width = 95 });
            dgvCambios_626CV.Columns.Add(new DataGridViewTextBoxColumn { Name = "colEstado", HeaderText = "Estado", Width = 110 });
            dgvCambios_626CV.Columns.Add(new DataGridViewTextBoxColumn { Name = "colAct", HeaderText = "Act.", Width = 55 });
        }
    }
}
