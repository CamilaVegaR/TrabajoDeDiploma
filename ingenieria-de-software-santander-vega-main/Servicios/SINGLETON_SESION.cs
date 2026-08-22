using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    public class SINGLETON_SESION
    {
        private static SINGLETON_SESION _instance = null;

        private static readonly object _padlock = new object();

        public BE.USUARIO usuario { get; set; }
        public DateTime FechaLogin { get; set; }

        private SINGLETON_SESION() { }

        public static SINGLETON_SESION GetInstance()
        {
            lock (_padlock)
            {
                if (_instance == null)
                {
                    _instance = new SINGLETON_SESION();
                }
                return _instance;
            }
        }

        public static bool IsLoggedIn()
        {
            lock (_padlock)
            {
                return _instance != null && _instance.usuario != null;
            }
        }

        public static void login(USUARIO pUsuario)
        {
            lock (_padlock)
            {
                if (_instance == null)
                {
                    _instance = new SINGLETON_SESION();
                    _instance.usuario = pUsuario;
                    _instance.FechaLogin = DateTime.Now;
                }
            }
        }

        public static void logout()
        {
            lock (_padlock)
            {
                if (_instance != null)
                {
                    _instance = null;
                }
            }
        }
    }
}
