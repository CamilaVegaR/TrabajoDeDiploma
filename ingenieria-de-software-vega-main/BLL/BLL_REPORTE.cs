using BE;
using DAL;
using Servicios;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{

    public class BLL_REPORTE
    {
        private DAL_REPORTE mapperReporte_626CV = new DAL_REPORTE();
        private static readonly CultureInfo cultura_626CV = new CultureInfo("es-AR");

        public REPORTE GenerarReporteReservas_626CV(DateTime desde_626CV, DateTime hasta_626CV)
        {
            ValidarPeriodo_626CV(desde_626CV, hasta_626CV);
            List<RESERVA> reservas_626CV = mapperReporte_626CV.ListarReservasPeriodo_626CV(desde_626CV, hasta_626CV);

            REPORTE reporte_626CV = CrearReporte_626CV("Reporte de reservas (RF1)", desde_626CV, hasta_626CV);

            REPORTE_SECCION detalle_626CV = new REPORTE_SECCION("Detalle de reservas", "Fecha", "Horario", "Cliente", "DNI", "Canal", "Estado");
            foreach (RESERVA reserva_626CV in reservas_626CV)
            {
                AgregarFila_626CV(detalle_626CV,
                    reserva_626CV.FechaClase_626CV.ToString("dd/MM/yyyy"),
                    DescribirHorario_626CV(reserva_626CV.Horario_626CV),
                    $"{reserva_626CV.Cliente_626CV.Apellido_626CV}, {reserva_626CV.Cliente_626CV.Nombre_626CV}",
                    reserva_626CV.Cliente_626CV.Dni_626CV,
                    DescribirCanal_626CV(reserva_626CV.CanalContacto_626CV),
                    new BLL_RESERVA().ObtenerDescripcionEstado_626CV(reserva_626CV));
            }

            int activas_626CV = reservas_626CV.Count(r => r.Estado_626CV == ESTADO_RESERVA_ENUM.ACTIVA_626CV);
            int canceladas_626CV = reservas_626CV.Count - activas_626CV;
            detalle_626CV.Resumen_626CV.Add($"Total de reservas: {reservas_626CV.Count}");
            detalle_626CV.Resumen_626CV.Add($"Reservas activas: {activas_626CV}");
            detalle_626CV.Resumen_626CV.Add($"Reservas canceladas: {canceladas_626CV}");

            REPORTE_SECCION ocupacion_626CV = ArmarOcupacionPorClase_626CV(reservas_626CV);

            reporte_626CV.Secciones_626CV.Add(detalle_626CV);
            reporte_626CV.Secciones_626CV.Add(ocupacion_626CV);
            return reporte_626CV;
        }


        public REPORTE GenerarReporteVentas_626CV(DateTime desde_626CV, DateTime hasta_626CV)
        {
            ValidarPeriodo_626CV(desde_626CV, hasta_626CV);
            List<COMPRA> compras_626CV = mapperReporte_626CV.ListarVentasPeriodo_626CV(desde_626CV, hasta_626CV);

            REPORTE reporte_626CV = CrearReporte_626CV("Reporte de ventas de clases y packs (RF2)", desde_626CV, hasta_626CV);

            REPORTE_SECCION detalle_626CV = new REPORTE_SECCION("Detalle de compras", "Fecha", "Cliente", "DNI", "Pack", "Clases", "Descuento", "Importe", "Canal");
            foreach (COMPRA compra_626CV in compras_626CV)
            {
                AgregarFila_626CV(detalle_626CV,
                    compra_626CV.FechaCompra_626CV.ToString("dd/MM/yyyy HH:mm"),
                    $"{compra_626CV.Cliente_626CV.Apellido_626CV}, {compra_626CV.Cliente_626CV.Nombre_626CV}",
                    compra_626CV.Cliente_626CV.Dni_626CV,
                    compra_626CV.Pack_626CV.Nombre_626CV,
                    compra_626CV.CantidadClases_626CV.ToString(),
                    FormatearPorcentaje_626CV(compra_626CV.PorcentajeDescuento_626CV),
                    FormatearImporte_626CV(compra_626CV.Importe_626CV),
                    DescribirCanal_626CV(compra_626CV.CanalContacto_626CV));
            }

            detalle_626CV.Resumen_626CV.Add($"Compras registradas: {compras_626CV.Count}");
            detalle_626CV.Resumen_626CV.Add($"Clases vendidas: {compras_626CV.Sum(c => c.CantidadClases_626CV)}");
            detalle_626CV.Resumen_626CV.Add($"Ingresos del período: {FormatearImporte_626CV(compras_626CV.Sum(c => c.Importe_626CV))}");

            reporte_626CV.Secciones_626CV.Add(detalle_626CV);
            reporte_626CV.Secciones_626CV.Add(ArmarVentasPorPack_626CV(compras_626CV));
            return reporte_626CV;
        }


        public REPORTE GenerarReporteGlobal_626CV(DateTime desde_626CV, DateTime hasta_626CV)
        {
            ValidarPeriodo_626CV(desde_626CV, hasta_626CV);
            List<RESERVA> reservas_626CV = mapperReporte_626CV.ListarReservasPeriodo_626CV(desde_626CV, hasta_626CV);
            List<COMPRA> compras_626CV = mapperReporte_626CV.ListarVentasPeriodo_626CV(desde_626CV, hasta_626CV);
            List<CLIENTE> clientesConSaldo_626CV = mapperReporte_626CV.ListarClientesConSaldo_626CV();

            REPORTE reporte_626CV = CrearReporte_626CV("Resumen de gestión (global)", desde_626CV, hasta_626CV);

            List<RESERVA> activas_626CV = reservas_626CV.Where(r => r.Estado_626CV == ESTADO_RESERVA_ENUM.ACTIVA_626CV).ToList();
            int canceladas_626CV = reservas_626CV.Count - activas_626CV.Count;
            int clasesVendidas_626CV = compras_626CV.Sum(c => c.CantidadClases_626CV);

            REPORTE_SECCION indicadores_626CV = new REPORTE_SECCION("Indicadores del período", "Indicador", "Valor");
            AgregarFila_626CV(indicadores_626CV, "Ingresos por ventas", FormatearImporte_626CV(compras_626CV.Sum(c => c.Importe_626CV)));
            AgregarFila_626CV(indicadores_626CV, "Compras registradas", compras_626CV.Count.ToString());
            AgregarFila_626CV(indicadores_626CV, "Clases vendidas", clasesVendidas_626CV.ToString());
            AgregarFila_626CV(indicadores_626CV, "Clases reservadas (activas)", activas_626CV.Count.ToString());
            AgregarFila_626CV(indicadores_626CV, "Reservas canceladas", canceladas_626CV.ToString());
            AgregarFila_626CV(indicadores_626CV, "Ocupación promedio de las clases", FormatearPorcentaje_626CV(CalcularOcupacionPromedio_626CV(activas_626CV)));
            AgregarFila_626CV(indicadores_626CV, "Clientes con saldo disponible", clientesConSaldo_626CV.Count.ToString());
            AgregarFila_626CV(indicadores_626CV, "Clases pendientes de uso (saldo total)", clientesConSaldo_626CV.Sum(c => c.SaldoClases_626CV).ToString());

            REPORTE_SECCION ocupacionHorario_626CV = new REPORTE_SECCION("Ocupación por horario", "Horario", "Clases con reservas", "Reservas activas", "Ocupación promedio");
            var gruposHorario_626CV = activas_626CV
                .GroupBy(r => r.Horario_626CV.Id_626CV)
                .Select(g => new
                {
                    Horario = g.First().Horario_626CV,
                    Clases = g.Select(r => r.FechaClase_626CV.Date).Distinct().Count(),
                    Reservas = g.Count()
                })
                .OrderBy(x => x.Horario.Dia_626CV == DayOfWeek.Sunday ? 7 : (int)x.Horario.Dia_626CV)
                .ThenBy(x => x.Horario.Hora_626CV);

            foreach (var grupo_626CV in gruposHorario_626CV)
            {
                decimal ocupacion_626CV = grupo_626CV.Clases * grupo_626CV.Horario.CupoMaximo_626CV == 0
                    ? 0
                    : (decimal)grupo_626CV.Reservas * 100m / (grupo_626CV.Clases * grupo_626CV.Horario.CupoMaximo_626CV);

                AgregarFila_626CV(ocupacionHorario_626CV,
                    DescribirHorario_626CV(grupo_626CV.Horario),
                    grupo_626CV.Clases.ToString(),
                    grupo_626CV.Reservas.ToString(),
                    FormatearPorcentaje_626CV(ocupacion_626CV));
            }

            REPORTE_SECCION saldos_626CV = new REPORTE_SECCION("Clientes con saldo disponible (al día de hoy)", "Cliente", "DNI", "Teléfono", "Clases disponibles");
            foreach (CLIENTE cliente_626CV in clientesConSaldo_626CV)
            {
                AgregarFila_626CV(saldos_626CV,
                    $"{cliente_626CV.Apellido_626CV}, {cliente_626CV.Nombre_626CV}",
                    cliente_626CV.Dni_626CV,
                    cliente_626CV.Telefono_626CV,
                    cliente_626CV.SaldoClases_626CV.ToString());
            }

            reporte_626CV.Secciones_626CV.Add(ArmarAnalisisInteligente_626CV(reservas_626CV, compras_626CV, clientesConSaldo_626CV, desde_626CV, hasta_626CV));
            reporte_626CV.Secciones_626CV.Add(indicadores_626CV);
            reporte_626CV.Secciones_626CV.Add(ArmarVentasPorPack_626CV(compras_626CV));
            reporte_626CV.Secciones_626CV.Add(ocupacionHorario_626CV);
            reporte_626CV.Secciones_626CV.Add(saldos_626CV);
            return reporte_626CV;
        }

        // A02 - Reporte inteligente: el sistema analiza los datos del período y genera
        // comparaciones, alertas, destacados y recomendaciones a partir de reglas.
        private const decimal OCUPACION_BAJA_626CV = 30m;
        private const decimal OCUPACION_ALTA_626CV = 90m;
        private const decimal CANCELACION_ALTA_626CV = 20m;

        private REPORTE_SECCION ArmarAnalisisInteligente_626CV(List<RESERVA> reservas_626CV, List<COMPRA> compras_626CV,
            List<CLIENTE> clientesConSaldo_626CV, DateTime desde_626CV, DateTime hasta_626CV)
        {
            REPORTE_SECCION seccion_626CV = new REPORTE_SECCION("Análisis inteligente: conclusiones y recomendaciones", "Tipo", "Análisis");
            List<RESERVA> activas_626CV = reservas_626CV.Where(r => r.Estado_626CV == ESTADO_RESERVA_ENUM.ACTIVA_626CV).ToList();
            int alertas_626CV = 0;

            // 1. Comparación con el período anterior de la misma duración
            int dias_626CV = (hasta_626CV.Date - desde_626CV.Date).Days + 1;
            DateTime hastaAnterior_626CV = desde_626CV.Date.AddDays(-1);
            DateTime desdeAnterior_626CV = hastaAnterior_626CV.AddDays(-(dias_626CV - 1));
            List<COMPRA> comprasAnteriores_626CV = mapperReporte_626CV.ListarVentasPeriodo_626CV(desdeAnterior_626CV, hastaAnterior_626CV);
            List<RESERVA> reservasAnteriores_626CV = mapperReporte_626CV.ListarReservasPeriodo_626CV(desdeAnterior_626CV, hastaAnterior_626CV);
            string periodoAnterior_626CV = $"{desdeAnterior_626CV:dd/MM/yyyy} al {hastaAnterior_626CV:dd/MM/yyyy}";

            AgregarFila_626CV(seccion_626CV, "Comparación", DescribirVariacion_626CV("Los ingresos",
                compras_626CV.Sum(c => c.Importe_626CV), comprasAnteriores_626CV.Sum(c => c.Importe_626CV), periodoAnterior_626CV));
            AgregarFila_626CV(seccion_626CV, "Comparación", DescribirVariacion_626CV("Las reservas activas",
                activas_626CV.Count, reservasAnteriores_626CV.Count(r => r.Estado_626CV == ESTADO_RESERVA_ENUM.ACTIVA_626CV), periodoAnterior_626CV));

            // 2. Ocupación por horario: alertas de ocupación baja o alta
            var horarios_626CV = activas_626CV
                .GroupBy(r => r.Horario_626CV.Id_626CV)
                .Select(g => new
                {
                    Horario = g.First().Horario_626CV,
                    Ocupacion = CalcularOcupacionPromedio_626CV(g.ToList())
                })
                .OrderByDescending(x => x.Ocupacion)
                .ToList();

            foreach (var horario_626CV in horarios_626CV)
            {
                string descripcion_626CV = DescribirHorario_626CV(horario_626CV.Horario);
                if (horario_626CV.Ocupacion < OCUPACION_BAJA_626CV)
                {
                    alertas_626CV++;
                    AgregarFila_626CV(seccion_626CV, "Alerta", $"El horario {descripcion_626CV} tiene una ocupación baja ({FormatearPorcentaje_626CV(horario_626CV.Ocupacion)}).");
                    AgregarFila_626CV(seccion_626CV, "Recomendación", $"Evaluar mover, unificar o promocionar el horario {descripcion_626CV}.");
                }
                else if (horario_626CV.Ocupacion > OCUPACION_ALTA_626CV)
                {
                    alertas_626CV++;
                    AgregarFila_626CV(seccion_626CV, "Alerta", $"El horario {descripcion_626CV} está casi completo ({FormatearPorcentaje_626CV(horario_626CV.Ocupacion)}).");
                    AgregarFila_626CV(seccion_626CV, "Recomendación", $"Evaluar abrir un horario adicional cercano a {descripcion_626CV}.");
                }
            }

            // 3. Tasa de cancelación
            if (reservas_626CV.Count > 0)
            {
                decimal tasaCancelacion_626CV = (decimal)(reservas_626CV.Count - activas_626CV.Count) * 100m / reservas_626CV.Count;
                if (tasaCancelacion_626CV > CANCELACION_ALTA_626CV)
                {
                    alertas_626CV++;
                    AgregarFila_626CV(seccion_626CV, "Alerta", $"La tasa de cancelación es alta ({FormatearPorcentaje_626CV(tasaCancelacion_626CV)} de las reservas).");
                    AgregarFila_626CV(seccion_626CV, "Recomendación", "Enviar recordatorios a los clientes antes de cada clase.");
                }
            }

            // 4. Packs activos sin ventas en el período
            List<string> packsVendidos_626CV = compras_626CV.Select(c => c.Pack_626CV.Nombre_626CV).Distinct().ToList();
            foreach (PACK pack_626CV in new BLL_PACK().ListarActivos_626CV().Where(p => !packsVendidos_626CV.Contains(p.Nombre_626CV)))
            {
                alertas_626CV++;
                AgregarFila_626CV(seccion_626CV, "Alerta", $"El pack {pack_626CV.Nombre_626CV} no tuvo ventas en el período.");
                AgregarFila_626CV(seccion_626CV, "Recomendación", $"Revisar el precio o el descuento del pack {pack_626CV.Nombre_626CV}.");
            }

            // 5. Clientes con saldo que no reservaron en el período
            List<int> clientesConReserva_626CV = activas_626CV.Select(r => r.Cliente_626CV.Id_626CV).Distinct().ToList();
            int clientesInactivos_626CV = clientesConSaldo_626CV.Count(c => !clientesConReserva_626CV.Contains(c.Id_626CV));
            if (clientesInactivos_626CV > 0)
            {
                alertas_626CV++;
                AgregarFila_626CV(seccion_626CV, "Alerta", $"{clientesInactivos_626CV} cliente(s) tienen clases disponibles pero no reservaron en el período.");
                AgregarFila_626CV(seccion_626CV, "Recomendación", "Contactar a esos clientes para que usen su saldo (ver sección Clientes con saldo disponible).");
            }

            // 6. Destacados del período
            if (horarios_626CV.Count > 0)
                AgregarFila_626CV(seccion_626CV, "Destacado", $"Horario más ocupado: {DescribirHorario_626CV(horarios_626CV.First().Horario)} ({FormatearPorcentaje_626CV(horarios_626CV.First().Ocupacion)}).");

            if (compras_626CV.Count > 0)
            {
                var packMasVendido_626CV = compras_626CV.GroupBy(c => c.Pack_626CV.Nombre_626CV)
                    .OrderByDescending(g => g.Sum(c => c.Importe_626CV)).First();
                AgregarFila_626CV(seccion_626CV, "Destacado", $"Pack con más ingresos: {packMasVendido_626CV.Key} ({FormatearImporte_626CV(packMasVendido_626CV.Sum(c => c.Importe_626CV))}).");

                var canalPrincipal_626CV = compras_626CV.GroupBy(c => c.CanalContacto_626CV)
                    .OrderByDescending(g => g.Count()).First();
                AgregarFila_626CV(seccion_626CV, "Destacado", $"Canal con más ventas: {DescribirCanal_626CV(canalPrincipal_626CV.Key)} ({canalPrincipal_626CV.Count()} compras).");
            }

            if (alertas_626CV == 0)
                AgregarFila_626CV(seccion_626CV, "Conclusión", "No se detectaron alertas en el período.");

            seccion_626CV.Resumen_626CV.Add($"Alertas detectadas: {alertas_626CV}");
            seccion_626CV.Resumen_626CV.Add($"Criterios: ocupación baja < {OCUPACION_BAJA_626CV}%, ocupación alta > {OCUPACION_ALTA_626CV}%, cancelación alta > {CANCELACION_ALTA_626CV}%.");
            return seccion_626CV;
        }

        private string DescribirVariacion_626CV(string indicador_626CV, decimal actual_626CV, decimal anterior_626CV, string periodoAnterior_626CV)
        {
            if (anterior_626CV == 0)
                return $"{indicador_626CV} no se pueden comparar porque no hubo movimientos en el período anterior ({periodoAnterior_626CV}).";

            decimal variacion_626CV = (actual_626CV - anterior_626CV) * 100m / anterior_626CV;
            if (variacion_626CV == 0)
                return $"{indicador_626CV} se mantuvieron igual que en el período anterior ({periodoAnterior_626CV}).";

            string sentido_626CV = variacion_626CV > 0 ? "subieron" : "bajaron";
            return $"{indicador_626CV} {sentido_626CV} un {FormatearPorcentaje_626CV(Math.Abs(variacion_626CV))} respecto del período anterior ({periodoAnterior_626CV}).";
        }


        public void ExportarPdf_626CV(REPORTE reporte_626CV, string rutaArchivo_626CV)
        {
            if (reporte_626CV == null)
                throw new ArgumentException("Primero debe generar un reporte.");

            GESTOR_PDF.GenerarPdf_626CV(reporte_626CV, rutaArchivo_626CV);
        }

        private void ValidarPeriodo_626CV(DateTime desde_626CV, DateTime hasta_626CV)
        {
            if (desde_626CV.Date > hasta_626CV.Date)
                throw new ArgumentException("La fecha desde no puede ser posterior a la fecha hasta.");
        }

        private REPORTE CrearReporte_626CV(string titulo_626CV, DateTime desde_626CV, DateTime hasta_626CV)
        {
            USUARIO usuario_626CV = SINGLETON_SESION.GetInstance().usuario;
            return new REPORTE
            {
                Titulo_626CV = titulo_626CV,
                Subtitulo_626CV = $"Período: {desde_626CV:dd/MM/yyyy} al {hasta_626CV:dd/MM/yyyy}",
                GeneradoPor_626CV = usuario_626CV != null ? usuario_626CV.ToString() : ""
            };
        }

        private REPORTE_SECCION ArmarOcupacionPorClase_626CV(List<RESERVA> reservas_626CV)
        {
            REPORTE_SECCION seccion_626CV = new REPORTE_SECCION("Ocupación por clase", "Fecha", "Horario", "Reservas activas", "Cupo", "Ocupación");
            List<RESERVA> activas_626CV = reservas_626CV.Where(r => r.Estado_626CV == ESTADO_RESERVA_ENUM.ACTIVA_626CV).ToList();

            var clases_626CV = activas_626CV
                .GroupBy(r => new { Fecha = r.FechaClase_626CV.Date, IdHorario = r.Horario_626CV.Id_626CV })
                .Select(g => new { g.Key.Fecha, Horario = g.First().Horario_626CV, Reservas = g.Count() })
                .OrderBy(x => x.Fecha).ThenBy(x => x.Horario.Hora_626CV);

            foreach (var clase_626CV in clases_626CV)
            {
                int cupo_626CV = clase_626CV.Horario.CupoMaximo_626CV;
                decimal ocupacion_626CV = cupo_626CV == 0 ? 0 : (decimal)clase_626CV.Reservas * 100m / cupo_626CV;
                AgregarFila_626CV(seccion_626CV,
                    clase_626CV.Fecha.ToString("dd/MM/yyyy"),
                    DescribirHorario_626CV(clase_626CV.Horario),
                    clase_626CV.Reservas.ToString(),
                    cupo_626CV.ToString(),
                    FormatearPorcentaje_626CV(ocupacion_626CV));
            }

            seccion_626CV.Resumen_626CV.Add($"Clases con reservas: {seccion_626CV.Filas_626CV.Count}");
            seccion_626CV.Resumen_626CV.Add($"Ocupación promedio: {FormatearPorcentaje_626CV(CalcularOcupacionPromedio_626CV(activas_626CV))}");
            return seccion_626CV;
        }

        private REPORTE_SECCION ArmarVentasPorPack_626CV(List<COMPRA> compras_626CV)
        {
            REPORTE_SECCION seccion_626CV = new REPORTE_SECCION("Ventas por pack", "Pack", "Compras", "Clases vendidas", "Importe total");

            var packs_626CV = compras_626CV
                .GroupBy(c => c.Pack_626CV.Nombre_626CV)
                .Select(g => new
                {
                    Pack = g.Key,
                    Compras = g.Count(),
                    Clases = g.Sum(c => c.CantidadClases_626CV),
                    Importe = g.Sum(c => c.Importe_626CV)
                })
                .OrderByDescending(x => x.Importe);

            foreach (var pack_626CV in packs_626CV)
            {
                AgregarFila_626CV(seccion_626CV,
                    pack_626CV.Pack,
                    pack_626CV.Compras.ToString(),
                    pack_626CV.Clases.ToString(),
                    FormatearImporte_626CV(pack_626CV.Importe));
            }

            seccion_626CV.Resumen_626CV.Add($"Total: {FormatearImporte_626CV(compras_626CV.Sum(c => c.Importe_626CV))}");
            return seccion_626CV;
        }


        private decimal CalcularOcupacionPromedio_626CV(List<RESERVA> activas_626CV)
        {
            int cupoTotal_626CV = activas_626CV
                .GroupBy(r => new { Fecha = r.FechaClase_626CV.Date, IdHorario = r.Horario_626CV.Id_626CV })
                .Sum(g => g.First().Horario_626CV.CupoMaximo_626CV);

            return cupoTotal_626CV == 0 ? 0 : (decimal)activas_626CV.Count * 100m / cupoTotal_626CV;
        }

        private string DescribirHorario_626CV(HORARIO_CLASE horario_626CV)
        {
            return $"{new BLL_HORARIO_CLASE().TraducirDia_626CV(horario_626CV.Dia_626CV)} {horario_626CV.Hora_626CV:hh\\:mm}";
        }

        private string DescribirCanal_626CV(CANAL_CONTACTO_ENUM canal_626CV)
        {
            switch (canal_626CV)
            {
                case CANAL_CONTACTO_ENUM.TELEFONICO_626CV: return "Telefónico";
                case CANAL_CONTACTO_ENUM.REDES_SOCIALES_626CV: return "Redes sociales";
                default: return "Presencial";
            }
        }

        private string FormatearImporte_626CV(decimal importe_626CV)
        {
            return "$ " + importe_626CV.ToString("N2", cultura_626CV);
        }

        private string FormatearPorcentaje_626CV(decimal porcentaje_626CV)
        {
            return porcentaje_626CV.ToString("0.#", cultura_626CV) + "%";
        }

        private void AgregarFila_626CV(REPORTE_SECCION seccion_626CV, params string[] valores_626CV)
        {
            seccion_626CV.Filas_626CV.Add(valores_626CV);
        }
    }
}