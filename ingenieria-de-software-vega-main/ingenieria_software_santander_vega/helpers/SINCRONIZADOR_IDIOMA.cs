using BE;
using BLL;
using Servicios;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ingenieria_software_santander_vega
{
    public class SINCRONIZADOR_IDIOMA
    {
        private BLL_ETIQUETAS bllEtiquetas = new BLL_ETIQUETAS();

        public void SincronizarFormulario(Form formulario)
        {
            List<ETIQUETA> faltantes = new List<ETIQUETA>();
            var diccionarioActual = SESSION_IDIOMA.GetInstance().Traducciones;

            if (diccionarioActual == null) return;

            ExtraerFaltantes(formulario, diccionarioActual, faltantes, formulario.Name);

            foreach (var etiqueta in faltantes)
            {
                bllEtiquetas.InsertarEtiqueta(etiqueta);
            }
        }

        private void ExtraerFaltantes(Control controlPadre, Dictionary<string, string> diccionario, List<ETIQUETA> faltantes, string nombreFormulario)
        {
            Registrar(controlPadre.Name, diccionario, faltantes, nombreFormulario);

            if (controlPadre is ToolStrip toolStrip)
            {
                foreach (ToolStripItem item in toolStrip.Items)
                {
                    ExtraerFaltantesMenu(item, diccionario, faltantes, nombreFormulario);
                }
            }

            foreach (Control control in controlPadre.Controls)
            {
                ExtraerFaltantes(control, diccionario, faltantes, nombreFormulario);
            }
        }

        private void ExtraerFaltantesMenu(ToolStripItem item, Dictionary<string, string> diccionario, List<ETIQUETA> faltantes, string nombreFormulario)
        {
            Registrar(item.Name, diccionario, faltantes, nombreFormulario);

            if (item is ToolStripMenuItem menuItem)
            {
                foreach (ToolStripItem subItem in menuItem.DropDownItems)
                {
                    ExtraerFaltantesMenu(subItem, diccionario, faltantes, nombreFormulario);
                }
            }
        }

        private void Registrar(string nombre, Dictionary<string, string> diccionario, List<ETIQUETA> faltantes, string nombreFormulario)
        {
            if (!string.IsNullOrWhiteSpace(nombre) && !diccionario.ContainsKey(nombre))
            {
                ETIQUETA nuevaEtiqueta = new ETIQUETA
                {
                    Formulario = nombreFormulario,
                    Clave = nombre
                };

                faltantes.Add(nuevaEtiqueta);
                diccionario.Add(nuevaEtiqueta.Clave, "[Nueva Etiqueta - Traducir en BD]");
            }
        }
    }
}