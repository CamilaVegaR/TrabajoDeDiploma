using System;
using System.Collections.Generic;
using System.Windows.Forms;
using BE;
using BLL;
using Microsoft.VisualBasic;
using System.ComponentModel;
using System.Linq;

namespace ingenieria_software_santander_vega
{
    public partial class frmIdiomas : FORM_BASE
    {
        private BLL_IDIOMA bllIdioma;
        private BLL_TRADUCCION bllTraduccion;
        private IDIOMA idiomaSeleccionado;

        private BindingList<TRADUCCION> traduccionesEnPantalla;
        private Dictionary<int, string> textoOriginalPorEtiqueta = new Dictionary<int, string>();

        public frmIdiomas()
        {
            InitializeComponent();
            bllIdioma = new BLL_IDIOMA();
            bllTraduccion = new BLL_TRADUCCION();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            CargarIdiomas();
        }

        private void CargarIdiomas()
        {
            try
            {
                lstIdiomas.DataSource = null;
                lstIdiomas.DataSource = bllIdioma.ListarIdiomas();
                lstIdiomas.DisplayMember = "Nombre";
                lstIdiomas.ValueMember = "Id";
            } catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los idiomas", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void ConfigurarGrillaTraducciones()
        {
            if (dgvTraducciones.Columns.Count == 0)
                return;

            dgvTraducciones.Columns["Idioma"].Visible = false;
            dgvTraducciones.Columns["Etiqueta"].Visible = false;
            dgvTraducciones.Columns["DVH"].Visible = false;

            dgvTraducciones.Columns["Formulario"].ReadOnly = true;
            dgvTraducciones.Columns["Formulario"].Width = 150;

            dgvTraducciones.Columns["Clave"].ReadOnly = true;
            dgvTraducciones.Columns["Clave"].Width = 150;

            dgvTraducciones.Columns["Texto"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void lstIdiomas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstIdiomas.SelectedItem == null)
                return;

            try
            {
                idiomaSeleccionado = (IDIOMA)lstIdiomas.SelectedItem;

                List<TRADUCCION> traducciones = bllTraduccion.ObtenerTraduccionesPorIdioma(idiomaSeleccionado.Id);

                textoOriginalPorEtiqueta = traducciones.ToDictionary(
                    t => t.Etiqueta.Id,
                    t => t.Texto ?? string.Empty
                );

                traduccionesEnPantalla = new BindingList<TRADUCCION>(traducciones);

                dgvTraducciones.DataSource = null;
                dgvTraducciones.DataSource = traduccionesEnPantalla;

                ConfigurarGrillaTraducciones();
            }
            catch (Exception)
            {
                MessageBox.Show(
                    "Error al cargar las traducciones para el idioma seleccionado",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void btnGuardarTraduccion_Click(object sender, EventArgs e)
        {
            if (traduccionesEnPantalla == null || traduccionesEnPantalla.Count == 0)
                return;

            try
            {
                dgvTraducciones.EndEdit();

                CurrencyManager currencyManager = BindingContext[dgvTraducciones.DataSource] as CurrencyManager;
                if (currencyManager != null)
                    currencyManager.EndCurrentEdit();

                List<TRADUCCION> traduccionesModificadas = traduccionesEnPantalla
                    .Where(t =>
                    {
                        string textoOriginal;

                        if (!textoOriginalPorEtiqueta.TryGetValue(t.Etiqueta.Id, out textoOriginal))
                            return true;

                        string textoActual = t.Texto ?? string.Empty;

                        return !string.Equals(textoOriginal, textoActual, StringComparison.Ordinal);
                    })
                    .ToList();

                if (traduccionesModificadas.Count == 0)
                {
                    MessageBox.Show(
                        "No hay traducciones modificadas.",
                        "Información",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    return;
                }

                bllTraduccion.GuardarTraduccionesModificadas(traduccionesModificadas);

                foreach (TRADUCCION traduccion in traduccionesModificadas)
                {
                    textoOriginalPorEtiqueta[traduccion.Etiqueta.Id] = traduccion.Texto ?? string.Empty;
                }

                MessageBox.Show(
                    "Las traducciones se han guardado correctamente.",
                    "Éxito",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            catch (Exception)
            {
                MessageBox.Show(
                    "Error al guardar las traducciones",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void btnNuevoIdioma_Click(object sender, EventArgs e)
        {
            try
            {
                string nombreNuevoIdioma = Interaction.InputBox("Ingrese el nombre del nuevo idioma:", "Nuevo Idioma", "");

                if (!string.IsNullOrWhiteSpace(nombreNuevoIdioma))
                {
                    bllIdioma.CrearIdioma(nombreNuevoIdioma);
                    CargarIdiomas();
                }
            } catch (Exception ex)
            {
                MessageBox.Show("Error al crear el nuevo idioma", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            Navegar(new frmMenu(), null, true);
        }
    }
}