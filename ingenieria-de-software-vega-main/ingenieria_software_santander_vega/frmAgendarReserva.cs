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
    public partial class frmAgendarReserva : FORM_BASE
    {
       
        private CLIENTE clienteActual_626CV;
        private BLL_RESERVA bllReserva_626CV;
        private BLL_CLIENTE bllCliente_626CV;
        private BLL_HORARIO_CLASE bllHorario_626CV;

        public frmAgendarReserva()
        {
            InitializeComponent();
            bllReserva_626CV = new BLL_RESERVA();
            bllCliente_626CV = new BLL_CLIENTE();
            bllHorario_626CV = new BLL_HORARIO_CLASE();
        }
        private void ActualizarCupoDisponible_626CV()
        {
            HORARIO_CLASE horarioSeleccionado_626CV = cmbHorario_626CV.SelectedItem as HORARIO_CLASE;

            if (horarioSeleccionado_626CV == null)
            {
                lblCupo_626CV.Text = "";
                return;
            }

            DateTime fechaClase_626CV = dtpFechaClase_626CV.Value.Date;

            int cupoMaximo_626CV, cupoOcupado_626CV;
            bool hayCupo_626CV = bllHorario_626CV.ConsultarCupoDisponible_626CV(
                horarioSeleccionado_626CV.Id_626CV, fechaClase_626CV, out cupoMaximo_626CV, out cupoOcupado_626CV);

            lblCupo_626CV.Text = $"Cupo: {cupoOcupado_626CV}/{cupoMaximo_626CV}";
            lblCupo_626CV.ForeColor = hayCupo_626CV ? System.Drawing.Color.DarkGreen : System.Drawing.Color.DarkRed;
        }


        private void cmbHorario_626CV_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActualizarCupoDisponible_626CV();
        }


        private void dtpFechaClase_626CV_ValueChanged(object sender, EventArgs e)
        {
            ActualizarCupoDisponible_626CV();
        }
        private void CargarHorarios_626CV()
        {
            cmbHorario_626CV.DataSource = bllHorario_626CV.ListarHorarios_626CV();
        }

        private void LimpiarCliente_626CV()
        {
            clienteActual_626CV = null;
            lblClienteEncontrado_626CV.Text = "";
            pnlClienteNuevo_626CV.Visible = false;
            dgvReservas_626CV.DataSource = null;
        }
        private void btnAgendar_626CV_Click(object sender, EventArgs e)
        {
            if (clienteActual_626CV == null)
            {
                MessageBox.Show("Debe buscar o registrar un cliente primero.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            HORARIO_CLASE horarioSeleccionado_626CV = (HORARIO_CLASE)cmbHorario_626CV.SelectedItem;
            DateTime fechaClase_626CV = dtpFechaClase_626CV.Value.Date;

            if (!ValidarDiaCoincideConFecha_626CV(horarioSeleccionado_626CV, fechaClase_626CV))   
            {
                MessageBox.Show(
                    $"La fecha elegida ({fechaClase_626CV:dd/MM/yyyy}) no es un {HORARIO_CLASE.TraducirDia_626CV(horarioSeleccionado_626CV.Dia_626CV)}.\nElija una fecha que coincida con el día del horario.",
                    "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string resultado_626CV = bllReserva_626CV.AgendarReserva_626CV(
            clienteActual_626CV, horarioSeleccionado_626CV, fechaClase_626CV, ObtenerCanalContactoSeleccionado_626CV());
            

            if (resultado_626CV == null)
            {
                clienteActual_626CV = bllCliente_626CV.ObtenerPorId_626CV(clienteActual_626CV.Id_626CV);
                lblClienteEncontrado_626CV.Text = $"Cliente: {clienteActual_626CV.Nombre_626CV} {clienteActual_626CV.Apellido_626CV} — Saldo: {clienteActual_626CV.SaldoClases_626CV} clase(s)";

                ActualizarCupoDisponible_626CV(); 

                MessageBox.Show(
                    $"Reserva agendada correctamente.\nSaldo restante: {clienteActual_626CV.SaldoClases_626CV} clase(s).\n{lblCupo_626CV.Text}.",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                CargarReservasCliente_626CV();
            }
            else
            {
                MessageBox.Show(resultado_626CV, "No se pudo agendar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnBuscarCliente_626CV_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDni_626CV.Text))
            {
                MessageBox.Show("Ingrese un DNI para buscar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            clienteActual_626CV = bllCliente_626CV.ObtenerPorDni_626CV(txtDni_626CV.Text.Trim());

            if (clienteActual_626CV != null)
            {
                lblClienteEncontrado_626CV.Text = $"Cliente: {clienteActual_626CV.Nombre_626CV} {clienteActual_626CV.Apellido_626CV} — Saldo: {clienteActual_626CV.SaldoClases_626CV} clase(s)";
                pnlClienteNuevo_626CV.Visible = false;
                CargarReservasCliente_626CV();
            }
            else
            {
                lblClienteEncontrado_626CV.Text = "Cliente no encontrado. Complete los datos para registrarlo.";
                pnlClienteNuevo_626CV.Visible = true;
                dgvReservas_626CV.DataSource = null;
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
            lblClienteEncontrado_626CV.Text = $"Cliente: {clienteActual_626CV.Nombre_626CV} {clienteActual_626CV.Apellido_626CV} — Saldo: {clienteActual_626CV.SaldoClases_626CV} clase(s)";
            pnlClienteNuevo_626CV.Visible = false;

            MessageBox.Show("Cliente registrado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            CargarReservasCliente_626CV();

        }

        private void btnCancelarReserva_626CV_Click(object sender, EventArgs e)
        {
            if (dgvReservas_626CV.CurrentRow == null)
            {
                MessageBox.Show("Seleccione una reserva de la lista.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idReservaSeleccionada_626CV = Convert.ToInt32(dgvReservas_626CV.CurrentRow.Cells["colIdReserva"].Value);

            string resultado_626CV = bllReserva_626CV.CancelarReserva_626CV(idReservaSeleccionada_626CV);

            MessageBox.Show(resultado_626CV, "Cancelación de reserva", MessageBoxButtons.OK, MessageBoxIcon.Information);

            clienteActual_626CV = bllCliente_626CV.ObtenerPorId_626CV(clienteActual_626CV.Id_626CV);
            lblClienteEncontrado_626CV.Text = $"Cliente: {clienteActual_626CV.Nombre_626CV} {clienteActual_626CV.Apellido_626CV} — Saldo: {clienteActual_626CV.SaldoClases_626CV} clase(s)";

            ActualizarCupoDisponible_626CV();
            CargarReservasCliente_626CV();
        }
     
        private void btnVolver_626CV_Click(object sender, EventArgs e)
        {
            Navegar(new frmMenu(), null, true);
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            ConfigurarColumnasGrilla_626CV();
            CargarCanalesContacto_626CV();
            CargarHorarios_626CV();
            LimpiarCliente_626CV();
            ActualizarCupoDisponible_626CV();
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
        private void CargarCanalesContacto_626CV()
        {
            cmbCanalContacto_626CV.Items.Clear();
            cmbCanalContacto_626CV.Items.Add("Presencial");
            cmbCanalContacto_626CV.Items.Add("Telefónico");
            cmbCanalContacto_626CV.Items.Add("Redes sociales");
            cmbCanalContacto_626CV.SelectedIndex = 0;
        }
        private void ConfigurarColumnasGrilla_626CV()
        {
            dgvReservas_626CV.AutoGenerateColumns = false;
            dgvReservas_626CV.Columns.Clear();

            dgvReservas_626CV.Columns.Add(new DataGridViewTextBoxColumn { Name = "colIdReserva", HeaderText = "N° Reserva", Width = 90 });
            dgvReservas_626CV.Columns.Add(new DataGridViewTextBoxColumn { Name = "colHorario", HeaderText = "Horario", Width = 160 });
            dgvReservas_626CV.Columns.Add(new DataGridViewTextBoxColumn { Name = "colFecha", HeaderText = "Fecha", Width = 100 });
            dgvReservas_626CV.Columns.Add(new DataGridViewTextBoxColumn { Name = "colEstado", HeaderText = "Estado", Width = 100 });
        }

        private void CargarReservasCliente_626CV()
        {
            dgvReservas_626CV.Rows.Clear();

            if (clienteActual_626CV == null) return;

            List<RESERVA> reservas_626CV = bllReserva_626CV.ListarPorClienteDetalle_626CV(clienteActual_626CV.Id_626CV);

            foreach (RESERVA reserva_626CV in reservas_626CV)
            {
                dgvReservas_626CV.Rows.Add(
                    reserva_626CV.Id_626CV,
                    reserva_626CV.ObtenerDescripcionHorario_626CV(),
                    reserva_626CV.ObtenerDescripcionFechaClase_626CV(),
                    reserva_626CV.ObtenerDescripcionEstado_626CV());
            }
        }
        private bool ValidarDiaCoincideConFecha_626CV(HORARIO_CLASE horario_626CV, DateTime fecha_626CV)
        {
            if (horario_626CV == null) return false;
            return fecha_626CV.DayOfWeek == horario_626CV.Dia_626CV;
        }

        private void btnModificarReserva_626CV_Click(object sender, EventArgs e)
        {
            if (dgvReservas_626CV.CurrentRow == null)
            {
                MessageBox.Show("Seleccione una reserva de la lista.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idReservaSeleccionada_626CV = Convert.ToInt32(dgvReservas_626CV.CurrentRow.Cells["colIdReserva"].Value);

            HORARIO_CLASE horarioNuevo_626CV = (HORARIO_CLASE)cmbHorario_626CV.SelectedItem;
            DateTime fechaNueva_626CV = dtpFechaClase_626CV.Value.Date;

            if (!ValidarDiaCoincideConFecha_626CV(horarioNuevo_626CV, fechaNueva_626CV))
            {
                MessageBox.Show(
                    $"La fecha elegida ({fechaNueva_626CV:dd/MM/yyyy}) no es un {HORARIO_CLASE.TraducirDia_626CV(horarioNuevo_626CV.Dia_626CV)}.\nElija una fecha que coincida con el día del horario.",
                    "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string resultado_626CV = bllReserva_626CV.ModificarReserva_626CV(idReservaSeleccionada_626CV, horarioNuevo_626CV, fechaNueva_626CV);

            if (resultado_626CV == null)
            {
                MessageBox.Show("Reserva modificada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ActualizarCupoDisponible_626CV();
                CargarReservasCliente_626CV();
            }
            else
            {
                MessageBox.Show(resultado_626CV, "No se pudo modificar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
