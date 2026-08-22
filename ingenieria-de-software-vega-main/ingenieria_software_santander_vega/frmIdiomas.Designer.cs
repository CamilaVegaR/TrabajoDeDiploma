namespace ingenieria_software_santander_vega
{
    partial class frmIdiomas
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
            this.btnNuevoIdioma = new System.Windows.Forms.Button();
            this.dgvTraducciones = new System.Windows.Forms.DataGridView();
            this.lstIdiomas = new System.Windows.Forms.ListBox();
            this.lblIdiomas = new System.Windows.Forms.Label();
            this.lblTraducciones = new System.Windows.Forms.Label();
            this.btnGuardarTraduccion = new System.Windows.Forms.Button();
            this.btnVolver = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTraducciones)).BeginInit();
            this.SuspendLayout();
            // 
            // btnNuevoIdioma
            // 
            this.btnNuevoIdioma.Location = new System.Drawing.Point(15, 282);
            this.btnNuevoIdioma.Name = "btnNuevoIdioma";
            this.btnNuevoIdioma.Size = new System.Drawing.Size(230, 23);
            this.btnNuevoIdioma.TabIndex = 0;
            this.btnNuevoIdioma.Text = "Nuevo Idioma";
            this.btnNuevoIdioma.UseVisualStyleBackColor = true;
            this.btnNuevoIdioma.Click += new System.EventHandler(this.btnNuevoIdioma_Click);
            // 
            // dgvTraducciones
            // 
            this.dgvTraducciones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTraducciones.Location = new System.Drawing.Point(252, 25);
            this.dgvTraducciones.Name = "dgvTraducciones";
            this.dgvTraducciones.Size = new System.Drawing.Size(672, 494);
            this.dgvTraducciones.TabIndex = 1;
            // 
            // lstIdiomas
            // 
            this.lstIdiomas.FormattingEnabled = true;
            this.lstIdiomas.Location = new System.Drawing.Point(15, 25);
            this.lstIdiomas.Name = "lstIdiomas";
            this.lstIdiomas.Size = new System.Drawing.Size(230, 251);
            this.lstIdiomas.TabIndex = 2;
            this.lstIdiomas.SelectedIndexChanged += new System.EventHandler(this.lstIdiomas_SelectedIndexChanged);
            // 
            // lblIdiomas
            // 
            this.lblIdiomas.AutoSize = true;
            this.lblIdiomas.Location = new System.Drawing.Point(12, 9);
            this.lblIdiomas.Name = "lblIdiomas";
            this.lblIdiomas.Size = new System.Drawing.Size(43, 13);
            this.lblIdiomas.TabIndex = 3;
            this.lblIdiomas.Text = "Idiomas";
            // 
            // lblTraducciones
            // 
            this.lblTraducciones.AutoSize = true;
            this.lblTraducciones.Location = new System.Drawing.Point(252, 9);
            this.lblTraducciones.Name = "lblTraducciones";
            this.lblTraducciones.Size = new System.Drawing.Size(72, 13);
            this.lblTraducciones.TabIndex = 4;
            this.lblTraducciones.Text = "Traducciones";
            // 
            // btnGuardarTraduccion
            // 
            this.btnGuardarTraduccion.Location = new System.Drawing.Point(849, 525);
            this.btnGuardarTraduccion.Name = "btnGuardarTraduccion";
            this.btnGuardarTraduccion.Size = new System.Drawing.Size(75, 23);
            this.btnGuardarTraduccion.TabIndex = 5;
            this.btnGuardarTraduccion.Text = "Guardar";
            this.btnGuardarTraduccion.UseVisualStyleBackColor = true;
            this.btnGuardarTraduccion.Click += new System.EventHandler(this.btnGuardarTraduccion_Click);
            // 
            // btnVolver
            // 
            this.btnVolver.Location = new System.Drawing.Point(15, 527);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(75, 23);
            this.btnVolver.TabIndex = 6;
            this.btnVolver.Text = "Volver";
            this.btnVolver.UseVisualStyleBackColor = true;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // frmIdiomas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(937, 562);
            this.Controls.Add(this.btnVolver);
            this.Controls.Add(this.btnGuardarTraduccion);
            this.Controls.Add(this.lblTraducciones);
            this.Controls.Add(this.lblIdiomas);
            this.Controls.Add(this.lstIdiomas);
            this.Controls.Add(this.dgvTraducciones);
            this.Controls.Add(this.btnNuevoIdioma);
            this.Name = "frmIdiomas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmIdiomas";
            ((System.ComponentModel.ISupportInitialize)(this.dgvTraducciones)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnNuevoIdioma;
        private System.Windows.Forms.DataGridView dgvTraducciones;
        private System.Windows.Forms.ListBox lstIdiomas;
        private System.Windows.Forms.Label lblIdiomas;
        private System.Windows.Forms.Label lblTraducciones;
        private System.Windows.Forms.Button btnGuardarTraduccion;
        private System.Windows.Forms.Button btnVolver;
    }
}