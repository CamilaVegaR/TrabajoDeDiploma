using BE;
using BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ingenieria_software_santander_vega
{

    public partial class frmSerializacion : FORM_BASE
    {
        private BLL_SERIALIZACION bllSerializacion_626CV;
        private BLL_PACK bllPack_626CV = new BLL_PACK();

        public frmSerializacion()
        {
            InitializeComponent();
            bllSerializacion_626CV = new BLL_SERIALIZACION();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            cmbEntidad_626CV.Items.Clear();
            cmbEntidad_626CV.Items.Add("Packs");
            cmbEntidad_626CV.Items.Add("Clientes");
            cmbEntidad_626CV.SelectedIndex = 0;

            LimpiarResultado_626CV();
        }

        private bool EsPacks_626CV()
        {
            return cmbEntidad_626CV.SelectedIndex == 0;
        }

        private string CarpetaPorDefecto_626CV()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        }

        private void btnSerializar_626CV_Click(object sender, EventArgs e)
        {
            string nombre_626CV = (EsPacks_626CV() ? "Packs" : "Clientes") + "_" + DateTime.Now.ToString("yyyyMMdd_HHmm") + ".xml";

            using (SaveFileDialog dialogo_626CV = new SaveFileDialog())
            {
                dialogo_626CV.Title = "Guardar archivo XML";
                dialogo_626CV.Filter = "Archivo XML (*.xml)|*.xml";
                dialogo_626CV.InitialDirectory = CarpetaPorDefecto_626CV();
                dialogo_626CV.FileName = nombre_626CV;

                if (dialogo_626CV.ShowDialog(this) != DialogResult.OK)
                    return;

                try
                {
                    Cursor = Cursors.WaitCursor;
                    int cantidad_626CV = EsPacks_626CV()
                        ? bllSerializacion_626CV.SerializarPacks_626CV(dialogo_626CV.FileName)
                        : bllSerializacion_626CV.SerializarClientes_626CV(dialogo_626CV.FileName);
                    Cursor = Cursors.Default;

                    MostrarXml_626CV(dialogo_626CV.FileName);
                    MessageBox.Show($"Se serializaron {cantidad_626CV} registros en:\n{dialogo_626CV.FileName}",
                        "Serialización", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (UnauthorizedAccessException)
                {
                    Cursor = Cursors.Default;
                    MessageBox.Show("No tiene permiso para guardar en esa carpeta. Elija otra, por ejemplo Mis documentos.",
                        "Serialización", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (Exception)
                {
                    Cursor = Cursors.Default;
                    MessageBox.Show("No se pudo generar el archivo XML. Intente nuevamente.",
                        "Serialización", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnDeserializar_626CV_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialogo_626CV = new OpenFileDialog())
            {
                dialogo_626CV.Title = "Abrir archivo XML de " + (EsPacks_626CV() ? "Packs" : "Clientes");
                dialogo_626CV.Filter = "Archivo XML (*.xml)|*.xml";
                dialogo_626CV.InitialDirectory = CarpetaPorDefecto_626CV();

                if (dialogo_626CV.ShowDialog(this) != DialogResult.OK)
                    return;

                LimpiarResultado_626CV();

                try
                {
                    Cursor = Cursors.WaitCursor;
                    if (EsPacks_626CV())
                        MostrarPacks_626CV(bllSerializacion_626CV.DeserializarPacks_626CV(dialogo_626CV.FileName));
                    else
                        MostrarClientes_626CV(bllSerializacion_626CV.DeserializarClientes_626CV(dialogo_626CV.FileName));
                    Cursor = Cursors.Default;

                    MostrarXml_626CV(dialogo_626CV.FileName);
                }
                catch (InvalidDataException ex)
                {
                    Cursor = Cursors.Default;
                    MessageBox.Show(ex.Message, "Deserialización", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (Exception)
                {
                    Cursor = Cursors.Default;
                    MessageBox.Show("No se pudo leer el archivo XML. Intente nuevamente.",
                        "Deserialización", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void MostrarPacks_626CV(List<PACK> packs_626CV)
        {
            DataTable tabla_626CV = new DataTable();
            tabla_626CV.Columns.Add("Id");
            tabla_626CV.Columns.Add("Nombre");
            tabla_626CV.Columns.Add("Clases");
            tabla_626CV.Columns.Add("Precio de lista");
            tabla_626CV.Columns.Add("% Descuento");
            tabla_626CV.Columns.Add("Precio final");
            tabla_626CV.Columns.Add("Estado");
            tabla_626CV.Columns.Add("Integridad");

            int alterados_626CV = 0;
            foreach (PACK pack_626CV in packs_626CV)
            {
                bool integro_626CV = bllSerializacion_626CV.RegistroIntegro_626CV(pack_626CV);
                if (!integro_626CV) alterados_626CV++;

                tabla_626CV.Rows.Add(
                    pack_626CV.Id_626CV,
                    pack_626CV.Nombre_626CV,
                    pack_626CV.CantidadClases_626CV,
                    pack_626CV.PrecioLista_626CV.ToString("N2"),
                    pack_626CV.PorcentajeDescuento_626CV.ToString("N2"),
                    bllPack_626CV.CalcularPrecioFinal_626CV(pack_626CV).ToString("N2"),
                    bllPack_626CV.ObtenerDescripcionEstado_626CV(pack_626CV),
                    integro_626CV ? "Íntegro" : "ALTERADO");
            }

            MostrarTabla_626CV(tabla_626CV, packs_626CV.Count, alterados_626CV, "packs");
        }

        private void MostrarClientes_626CV(List<CLIENTE> clientes_626CV)
        {
            DataTable tabla_626CV = new DataTable();
            tabla_626CV.Columns.Add("Id");
            tabla_626CV.Columns.Add("Nombre");
            tabla_626CV.Columns.Add("Apellido");
            tabla_626CV.Columns.Add("DNI");
            tabla_626CV.Columns.Add("Teléfono");
            tabla_626CV.Columns.Add("Email");
            tabla_626CV.Columns.Add("Saldo de clases");
            tabla_626CV.Columns.Add("Integridad");

            int alterados_626CV = 0;
            foreach (CLIENTE cliente_626CV in clientes_626CV)
            {
                bool integro_626CV = bllSerializacion_626CV.RegistroIntegro_626CV(cliente_626CV);
                if (!integro_626CV) alterados_626CV++;

                tabla_626CV.Rows.Add(
                    cliente_626CV.Id_626CV,
                    cliente_626CV.Nombre_626CV,
                    cliente_626CV.Apellido_626CV,
                    cliente_626CV.Dni_626CV,
                    cliente_626CV.Telefono_626CV,
                    cliente_626CV.Email_626CV,
                    cliente_626CV.SaldoClases_626CV,
                    integro_626CV ? "Íntegro" : "ALTERADO");
            }

            MostrarTabla_626CV(tabla_626CV, clientes_626CV.Count, alterados_626CV, "clientes");
        }

        private void MostrarTabla_626CV(DataTable tabla_626CV, int total_626CV, int alterados_626CV, string entidad_626CV)
        {
            dgvDatos_626CV.DataSource = tabla_626CV;

            foreach (DataGridViewRow fila_626CV in dgvDatos_626CV.Rows)
            {
                if (Convert.ToString(fila_626CV.Cells["Integridad"].Value) == "ALTERADO")
                {
                    fila_626CV.DefaultCellStyle.BackColor = Color.MistyRose;
                    fila_626CV.DefaultCellStyle.ForeColor = Color.DarkRed;
                }
            }

            lblResumen_626CV.Text = alterados_626CV == 0
                ? $"Se leyeron {total_626CV} {entidad_626CV}. Todos los registros están íntegros."
                : $"Se leyeron {total_626CV} {entidad_626CV}. {alterados_626CV} registro(s) fueron ALTERADOS fuera del sistema.";
            lblResumen_626CV.ForeColor = alterados_626CV == 0 ? Color.DarkGreen : Color.DarkRed;
        }

        private void MostrarXml_626CV(string ruta_626CV)
        {
            try
            {
                txtXml_626CV.Text = File.ReadAllText(ruta_626CV, Encoding.UTF8);
            }
            catch
            {
                txtXml_626CV.Text = string.Empty;
            }
        }

        private void LimpiarResultado_626CV()
        {
            dgvDatos_626CV.DataSource = null;
            txtXml_626CV.Text = string.Empty;
            lblResumen_626CV.Text = string.Empty;
        }

        private void cmbEntidad_626CV_SelectedIndexChanged(object sender, EventArgs e)
        {
            LimpiarResultado_626CV();
        }

        private void btnVolver_626CV_Click(object sender, EventArgs e)
        {
            Navegar(new frmMenu(), null, true);
        }
    }
}
