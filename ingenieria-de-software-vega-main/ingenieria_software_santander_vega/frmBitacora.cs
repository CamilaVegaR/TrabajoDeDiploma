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
    public partial class frmBitacora : FORM_BASE
    {
        public frmBitacora()
        {
            InitializeComponent();
            preparar();
        }

        BLL_BITACORA bllBitacora;
        List<BE.BITACORA> bitacoras;

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            filtrar();
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            Navegar(new frmMenu(), null, true);
        }

        private void preparar()
        {
            dgvBitacora.DataSource = null;
            dgvBitacora.Rows.Clear();
            setDgvBitacoraColumnas();
            cbUsuarios.Items.Clear();
            cbUsuarios.DisplayMember = "NombreUsuario";
            //cbUsuarios.SelectedIndex = 0;
            cbUsuarios.ValueMember = "Id";
            bllBitacora = new BLL_BITACORA();
            bitacoras = bllBitacora.Listar();
            llenardDgv(bitacoras);
            rellenarComboUsuarios(bitacoras);
        }

        private void llenardDgv(List<BE.BITACORA> bitacoras)
        {
            dgvBitacora.Rows.Clear();
            foreach (BE.BITACORA bitacora in bitacoras)
            {
                dgvBitacora.Rows.Add(bitacora.Usuario?.NombreUsuario, bitacora.FechaHora, bitacora.Accion, bitacora.Observacion);
            }
        }

        private void rellenarComboUsuarios(List<BE.BITACORA> bitacoras)
        {
            foreach (BE.BITACORA bitacora in bitacoras)
            {
                if (bitacora.Usuario != null && !cbUsuarios.Items.Cast<USUARIO>().Any(u => u.Id == bitacora.Usuario.Id))
                {
                    cbUsuarios.Items.Add(bitacora.Usuario);
                }
            }
        }

        private void filtrar()
        {
            USUARIO usuarioSeleccionado = cbUsuarios.SelectedItem as USUARIO;

            List<BE.BITACORA> bitacorasFiltradas = new List<BE.BITACORA>();
            if (usuarioSeleccionado != null)
            {
                bitacorasFiltradas = bitacoras.Where(b => b.Usuario.Id == usuarioSeleccionado.Id).ToList();
                dgvBitacora.Rows.Clear();
            }
            llenardDgv(bitacorasFiltradas);
        }

        private void setDgvBitacoraColumnas()
        {
            dgvBitacora.Columns.Add("Usuario", "Usuario");
            dgvBitacora.Columns.Add("FechaHora", "Fecha y Hora");
            dgvBitacora.Columns.Add("Actividad", "Actividad");
            dgvBitacora.Columns.Add("Observacion", "Observación");
        }

        private void btnLimpiarFiltros_Click(object sender, EventArgs e)
        {
            cbUsuarios.SelectedIndex = 0;
            llenardDgv(bitacoras);
        }
    }
}
