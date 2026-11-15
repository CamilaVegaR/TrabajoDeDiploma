using DAL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BLL
{
    public enum RESULTADO_PREPARACION_BD
    {
        YA_EXISTIA,         
        CREADA,           
        INICIALIZADA         
    }
    public class BLL_BASE_DATOS
    {
        private DAL_BASE_DATOS mapper_626CV = new DAL_BASE_DATOS();

        public string Servidor_626CV
        {
            get { return mapper_626CV.ObtenerServidor_626CV(); }
        }

        public string NombreBase_626CV
        {
            get { return mapper_626CV.ObtenerNombreBase_626CV(); }
        }

        public RESULTADO_PREPARACION_BD PrepararBaseDeDatos_626CV()
        {
            string servidor_626CV;
            string base_626CV;

            try
            {
                servidor_626CV = Servidor_626CV;
                base_626CV = NombreBase_626CV;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n\nRevise el archivo ingenieria_software_santander_vega.exe.config en la carpeta del programa.");
            }

            try
            {
                mapper_626CV.ProbarServidor_626CV();
            }
            catch (SqlException)
            {
                throw new Exception(
                    "No se pudo conectar con SQL Server en \"" + servidor_626CV + "\".\n\n" +
                    "Verifique que SQL Server esté instalado e iniciado y que el nombre de la instancia sea correcto.\n" +
                    "Para cambiarla, abra con el Bloc de notas el archivo ingenieria_software_santander_vega.exe.config " +
                    "de la carpeta del programa y modifique \"Data Source\" (por ejemplo .\\SQLEXPRESS).");
            }

            bool existia_626CV = mapper_626CV.ExisteBase_626CV();

            if (!existia_626CV)
            {
                try
                {
                    mapper_626CV.CrearBase_626CV();
                }
                catch (SqlException ex) when (ex.Number == 262 || ex.Number == 1802)
                {
                    throw new Exception(
                        "Su usuario de Windows no tiene permiso para crear bases de datos en \"" + servidor_626CV + "\".\n\n" +
                        "Pida al encargado de la sala que cree una base vacía llamada \"" + base_626CV + "\" " +
                        "(o que le asigne el rol dbcreator) y vuelva a abrir el programa: las tablas se cargan solas.");
                }
            }
            else if (mapper_626CV.BaseTieneTablas_626CV())
            {
                return RESULTADO_PREPARACION_BD.YA_EXISTIA;
            }
            try
            {
                mapper_626CV.EjecutarScriptInicial_626CV();
            }
            catch (Exception ex)
            {
                throw new Exception("La base \"" + base_626CV + "\" se creó, pero falló la carga de las tablas y los datos.\n\nDetalle: " + ex.Message);
            }

            try
            {
                new BLL_DIGITO_VERIFICADOR().RecalcularTablas();
            }
            catch (Exception ex)
            {
                throw new Exception("La base \"" + base_626CV + "\" se creó, pero no se pudieron calcular los dígitos verificadores.\n\nDetalle: " + ex.Message);
            }

            return existia_626CV ? RESULTADO_PREPARACION_BD.INICIALIZADA : RESULTADO_PREPARACION_BD.CREADA;
        }
    }
}
