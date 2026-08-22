using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

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
    }
}