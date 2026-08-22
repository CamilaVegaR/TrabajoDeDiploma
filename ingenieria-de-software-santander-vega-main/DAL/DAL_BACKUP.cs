using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL_BACKUP
    {
        private ACCESO acceso;

        public bool GenerarBackup()
        {
            acceso = new ACCESO();

            try
            {
                acceso.Abrir();

                int resultado = acceso.EjecutarProcedimiento(
                    "SP_BACKUP_GENERAR",
                    null,
                    300
                );

                acceso.Cerrar();

                return resultado >= -1;
            }
            catch
            {
                if (acceso != null)
                {
                    try
                    {
                        acceso.Cerrar();
                    }
                    catch
                    {
                    }
                }

                throw;
            }
        }
    }
}