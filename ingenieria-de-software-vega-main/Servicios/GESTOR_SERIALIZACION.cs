using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Servicios
{
    // A03 - Serialización y deserialización en XML.
    // Es genérico: sirve para cualquier lista de entidades (PACK, CLIENTE, ...).
    // Las clases a serializar deben ser públicas, con constructor vacío y propiedades
    // públicas con get y set (los métodos no se serializan).
    public static class GESTOR_SERIALIZACION
    {
        public static void Serializar_626CV<T>(List<T> datos_626CV, string ruta_626CV, string raiz_626CV)
        {
            XmlSerializer serializador_626CV = new XmlSerializer(typeof(List<T>), new XmlRootAttribute(raiz_626CV));

            XmlWriterSettings config_626CV = new XmlWriterSettings
            {
                Indent = true,
                Encoding = new UTF8Encoding(false)
            };

            using (XmlWriter escritor_626CV = XmlWriter.Create(ruta_626CV, config_626CV))
            {
                serializador_626CV.Serialize(escritor_626CV, datos_626CV);
            }
        }

        // Lanza InvalidDataException si el archivo no es un XML válido
        // o si no corresponde al tipo de entidad esperado (raíz distinta).
        public static List<T> Deserializar_626CV<T>(string ruta_626CV, string raiz_626CV)
        {
            XmlSerializer serializador_626CV = new XmlSerializer(typeof(List<T>), new XmlRootAttribute(raiz_626CV));

            try
            {
                using (XmlReader lector_626CV = XmlReader.Create(ruta_626CV))
                {
                    if (!serializador_626CV.CanDeserialize(lector_626CV))
                        throw new InvalidDataException("El archivo no contiene datos del tipo esperado (" + raiz_626CV + ").");

                    return (List<T>)serializador_626CV.Deserialize(lector_626CV);
                }
            }
            catch (XmlException)
            {
                throw new InvalidDataException("El archivo no es un XML válido.");
            }
            catch (InvalidOperationException)
            {
                throw new InvalidDataException("El archivo tiene un formato incorrecto o fue modificado a mano.");
            }
        }
    }
}
