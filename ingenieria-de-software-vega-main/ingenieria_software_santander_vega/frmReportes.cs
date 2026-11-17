using BE;
using BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ingenieria_software_santander_vega
{

    public partial class frmReportes : FORM_BASE
    {
        private BLL_REPORTE bllReporte_626CV;
        private REPORTE reporteActual_626CV;

        public frmReportes()
        {
            InitializeComponent();
            bllReporte_626CV = new BLL_REPORTE();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            cmbTipo_626CV.Items.Clear();
            cmbTipo_626CV.Items.Add("RF1 - Reservas");
            cmbTipo_626CV.Items.Add("RF2 - Ventas de clases y packs");
            cmbTipo_626CV.Items.Add("Global - Resumen de gestión");
            cmbTipo_626CV.SelectedIndex = 0;
            dtpHasta_626CV.Value = DateTime.Today;
            dtpDesde_626CV.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);

            LimpiarVistaPrevia_626CV();
        }

        private void btnGenerar_626CV_Click(object sender, EventArgs e)
        {
            DateTime desde_626CV = dtpDesde_626CV.Value.Date;
            DateTime hasta_626CV = dtpHasta_626CV.Value.Date;

            try
            {
                Cursor = Cursors.WaitCursor;
                switch (cmbTipo_626CV.SelectedIndex)
                {
                    case 0: reporteActual_626CV = bllReporte_626CV.GenerarReporteReservas_626CV(desde_626CV, hasta_626CV); break;
                    case 1: reporteActual_626CV = bllReporte_626CV.GenerarReporteVentas_626CV(desde_626CV, hasta_626CV); break;
                    default: reporteActual_626CV = bllReporte_626CV.GenerarReporteGlobal_626CV(desde_626CV, hasta_626CV); break;
                }
                Cursor = Cursors.Default;
            }
            catch (ArgumentException ex)
            {
                Cursor = Cursors.Default;
                MessageBox.Show(ex.Message, "Reportes", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            catch (Exception)
            {
                Cursor = Cursors.Default;
                MessageBox.Show("No se pudo generar el reporte. Intente nuevamente.", "Reportes", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            lblTituloReporte_626CV.Text = $"{reporteActual_626CV.Titulo_626CV} — {reporteActual_626CV.Subtitulo_626CV}";

            cmbSeccion_626CV.DisplayMember = "Titulo_626CV";
            cmbSeccion_626CV.Items.Clear();
            foreach (REPORTE_SECCION seccion_626CV in reporteActual_626CV.Secciones_626CV)
                cmbSeccion_626CV.Items.Add(seccion_626CV);
            cmbSeccion_626CV.Enabled = true;
            cmbSeccion_626CV.SelectedIndex = 0;

            btnExportarPdf_626CV.Enabled = true;
        }

    
        private void cmbSeccion_626CV_SelectedIndexChanged(object sender, EventArgs e)
        {
            REPORTE_SECCION seccion_626CV = cmbSeccion_626CV.SelectedItem as REPORTE_SECCION;
            if (seccion_626CV == null) return;

            dgvReporte_626CV.Columns.Clear();
            dgvReporte_626CV.Rows.Clear();

            for (int i = 0; i < seccion_626CV.Columnas_626CV.Length; i++)
            {
                dgvReporte_626CV.Columns.Add("col" + i, seccion_626CV.Columnas_626CV[i]);
            }

            foreach (string[] fila_626CV in seccion_626CV.Filas_626CV)
                dgvReporte_626CV.Rows.Add(fila_626CV);

            dgvReporte_626CV.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

            lblResumen_626CV.Text = seccion_626CV.Filas_626CV.Count == 0
                ? "Sin datos para el período seleccionado."
                : string.Join(Environment.NewLine, seccion_626CV.Resumen_626CV);
        }

        private void Filtro_626CV_Changed(object sender, EventArgs e)
        {
            LimpiarVistaPrevia_626CV();
        }

        private void btnExportarPdf_626CV_Click(object sender, EventArgs e)
        {
            if (reporteActual_626CV == null)
            {
                MessageBox.Show("Primero debe generar un reporte.", "Reportes", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SaveFileDialog dialogo_626CV = new SaveFileDialog())
            {
                dialogo_626CV.Filter = "Archivo PDF (*.pdf)|*.pdf";
               
                dialogo_626CV.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                dialogo_626CV.FileName = $"{NombreArchivo_626CV()}_{DateTime.Now:yyyyMMdd_HHmm}.pdf";

                if (dialogo_626CV.ShowDialog(this) != DialogResult.OK) return;

                try
                {
                    Cursor = Cursors.WaitCursor;
                    bllReporte_626CV.ExportarPdf_626CV(reporteActual_626CV, dialogo_626CV.FileName);
                    Cursor = Cursors.Default;
                }
                catch (Exception ex)
                {
                    Cursor = Cursors.Default;
                    MessageBox.Show("No se pudo exportar el reporte.\n" + ex.Message, "Reportes", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (MessageBox.Show("Reporte exportado correctamente. ¿Desea abrirlo?", "Reportes",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    try { Process.Start(dialogo_626CV.FileName); }
                    catch { MessageBox.Show("No se pudo abrir el archivo. Ábralo desde la carpeta donde lo guardó.", "Reportes"); }
                }
            }
        }

        private void btnVolver_626CV_Click(object sender, EventArgs e)
        {
            Navegar(new frmMenu(), null, true);
        }

        private void LimpiarVistaPrevia_626CV()
        {
            reporteActual_626CV = null;
            lblTituloReporte_626CV.Text = "Elija el tipo de reporte y el período, y presione \"Generar\".";
            cmbSeccion_626CV.Items.Clear();
            cmbSeccion_626CV.Enabled = false;
            dgvReporte_626CV.Columns.Clear();
            dgvReporte_626CV.Rows.Clear();
            lblResumen_626CV.Text = "";
            btnExportarPdf_626CV.Enabled = false;
        }

        private string NombreArchivo_626CV()
        {
            switch (cmbTipo_626CV.SelectedIndex)
            {
                case 0: return "Reporte_Reservas";
                case 1: return "Reporte_Ventas";
                default: return "Reporte_Global";
            }
        }
    }
}
