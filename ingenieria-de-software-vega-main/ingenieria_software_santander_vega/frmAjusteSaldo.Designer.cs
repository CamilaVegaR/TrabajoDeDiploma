namespace ingenieria_software_santander_vega
{
    partial class frmAjusteSaldo
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
            this.lblDni_626CV = new System.Windows.Forms.Label();
            this.txtDni_626CV = new System.Windows.Forms.TextBox();
            this.btnBuscarCliente_626CV = new System.Windows.Forms.Button();
            this.lblCliente_626CV = new System.Windows.Forms.Label();
            this.rdbSumar_626CV = new System.Windows.Forms.RadioButton();
            this.rdbRestar_626CV = new System.Windows.Forms.RadioButton();
            this.lblCantidad_626CV = new System.Windows.Forms.Label();
            this.nudCantidad_626CV = new System.Windows.Forms.NumericUpDown();
            this.lblMotivo_626CV = new System.Windows.Forms.Label();
            this.txtMotivo_626CV = new System.Windows.Forms.TextBox();
            this.btnAjustar_626CV = new System.Windows.Forms.Button();
            this.btnVolver_626CV = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nudCantidad_626CV)).BeginInit();
            this.SuspendLayout();
            //
            // lblDni_626CV
            //
            this.lblDni_626CV.AutoSize = true;
            this.lblDni_626CV.Location = new System.Drawing.Point(18, 25);
            this.lblDni_626CV.Name = "lblDni_626CV";
            this.lblDni_626CV.Size = new System.Drawing.Size(121, 20);
            this.lblDni_626CV.TabIndex = 0;
            this.lblDni_626CV.Text = "DNI del cliente";
            //
            // txtDni_626CV
            //
            this.txtDni_626CV.Location = new System.Drawing.Point(200, 22);
            this.txtDni_626CV.Name = "txtDni_626CV";
            this.txtDni_626CV.Size = new System.Drawing.Size(180, 26);
            this.txtDni_626CV.TabIndex = 1;
            this.txtDni_626CV.MaxLength = 20;
            //
            // btnBuscarCliente_626CV
            //
            this.btnBuscarCliente_626CV.Location = new System.Drawing.Point(395, 18);
            this.btnBuscarCliente_626CV.Name = "btnBuscarCliente_626CV";
            this.btnBuscarCliente_626CV.Size = new System.Drawing.Size(120, 35);
            this.btnBuscarCliente_626CV.TabIndex = 2;
            this.btnBuscarCliente_626CV.Text = "Buscar";
            this.btnBuscarCliente_626CV.UseVisualStyleBackColor = true;
            this.btnBuscarCliente_626CV.Click += new System.EventHandler(this.btnBuscarCliente_626CV_Click);
            //
            // lblCliente_626CV
            //
            this.lblCliente_626CV.AutoSize = true;
            this.lblCliente_626CV.Location = new System.Drawing.Point(18, 78);
            this.lblCliente_626CV.Name = "lblCliente_626CV";
            this.lblCliente_626CV.Size = new System.Drawing.Size(195, 20);
            this.lblCliente_626CV.TabIndex = 3;
            this.lblCliente_626CV.Text = "Busque un cliente por DNI.";
            //
            // rdbSumar_626CV
            //
            this.rdbSumar_626CV.AutoSize = true;
            this.rdbSumar_626CV.Location = new System.Drawing.Point(22, 125);
            this.rdbSumar_626CV.Name = "rdbSumar_626CV";
            this.rdbSumar_626CV.Size = new System.Drawing.Size(134, 24);
            this.rdbSumar_626CV.TabIndex = 4;
            this.rdbSumar_626CV.Text = "Sumar clases";
            this.rdbSumar_626CV.Checked = true;
            this.rdbSumar_626CV.TabStop = true;
            this.rdbSumar_626CV.UseVisualStyleBackColor = true;
            //
            // rdbRestar_626CV
            //
            this.rdbRestar_626CV.AutoSize = true;
            this.rdbRestar_626CV.Location = new System.Drawing.Point(200, 125);
            this.rdbRestar_626CV.Name = "rdbRestar_626CV";
            this.rdbRestar_626CV.Size = new System.Drawing.Size(137, 24);
            this.rdbRestar_626CV.TabIndex = 5;
            this.rdbRestar_626CV.Text = "Restar clases";
            this.rdbRestar_626CV.UseVisualStyleBackColor = true;
            //
            // lblCantidad_626CV
            //
            this.lblCantidad_626CV.AutoSize = true;
            this.lblCantidad_626CV.Location = new System.Drawing.Point(18, 178);
            this.lblCantidad_626CV.Name = "lblCantidad_626CV";
            this.lblCantidad_626CV.Size = new System.Drawing.Size(144, 20);
            this.lblCantidad_626CV.TabIndex = 6;
            this.lblCantidad_626CV.Text = "Cantidad de clases";
            //
            // nudCantidad_626CV
            //
            this.nudCantidad_626CV.Location = new System.Drawing.Point(200, 175);
            this.nudCantidad_626CV.Name = "nudCantidad_626CV";
            this.nudCantidad_626CV.Size = new System.Drawing.Size(100, 26);
            this.nudCantidad_626CV.TabIndex = 7;
            this.nudCantidad_626CV.Maximum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.nudCantidad_626CV.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudCantidad_626CV.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            //
            // lblMotivo_626CV
            //
            this.lblMotivo_626CV.AutoSize = true;
            this.lblMotivo_626CV.Location = new System.Drawing.Point(18, 223);
            this.lblMotivo_626CV.Name = "lblMotivo_626CV";
            this.lblMotivo_626CV.Size = new System.Drawing.Size(59, 20);
            this.lblMotivo_626CV.TabIndex = 8;
            this.lblMotivo_626CV.Text = "Motivo";
            //
            // txtMotivo_626CV
            //
            this.txtMotivo_626CV.Location = new System.Drawing.Point(200, 220);
            this.txtMotivo_626CV.Name = "txtMotivo_626CV";
            this.txtMotivo_626CV.Size = new System.Drawing.Size(315, 26);
            this.txtMotivo_626CV.TabIndex = 9;
            this.txtMotivo_626CV.MaxLength = 30;
            //
            // btnAjustar_626CV
            //
            this.btnAjustar_626CV.Location = new System.Drawing.Point(18, 290);
            this.btnAjustar_626CV.Name = "btnAjustar_626CV";
            this.btnAjustar_626CV.Size = new System.Drawing.Size(180, 35);
            this.btnAjustar_626CV.TabIndex = 10;
            this.btnAjustar_626CV.Text = "Ajustar saldo";
            this.btnAjustar_626CV.UseVisualStyleBackColor = true;
            this.btnAjustar_626CV.Click += new System.EventHandler(this.btnAjustar_626CV_Click);
            //
            // btnVolver_626CV
            //
            this.btnVolver_626CV.Location = new System.Drawing.Point(403, 290);
            this.btnVolver_626CV.Name = "btnVolver_626CV";
            this.btnVolver_626CV.Size = new System.Drawing.Size(112, 35);
            this.btnVolver_626CV.TabIndex = 11;
            this.btnVolver_626CV.Text = "Volver";
            this.btnVolver_626CV.UseVisualStyleBackColor = true;
            this.btnVolver_626CV.Click += new System.EventHandler(this.btnVolver_626CV_Click);
            //
            // frmAjusteSaldo
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(540, 350);
            this.Controls.Add(this.lblDni_626CV);
            this.Controls.Add(this.txtDni_626CV);
            this.Controls.Add(this.btnBuscarCliente_626CV);
            this.Controls.Add(this.lblCliente_626CV);
            this.Controls.Add(this.rdbSumar_626CV);
            this.Controls.Add(this.rdbRestar_626CV);
            this.Controls.Add(this.lblCantidad_626CV);
            this.Controls.Add(this.nudCantidad_626CV);
            this.Controls.Add(this.lblMotivo_626CV);
            this.Controls.Add(this.txtMotivo_626CV);
            this.Controls.Add(this.btnAjustar_626CV);
            this.Controls.Add(this.btnVolver_626CV);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "frmAjusteSaldo";
            this.Text = "Ajuste de saldo de clases - Olimpic";
            ((System.ComponentModel.ISupportInitialize)(this.nudCantidad_626CV)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblDni_626CV;
        private System.Windows.Forms.TextBox txtDni_626CV;
        private System.Windows.Forms.Button btnBuscarCliente_626CV;
        private System.Windows.Forms.Label lblCliente_626CV;
        private System.Windows.Forms.RadioButton rdbSumar_626CV;
        private System.Windows.Forms.RadioButton rdbRestar_626CV;
        private System.Windows.Forms.Label lblCantidad_626CV;
        private System.Windows.Forms.NumericUpDown nudCantidad_626CV;
        private System.Windows.Forms.Label lblMotivo_626CV;
        private System.Windows.Forms.TextBox txtMotivo_626CV;
        private System.Windows.Forms.Button btnAjustar_626CV;
        private System.Windows.Forms.Button btnVolver_626CV;
    }
}