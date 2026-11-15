using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BE
{
    public enum BITACORA_ACTIVIDAD_ENUM
    {
        LOGIN,
        LOGOUT,
        CREACION_USUARIO,
        ELIMINACION_USUARIO,
        MODIFICACION_USUARIO,
        ASIGNACION_PERMISOS,
        ASIGNACION_ROLES,
        ACCESO_FALLIDO,
        BLOQUEO_CUENTA,
        MODIFICACION,
        // RF2 - Gestión de Clases y Packs
        PACK_CREADO,
        PACK_MODIFICADO,
        PACK_DESACTIVADO,
        COMPRA_REGISTRADA,
        // CU-04 Cambiar de idioma
        CAMBIO_IDIOMA,
        // A03 - Serialización
        SERIALIZACION,
        DESERIALIZACION,
        // Tercera entrega - Control de cambios de PACK
        PACK_VERSION_ACTIVADA
    }
}
