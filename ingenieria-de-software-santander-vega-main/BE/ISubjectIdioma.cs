using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BE
{
    public interface ISUBJECT_IDIOMA
    {
        void Suscribir(IOBSERVER_IDIOMA observer);
        void Desuscribir(IOBSERVER_IDIOMA observer);
        void Notificar();
    }
}