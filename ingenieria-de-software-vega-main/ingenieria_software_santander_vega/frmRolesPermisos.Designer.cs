namespace ingenieria_software_santander_vega
{
    partial class frmRolesPermisos
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
            this.btnVolver = new System.Windows.Forms.Button();
            this.gbConfiguradorRelaciones = new System.Windows.Forms.GroupBox();
            this.lblSeleccionados = new System.Windows.Forms.Label();
            this.gbGestionUsuarios = new System.Windows.Forms.GroupBox();
            this.btnDesasignarRol = new System.Windows.Forms.Button();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.treeUsuariosRolesPermisos = new System.Windows.Forms.TreeView();
            this.btnAsignarRolUsuario = new System.Windows.Forms.Button();
            this.lstRoles = new System.Windows.Forms.ListBox();
            this.cbUsuarios = new System.Windows.Forms.ComboBox();
            this.lblDisponibles = new System.Windows.Forms.Label();
            this.lstPatentesFamiliasDisponibles = new System.Windows.Forms.ListBox();
            this.lstPatentesFamiliasSeleccionados = new System.Windows.Forms.ListBox();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.treeRolesPermisos = new System.Windows.Forms.TreeView();
            this.btnQuitar = new System.Windows.Forms.Button();
            this.gbExplorador = new System.Windows.Forms.GroupBox();
            this.btnNuevaFamilia = new System.Windows.Forms.Button();
            this.lstPermisos = new System.Windows.Forms.ListBox();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnNuevaPatente = new System.Windows.Forms.Button();
            this.gbConfiguradorRelaciones.SuspendLayout();
            this.gbGestionUsuarios.SuspendLayout();
            this.gbExplorador.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnVolver
            // 
            this.btnVolver.Location = new System.Drawing.Point(18, 658);
            this.btnVolver.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(112, 35);
            this.btnVolver.TabIndex = 16;
            this.btnVolver.Text = "Volver";
            this.btnVolver.UseVisualStyleBackColor = true;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // gbConfiguradorRelaciones
            // 
            this.gbConfiguradorRelaciones.Controls.Add(this.lblSeleccionados);
            this.gbConfiguradorRelaciones.Controls.Add(this.gbGestionUsuarios);
            this.gbConfiguradorRelaciones.Controls.Add(this.lblDisponibles);
            this.gbConfiguradorRelaciones.Controls.Add(this.lstPatentesFamiliasDisponibles);
            this.gbConfiguradorRelaciones.Controls.Add(this.lstPatentesFamiliasSeleccionados);
            this.gbConfiguradorRelaciones.Controls.Add(this.btnAgregar);
            this.gbConfiguradorRelaciones.Controls.Add(this.treeRolesPermisos);
            this.gbConfiguradorRelaciones.Controls.Add(this.btnQuitar);
            this.gbConfiguradorRelaciones.Location = new System.Drawing.Point(478, 18);
            this.gbConfiguradorRelaciones.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbConfiguradorRelaciones.Name = "gbConfiguradorRelaciones";
            this.gbConfiguradorRelaciones.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbConfiguradorRelaciones.Size = new System.Drawing.Size(1413, 631);
            this.gbConfiguradorRelaciones.TabIndex = 18;
            this.gbConfiguradorRelaciones.TabStop = false;
            this.gbConfiguradorRelaciones.Text = "Configurador Relaciones";
            // 
            // lblSeleccionados
            // 
            this.lblSeleccionados.AutoSize = true;
            this.lblSeleccionados.Location = new System.Drawing.Point(867, 29);
            this.lblSeleccionados.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSeleccionados.Name = "lblSeleccionados";
            this.lblSeleccionados.Size = new System.Drawing.Size(113, 20);
            this.lblSeleccionados.TabIndex = 14;
            this.lblSeleccionados.Text = "Seleccionados";
            // 
            // gbGestionUsuarios
            // 
            this.gbGestionUsuarios.Controls.Add(this.btnDesasignarRol);
            this.gbGestionUsuarios.Controls.Add(this.lblUsuario);
            this.gbGestionUsuarios.Controls.Add(this.treeUsuariosRolesPermisos);
            this.gbGestionUsuarios.Controls.Add(this.btnAsignarRolUsuario);
            this.gbGestionUsuarios.Controls.Add(this.lstRoles);
            this.gbGestionUsuarios.Controls.Add(this.cbUsuarios);
            this.gbGestionUsuarios.Location = new System.Drawing.Point(1063, 25);
            this.gbGestionUsuarios.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbGestionUsuarios.Name = "gbGestionUsuarios";
            this.gbGestionUsuarios.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbGestionUsuarios.Size = new System.Drawing.Size(352, 617);
            this.gbGestionUsuarios.TabIndex = 19;
            this.gbGestionUsuarios.TabStop = false;
            this.gbGestionUsuarios.Text = "Gestión Usuarios";
            // 
            // btnDesasignarRol
            // 
            this.btnDesasignarRol.Location = new System.Drawing.Point(190, 372);
            this.btnDesasignarRol.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnDesasignarRol.Name = "btnDesasignarRol";
            this.btnDesasignarRol.Size = new System.Drawing.Size(159, 35);
            this.btnDesasignarRol.TabIndex = 15;
            this.btnDesasignarRol.Text = "Desasignar Rol";
            this.btnDesasignarRol.UseVisualStyleBackColor = true;
            this.btnDesasignarRol.Click += new System.EventHandler(this.btnDesasignarRol_Click);
            // 
            // lblUsuario
            // 
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.Location = new System.Drawing.Point(24, 24);
            this.lblUsuario.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(64, 20);
            this.lblUsuario.TabIndex = 14;
            this.lblUsuario.Text = "Usuario";
            // 
            // treeUsuariosRolesPermisos
            // 
            this.treeUsuariosRolesPermisos.Location = new System.Drawing.Point(25, 96);
            this.treeUsuariosRolesPermisos.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.treeUsuariosRolesPermisos.Name = "treeUsuariosRolesPermisos";
            this.treeUsuariosRolesPermisos.Size = new System.Drawing.Size(319, 266);
            this.treeUsuariosRolesPermisos.TabIndex = 5;
            // 
            // btnAsignarRolUsuario
            // 
            this.btnAsignarRolUsuario.Location = new System.Drawing.Point(28, 372);
            this.btnAsignarRolUsuario.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnAsignarRolUsuario.Name = "btnAsignarRolUsuario";
            this.btnAsignarRolUsuario.Size = new System.Drawing.Size(159, 35);
            this.btnAsignarRolUsuario.TabIndex = 6;
            this.btnAsignarRolUsuario.Text = "Asignar Rol";
            this.btnAsignarRolUsuario.UseVisualStyleBackColor = true;
            this.btnAsignarRolUsuario.Click += new System.EventHandler(this.btnAsignarRolUsuario_Click);
            // 
            // lstRoles
            // 
            this.lstRoles.FormattingEnabled = true;
            this.lstRoles.ItemHeight = 20;
            this.lstRoles.Location = new System.Drawing.Point(28, 417);
            this.lstRoles.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lstRoles.Name = "lstRoles";
            this.lstRoles.Size = new System.Drawing.Size(319, 184);
            this.lstRoles.TabIndex = 7;
            // 
            // cbUsuarios
            // 
            this.cbUsuarios.FormattingEnabled = true;
            this.cbUsuarios.Location = new System.Drawing.Point(23, 54);
            this.cbUsuarios.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbUsuarios.Name = "cbUsuarios";
            this.cbUsuarios.Size = new System.Drawing.Size(319, 28);
            this.cbUsuarios.TabIndex = 8;
            this.cbUsuarios.SelectedIndexChanged += new System.EventHandler(this.cbUsuarios_SelectedIndexChanged);
            // 
            // lblDisponibles
            // 
            this.lblDisponibles.AutoSize = true;
            this.lblDisponibles.Location = new System.Drawing.Point(454, 25);
            this.lblDisponibles.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDisponibles.Name = "lblDisponibles";
            this.lblDisponibles.Size = new System.Drawing.Size(91, 20);
            this.lblDisponibles.TabIndex = 13;
            this.lblDisponibles.Text = "Disponibles";
            // 
            // lstPatentesFamiliasDisponibles
            // 
            this.lstPatentesFamiliasDisponibles.FormattingEnabled = true;
            this.lstPatentesFamiliasDisponibles.ItemHeight = 20;
            this.lstPatentesFamiliasDisponibles.Location = new System.Drawing.Point(459, 54);
            this.lstPatentesFamiliasDisponibles.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lstPatentesFamiliasDisponibles.Name = "lstPatentesFamiliasDisponibles";
            this.lstPatentesFamiliasDisponibles.Size = new System.Drawing.Size(226, 544);
            this.lstPatentesFamiliasDisponibles.TabIndex = 1;
            // 
            // lstPatentesFamiliasSeleccionados
            // 
            this.lstPatentesFamiliasSeleccionados.FormattingEnabled = true;
            this.lstPatentesFamiliasSeleccionados.ItemHeight = 20;
            this.lstPatentesFamiliasSeleccionados.Location = new System.Drawing.Point(813, 56);
            this.lstPatentesFamiliasSeleccionados.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lstPatentesFamiliasSeleccionados.Name = "lstPatentesFamiliasSeleccionados";
            this.lstPatentesFamiliasSeleccionados.Size = new System.Drawing.Size(226, 544);
            this.lstPatentesFamiliasSeleccionados.TabIndex = 2;
            // 
            // btnAgregar
            // 
            this.btnAgregar.Location = new System.Drawing.Point(693, 194);
            this.btnAgregar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(112, 35);
            this.btnAgregar.TabIndex = 3;
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.UseVisualStyleBackColor = true;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // treeRolesPermisos
            // 
            this.treeRolesPermisos.Location = new System.Drawing.Point(9, 54);
            this.treeRolesPermisos.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.treeRolesPermisos.Name = "treeRolesPermisos";
            this.treeRolesPermisos.Size = new System.Drawing.Size(402, 547);
            this.treeRolesPermisos.TabIndex = 12;
            this.treeRolesPermisos.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeRolesPermisos_AfterSelect);
            // 
            // btnQuitar
            // 
            this.btnQuitar.Location = new System.Drawing.Point(693, 265);
            this.btnQuitar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnQuitar.Name = "btnQuitar";
            this.btnQuitar.Size = new System.Drawing.Size(112, 35);
            this.btnQuitar.TabIndex = 4;
            this.btnQuitar.Text = "Quitar";
            this.btnQuitar.UseVisualStyleBackColor = true;
            this.btnQuitar.Click += new System.EventHandler(this.btnQuitar_Click);
            // 
            // gbExplorador
            // 
            this.gbExplorador.Controls.Add(this.btnNuevaFamilia);
            this.gbExplorador.Controls.Add(this.lstPermisos);
            this.gbExplorador.Controls.Add(this.btnEliminar);
            this.gbExplorador.Controls.Add(this.btnNuevaPatente);
            this.gbExplorador.Location = new System.Drawing.Point(18, 18);
            this.gbExplorador.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbExplorador.Name = "gbExplorador";
            this.gbExplorador.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbExplorador.Size = new System.Drawing.Size(339, 600);
            this.gbExplorador.TabIndex = 17;
            this.gbExplorador.TabStop = false;
            this.gbExplorador.Text = "Explorador de Estructura (Roles y Patentes)";
            // 
            // btnNuevaFamilia
            // 
            this.btnNuevaFamilia.Location = new System.Drawing.Point(9, 449);
            this.btnNuevaFamilia.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnNuevaFamilia.Name = "btnNuevaFamilia";
            this.btnNuevaFamilia.Size = new System.Drawing.Size(321, 35);
            this.btnNuevaFamilia.TabIndex = 17;
            this.btnNuevaFamilia.Text = "Nuevo Rol";
            this.btnNuevaFamilia.UseVisualStyleBackColor = true;
            this.btnNuevaFamilia.Click += new System.EventHandler(this.btnNuevaFamilia_Click);
            // 
            // lstPermisos
            // 
            this.lstPermisos.FormattingEnabled = true;
            this.lstPermisos.ItemHeight = 20;
            this.lstPermisos.Location = new System.Drawing.Point(9, 54);
            this.lstPermisos.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lstPermisos.Name = "lstPermisos";
            this.lstPermisos.Size = new System.Drawing.Size(319, 384);
            this.lstPermisos.TabIndex = 16;
            // 
            // btnEliminar
            // 
            this.btnEliminar.Location = new System.Drawing.Point(9, 538);
            this.btnEliminar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(321, 35);
            this.btnEliminar.TabIndex = 15;
            this.btnEliminar.Text = "Eliminar Seleccionado";
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // btnNuevaPatente
            // 
            this.btnNuevaPatente.Location = new System.Drawing.Point(9, 494);
            this.btnNuevaPatente.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnNuevaPatente.Name = "btnNuevaPatente";
            this.btnNuevaPatente.Size = new System.Drawing.Size(321, 35);
            this.btnNuevaPatente.TabIndex = 14;
            this.btnNuevaPatente.Text = "Nuevo Permiso";
            this.btnNuevaPatente.UseVisualStyleBackColor = true;
            this.btnNuevaPatente.Click += new System.EventHandler(this.btnNuevaPatente_Click);
            // 
            // frmRolesPermisos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1924, 706);
            this.Controls.Add(this.btnVolver);
            this.Controls.Add(this.gbConfiguradorRelaciones);
            this.Controls.Add(this.gbExplorador);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "frmRolesPermisos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmRolesPermisos";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmRolesPermisos_Load);
            this.gbConfiguradorRelaciones.ResumeLayout(false);
            this.gbConfiguradorRelaciones.PerformLayout();
            this.gbGestionUsuarios.ResumeLayout(false);
            this.gbGestionUsuarios.PerformLayout();
            this.gbExplorador.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.GroupBox gbConfiguradorRelaciones;
        private System.Windows.Forms.Label lblSeleccionados;
        private System.Windows.Forms.Label lblDisponibles;
        private System.Windows.Forms.ListBox lstPatentesFamiliasDisponibles;
        private System.Windows.Forms.ListBox lstPatentesFamiliasSeleccionados;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.TreeView treeRolesPermisos;
        private System.Windows.Forms.Button btnQuitar;
        private System.Windows.Forms.GroupBox gbExplorador;
        private System.Windows.Forms.Button btnNuevaFamilia;
        private System.Windows.Forms.ListBox lstPermisos;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnNuevaPatente;
        private System.Windows.Forms.GroupBox gbGestionUsuarios;
        private System.Windows.Forms.Button btnDesasignarRol;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.TreeView treeUsuariosRolesPermisos;
        private System.Windows.Forms.Button btnAsignarRolUsuario;
        private System.Windows.Forms.ListBox lstRoles;
        private System.Windows.Forms.ComboBox cbUsuarios;
    }
}