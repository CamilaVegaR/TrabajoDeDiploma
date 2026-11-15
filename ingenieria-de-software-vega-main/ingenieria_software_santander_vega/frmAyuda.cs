using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ingenieria_software_santander_vega
{

    public class frmAyuda : Form
    {
        private ListBox lstTemas_626CV;
        private RichTextBox rtbContenido_626CV;
        private Button btnCerrar_626CV;

        private static readonly List<TEMA_AYUDA> temas_626CV = TEMA_AYUDA.CargarTemas_626CV();

        public frmAyuda(string nombreFormulario_626CV)
        {
            ConstruirPantalla_626CV();

            foreach (TEMA_AYUDA tema_626CV in temas_626CV)
                lstTemas_626CV.Items.Add(tema_626CV);

        
            TEMA_AYUDA inicial_626CV = temas_626CV.FirstOrDefault(t => t.Formulario_626CV == nombreFormulario_626CV)
                                       ?? temas_626CV[0];
            lstTemas_626CV.SelectedItem = inicial_626CV;
        }

      
        public static void Mostrar_626CV(string nombreFormulario_626CV, IWin32Window ventana_626CV)
        {
            using (frmAyuda ayuda_626CV = new frmAyuda(nombreFormulario_626CV))
            {
                ayuda_626CV.ShowDialog(ventana_626CV);
            }
        }

        private void lstTemas_626CV_SelectedIndexChanged(object sender, EventArgs e)
        {
            TEMA_AYUDA tema_626CV = lstTemas_626CV.SelectedItem as TEMA_AYUDA;
            if (tema_626CV == null) return;

            rtbContenido_626CV.Clear();

            rtbContenido_626CV.SelectionFont = new Font(rtbContenido_626CV.Font.FontFamily, 14F, FontStyle.Bold);
            rtbContenido_626CV.AppendText(tema_626CV.Titulo_626CV + Environment.NewLine + Environment.NewLine);

            foreach (string linea_626CV in tema_626CV.Texto_626CV.Split('\n'))
            {
                
                bool esSubtitulo_626CV = linea_626CV.StartsWith("#");
                rtbContenido_626CV.SelectionFont = new Font(rtbContenido_626CV.Font, esSubtitulo_626CV ? FontStyle.Bold : FontStyle.Regular);
                rtbContenido_626CV.AppendText((esSubtitulo_626CV ? linea_626CV.Substring(1).Trim() : linea_626CV) + Environment.NewLine);
            }

            rtbContenido_626CV.SelectionStart = 0;
            rtbContenido_626CV.ScrollToCaret();
        }

        private void ConstruirPantalla_626CV()
        {
            this.Text = "Ayuda - Olimpic";
            this.Name = "frmAyuda";
            this.StartPosition = FormStartPosition.CenterParent;
            this.ClientSize = new Size(980, 600);
            this.MinimizeBox = false;
            this.ShowInTaskbar = false;
            this.Font = new Font("Microsoft Sans Serif", 9.5F);

            lstTemas_626CV = new ListBox();
            lstTemas_626CV.Dock = DockStyle.Left;
            lstTemas_626CV.Width = 290;
            lstTemas_626CV.IntegralHeight = false;
            lstTemas_626CV.DisplayMember = "Titulo_626CV";
            lstTemas_626CV.SelectedIndexChanged += lstTemas_626CV_SelectedIndexChanged;

            rtbContenido_626CV = new RichTextBox();
            rtbContenido_626CV.Dock = DockStyle.Fill;
            rtbContenido_626CV.ReadOnly = true;
            rtbContenido_626CV.BackColor = Color.White;
            rtbContenido_626CV.BorderStyle = BorderStyle.None;
            rtbContenido_626CV.TabStop = false;

            Panel panelContenido_626CV = new Panel();
            panelContenido_626CV.Dock = DockStyle.Fill;
            panelContenido_626CV.Padding = new Padding(18, 14, 14, 10);
            panelContenido_626CV.BackColor = Color.White;
            panelContenido_626CV.Controls.Add(rtbContenido_626CV);

            btnCerrar_626CV = new Button();
            btnCerrar_626CV.Text = "Cerrar";
            btnCerrar_626CV.Size = new Size(120, 34);
            btnCerrar_626CV.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            btnCerrar_626CV.DialogResult = DialogResult.Cancel;

            Panel panelBotones_626CV = new Panel();
            panelBotones_626CV.Dock = DockStyle.Bottom;
            panelBotones_626CV.Height = 52;
            panelBotones_626CV.Controls.Add(btnCerrar_626CV);
            btnCerrar_626CV.Location = new Point(980 - 120 - 16, 9);

            this.Controls.Add(panelContenido_626CV);
            this.Controls.Add(lstTemas_626CV);
            this.Controls.Add(panelBotones_626CV);
            this.CancelButton = btnCerrar_626CV;   
        }
    }


    public class TEMA_AYUDA
    {
        public string Formulario_626CV { get; set; }
        public string Titulo_626CV { get; set; }
        public string Texto_626CV { get; set; }

        public TEMA_AYUDA(string formulario_626CV, string titulo_626CV, string texto_626CV)
        {
            Formulario_626CV = formulario_626CV;
            Titulo_626CV = titulo_626CV;
            Texto_626CV = texto_626CV.Replace("\r\n", "\n").Trim();
        }

        public static List<TEMA_AYUDA> CargarTemas_626CV()
        {
            return new List<TEMA_AYUDA>
            {
                new TEMA_AYUDA("frmMenu", "Cómo usar el sistema", @"
Olimpic es el sistema de gestión del estudio de Pilates: reservas de clases, clientes, packs y compras.

# Ayuda
En cualquier pantalla puede apretar F1 para ver la ayuda de esa pantalla. En la lista de la izquierda puede elegir cualquier otro tema.

# Menú principal
Cada usuario ve solo las opciones para las que tiene permiso.
• Agendar Reserva: reservar una clase, cambiarle el horario o cancelarla.
• Compra de Clases: registrar la compra de un pack y configurar los packs.
• Reportes: reservas y ventas de un período, con exportación a PDF.
• Maestros: control de cambios de los packs.
• Admin: bitácora, usuarios, roles y permisos, idiomas y etiquetas.
• Usuario > Mi Cuenta: sus datos y su contraseña.

# Idioma
El desplegable ""Idioma"" del menú cambia el idioma de todas las pantallas. La elección queda guardada para su usuario.

# Salir
""Logout"" cierra la sesión y vuelve a la pantalla de ingreso. En las demás pantallas, ""Volver"" regresa a este menú."),

                new TEMA_AYUDA("frmIniciarSesion", "Iniciar sesión", @"
# Ingresar
1. Escriba su nombre de usuario y su contraseña.
2. Toque ""Login"".

# Si no puede ingresar
• Usuario o contraseña incorrectos: revise mayúsculas y minúsculas. Después de varios intentos fallidos la cuenta se bloquea y solo un administrador puede desbloquearla.
• Aviso de integridad de datos: la base fue modificada por fuera del sistema. Si usted no es administrador, no podrá ingresar hasta que el administrador revise la base. Avísele.

# Crear una cuenta
""Crear"" abre la pantalla para dar de alta un usuario nuevo."),

                new TEMA_AYUDA("frmAgendarReserva", "Agendar una reserva", @"
# Agendar
1. Escriba el DNI del cliente y toque ""Buscar"". Se muestran sus datos, su saldo de clases y sus reservas.
2. Si el cliente no existe, se abre el panel para registrarlo: complete nombre, apellido, teléfono, email y canal de contacto, y toque ""Registrar cliente"".
3. Elija el horario y la fecha. El sistema muestra el cupo disponible.
4. Toque ""Agendar"". La reserva descuenta una clase del saldo del cliente.

# Modificar el horario de una reserva
1. Seleccione la reserva en la grilla.
2. Elija el nuevo horario y la nueva fecha.
3. Toque ""Modificar horario"".

# Cancelar una reserva
Seleccione la reserva en la grilla y toque ""Cancelar reserva"".

# Mensajes frecuentes
• Sin saldo: el cliente no tiene clases disponibles. Registre una compra en Compra de Clases > Registrar Compra.
• Sin cupo: ese horario está completo en esa fecha. Elija otro.
• El día no coincide: la fecha elegida no cae en el día de la semana del horario."),

                new TEMA_AYUDA("frmCompras", "Registrar una compra", @"
# Registrar la compra de un pack
1. Escriba el DNI del cliente y toque ""Buscar"".
2. Si el cliente no existe, complete sus datos y toque ""Registrar cliente"".
3. Elija el pack. Se muestra el precio final con el descuento aplicado.
4. Toque ""Registrar compra"". Las clases del pack se suman al saldo del cliente.

# Historial
""Historial de compras"" muestra todas las compras del cliente buscado.

# Importante
Solo se pueden vender packs activos. Los packs se administran en Compra de Clases > Configurar Packs."),

                new TEMA_AYUDA("frmPacks", "Configurar packs", @"
# Crear un pack
1. Toque ""Nuevo"".
2. Complete nombre, cantidad de clases, precio de lista y descuento (de 0 a 100).
3. Toque ""Guardar"".

# Modificar un pack
1. Haga clic en el pack en la grilla: sus datos se cargan abajo.
2. Cambie lo que necesite y toque ""Guardar"".

# Desactivar un pack
Seleccione el pack y toque ""Desactivar"". El pack no se borra: deja de ofrecerse para la venta y queda en gris en la grilla.

# Importante
• No puede haber dos packs con el mismo nombre.
• Un pack desactivado no se puede modificar.
• Cada alta, modificación o desactivación queda registrada automáticamente en Maestros > Control de cambios de packs."),

                new TEMA_AYUDA("frmControlCambiosPack", "Control de cambios de packs", @"
Esta pantalla muestra todas las versiones que tuvo cada pack. Cada vez que un pack se crea, se modifica o se desactiva, el sistema guarda una fila nueva con la fecha y la hora.

# Cómo leer la grilla
• Cada fila es una versión de un pack.
• La columna ""Act."" vale 1 en la versión vigente de cada pack (en negrita) y 0 en las anteriores.

# Filtrar
1. Elija un código de pack, escriba o elija un nombre, y/o tilde una fecha inicial y una fecha final.
2. Toque ""Aplicar"".
""Limpiar"" quita los filtros y vuelve a mostrar todo.

# Volver a una versión anterior
1. Seleccione la fila de la versión que quiere recuperar (una con Act. en 0).
2. Toque ""Activar"" y confirme.
El pack vuelve a tener los datos de esa versión, que pasa a ser la vigente.

# Importante
• De esta pantalla nunca se borra nada.
• No se puede activar una versión si otro pack ya usa ese nombre."),

                new TEMA_AYUDA("frmReportes", "Reportes", @"
# Generar un reporte
1. Elija el reporte: reservas, ventas o global.
2. Elija las fechas ""Desde"" y ""Hasta"".
3. Toque ""Generar"". El resultado se muestra en pantalla, por secciones.

# Exportar a PDF
Con el reporte generado, toque ""Exportar a PDF"" y elija dónde guardarlo.

# Importante
La fecha ""Desde"" no puede ser posterior a la fecha ""Hasta""."),

                new TEMA_AYUDA("frmSerializacion", "Serialización", @"
Permite guardar datos del sistema en un archivo XML y volver a leerlos.

# Guardar en XML
1. Elija qué datos quiere guardar (packs o clientes).
2. Toque ""Serializar a XML"" y elija dónde guardar el archivo.

# Leer un XML
Toque ""Deserializar desde XML"" y elija el archivo. Los registros leídos se muestran en la grilla y el contenido del archivo, abajo."),

                new TEMA_AYUDA("frmBitacora", "Bitácora", @"
Muestra las actividades registradas en el sistema: ingresos y salidas, altas y cambios de usuarios, packs, compras, cambios de idioma, etc.

# Filtrar por usuario
Elija un usuario y toque ""Filtrar"". ""Limpiar Filtros"" vuelve a mostrar todo."),

                new TEMA_AYUDA("frmAdminUsuario", "Usuarios e historial", @"
# Modificar un usuario
Seleccione el usuario, cambie sus datos y toque ""Editar"".

# Eliminar y reactivar
""Eliminar"" da de baja al usuario sin borrarlo. ""Reactivar"" lo vuelve a habilitar.

# Historial
Cada cambio de un usuario queda guardado. Seleccione una versión anterior en el historial y toque ""Restaurar"" para volver a ese estado."),

                new TEMA_AYUDA("frmRolesPermisos", "Roles y permisos", @"
Un permiso (patente) habilita una opción del sistema. Un rol (familia) agrupa permisos y otros roles.

# Armar un rol
1. Toque ""Nuevo Rol"" y escriba su nombre.
2. Elija el rol, seleccione en ""Disponibles"" lo que quiere incluir y toque ""Agregar"". ""Quitar"" lo saca.

# Asignar un rol a un usuario
Elija el usuario y el rol, y toque ""Asignar Rol"". ""Desasignar Rol"" se lo quita.

# Importante
Los cambios se ven la próxima vez que ese usuario inicia sesión."),

                new TEMA_AYUDA("frmIdiomas", "Idiomas y traducciones", @"
# Agregar un idioma
Toque ""Nuevo Idioma"" y escriba su nombre.

# Traducir
1. Elija el idioma.
2. Escriba la traducción de cada texto en la grilla.
3. Toque ""Guardar"".
Los textos sin traducir se muestran en el idioma original."),

                new TEMA_AYUDA("frmEtiquetas", "Etiquetas", @"
Las etiquetas son los textos de las pantallas (botones, títulos, rótulos) que se pueden traducir. Se identifican por el formulario y la clave.

Aquí puede revisar y corregir las etiquetas. Las traducciones de cada una se cargan en Admin > Idiomas."),

                new TEMA_AYUDA("frmMiCuenta", "Mi cuenta", @"
# Cambiar sus datos
Modifique nombre, apellido o sexo y toque ""Editar"".

# Cambiar la contraseña
Escriba la nueva contraseña y toque ""Actualizar Contraseña"". La próxima vez ingrese con la nueva."),

                new TEMA_AYUDA("frmCrearCuenta", "Crear una cuenta", @"
1. Complete nombre, apellido, sexo, nombre de usuario y contraseña.
2. Toque ""Crear"".

# Importante
• El nombre de usuario no puede estar repetido.
• La cuenta nueva no tiene permisos hasta que un administrador le asigne un rol."),
            };
        }
    }
}