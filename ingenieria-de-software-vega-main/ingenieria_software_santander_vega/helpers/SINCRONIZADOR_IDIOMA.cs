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
            Dictionary<string, string> textosPorDefecto = new Dictionary<string, string>();
            var diccionarioActual = SESSION_IDIOMA.GetInstance().Traducciones;

            if (diccionarioActual == null) return;

            ExtraerFaltantes(formulario, diccionarioActual, faltantes, textosPorDefecto, formulario.Name);

            foreach (var etiqueta in faltantes)
            {
                string textoDefecto = textosPorDefecto.ContainsKey(etiqueta.Clave) ? textosPorDefecto[etiqueta.Clave] : null;
                bllEtiquetas.InsertarEtiqueta(etiqueta, textoDefecto);

                diccionarioActual[etiqueta.Clave] = !string.IsNullOrWhiteSpace(textoDefecto)
                    ? textoDefecto
                    : "[Cambiar traducción]";
            }
        }

        private void ExtraerFaltantes(Control controlPadre, Dictionary<string, string> diccionario, List<ETIQUETA> faltantes, Dictionary<string, string> textosPorDefecto, string nombreFormulario)
        {
            if (controlPadre is Form || EsTraducible(controlPadre))
                Registrar(controlPadre.Name, controlPadre.Text, diccionario, faltantes, textosPorDefecto, nombreFormulario);

            if (controlPadre is ToolStrip toolStrip)
            {
                foreach (ToolStripItem item in toolStrip.Items)
                {
                    ExtraerFaltantesMenu(item, diccionario, faltantes, textosPorDefecto, nombreFormulario);
                }
            }

            foreach (Control control in controlPadre.Controls)
            {
                ExtraerFaltantes(control, diccionario, faltantes, textosPorDefecto, nombreFormulario);
            }
        }

        private bool EsTraducible(Control control)
        {
            return control is Label
                || control is Button
                || control is GroupBox
                || control is CheckBox
                || control is RadioButton
                || control is TabPage
                || control is LinkLabel;
        }

        private void ExtraerFaltantesMenu(ToolStripItem item, Dictionary<string, string> diccionario, List<ETIQUETA> faltantes, Dictionary<string, string> textosPorDefecto, string nombreFormulario)
        {
            Registrar(item.Name, item.Text, diccionario, faltantes, textosPorDefecto, nombreFormulario);

            if (item is ToolStripMenuItem menuItem)
            {
                foreach (ToolStripItem subItem in menuItem.DropDownItems)
                {
                    ExtraerFaltantesMenu(subItem, diccionario, faltantes, textosPorDefecto, nombreFormulario);
                }
            }
        }
        private void Registrar(string nombre, string textoActual, Dictionary<string, string> diccionario, List<ETIQUETA> faltantes, Dictionary<string, string> textosPorDefecto, string nombreFormulario)
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

                if (!string.IsNullOrWhiteSpace(textoActual))
                    textosPorDefecto[nombre] = textoActual;
            }
        }
    }
}