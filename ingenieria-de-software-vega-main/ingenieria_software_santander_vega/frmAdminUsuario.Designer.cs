namespace ingenieria_software_santander_vega
{
    partial class frmAdminUsuario
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
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnRestaurar = new System.Windows.Forms.Button();
            this.lblIdioma = new System.Windows.Forms.Label();
            this.txtIdioma = new System.Windows.Forms.TextBox();
            this.btnVolver = new System.Windows.Forms.Button();
            this.btnEditar = new System.Windows.Forms.Button();
            this.lblSexo = new System.Windows.Forms.Label();
            this.cbSexo = new System.Windows.Forms.ComboBox();
            this.lblApellido = new System.Windows.Forms.Label();
            this.txtApellido = new System.Windows.Forms.TextBox();
            this.lblNombre = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.lblNombreUsuario = new System.Windows.Forms.Label();
            this.txtNombreUsuario = new System.Windows.Forms.TextBox();
            this.lblHistorial = new System.Windows.Forms.Label();
            this.dgvHistorialUsuario = new System.Windows.Forms.DataGridView();
            this.lblCbUsuario = new System.Windows.Forms.Label();
            this.cbUsuario = new System.Windows.Forms.ComboBox();
            this.lblAtributos = new System.Windows.Forms.Label();
            this.lstAtributos = new System.Windows.Forms.ListBox();
            this.btnReactivar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistorialUsuario)).BeginInit();
            this.SuspendLayout();
            // 
            // btnEliminar
            // 
            this.btnEliminar.Location = new System.Drawing.Point(185, 225);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(95, 23);
            this.btnEliminar.TabIndex = 74;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // btnRestaurar
            // 
            this.btnRestaurar.Location = new System.Drawing.Point(490, 407);
            this.btnRestaurar.Name = "btnRestaurar";
            this.btnRestaurar.Size = new System.Drawing.Size(95, 23);
            this.btnRestaurar.TabIndex = 73;
            this.btnRestaurar.Text = "Restaurar";
            this.btnRestaurar.UseVisualStyleBackColor = true;
            this.btnRestaurar.Click += new System.EventHandler(this.btnRestaurar_Click);
            // 
            // lblIdioma
            // 
            this.lblIdioma.AutoSize = true;
            this.lblIdioma.Location = new System.Drawing.Point(172, 141);
            this.lblIdioma.Name = "lblIdioma";
            this.lblIdioma.Size = new System.Drawing.Size(38, 13);
            this.lblIdioma.TabIndex = 72;
            this.lblIdioma.Text = "Idioma";
            // 
            // txtIdioma
            // 
            this.txtIdioma.Enabled = false;
            this.txtIdioma.Location = new System.Drawing.Point(175, 157);
            this.txtIdioma.Name = "txtIdioma";
            this.txtIdioma.Size = new System.Drawing.Size(121, 20);
            this.txtIdioma.TabIndex = 71;
            // 
            // btnVolver
            // 
            this.btnVolver.Location = new System.Drawing.Point(15, 407);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(100, 23);
            this.btnVolver.TabIndex = 70;
            this.btnVolver.Text = "Volver";
            this.btnVolver.UseVisualStyleBackColor = true;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // btnEditar
            // 
            this.btnEditar.Location = new System.Drawing.Point(15, 225);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(95, 23);
            this.btnEditar.TabIndex = 69;
            this.btnEditar.Text = "Editar";
            this.btnEditar.UseVisualStyleBackColor = true;
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            // 
            // lblSexo
            // 
            this.lblSexo.AutoSize = true;
            this.lblSexo.Location = new System.Drawing.Point(326, 75);
            this.lblSexo.Name = "lblSexo";
            this.lblSexo.Size = new System.Drawing.Size(31, 13);
            this.lblSexo.TabIndex = 66;
            this.lblSexo.Text = "Sexo";
            // 
            // cbSexo
            // 
            this.cbSexo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSexo.FormattingEnabled = true;
            this.cbSexo.Location = new System.Drawing.Point(329, 94);
            this.cbSexo.Name = "cbSexo";
            this.cbSexo.Size = new System.Drawing.Size(121, 21);
            this.cbSexo.TabIndex = 65;
            // 
            // lblApellido
            // 
            this.lblApellido.AutoSize = true;
            this.lblApellido.Location = new System.Drawing.Point(172, 78);
            this.lblApellido.Name = "lblApellido";
            this.lblApellido.Size = new System.Drawing.Size(44, 13);
            this.lblApellido.TabIndex = 62;
            this.lblApellido.Text = "Apellido";
            // 
            // txtApellido
            // 
            this.txtApellido.Location = new System.Drawing.Point(175, 94);
            this.txtApellido.Name = "txtApellido";
            this.txtApellido.Size = new System.Drawing.Size(121, 20);
            this.txtApellido.TabIndex = 61;
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.Location = new System.Drawing.Point(12, 78);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(44, 13);
            this.lblNombre.TabIndex = 60;
            this.lblNombre.Text = "Nombre";
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(15, 94);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(121, 20);
            this.txtNombre.TabIndex = 59;
            // 
            // lblNombreUsuario
            // 
            this.lblNombreUsuario.AutoSize = true;
            this.lblNombreUsuario.Location = new System.Drawing.Point(12, 141);
            this.lblNombreUsuario.Name = "lblNombreUsuario";
            this.lblNombreUsuario.Size = new System.Drawing.Size(98, 13);
            this.lblNombreUsuario.TabIndex = 58;
            this.lblNombreUsuario.Text = "Nombre de Usuario";
            // 
            // txtNombreUsuario
            // 
            this.txtNombreUsuario.Location = new System.Drawing.Point(15, 157);
            this.txtNombreUsuario.Name = "txtNombreUsuario";
            this.txtNombreUsuario.Size = new System.Drawing.Size(121, 20);
            this.txtNombreUsuario.TabIndex = 57;
            // 
            // lblHistorial
            // 
            this.lblHistorial.AutoSize = true;
            this.lblHistorial.Location = new System.Drawing.Point(487, 9);
            this.lblHistorial.Name = "lblHistorial";
            this.lblHistorial.Size = new System.Drawing.Size(44, 13);
            this.lblHistorial.TabIndex = 3;
            this.lblHistorial.Text = "Historial";
            // 
            // dgvHistorialUsuario
            // 
            this.dgvHistorialUsuario.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHistorialUsuario.Location = new System.Drawing.Point(490, 25);
            this.dgvHistorialUsuario.MultiSelect = false;
            this.dgvHistorialUsuario.Name = "dgvHistorialUsuario";
            this.dgvHistorialUsuario.ReadOnly = true;
            this.dgvHistorialUsuario.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvHistorialUsuario.Size = new System.Drawing.Size(753, 183);
            this.dgvHistorialUsuario.TabIndex = 2;
            this.dgvHistorialUsuario.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvHistorialUsuario_CellContentClick);
            this.dgvHistorialUsuario.SelectionChanged += new System.EventHandler(this.dgvHistorialUsuario_SelectionChanged);
            // 
            // lblCbUsuario
            // 
            this.lblCbUsuario.AutoSize = true;
            this.lblCbUsuario.Location = new System.Drawing.Point(12, 9);
            this.lblCbUsuario.Name = "lblCbUsuario";
            this.lblCbUsuario.Size = new System.Drawing.Size(48, 13);
            this.lblCbUsuario.TabIndex = 1;
            this.lblCbUsuario.Text = "Usuarios";
            // 
            // cbUsuario
            // 
            this.cbUsuario.FormattingEnabled = true;
            this.cbUsuario.Location = new System.Drawing.Point(15, 25);
            this.cbUsuario.Name = "cbUsuario";
            this.cbUsuario.Size = new System.Drawing.Size(121, 21);
            this.cbUsuario.TabIndex = 0;
            this.cbUsuario.SelectedIndexChanged += new System.EventHandler(this.cbUsuario_SelectedIndexChanged);
            // 
            // lblAtributos
            // 
            this.lblAtributos.AutoSize = true;
            this.lblAtributos.Location = new System.Drawing.Point(487, 225);
            this.lblAtributos.Name = "lblAtributos";
            this.lblAtributos.Size = new System.Drawing.Size(48, 13);
            this.lblAtributos.TabIndex = 76;
            this.lblAtributos.Text = "Atributos";
            // 
            // lstAtributos
            // 
            this.lstAtributos.FormattingEnabled = true;
            this.lstAtributos.Location = new System.Drawing.Point(490, 241);
            this.lstAtributos.Name = "lstAtributos";
            this.lstAtributos.Size = new System.Drawing.Size(392, 160);
            this.lstAtributos.TabIndex = 77;
            // 
            // btnReactivar
            // 
            this.btnReactivar.Location = new System.Drawing.Point(343, 225);
            this.btnReactivar.Name = "btnReactivar";
            this.btnReactivar.Size = new System.Drawing.Size(95, 23);
            this.btnReactivar.TabIndex = 78;
            this.btnReactivar.Text = "Reactivar";
            this.btnReactivar.UseVisualStyleBackColor = true;
            this.btnReactivar.Click += new System.EventHandler(this.btnReactivar_Click);
            // 
            // frmAdminUsuario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1255, 447);
            this.Controls.Add(this.btnReactivar);
            this.Controls.Add(this.lstAtributos);
            this.Controls.Add(this.lblAtributos);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.btnRestaurar);
            this.Controls.Add(this.lblIdioma);
            this.Controls.Add(this.txtIdioma);
            this.Controls.Add(this.btnVolver);
            this.Controls.Add(this.btnEditar);
            this.Controls.Add(this.lblSexo);
            this.Controls.Add(this.cbSexo);
            this.Controls.Add(this.lblApellido);
            this.Controls.Add(this.txtApellido);
            this.Controls.Add(this.lblNombre);
            this.Controls.Add(this.txtNombre);
            this.Controls.Add(this.lblNombreUsuario);
            this.Controls.Add(this.txtNombreUsuario);
            this.Controls.Add(this.lblHistorial);
            this.Controls.Add(this.dgvHistorialUsuario);
            this.Controls.Add(this.lblCbUsuario);
            this.Controls.Add(this.cbUsuario);
            this.Name = "frmAdminUsuario";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmHistorialUsuario";
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistorialUsuario)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbUsuario;
        private System.Windows.Forms.Label lblCbUsuario;
        private System.Windows.Forms.DataGridView dgvHistorialUsuario;
        private System.Windows.Forms.Label lblHistorial;
        private System.Windows.Forms.Label lblSexo;
        private System.Windows.Forms.ComboBox cbSexo;
        private System.Windows.Forms.Label lblApellido;
        private System.Windows.Forms.TextBox txtApellido;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label lblNombreUsuario;
        private System.Windows.Forms.TextBox txtNombreUsuario;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.TextBox txtIdioma;
        private System.Windows.Forms.Label lblIdioma;
        private System.Windows.Forms.Button btnRestaurar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Label lblAtributos;
        private System.Windows.Forms.ListBox lstAtributos;
        private System.Windows.Forms.Button btnReactivar;
    }
}