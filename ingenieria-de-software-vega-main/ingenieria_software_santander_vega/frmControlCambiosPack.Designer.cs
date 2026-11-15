namespace ingenieria_software_santander_vega
{
    partial class frmControlCambiosPack
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
            this.lblTitulo_626CV = new System.Windows.Forms.Label();
            this.dgvCambios_626CV = new System.Windows.Forms.DataGridView();
            this.lblCodPack_626CV = new System.Windows.Forms.Label();
            this.cbCodPack_626CV = new System.Windows.Forms.ComboBox();
            this.lblNombre_626CV = new System.Windows.Forms.Label();
            this.cbNombre_626CV = new System.Windows.Forms.ComboBox();
            this.lblFechaIni_626CV = new System.Windows.Forms.Label();
            this.dtpFechaIni_626CV = new System.Windows.Forms.DateTimePicker();
            this.lblFechaFin_626CV = new System.Windows.Forms.Label();
            this.dtpFechaFin_626CV = new System.Windows.Forms.DateTimePicker();
            this.btnAplicar_626CV = new System.Windows.Forms.Button();
            this.btnLimpiar_626CV = new System.Windows.Forms.Button();
            this.btnActivar_626CV = new System.Windows.Forms.Button();
            this.btnSalir_626CV = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCambios_626CV)).BeginInit();
            this.SuspendLayout();
            //
            // lblTitulo_626CV
            //
            this.lblTitulo_626CV.AutoSize = true;
            this.lblTitulo_626CV.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.lblTitulo_626CV.Location = new System.Drawing.Point(18, 15);
            this.lblTitulo_626CV.Name = "lblTitulo_626CV";
            this.lblTitulo_626CV.Size = new System.Drawing.Size(330, 22);
            this.lblTitulo_626CV.TabIndex = 0;
            this.lblTitulo_626CV.Text = "Bitácora de cambios - Tabla PACK_C";
            //
            // dgvCambios_626CV
            //
            this.dgvCambios_626CV.AllowUserToAddRows = false;
            this.dgvCambios_626CV.AllowUserToDeleteRows = false;
            this.dgvCambios_626CV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCambios_626CV.Location = new System.Drawing.Point(18, 50);
            this.dgvCambios_626CV.MultiSelect = false;
            this.dgvCambios_626CV.Name = "dgvCambios_626CV";
            this.dgvCambios_626CV.ReadOnly = true;
            this.dgvCambios_626CV.RowHeadersVisible = false;
            this.dgvCambios_626CV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCambios_626CV.Size = new System.Drawing.Size(940, 300);
            this.dgvCambios_626CV.TabIndex = 1;
            //
            // lblCodPack_626CV
            //
            this.lblCodPack_626CV.AutoSize = true;
            this.lblCodPack_626CV.Location = new System.Drawing.Point(18, 378);
            this.lblCodPack_626CV.Name = "lblCodPack_626CV";
            this.lblCodPack_626CV.Size = new System.Drawing.Size(83, 20);
            this.lblCodPack_626CV.TabIndex = 2;
            this.lblCodPack_626CV.Text = "Cod. Pack";
            //
            // cbCodPack_626CV
            //
            this.cbCodPack_626CV.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCodPack_626CV.FormattingEnabled = true;
            this.cbCodPack_626CV.Location = new System.Drawing.Point(130, 374);
            this.cbCodPack_626CV.Name = "cbCodPack_626CV";
            this.cbCodPack_626CV.Size = new System.Drawing.Size(250, 28);
            this.cbCodPack_626CV.TabIndex = 3;
            //
            // lblNombre_626CV
            //
            this.lblNombre_626CV.AutoSize = true;
            this.lblNombre_626CV.Location = new System.Drawing.Point(520, 378);
            this.lblNombre_626CV.Name = "lblNombre_626CV";
            this.lblNombre_626CV.Size = new System.Drawing.Size(65, 20);
            this.lblNombre_626CV.TabIndex = 4;
            this.lblNombre_626CV.Text = "Nombre";
            //
            // cbNombre_626CV
            //
            this.cbNombre_626CV.FormattingEnabled = true;
            this.cbNombre_626CV.Location = new System.Drawing.Point(620, 374);
            this.cbNombre_626CV.MaxLength = 50;
            this.cbNombre_626CV.Name = "cbNombre_626CV";
            this.cbNombre_626CV.Size = new System.Drawing.Size(338, 28);
            this.cbNombre_626CV.TabIndex = 5;
            //
            // lblFechaIni_626CV
            //
            this.lblFechaIni_626CV.AutoSize = true;
            this.lblFechaIni_626CV.Location = new System.Drawing.Point(18, 428);
            this.lblFechaIni_626CV.Name = "lblFechaIni_626CV";
            this.lblFechaIni_626CV.Size = new System.Drawing.Size(80, 20);
            this.lblFechaIni_626CV.TabIndex = 6;
            this.lblFechaIni_626CV.Text = "Fecha inicial";
            //
            // dtpFechaIni_626CV
            //
            this.dtpFechaIni_626CV.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaIni_626CV.Location = new System.Drawing.Point(130, 424);
            this.dtpFechaIni_626CV.Name = "dtpFechaIni_626CV";
            this.dtpFechaIni_626CV.ShowCheckBox = true;
            this.dtpFechaIni_626CV.Size = new System.Drawing.Size(180, 26);
            this.dtpFechaIni_626CV.TabIndex = 7;
            //
            // lblFechaFin_626CV
            //
            this.lblFechaFin_626CV.AutoSize = true;
            this.lblFechaFin_626CV.Location = new System.Drawing.Point(18, 478);
            this.lblFechaFin_626CV.Name = "lblFechaFin_626CV";
            this.lblFechaFin_626CV.Size = new System.Drawing.Size(83, 20);
            this.lblFechaFin_626CV.TabIndex = 8;
            this.lblFechaFin_626CV.Text = "Fecha final";
            //
            // dtpFechaFin_626CV
            //
            this.dtpFechaFin_626CV.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaFin_626CV.Location = new System.Drawing.Point(130, 474);
            this.dtpFechaFin_626CV.Name = "dtpFechaFin_626CV";
            this.dtpFechaFin_626CV.ShowCheckBox = true;
            this.dtpFechaFin_626CV.Size = new System.Drawing.Size(180, 26);
            this.dtpFechaFin_626CV.TabIndex = 9;
            //
            // btnAplicar_626CV
            //
            this.btnAplicar_626CV.Location = new System.Drawing.Point(400, 420);
            this.btnAplicar_626CV.Name = "btnAplicar_626CV";
            this.btnAplicar_626CV.Size = new System.Drawing.Size(180, 35);
            this.btnAplicar_626CV.TabIndex = 10;
            this.btnAplicar_626CV.Text = "Aplicar";
            this.btnAplicar_626CV.UseVisualStyleBackColor = true;
            this.btnAplicar_626CV.Click += new System.EventHandler(this.btnAplicar_626CV_Click);
            //
            // btnLimpiar_626CV
            //
            this.btnLimpiar_626CV.Location = new System.Drawing.Point(400, 470);
            this.btnLimpiar_626CV.Name = "btnLimpiar_626CV";
            this.btnLimpiar_626CV.Size = new System.Drawing.Size(180, 35);
            this.btnLimpiar_626CV.TabIndex = 11;
            this.btnLimpiar_626CV.Text = "Limpiar";
            this.btnLimpiar_626CV.UseVisualStyleBackColor = true;
            this.btnLimpiar_626CV.Click += new System.EventHandler(this.btnLimpiar_626CV_Click);
            //
            // btnSalir_626CV
            //
            this.btnSalir_626CV.Location = new System.Drawing.Point(620, 420);
            this.btnSalir_626CV.Name = "btnSalir_626CV";
            this.btnSalir_626CV.Size = new System.Drawing.Size(180, 35);
            this.btnSalir_626CV.TabIndex = 13;
            this.btnSalir_626CV.Text = "Salir";
            this.btnSalir_626CV.UseVisualStyleBackColor = true;
            this.btnSalir_626CV.Click += new System.EventHandler(this.btnSalir_626CV_Click);
            //
            // btnActivar_626CV
            //
            this.btnActivar_626CV.Location = new System.Drawing.Point(620, 470);
            this.btnActivar_626CV.Name = "btnActivar_626CV";
            this.btnActivar_626CV.Size = new System.Drawing.Size(180, 35);
            this.btnActivar_626CV.TabIndex = 12;
            this.btnActivar_626CV.Text = "Activar";
            this.btnActivar_626CV.UseVisualStyleBackColor = true;
            this.btnActivar_626CV.Click += new System.EventHandler(this.btnActivar_626CV_Click);
            //
            // frmControlCambiosPack
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(980, 530);
            this.Controls.Add(this.lblTitulo_626CV);
            this.Controls.Add(this.dgvCambios_626CV);
            this.Controls.Add(this.lblCodPack_626CV);
            this.Controls.Add(this.cbCodPack_626CV);
            this.Controls.Add(this.lblNombre_626CV);
            this.Controls.Add(this.cbNombre_626CV);
            this.Controls.Add(this.lblFechaIni_626CV);
            this.Controls.Add(this.dtpFechaIni_626CV);
            this.Controls.Add(this.lblFechaFin_626CV);
            this.Controls.Add(this.dtpFechaFin_626CV);
            this.Controls.Add(this.btnAplicar_626CV);
            this.Controls.Add(this.btnLimpiar_626CV);
            this.Controls.Add(this.btnActivar_626CV);
            this.Controls.Add(this.btnSalir_626CV);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "frmControlCambiosPack";
            this.Text = "Control de cambios de packs - Olimpic";
            ((System.ComponentModel.ISupportInitialize)(this.dgvCambios_626CV)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitulo_626CV;
        private System.Windows.Forms.DataGridView dgvCambios_626CV;
        private System.Windows.Forms.Label lblCodPack_626CV;
        private System.Windows.Forms.ComboBox cbCodPack_626CV;
        private System.Windows.Forms.Label lblNombre_626CV;
        private System.Windows.Forms.ComboBox cbNombre_626CV;
        private System.Windows.Forms.Label lblFechaIni_626CV;
        private System.Windows.Forms.DateTimePicker dtpFechaIni_626CV;
        private System.Windows.Forms.Label lblFechaFin_626CV;
        private System.Windows.Forms.DateTimePicker dtpFechaFin_626CV;
        private System.Windows.Forms.Button btnAplicar_626CV;
        private System.Windows.Forms.Button btnLimpiar_626CV;
        private System.Windows.Forms.Button btnActivar_626CV;
        private System.Windows.Forms.Button btnSalir_626CV;
    }
}