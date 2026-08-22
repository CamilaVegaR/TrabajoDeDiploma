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
        MODIFICACION
    }
}