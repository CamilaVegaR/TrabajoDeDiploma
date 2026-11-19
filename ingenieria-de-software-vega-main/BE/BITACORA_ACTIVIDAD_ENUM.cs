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
        // Control de cambios de PACK
        PACK_VERSION_ACTIVADA,
        // RF2 - Ajustar Saldo de Clases
        SALDO_AJUSTADO,
        // RF1 - Gestión de reservas
        RESERVA_AGENDADA,
        RESERVA_MODIFICADA,
        RESERVA_CANCELADA,
        CLIENTE_REGISTRADO,
        // T02 - Gestión de usuarios, idiomas y perfiles
        MODIFICACION_CONTRASENA,
        CREACION_IDIOMA,
        MODIFICACION_TRADUCCION,
        CREACION_ETIQUETA,
        CREACION_FAMILIA,
        CREACION_PERMISO,
        ELIMINACION_COMPONENTE,
        REACTIVACION_USUARIO,
        // T08 - Dígitos verificadores
        RECALCULO_DV
    }
}