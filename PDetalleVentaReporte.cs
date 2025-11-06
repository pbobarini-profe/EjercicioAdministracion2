using Microsoft.Reporting.WinForms;
using Modelos;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace EjercicioAdministracion2
{
    public partial class PDetalleVentaReporte : Form
    {
        private Ventas ventaSeleccionada;

        public PDetalleVentaReporte(Ventas venta)
        {
            InitializeComponent();
            ventaSeleccionada = venta;
        }

        private void PDetalleVentaReporte_Load(object sender, EventArgs e)
        {
            try
            {
                List<DetalleVentas> detalles = NDetalleVenta.GetById(ventaSeleccionada);

                List<DtoDetalleVentaReporte> lista = detalles.Select(d => new DtoDetalleVentaReporte
                {
                    id = d.id,
                    Producto = d.producto.descripcion,
                    Cantidad = d.cantidad,
                    PrecioUnitario = d.precioUnitario,
                    Subtotal = d.cantidad * d.precioUnitario
                }).ToList();

                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.LocalReport.ReportEmbeddedResource = "EjercicioAdministracion2.ReporteDetalleVentas.rdlc"; // Ruta dentro del proyecto
                reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", lista));

                reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el reporte: " + ex.Message);
            }
        }

       
    }
}
