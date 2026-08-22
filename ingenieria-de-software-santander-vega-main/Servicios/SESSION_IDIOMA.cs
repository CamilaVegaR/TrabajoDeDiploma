using BE;
using DAL;
using System.Collections.Generic;

namespace Servicios
{
    public class SESSION_IDIOMA : ISUBJECT_IDIOMA
    {
        private static SESSION_IDIOMA _instance;
        private static readonly object _padlock = new object();

        private List<IOBSERVER_IDIOMA> observadores;
        private DAL.DAL_IDIOMA mapperIdioma;

        public IDIOMA IdiomaActual { get; private set; }
        public Dictionary<string, string> Traducciones { get; private set; }

        private SESSION_IDIOMA()
        {
            observadores = new List<IOBSERVER_IDIOMA>();
            Traducciones = new Dictionary<string, string>();
            mapperIdioma = new DAL.DAL_IDIOMA();
        }

        public static SESSION_IDIOMA GetInstance()
        {
            lock (_padlock)
            {
                if (_instance == null)
                {
                    _instance = new SESSION_IDIOMA();
                }
                return _instance;
            }
        }

        public void Suscribir(IOBSERVER_IDIOMA observer)
        {
            if (!observadores.Contains(observer))
            {
                observadores.Add(observer);
            }
        }

        public void Desuscribir(IOBSERVER_IDIOMA observer)
        {
            if (observadores.Contains(observer))
            {
                observadores.Remove(observer);
            }
        }

        public void Notificar()
        {
            foreach (var observer in observadores)
            {
                observer.ActualizarIdioma(IdiomaActual);
            }
        }

        public void CambiarIdioma(IDIOMA nuevoIdioma)
        {
            IdiomaActual = nuevoIdioma;
            CargarTraducciones();
            Notificar();
        }

        private void CargarTraducciones()
        {
            Traducciones.Clear();
            if (IdiomaActual != null)
            {
                List<TRADUCCION> lista = mapperIdioma.ObtenerTraduccionesPorIdioma(IdiomaActual.Id);
                foreach (var trad in lista)
                {
                    if (!Traducciones.ContainsKey(trad.Etiqueta.Clave))
                    {
                        Traducciones.Add(trad.Etiqueta.Clave, trad.Texto);
                    }
                }
            }
        }
    }
}