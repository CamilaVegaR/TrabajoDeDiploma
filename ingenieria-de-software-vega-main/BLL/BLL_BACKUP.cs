using DAL;
using System;

namespace BLL
{
    public class BLL_BACKUP
    {
        private DAL_BACKUP mapperBackup;

        public BLL_BACKUP()
        {
            mapperBackup = new DAL_BACKUP();
        }

        public void GenerarBackup()
        {
            bool generado = mapperBackup.GenerarBackup();

            if (!generado)
            {
                throw new Exception("No se pudo generar el backup de la base de datos.");
            }
        }

        public DateTime? ObtenerFechaUltimoBackup()
        {
            string ruta;
            return mapperBackup.ObtenerUltimoBackup(out ruta);
        }


        public void RestaurarUltimoBackup()
        {
            string ruta;
            DateTime? fecha = mapperBackup.ObtenerUltimoBackup(out ruta);

            if (fecha == null || string.IsNullOrWhiteSpace(ruta))
                throw new Exception("No hay ningún backup disponible para restaurar.");

            mapperBackup.RestaurarBackup(ruta);
        }
    }
}
