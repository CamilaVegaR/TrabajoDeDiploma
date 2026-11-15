using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BE
{

    public class REPORTE
    {
        public REPORTE()
        {
            Secciones_626CV = new List<REPORTE_SECCION>();
            FechaGeneracion_626CV = DateTime.Now;
        }

        public string Titulo_626CV { get; set; }
        public string Subtitulo_626CV { get; set; }
        public DateTime FechaGeneracion_626CV { get; set; }
        public string GeneradoPor_626CV { get; set; }
        public List<REPORTE_SECCION> Secciones_626CV { get; set; }
    }

    public class REPORTE_SECCION
    {
        public REPORTE_SECCION(string pTitulo_626CV, params string[] pColumnas_626CV)
        {
            Titulo_626CV = pTitulo_626CV;
            Columnas_626CV = pColumnas_626CV;
            Filas_626CV = new List<string[]>();
            Resumen_626CV = new List<string>();
        }

        public string Titulo_626CV { get; set; }
        public string[] Columnas_626CV { get; set; }
        public List<string[]> Filas_626CV { get; set; }
        public List<string> Resumen_626CV { get; set; }

        public void AgregarFila_626CV(params string[] valores_626CV)
        {
            Filas_626CV.Add(valores_626CV);
        }

        public override string ToString()
        {
            return Titulo_626CV;
        }
    }
}
