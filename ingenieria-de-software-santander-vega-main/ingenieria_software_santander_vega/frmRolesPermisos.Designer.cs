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
            this.gbGestionUsuarios = new System.Windows.Forms.GroupBox();
            this.btnDesasignarRol = new System.Windows.Forms.Button();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.treeUsuariosRolesPermisos = new System.Windows.Forms.TreeView();
            this.btnAsignarRolUsuario = new System.Windows.Forms.Button();
            this.lstRoles = new System.Windows.Forms.ListBox();
            this.cbUsuarios = new System.Windows.Forms.ComboBox();
            this.gbConfiguradorRelaciones = new System.Windows.Forms.GroupBox();
            this.lblSeleccionados = new System.Windows.Forms.Label();
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
            this.gbGestionUsuarios.SuspendLayout();
            this.gbConfiguradorRelaciones.SuspendLayout();
            this.gbExplorador.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnVolver
            // 
            this.btnVolver.Location = new System.Drawing.Point(12, 428);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(75, 23);
            this.btnVolver.TabIndex = 16;
            this.btnVolver.Text = "Volver";
            this.btnVolver.UseVisualStyleBackColor = true;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // gbGestionUsuarios
            // 
            this.gbGestionUsuarios.Controls.Add(this.btnDesasignarRol);
            this.gbGestionUsuarios.Controls.Add(this.lblUsuario);
            this.gbGestionUsuarios.Controls.Add(this.treeUsuariosRolesPermisos);
            this.gbGestionUsuarios.Controls.Add(this.btnAsignarRolUsuario);
            this.gbGestionUsuarios.Controls.Add(this.lstRoles);
            this.gbGestionUsuarios.Controls.Add(this.cbUsuarios);
            this.gbGestionUsuarios.Location = new System.Drawing.Point(1066, 12);
            this.gbGestionUsuarios.Name = "gbGestionUsuarios";
            this.gbGestionUsuarios.Size = new System.Drawing.Size(251, 410);
            this.gbGestionUsuarios.TabIndex = 19;
            this.gbGestionUsuarios.TabStop = false;
            this.gbGestionUsuarios.Text = "Gestión Usuarios";
            // 
            // btnDesasignarRol
            // 
            this.btnDesasignarRol.Location = new System.Drawing.Point(127, 242);
            this.btnDesasignarRol.Name = "btnDesasignarRol";
            this.btnDesasignarRol.Size = new System.Drawing.Size(106, 23);
            this.btnDesasignarRol.TabIndex = 15;
            this.btnDesasignarRol.Text = "Desasignar Rol";
            this.btnDesasignarRol.UseVisualStyleBackColor = true;
            this.btnDesasignarRol.Click += new System.EventHandler(this.btnDesasignarRol_Click);
            // 
            // lblUsuario
            // 
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.Location = new System.Drawing.Point(16, 19);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(43, 13);
            this.lblUsuario.TabIndex = 14;
            this.lblUsuario.Text = "Usuario";
            // 
            // treeUsuariosRolesPermisos
            // 
            this.treeUsuariosRolesPermisos.Location = new System.Drawing.Point(19, 62);
            this.treeUsuariosRolesPermisos.Name = "treeUsuariosRolesPermisos";
            this.treeUsuariosRolesPermisos.Size = new System.Drawing.Size(214, 174);
            this.treeUsuariosRolesPermisos.TabIndex = 5;
            // 
            // btnAsignarRolUsuario
            // 
            this.btnAsignarRolUsuario.Location = new System.Drawing.Point(19, 242);
            this.btnAsignarRolUsuario.Name = "btnAsignarRolUsuario";
            this.btnAsignarRolUsuario.Size = new System.Drawing.Size(106, 23);
            this.btnAsignarRolUsuario.TabIndex = 6;
            this.btnAsignarRolUsuario.Text = "Asignar Rol";
            this.btnAsignarRolUsuario.UseVisualStyleBackColor = true;
            this.btnAsignarRolUsuario.Click += new System.EventHandler(this.btnAsignarRolUsuario_Click);
            // 
            // lstRoles
            // 
            this.lstRoles.FormattingEnabled = true;
            this.lstRoles.Location = new System.Drawing.Point(19, 271);
            this.lstRoles.Name = "lstRoles";
            this.lstRoles.Size = new System.Drawing.Size(214, 121);
            this.lstRoles.TabIndex = 7;
            // 
            // cbUsuarios
            // 
            this.cbUsuarios.FormattingEnabled = true;
            this.cbUsuarios.Location = new System.Drawing.Point(19, 35);
            this.cbUsuarios.Name = "cbUsuarios";
            this.cbUsuarios.Size = new System.Drawing.Size(214, 21);
            this.cbUsuarios.TabIndex = 8;
            this.cbUsuarios.SelectedIndexChanged += new System.EventHandler(this.cbUsuarios_SelectedIndexChanged);
            // 
            // gbConfiguradorRelaciones
            // 
            this.gbConfiguradorRelaciones.Controls.Add(this.lblSeleccionados);
            this.gbConfiguradorRelaciones.Controls.Add(this.lblDisponibles);
            this.gbConfiguradorRelaciones.Controls.Add(this.lstPatentesFamiliasDisponibles);
            this.gbConfiguradorRelaciones.Controls.Add(this.lstPatentesFamiliasSeleccionados);
            this.gbConfiguradorRelaciones.Controls.Add(this.btnAgregar);
            this.gbConfiguradorRelaciones.Controls.Add(this.treeRolesPermisos);
            this.gbConfiguradorRelaciones.Controls.Add(this.btnQuitar);
            this.gbConfiguradorRelaciones.Location = new System.Drawing.Point(319, 12);
            this.gbConfiguradorRelaciones.Name = "gbConfiguradorRelaciones";
            this.gbConfiguradorRelaciones.Size = new System.Drawing.Size(741, 410);
            this.gbConfiguradorRelaciones.TabIndex = 18;
            this.gbConfiguradorRelaciones.TabStop = false;
            this.gbConfiguradorRelaciones.Text = "Configurador Relaciones";
            // 
            // lblSeleccionados
            // 
            this.lblSeleccionados.AutoSize = true;
            this.lblSeleccionados.Location = new System.Drawing.Point(578, 19);
            this.lblSeleccionados.Name = "lblSeleccionados";
            this.lblSeleccionados.Size = new System.Drawing.Size(77, 13);
            this.lblSeleccionados.TabIndex = 14;
            this.lblSeleccionados.Text = "Seleccionados";
            // 
            // lblDisponibles
            // 
            this.lblDisponibles.AutoSize = true;
            this.lblDisponibles.Location = new System.Drawing.Point(303, 16);
            this.lblDisponibles.Name = "lblDisponibles";
            this.lblDisponibles.Size = new System.Drawing.Size(61, 13);
            this.lblDisponibles.TabIndex = 13;
            this.lblDisponibles.Text = "Disponibles";
            // 
            // lstPatentesFamiliasDisponibles
            // 
            this.lstPatentesFamiliasDisponibles.FormattingEnabled = true;
            this.lstPatentesFamiliasDisponibles.Location = new System.Drawing.Point(306, 35);
            this.lstPatentesFamiliasDisponibles.Name = "lstPatentesFamiliasDisponibles";
            this.lstPatentesFamiliasDisponibles.Size = new System.Drawing.Size(152, 355);
            this.lstPatentesFamiliasDisponibles.TabIndex = 1;
            // 
            // lstPatentesFamiliasSeleccionados
            // 
            this.lstPatentesFamiliasSeleccionados.FormattingEnabled = true;
            this.lstPatentesFamiliasSeleccionados.Location = new System.Drawing.Point(581, 35);
            this.lstPatentesFamiliasSeleccionados.Name = "lstPatentesFamiliasSeleccionados";
            this.lstPatentesFamiliasSeleccionados.Size = new System.Drawing.Size(152, 355);
            this.lstPatentesFamiliasSeleccionados.TabIndex = 2;
            // 
            // btnAgregar
            // 
            this.btnAgregar.Location = new System.Drawing.Point(484, 176);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(75, 23);
            this.btnAgregar.TabIndex = 3;
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.UseVisualStyleBackColor = true;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // treeRolesPermisos
            // 
            this.treeRolesPermisos.Location = new System.Drawing.Point(6, 35);
            this.treeRolesPermisos.Name = "treeRolesPermisos";
            this.treeRolesPermisos.Size = new System.Drawing.Size(269, 357);
            this.treeRolesPermisos.TabIndex = 12;
            this.treeRolesPermisos.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeRolesPermisos_AfterSelect);
            // 
            // btnQuitar
            // 
            this.btnQuitar.Location = new System.Drawing.Point(484, 230);
            this.btnQuitar.Name = "btnQuitar";
            this.btnQuitar.Size = new System.Drawing.Size(75, 23);
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
            this.gbExplorador.Location = new System.Drawing.Point(12, 12);
            this.gbExplorador.Name = "gbExplorador";
            this.gbExplorador.Size = new System.Drawing.Size(226, 390);
            this.gbExplorador.TabIndex = 17;
            this.gbExplorador.TabStop = false;
            this.gbExplorador.Text = "Explorador de Estructura (Roles y Patentes)";
            // 
            // btnNuevaFamilia
            // 
            this.btnNuevaFamilia.Location = new System.Drawing.Point(6, 292);
            this.btnNuevaFamilia.Name = "btnNuevaFamilia";
            this.btnNuevaFamilia.Size = new System.Drawing.Size(214, 23);
            this.btnNuevaFamilia.TabIndex = 17;
            this.btnNuevaFamilia.Text = "Nuevo Rol";
            this.btnNuevaFamilia.UseVisualStyleBackColor = true;
            this.btnNuevaFamilia.Click += new System.EventHandler(this.btnNuevaFamilia_Click);
            // 
            // lstPermisos
            // 
            this.lstPermisos.FormattingEnabled = true;
            this.lstPermisos.Location = new System.Drawing.Point(6, 35);
            this.lstPermisos.Name = "lstPermisos";
            this.lstPermisos.Size = new System.Drawing.Size(214, 251);
            this.lstPermisos.TabIndex = 16;
            // 
            // btnEliminar
            // 
            this.btnEliminar.Location = new System.Drawing.Point(6, 350);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(214, 23);
            this.btnEliminar.TabIndex = 15;
            this.btnEliminar.Text = "Eliminar Seleccionado";
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // btnNuevaPatente
            // 
            this.btnNuevaPatente.Location = new System.Drawing.Point(6, 321);
            this.btnNuevaPatente.Name = "btnNuevaPatente";
            this.btnNuevaPatente.Size = new System.Drawing.Size(214, 23);
            this.btnNuevaPatente.TabIndex = 14;
            this.btnNuevaPatente.Text = "Nuevo Permiso";
            this.btnNuevaPatente.UseVisualStyleBackColor = true;
            this.btnNuevaPatente.Click += new System.EventHandler(this.btnNuevaPatente_Click);
            // 
            // frmRolesPermisos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1330, 459);
            this.Controls.Add(this.btnVolver);
            this.Controls.Add(this.gbGestionUsuarios);
            this.Controls.Add(this.gbConfiguradorRelaciones);
            this.Controls.Add(this.gbExplorador);
            this.Name = "frmRolesPermisos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmRolesPermisos";
            this.Load += new System.EventHandler(this.frmRolesPermisos_Load);
            this.gbGestionUsuarios.ResumeLayout(false);
            this.gbGestionUsuarios.PerformLayout();
            this.gbConfiguradorRelaciones.ResumeLayout(false);
            this.gbConfiguradorRelaciones.PerformLayout();
            this.gbExplorador.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.GroupBox gbGestionUsuarios;
        private System.Windows.Forms.Button btnDesasignarRol;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.TreeView treeUsuariosRolesPermisos;
        private System.Windows.Forms.Button btnAsignarRolUsuario;
        private System.Windows.Forms.ListBox lstRoles;
        private System.Windows.Forms.ComboBox cbUsuarios;
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
    }
}