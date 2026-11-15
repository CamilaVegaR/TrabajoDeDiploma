namespace ingenieria_software_santander_vega
{
    partial class frmReportes
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblTipo_626CV = new System.Windows.Forms.Label();
            this.cmbTipo_626CV = new System.Windows.Forms.ComboBox();
            this.lblDesde_626CV = new System.Windows.Forms.Label();
            this.dtpDesde_626CV = new System.Windows.Forms.DateTimePicker();
            this.lblHasta_626CV = new System.Windows.Forms.Label();
            this.dtpHasta_626CV = new System.Windows.Forms.DateTimePicker();
            this.btnGenerar_626CV = new System.Windows.Forms.Button();
            this.lblTituloReporte_626CV = new System.Windows.Forms.Label();
            this.lblSeccion_626CV = new System.Windows.Forms.Label();
            this.cmbSeccion_626CV = new System.Windows.Forms.ComboBox();
            this.dgvReporte_626CV = new System.Windows.Forms.DataGridView();
            this.lblResumen_626CV = new System.Windows.Forms.Label();
            this.btnExportarPdf_626CV = new System.Windows.Forms.Button();
            this.btnVolver_626CV = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReporte_626CV)).BeginInit();
            this.SuspendLayout();
            //
            // lblTipo_626CV
            //
            this.lblTipo_626CV.AutoSize = true;
            this.lblTipo_626CV.Location = new System.Drawing.Point(18, 23);
            this.lblTipo_626CV.Name = "lblTipo_626CV";
            this.lblTipo_626CV.Size = new System.Drawing.Size(66, 20);
            this.lblTipo_626CV.TabIndex = 0;
            this.lblTipo_626CV.Text = "Reporte";
            //
            // cmbTipo_626CV
            //
            this.cmbTipo_626CV.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipo_626CV.FormattingEnabled = true;
            this.cmbTipo_626CV.Location = new System.Drawing.Point(100, 18);
            this.cmbTipo_626CV.Name = "cmbTipo_626CV";
            this.cmbTipo_626CV.Size = new System.Drawing.Size(320, 28);
            this.cmbTipo_626CV.TabIndex = 1;
            this.cmbTipo_626CV.SelectedIndexChanged += new System.EventHandler(this.Filtro_626CV_Changed);
            //
            // lblDesde_626CV
            //
            this.lblDesde_626CV.AutoSize = true;
            this.lblDesde_626CV.Location = new System.Drawing.Point(18, 68);
            this.lblDesde_626CV.Name = "lblDesde_626CV";
            this.lblDesde_626CV.Size = new System.Drawing.Size(56, 20);
            this.lblDesde_626CV.TabIndex = 2;
            this.lblDesde_626CV.Text = "Desde";
            //
            // dtpDesde_626CV
            //
            this.dtpDesde_626CV.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDesde_626CV.Location = new System.Drawing.Point(100, 63);
            this.dtpDesde_626CV.Name = "dtpDesde_626CV";
            this.dtpDesde_626CV.Size = new System.Drawing.Size(150, 26);
            this.dtpDesde_626CV.TabIndex = 3;
            this.dtpDesde_626CV.ValueChanged += new System.EventHandler(this.Filtro_626CV_Changed);
            //
            // lblHasta_626CV
            //
            this.lblHasta_626CV.AutoSize = true;
            this.lblHasta_626CV.Location = new System.Drawing.Point(270, 68);
            this.lblHasta_626CV.Name = "lblHasta_626CV";
            this.lblHasta_626CV.Size = new System.Drawing.Size(51, 20);
            this.lblHasta_626CV.TabIndex = 4;
            this.lblHasta_626CV.Text = "Hasta";
            //
            // dtpHasta_626CV
            //
            this.dtpHasta_626CV.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpHasta_626CV.Location = new System.Drawing.Point(330, 63);
            this.dtpHasta_626CV.Name = "dtpHasta_626CV";
            this.dtpHasta_626CV.Size = new System.Drawing.Size(150, 26);
            this.dtpHasta_626CV.TabIndex = 5;
            this.dtpHasta_626CV.ValueChanged += new System.EventHandler(this.Filtro_626CV_Changed);
            //
            // btnGenerar_626CV
            //
            this.btnGenerar_626CV.Location = new System.Drawing.Point(500, 58);
            this.btnGenerar_626CV.Name = "btnGenerar_626CV";
            this.btnGenerar_626CV.Size = new System.Drawing.Size(150, 35);
            this.btnGenerar_626CV.TabIndex = 6;
            this.btnGenerar_626CV.Text = "Generar";
            this.btnGenerar_626CV.UseVisualStyleBackColor = true;
            this.btnGenerar_626CV.Click += new System.EventHandler(this.btnGenerar_626CV_Click);
            //
            // lblTituloReporte_626CV
            //
            this.lblTituloReporte_626CV.AutoSize = true;
            this.lblTituloReporte_626CV.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblTituloReporte_626CV.Location = new System.Drawing.Point(18, 112);
            this.lblTituloReporte_626CV.Name = "lblTituloReporte_626CV";
            this.lblTituloReporte_626CV.Size = new System.Drawing.Size(0, 20);
            this.lblTituloReporte_626CV.TabIndex = 7;
            //
            // lblSeccion_626CV
            //
            this.lblSeccion_626CV.AutoSize = true;
            this.lblSeccion_626CV.Location = new System.Drawing.Point(18, 152);
            this.lblSeccion_626CV.Name = "lblSeccion_626CV";
            this.lblSeccion_626CV.Size = new System.Drawing.Size(66, 20);
            this.lblSeccion_626CV.TabIndex = 8;
            this.lblSeccion_626CV.Text = "Sección";
            //
            // cmbSeccion_626CV
            //
            this.cmbSeccion_626CV.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSeccion_626CV.FormattingEnabled = true;
            this.cmbSeccion_626CV.Location = new System.Drawing.Point(100, 147);
            this.cmbSeccion_626CV.Name = "cmbSeccion_626CV";
            this.cmbSeccion_626CV.Size = new System.Drawing.Size(380, 28);
            this.cmbSeccion_626CV.TabIndex = 9;
            this.cmbSeccion_626CV.SelectedIndexChanged += new System.EventHandler(this.cmbSeccion_626CV_SelectedIndexChanged);
            //
            // dgvReporte_626CV
            //
            this.dgvReporte_626CV.AllowUserToAddRows = false;
            this.dgvReporte_626CV.AllowUserToDeleteRows = false;
            this.dgvReporte_626CV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReporte_626CV.Location = new System.Drawing.Point(18, 190);
            this.dgvReporte_626CV.Name = "dgvReporte_626CV";
            this.dgvReporte_626CV.ReadOnly = true;
            this.dgvReporte_626CV.RowHeadersVisible = false;
            this.dgvReporte_626CV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvReporte_626CV.Size = new System.Drawing.Size(860, 320);
            this.dgvReporte_626CV.TabIndex = 10;
            //
            // lblResumen_626CV
            //
            this.lblResumen_626CV.Location = new System.Drawing.Point(18, 520);
            this.lblResumen_626CV.Name = "lblResumen_626CV";
            this.lblResumen_626CV.Size = new System.Drawing.Size(860, 80);
            this.lblResumen_626CV.TabIndex = 11;
            //
            // btnExportarPdf_626CV
            //
            this.btnExportarPdf_626CV.Location = new System.Drawing.Point(18, 612);
            this.btnExportarPdf_626CV.Name = "btnExportarPdf_626CV";
            this.btnExportarPdf_626CV.Size = new System.Drawing.Size(180, 35);
            this.btnExportarPdf_626CV.TabIndex = 12;
            this.btnExportarPdf_626CV.Text = "Exportar a PDF";
            this.btnExportarPdf_626CV.UseVisualStyleBackColor = true;
            this.btnExportarPdf_626CV.Click += new System.EventHandler(this.btnExportarPdf_626CV_Click);
            //
            // btnVolver_626CV
            //
            this.btnVolver_626CV.Location = new System.Drawing.Point(766, 612);
            this.btnVolver_626CV.Name = "btnVolver_626CV";
            this.btnVolver_626CV.Size = new System.Drawing.Size(112, 35);
            this.btnVolver_626CV.TabIndex = 13;
            this.btnVolver_626CV.Text = "Volver";
            this.btnVolver_626CV.UseVisualStyleBackColor = true;
            this.btnVolver_626CV.Click += new System.EventHandler(this.btnVolver_626CV_Click);
            //
            // frmReportes
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 665);
            this.Controls.Add(this.lblTipo_626CV);
            this.Controls.Add(this.cmbTipo_626CV);
            this.Controls.Add(this.lblDesde_626CV);
            this.Controls.Add(this.dtpDesde_626CV);
            this.Controls.Add(this.lblHasta_626CV);
            this.Controls.Add(this.dtpHasta_626CV);
            this.Controls.Add(this.btnGenerar_626CV);
            this.Controls.Add(this.lblTituloReporte_626CV);
            this.Controls.Add(this.lblSeccion_626CV);
            this.Controls.Add(this.cmbSeccion_626CV);
            this.Controls.Add(this.dgvReporte_626CV);
            this.Controls.Add(this.lblResumen_626CV);
            this.Controls.Add(this.btnExportarPdf_626CV);
            this.Controls.Add(this.btnVolver_626CV);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "frmReportes";
            this.Text = "Reportes - Olimpic";
            ((System.ComponentModel.ISupportInitialize)(this.dgvReporte_626CV)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTipo_626CV;
        private System.Windows.Forms.ComboBox cmbTipo_626CV;
        private System.Windows.Forms.Label lblDesde_626CV;
        private System.Windows.Forms.DateTimePicker dtpDesde_626CV;
        private System.Windows.Forms.Label lblHasta_626CV;
        private System.Windows.Forms.DateTimePicker dtpHasta_626CV;
        private System.Windows.Forms.Button btnGenerar_626CV;
        private System.Windows.Forms.Label lblTituloReporte_626CV;
        private System.Windows.Forms.Label lblSeccion_626CV;
        private System.Windows.Forms.ComboBox cmbSeccion_626CV;
        private System.Windows.Forms.DataGridView dgvReporte_626CV;
        private System.Windows.Forms.Label lblResumen_626CV;
        private System.Windows.Forms.Button btnExportarPdf_626CV;
        private System.Windows.Forms.Button btnVolver_626CV;
    }
}
