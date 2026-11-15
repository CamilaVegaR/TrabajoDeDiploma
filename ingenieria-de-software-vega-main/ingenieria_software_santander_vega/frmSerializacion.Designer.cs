namespace ingenieria_software_santander_vega
{
    partial class frmSerializacion
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
            this.lblEntidad_626CV = new System.Windows.Forms.Label();
            this.cmbEntidad_626CV = new System.Windows.Forms.ComboBox();
            this.btnSerializar_626CV = new System.Windows.Forms.Button();
            this.btnDeserializar_626CV = new System.Windows.Forms.Button();
            this.lblDatos_626CV = new System.Windows.Forms.Label();
            this.dgvDatos_626CV = new System.Windows.Forms.DataGridView();
            this.lblResumen_626CV = new System.Windows.Forms.Label();
            this.lblXml_626CV = new System.Windows.Forms.Label();
            this.txtXml_626CV = new System.Windows.Forms.TextBox();
            this.btnVolver_626CV = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos_626CV)).BeginInit();
            this.SuspendLayout();
            //
            // lblEntidad_626CV
            //
            this.lblEntidad_626CV.AutoSize = true;
            this.lblEntidad_626CV.Location = new System.Drawing.Point(18, 23);
            this.lblEntidad_626CV.Name = "lblEntidad_626CV";
            this.lblEntidad_626CV.Size = new System.Drawing.Size(55, 20);
            this.lblEntidad_626CV.TabIndex = 0;
            this.lblEntidad_626CV.Text = "Datos";
            //
            // cmbEntidad_626CV
            //
            this.cmbEntidad_626CV.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEntidad_626CV.FormattingEnabled = true;
            this.cmbEntidad_626CV.Location = new System.Drawing.Point(100, 18);
            this.cmbEntidad_626CV.Name = "cmbEntidad_626CV";
            this.cmbEntidad_626CV.Size = new System.Drawing.Size(200, 28);
            this.cmbEntidad_626CV.TabIndex = 1;
            this.cmbEntidad_626CV.SelectedIndexChanged += new System.EventHandler(this.cmbEntidad_626CV_SelectedIndexChanged);
            //
            // btnSerializar_626CV
            //
            this.btnSerializar_626CV.Location = new System.Drawing.Point(330, 14);
            this.btnSerializar_626CV.Name = "btnSerializar_626CV";
            this.btnSerializar_626CV.Size = new System.Drawing.Size(220, 35);
            this.btnSerializar_626CV.TabIndex = 2;
            this.btnSerializar_626CV.Text = "Serializar a XML";
            this.btnSerializar_626CV.UseVisualStyleBackColor = true;
            this.btnSerializar_626CV.Click += new System.EventHandler(this.btnSerializar_626CV_Click);
            //
            // btnDeserializar_626CV
            //
            this.btnDeserializar_626CV.Location = new System.Drawing.Point(565, 14);
            this.btnDeserializar_626CV.Name = "btnDeserializar_626CV";
            this.btnDeserializar_626CV.Size = new System.Drawing.Size(220, 35);
            this.btnDeserializar_626CV.TabIndex = 3;
            this.btnDeserializar_626CV.Text = "Deserializar desde XML";
            this.btnDeserializar_626CV.UseVisualStyleBackColor = true;
            this.btnDeserializar_626CV.Click += new System.EventHandler(this.btnDeserializar_626CV_Click);
            //
            // lblDatos_626CV
            //
            this.lblDatos_626CV.AutoSize = true;
            this.lblDatos_626CV.Location = new System.Drawing.Point(18, 68);
            this.lblDatos_626CV.Name = "lblDatos_626CV";
            this.lblDatos_626CV.Size = new System.Drawing.Size(177, 20);
            this.lblDatos_626CV.TabIndex = 4;
            this.lblDatos_626CV.Text = "Registros deserializados";
            //
            // dgvDatos_626CV
            //
            this.dgvDatos_626CV.AllowUserToAddRows = false;
            this.dgvDatos_626CV.AllowUserToDeleteRows = false;
            this.dgvDatos_626CV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvDatos_626CV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDatos_626CV.Location = new System.Drawing.Point(18, 93);
            this.dgvDatos_626CV.Name = "dgvDatos_626CV";
            this.dgvDatos_626CV.ReadOnly = true;
            this.dgvDatos_626CV.RowHeadersVisible = false;
            this.dgvDatos_626CV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDatos_626CV.Size = new System.Drawing.Size(860, 230);
            this.dgvDatos_626CV.TabIndex = 5;
            //
            // lblResumen_626CV
            //
            this.lblResumen_626CV.Location = new System.Drawing.Point(18, 330);
            this.lblResumen_626CV.Name = "lblResumen_626CV";
            this.lblResumen_626CV.Size = new System.Drawing.Size(860, 25);
            this.lblResumen_626CV.TabIndex = 6;
            //
            // lblXml_626CV
            //
            this.lblXml_626CV.AutoSize = true;
            this.lblXml_626CV.Location = new System.Drawing.Point(18, 362);
            this.lblXml_626CV.Name = "lblXml_626CV";
            this.lblXml_626CV.Size = new System.Drawing.Size(147, 20);
            this.lblXml_626CV.TabIndex = 7;
            this.lblXml_626CV.Text = "Contenido del XML";
            //
            // txtXml_626CV
            //
            this.txtXml_626CV.Font = new System.Drawing.Font("Consolas", 9F);
            this.txtXml_626CV.Location = new System.Drawing.Point(18, 387);
            this.txtXml_626CV.Multiline = true;
            this.txtXml_626CV.Name = "txtXml_626CV";
            this.txtXml_626CV.ReadOnly = true;
            this.txtXml_626CV.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtXml_626CV.Size = new System.Drawing.Size(860, 200);
            this.txtXml_626CV.TabIndex = 8;
            this.txtXml_626CV.WordWrap = false;
            //
            // btnVolver_626CV
            //
            this.btnVolver_626CV.Location = new System.Drawing.Point(766, 600);
            this.btnVolver_626CV.Name = "btnVolver_626CV";
            this.btnVolver_626CV.Size = new System.Drawing.Size(112, 35);
            this.btnVolver_626CV.TabIndex = 9;
            this.btnVolver_626CV.Text = "Volver";
            this.btnVolver_626CV.UseVisualStyleBackColor = true;
            this.btnVolver_626CV.Click += new System.EventHandler(this.btnVolver_626CV_Click);
            //
            // frmSerializacion
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 650);
            this.Controls.Add(this.lblEntidad_626CV);
            this.Controls.Add(this.cmbEntidad_626CV);
            this.Controls.Add(this.btnSerializar_626CV);
            this.Controls.Add(this.btnDeserializar_626CV);
            this.Controls.Add(this.lblDatos_626CV);
            this.Controls.Add(this.dgvDatos_626CV);
            this.Controls.Add(this.lblResumen_626CV);
            this.Controls.Add(this.lblXml_626CV);
            this.Controls.Add(this.txtXml_626CV);
            this.Controls.Add(this.btnVolver_626CV);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "frmSerializacion";
            this.Text = "Serialización - Olimpic";
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos_626CV)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblEntidad_626CV;
        private System.Windows.Forms.ComboBox cmbEntidad_626CV;
        private System.Windows.Forms.Button btnSerializar_626CV;
        private System.Windows.Forms.Button btnDeserializar_626CV;
        private System.Windows.Forms.Label lblDatos_626CV;
        private System.Windows.Forms.DataGridView dgvDatos_626CV;
        private System.Windows.Forms.Label lblResumen_626CV;
        private System.Windows.Forms.Label lblXml_626CV;
        private System.Windows.Forms.TextBox txtXml_626CV;
        private System.Windows.Forms.Button btnVolver_626CV;
    }
}
