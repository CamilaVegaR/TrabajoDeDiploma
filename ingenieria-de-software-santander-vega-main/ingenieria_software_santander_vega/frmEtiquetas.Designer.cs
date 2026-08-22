namespace ingenieria_software_santander_vega
{
    partial class frmEtiquetas
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
            this.dgvEtiquetas = new System.Windows.Forms.DataGridView();
            this.txtClave = new System.Windows.Forms.TextBox();
            this.txtFormulario = new System.Windows.Forms.TextBox();
            this.lblClave = new System.Windows.Forms.Label();
            this.lblFormulario = new System.Windows.Forms.Label();
            this.btnVolver = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEtiquetas)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvEtiquetas
            // 
            this.dgvEtiquetas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEtiquetas.Enabled = false;
            this.dgvEtiquetas.Location = new System.Drawing.Point(12, 12);
            this.dgvEtiquetas.Name = "dgvEtiquetas";
            this.dgvEtiquetas.Size = new System.Drawing.Size(550, 238);
            this.dgvEtiquetas.TabIndex = 22;
            // 
            // txtClave
            // 
            this.txtClave.Location = new System.Drawing.Point(149, 269);
            this.txtClave.Name = "txtClave";
            this.txtClave.Size = new System.Drawing.Size(100, 20);
            this.txtClave.TabIndex = 21;
            // 
            // txtFormulario
            // 
            this.txtFormulario.Location = new System.Drawing.Point(15, 269);
            this.txtFormulario.Name = "txtFormulario";
            this.txtFormulario.Size = new System.Drawing.Size(100, 20);
            this.txtFormulario.TabIndex = 20;
            // 
            // lblClave
            // 
            this.lblClave.AutoSize = true;
            this.lblClave.Location = new System.Drawing.Point(146, 253);
            this.lblClave.Name = "lblClave";
            this.lblClave.Size = new System.Drawing.Size(34, 13);
            this.lblClave.TabIndex = 19;
            this.lblClave.Text = "Clave";
            // 
            // lblFormulario
            // 
            this.lblFormulario.AutoSize = true;
            this.lblFormulario.Location = new System.Drawing.Point(12, 253);
            this.lblFormulario.Name = "lblFormulario";
            this.lblFormulario.Size = new System.Drawing.Size(55, 13);
            this.lblFormulario.TabIndex = 18;
            this.lblFormulario.Text = "Formulario";
            // 
            // btnVolver
            // 
            this.btnVolver.Location = new System.Drawing.Point(15, 325);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(75, 23);
            this.btnVolver.TabIndex = 17;
            this.btnVolver.Text = "Volver";
            this.btnVolver.UseVisualStyleBackColor = true;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(289, 269);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(75, 23);
            this.btnGuardar.TabIndex = 0;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // frmEtiquetas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(578, 368);
            this.Controls.Add(this.dgvEtiquetas);
            this.Controls.Add(this.txtClave);
            this.Controls.Add(this.txtFormulario);
            this.Controls.Add(this.lblClave);
            this.Controls.Add(this.lblFormulario);
            this.Controls.Add(this.btnVolver);
            this.Controls.Add(this.btnGuardar);
            this.Name = "frmEtiquetas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmEtiquetas";
            ((System.ComponentModel.ISupportInitialize)(this.dgvEtiquetas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.Label lblFormulario;
        private System.Windows.Forms.Label lblClave;
        private System.Windows.Forms.TextBox txtFormulario;
        private System.Windows.Forms.TextBox txtClave;
        private System.Windows.Forms.DataGridView dgvEtiquetas;
    }
}