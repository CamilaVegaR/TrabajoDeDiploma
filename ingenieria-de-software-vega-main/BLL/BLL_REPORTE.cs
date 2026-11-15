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
                detalle_626CV.AgregarFila_626CV(
                    reserva_626CV.FechaClase_626CV.ToString("dd/MM/yyyy"),
                    DescribirHorario_626CV(reserva_626CV.Horario_626CV),
                    $"{reserva_626CV.Cliente_626CV.Apellido_626CV}, {reserva_626CV.Cliente_626CV.Nombre_626CV}",
                    reserva_626CV.Cliente_626CV.Dni_626CV,
                    DescribirCanal_626CV(reserva_626CV.CanalContacto_626CV),
                    reserva_626CV.ObtenerDescripcionEstado_626CV());
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
                detalle_626CV.AgregarFila_626CV(
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
            indicadores_626CV.AgregarFila_626CV("Ingresos por ventas", FormatearImporte_626CV(compras_626CV.Sum(c => c.Importe_626CV)));
            indicadores_626CV.AgregarFila_626CV("Compras registradas", compras_626CV.Count.ToString());
            indicadores_626CV.AgregarFila_626CV("Clases vendidas", clasesVendidas_626CV.ToString());
            indicadores_626CV.AgregarFila_626CV("Clases reservadas (activas)", activas_626CV.Count.ToString());
            indicadores_626CV.AgregarFila_626CV("Reservas canceladas", canceladas_626CV.ToString());
            indicadores_626CV.AgregarFila_626CV("Ocupación promedio de las clases", FormatearPorcentaje_626CV(CalcularOcupacionPromedio_626CV(activas_626CV)));
            indicadores_626CV.AgregarFila_626CV("Clientes con saldo disponible", clientesConSaldo_626CV.Count.ToString());
            indicadores_626CV.AgregarFila_626CV("Clases pendientes de uso (saldo total)", clientesConSaldo_626CV.Sum(c => c.SaldoClases_626CV).ToString());

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

                ocupacionHorario_626CV.AgregarFila_626CV(
                    DescribirHorario_626CV(grupo_626CV.Horario),
                    grupo_626CV.Clases.ToString(),
                    grupo_626CV.Reservas.ToString(),
                    FormatearPorcentaje_626CV(ocupacion_626CV));
            }

            REPORTE_SECCION saldos_626CV = new REPORTE_SECCION("Clientes con saldo disponible (al día de hoy)", "Cliente", "DNI", "Teléfono", "Clases disponibles");
            foreach (CLIENTE cliente_626CV in clientesConSaldo_626CV)
            {
                saldos_626CV.AgregarFila_626CV(
                    $"{cliente_626CV.Apellido_626CV}, {cliente_626CV.Nombre_626CV}",
                    cliente_626CV.Dni_626CV,
                    cliente_626CV.Telefono_626CV,
                    cliente_626CV.SaldoClases_626CV.ToString());
            }

            reporte_626CV.Secciones_626CV.Add(indicadores_626CV);
            reporte_626CV.Secciones_626CV.Add(ArmarVentasPorPack_626CV(compras_626CV));
            reporte_626CV.Secciones_626CV.Add(ocupacionHorario_626CV);
            reporte_626CV.Secciones_626CV.Add(saldos_626CV);
            return reporte_626CV;
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
                seccion_626CV.AgregarFila_626CV(
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
                seccion_626CV.AgregarFila_626CV(
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
            return $"{HORARIO_CLASE.TraducirDia_626CV(horario_626CV.Dia_626CV)} {horario_626CV.Hora_626CV:hh\\:mm}";
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
    }
}

