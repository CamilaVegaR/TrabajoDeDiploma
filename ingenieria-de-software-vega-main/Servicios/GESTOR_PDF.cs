using BE;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Servicios
{

    public static class GESTOR_PDF
    {
        public static void GenerarPdf_626CV(REPORTE reporte_626CV, string rutaArchivo_626CV)
        {
            Font fuenteEncabezado_626CV = FontFactory.GetFont(FontFactory.HELVETICA, 9f, BaseColor.DARK_GRAY);
            Font fuenteTitulo_626CV = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16f, BaseColor.BLACK);
            Font fuenteSubtitulo_626CV = FontFactory.GetFont(FontFactory.HELVETICA, 11f, BaseColor.BLACK);
            Font fuenteSeccion_626CV = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12f, BaseColor.BLACK);
            Font fuenteColumna_626CV = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 9f, BaseColor.BLACK);
            Font fuenteCelda_626CV = FontFactory.GetFont(FontFactory.HELVETICA, 9f, BaseColor.BLACK);
            Font fuenteResumen_626CV = FontFactory.GetFont(FontFactory.HELVETICA, 10f, BaseColor.BLACK);

            using (FileStream archivo_626CV = new FileStream(rutaArchivo_626CV, FileMode.Create, FileAccess.Write))
            {
                Document documento_626CV = new Document(PageSize.A4, 36f, 36f, 40f, 40f);
                PdfWriter.GetInstance(documento_626CV, archivo_626CV);
                documento_626CV.Open();

                documento_626CV.Add(new Paragraph("Olimpic - Sistema de gestión de reservas y pagos", fuenteEncabezado_626CV));
                documento_626CV.Add(new Paragraph(reporte_626CV.Titulo_626CV, fuenteTitulo_626CV));
                documento_626CV.Add(new Paragraph(reporte_626CV.Subtitulo_626CV, fuenteSubtitulo_626CV));
                documento_626CV.Add(new Paragraph(
                    $"Generado el {reporte_626CV.FechaGeneracion_626CV:dd/MM/yyyy HH:mm} por {reporte_626CV.GeneradoPor_626CV}",
                    fuenteEncabezado_626CV));


                foreach (REPORTE_SECCION seccion_626CV in reporte_626CV.Secciones_626CV)
                {
                    Paragraph tituloSeccion_626CV = new Paragraph(seccion_626CV.Titulo_626CV, fuenteSeccion_626CV);
                    tituloSeccion_626CV.SpacingBefore = 16f;
                    tituloSeccion_626CV.SpacingAfter = 6f;
                    documento_626CV.Add(tituloSeccion_626CV);

                    if (seccion_626CV.Filas_626CV.Count == 0)
                    {
                        documento_626CV.Add(new Paragraph("Sin datos para el período seleccionado.", fuenteResumen_626CV));
                    }
                    else
                    {
                        PdfPTable tabla_626CV = new PdfPTable(seccion_626CV.Columnas_626CV.Length);
                        tabla_626CV.WidthPercentage = 100f;
                        tabla_626CV.HeaderRows = 1;

                        foreach (string columna_626CV in seccion_626CV.Columnas_626CV)
                        {
                            PdfPCell celda_626CV = new PdfPCell(new Phrase(columna_626CV, fuenteColumna_626CV));
                            celda_626CV.BackgroundColor = new BaseColor(220, 220, 220);
                            celda_626CV.Padding = 4f;
                            tabla_626CV.AddCell(celda_626CV);
                        }

                        foreach (string[] fila_626CV in seccion_626CV.Filas_626CV)
                        {
                            foreach (string valor_626CV in fila_626CV)
                            {
                                PdfPCell celda_626CV = new PdfPCell(new Phrase(valor_626CV ?? "", fuenteCelda_626CV));
                                celda_626CV.Padding = 3f;
                                tabla_626CV.AddCell(celda_626CV);
                            }
                        }

                        documento_626CV.Add(tabla_626CV);
                    }

                    foreach (string linea_626CV in seccion_626CV.Resumen_626CV)
                    {
                        Paragraph resumen_626CV = new Paragraph(linea_626CV, fuenteResumen_626CV);
                        resumen_626CV.SpacingBefore = 3f;
                        documento_626CV.Add(resumen_626CV);
                    }
                }

                documento_626CV.Close();
            }
        }
    }
}

