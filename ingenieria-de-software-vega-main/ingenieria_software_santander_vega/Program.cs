using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ingenieria_software_santander_vega
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Antes del login: si la base no existe se crea sola con el script embebido
            if (!PrepararBaseDeDatos())
                return;

            Application.Run(new frmIniciarSesion());
        }

        private static bool PrepararBaseDeDatos()
        {
            BLL_BASE_DATOS bllBase = new BLL_BASE_DATOS();

            try
            {
                Cursor.Current = Cursors.WaitCursor;
                RESULTADO_PREPARACION_BD resultado = bllBase.PrepararBaseDeDatos_626CV();
                Cursor.Current = Cursors.Default;

                if (resultado != RESULTADO_PREPARACION_BD.YA_EXISTIA)
                {
                    MessageBox.Show(
                        "Primer inicio del sistema: se preparó la base de datos \"" + bllBase.NombreBase_626CV +
                        "\" en el servidor \"" + bllBase.Servidor_626CV + "\" con sus tablas y datos iniciales.",
                        "Olimpic - Instalación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                return true;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message, "Olimpic - No se puede iniciar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}
