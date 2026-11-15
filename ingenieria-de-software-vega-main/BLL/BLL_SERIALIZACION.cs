using BE;
using DAL;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{

    public class BLL_SERIALIZACION
    {
        public const string RAIZ_PACKS_626CV = "Packs_626CV";
        public const string RAIZ_CLIENTES_626CV = "Clientes_626CV";


        public int SerializarPacks_626CV(string ruta_626CV)
        {
            List<PACK> packs_626CV = new BLL_PACK().ListarTodos_626CV();
            GESTOR_SERIALIZACION.Serializar_626CV(packs_626CV, ruta_626CV, RAIZ_PACKS_626CV);
            RegistrarBitacora_626CV(BITACORA_ACTIVIDAD_ENUM.SERIALIZACION, "Packs: " + packs_626CV.Count + " registros");
            return packs_626CV.Count;
        }

        public int SerializarClientes_626CV(string ruta_626CV)
        {
            List<CLIENTE> clientes_626CV = new DAL_CLIENTE().ListarClientes_626CV();
            GESTOR_SERIALIZACION.Serializar_626CV(clientes_626CV, ruta_626CV, RAIZ_CLIENTES_626CV);
            RegistrarBitacora_626CV(BITACORA_ACTIVIDAD_ENUM.SERIALIZACION, "Clientes: " + clientes_626CV.Count + " registros");
            return clientes_626CV.Count;
        }

        public List<PACK> DeserializarPacks_626CV(string ruta_626CV)
        {
            List<PACK> packs_626CV = GESTOR_SERIALIZACION.Deserializar_626CV<PACK>(ruta_626CV, RAIZ_PACKS_626CV);
            RegistrarBitacora_626CV(BITACORA_ACTIVIDAD_ENUM.DESERIALIZACION, "Packs: " + packs_626CV.Count + " registros");
            return packs_626CV;
        }

        public List<CLIENTE> DeserializarClientes_626CV(string ruta_626CV)
        {
            List<CLIENTE> clientes_626CV = GESTOR_SERIALIZACION.Deserializar_626CV<CLIENTE>(ruta_626CV, RAIZ_CLIENTES_626CV);
            RegistrarBitacora_626CV(BITACORA_ACTIVIDAD_ENUM.DESERIALIZACION, "Clientes: " + clientes_626CV.Count + " registros");
            return clientes_626CV;
        }

        public bool RegistroIntegro_626CV(IIntegrable registro_626CV)
        {
            if (string.IsNullOrEmpty(registro_626CV.DVH))
                return false;

            return registro_626CV.DVH == GESTOR_INTEGRIDAD.CalcularDVH(registro_626CV);
        }

        private void RegistrarBitacora_626CV(BITACORA_ACTIVIDAD_ENUM actividad_626CV, string observacion_626CV)
        {
        
            if (observacion_626CV != null && observacion_626CV.Length > 50)
                observacion_626CV = observacion_626CV.Substring(0, 50);

            BITACORA bitacora_626CV = new BITACORA(SINGLETON_SESION.GetInstance().usuario, DateTime.Now, actividad_626CV, observacion_626CV);
            new BLL_BITACORA().Insertar(bitacora_626CV);
        }
    }
}
