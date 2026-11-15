namespace ingenieria_software_santander_vega
{
    partial class frmPacks
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
            this.dgvPacks_626CV = new System.Windows.Forms.DataGridView();
            this.lblModo_626CV = new System.Windows.Forms.Label();
            this.lblNombre_626CV = new System.Windows.Forms.Label();
            this.txtNombre_626CV = new System.Windows.Forms.TextBox();
            this.lblCantidadClases_626CV = new System.Windows.Forms.Label();
            this.txtCantidadClases_626CV = new System.Windows.Forms.TextBox();
            this.lblPrecioLista_626CV = new System.Windows.Forms.Label();
            this.txtPrecioLista_626CV = new System.Windows.Forms.TextBox();
            this.lblDescuento_626CV = new System.Windows.Forms.Label();
            this.txtDescuento_626CV = new System.Windows.Forms.TextBox();
            this.lblPrecioFinal_626CV = new System.Windows.Forms.Label();
            this.btnNuevo_626CV = new System.Windows.Forms.Button();
            this.btnGuardar_626CV = new System.Windows.Forms.Button();
            this.btnDesactivar_626CV = new System.Windows.Forms.Button();
            this.btnVolver_626CV = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPacks_626CV)).BeginInit();
            this.SuspendLayout();
            //
            // dgvPacks_626CV
            //
            this.dgvPacks_626CV.AllowUserToAddRows = false;
            this.dgvPacks_626CV.AllowUserToDeleteRows = false;
            this.dgvPacks_626CV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPacks_626CV.Location = new System.Drawing.Point(18, 18);
            this.dgvPacks_626CV.MultiSelect = false;
            this.dgvPacks_626CV.Name = "dgvPacks_626CV";
            this.dgvPacks_626CV.ReadOnly = true;
            this.dgvPacks_626CV.RowHeadersVisible = false;
            this.dgvPacks_626CV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPacks_626CV.Size = new System.Drawing.Size(690, 250);
            this.dgvPacks_626CV.TabIndex = 0;
            this.dgvPacks_626CV.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPacks_626CV_CellClick);
            //
            // lblModo_626CV
            //
            this.lblModo_626CV.AutoSize = true;
            this.lblModo_626CV.Location = new System.Drawing.Point(18, 285);
            this.lblModo_626CV.Name = "lblModo_626CV";
            this.lblModo_626CV.Size = new System.Drawing.Size(92, 20);
            this.lblModo_626CV.TabIndex = 1;
            this.lblModo_626CV.Text = "Nuevo pack";
            //
            // lblNombre_626CV
            //
            this.lblNombre_626CV.AutoSize = true;
            this.lblNombre_626CV.Location = new System.Drawing.Point(18, 325);
            this.lblNombre_626CV.Name = "lblNombre_626CV";
            this.lblNombre_626CV.Size = new System.Drawing.Size(65, 20);
            this.lblNombre_626CV.TabIndex = 2;
            this.lblNombre_626CV.Text = "Nombre";
            //
            // txtNombre_626CV
            //
            this.txtNombre_626CV.Location = new System.Drawing.Point(200, 320);
            this.txtNombre_626CV.MaxLength = 50;
            this.txtNombre_626CV.Name = "txtNombre_626CV";
            this.txtNombre_626CV.Size = new System.Drawing.Size(300, 26);
            this.txtNombre_626CV.TabIndex = 3;
            //
            // lblCantidadClases_626CV
            //
            this.lblCantidadClases_626CV.AutoSize = true;
            this.lblCantidadClases_626CV.Location = new System.Drawing.Point(18, 365);
            this.lblCantidadClases_626CV.Name = "lblCantidadClases_626CV";
            this.lblCantidadClases_626CV.Size = new System.Drawing.Size(145, 20);
            this.lblCantidadClases_626CV.TabIndex = 4;
            this.lblCantidadClases_626CV.Text = "Cantidad de clases";
            //
            // txtCantidadClases_626CV
            //
            this.txtCantidadClases_626CV.Location = new System.Drawing.Point(200, 360);
            this.txtCantidadClases_626CV.Name = "txtCantidadClases_626CV";
            this.txtCantidadClases_626CV.Size = new System.Drawing.Size(120, 26);
            this.txtCantidadClases_626CV.TabIndex = 5;
            //
            // lblPrecioLista_626CV
            //
            this.lblPrecioLista_626CV.AutoSize = true;
            this.lblPrecioLista_626CV.Location = new System.Drawing.Point(18, 405);
            this.lblPrecioLista_626CV.Name = "lblPrecioLista_626CV";
            this.lblPrecioLista_626CV.Size = new System.Drawing.Size(115, 20);
            this.lblPrecioLista_626CV.TabIndex = 6;
            this.lblPrecioLista_626CV.Text = "Precio de lista $";
            //
            // txtPrecioLista_626CV
            //
            this.txtPrecioLista_626CV.Location = new System.Drawing.Point(200, 400);
            this.txtPrecioLista_626CV.Name = "txtPrecioLista_626CV";
            this.txtPrecioLista_626CV.Size = new System.Drawing.Size(120, 26);
            this.txtPrecioLista_626CV.TabIndex = 7;
            this.txtPrecioLista_626CV.TextChanged += new System.EventHandler(this.txtPrecioODescuento_626CV_TextChanged);
            //
            // lblDescuento_626CV
            //
            this.lblDescuento_626CV.AutoSize = true;
            this.lblDescuento_626CV.Location = new System.Drawing.Point(18, 445);
            this.lblDescuento_626CV.Name = "lblDescuento_626CV";
            this.lblDescuento_626CV.Size = new System.Drawing.Size(105, 20);
            this.lblDescuento_626CV.TabIndex = 8;
            this.lblDescuento_626CV.Text = "Descuento %";
            //
            // txtDescuento_626CV
            //
            this.txtDescuento_626CV.Location = new System.Drawing.Point(200, 440);
            this.txtDescuento_626CV.Name = "txtDescuento_626CV";
            this.txtDescuento_626CV.Size = new System.Drawing.Size(120, 26);
            this.txtDescuento_626CV.TabIndex = 9;
            this.txtDescuento_626CV.TextChanged += new System.EventHandler(this.txtPrecioODescuento_626CV_TextChanged);
            //
            // lblPrecioFinal_626CV
            //
            this.lblPrecioFinal_626CV.AutoSize = true;
            this.lblPrecioFinal_626CV.Location = new System.Drawing.Point(350, 445);
            this.lblPrecioFinal_626CV.Name = "lblPrecioFinal_626CV";
            this.lblPrecioFinal_626CV.Size = new System.Drawing.Size(110, 20);
            this.lblPrecioFinal_626CV.TabIndex = 10;
            this.lblPrecioFinal_626CV.Text = "Precio final: -";
            //
            // btnNuevo_626CV
            //
            this.btnNuevo_626CV.Location = new System.Drawing.Point(18, 495);
            this.btnNuevo_626CV.Name = "btnNuevo_626CV";
            this.btnNuevo_626CV.Size = new System.Drawing.Size(150, 35);
            this.btnNuevo_626CV.TabIndex = 11;
            this.btnNuevo_626CV.Text = "Nuevo";
            this.btnNuevo_626CV.UseVisualStyleBackColor = true;
            this.btnNuevo_626CV.Click += new System.EventHandler(this.btnNuevo_626CV_Click);
            //
            // btnGuardar_626CV
            //
            this.btnGuardar_626CV.Location = new System.Drawing.Point(180, 495);
            this.btnGuardar_626CV.Name = "btnGuardar_626CV";
            this.btnGuardar_626CV.Size = new System.Drawing.Size(150, 35);
            this.btnGuardar_626CV.TabIndex = 12;
            this.btnGuardar_626CV.Text = "Guardar";
            this.btnGuardar_626CV.UseVisualStyleBackColor = true;
            this.btnGuardar_626CV.Click += new System.EventHandler(this.btnGuardar_626CV_Click);
            //
            // btnDesactivar_626CV
            //
            this.btnDesactivar_626CV.Location = new System.Drawing.Point(342, 495);
            this.btnDesactivar_626CV.Name = "btnDesactivar_626CV";
            this.btnDesactivar_626CV.Size = new System.Drawing.Size(150, 35);
            this.btnDesactivar_626CV.TabIndex = 13;
            this.btnDesactivar_626CV.Text = "Desactivar";
            this.btnDesactivar_626CV.UseVisualStyleBackColor = true;
            this.btnDesactivar_626CV.Click += new System.EventHandler(this.btnDesactivar_626CV_Click);
            //
            // btnVolver_626CV
            //
            this.btnVolver_626CV.Location = new System.Drawing.Point(596, 495);
            this.btnVolver_626CV.Name = "btnVolver_626CV";
            this.btnVolver_626CV.Size = new System.Drawing.Size(112, 35);
            this.btnVolver_626CV.TabIndex = 14;
            this.btnVolver_626CV.Text = "Volver";
            this.btnVolver_626CV.UseVisualStyleBackColor = true;
            this.btnVolver_626CV.Click += new System.EventHandler(this.btnVolver_626CV_Click);
            //
            // frmPacks
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(735, 555);
            this.Controls.Add(this.dgvPacks_626CV);
            this.Controls.Add(this.lblModo_626CV);
            this.Controls.Add(this.lblNombre_626CV);
            this.Controls.Add(this.txtNombre_626CV);
            this.Controls.Add(this.lblCantidadClases_626CV);
            this.Controls.Add(this.txtCantidadClases_626CV);
            this.Controls.Add(this.lblPrecioLista_626CV);
            this.Controls.Add(this.txtPrecioLista_626CV);
            this.Controls.Add(this.lblDescuento_626CV);
            this.Controls.Add(this.txtDescuento_626CV);
            this.Controls.Add(this.lblPrecioFinal_626CV);
            this.Controls.Add(this.btnNuevo_626CV);
            this.Controls.Add(this.btnGuardar_626CV);
            this.Controls.Add(this.btnDesactivar_626CV);
            this.Controls.Add(this.btnVolver_626CV);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "frmPacks";
            this.Text = "Packs de clases - Olimpic";
            ((System.ComponentModel.ISupportInitialize)(this.dgvPacks_626CV)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvPacks_626CV;
        private System.Windows.Forms.Label lblModo_626CV;
        private System.Windows.Forms.Label lblNombre_626CV;
        private System.Windows.Forms.TextBox txtNombre_626CV;
        private System.Windows.Forms.Label lblCantidadClases_626CV;
        private System.Windows.Forms.TextBox txtCantidadClases_626CV;
        private System.Windows.Forms.Label lblPrecioLista_626CV;
        private System.Windows.Forms.TextBox txtPrecioLista_626CV;
        private System.Windows.Forms.Label lblDescuento_626CV;
        private System.Windows.Forms.TextBox txtDescuento_626CV;
        private System.Windows.Forms.Label lblPrecioFinal_626CV;
        private System.Windows.Forms.Button btnNuevo_626CV;
        private System.Windows.Forms.Button btnGuardar_626CV;
        private System.Windows.Forms.Button btnDesactivar_626CV;
        private System.Windows.Forms.Button btnVolver_626CV;
    }

}