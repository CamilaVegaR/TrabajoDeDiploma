using Servicios;
using System;
using System.Windows.Forms;

namespace ingenieria_software_santander_vega.helpers
{
    public class NAVEGACION_HELPER
    {
        public static void Redirigir(Form formActual, Form formDestino, string permisoNecesario = null, bool cerrarActual = false)
        {
            ValidarPermiso(permisoNecesario);

            formDestino.Show();

            if (cerrarActual)
            {
                formActual.Close();
            }
            else
            {
                formActual.Hide();
            }
        }

        private static void ValidarPermiso(string permisoNecesario)
        {
            if (string.IsNullOrEmpty(permisoNecesario)) return;

            bool tienePermiso = SINGLETON_SESION.GetInstance().usuario.ValidarPermiso(permisoNecesario);

            if (!tienePermiso)
            {
                throw new UnauthorizedAccessException("No tiene permisos para acceder a esta pantalla.");
            }
        }
    }
}