using Microsoft.Reporting.WinForms;
using Modelos;
using Negocio;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Presentacion  
{
    public partial class PProduccionProductosReporte : Form
    {
        private NProduccionProductos negocioProduccion = new NProduccionProductos();

        public PProduccionProductosReporte()
        {
            InitializeComponent();
        }

        private void PProduccionProductosReporte_Load(object sender, EventArgs e)
        {
            try
            {
                List<ProduccionProductos> listaProduccion = negocioProduccion.Listar();

                var listaReporte = new List<object>();
                foreach (var item in listaProduccion)
                {
                    listaReporte.Add(new
                    {
                        id = item.id,
                        fecha = item.fecha.ToString("dd/MM/yyyy"),
                        producto = item.producto.descripcion,
                        cantidad = item.cantidad
                    });
                }

                string rutaReporte = Application.StartupPath + "\\ReporteProduccionProductos.rdlc";

                if (!System.IO.File.Exists(rutaReporte))
                {
                    MessageBox.Show("No se encuentra el archivo RDLC en: " + rutaReporte,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                this.reportViewer1.LocalReport.DataSources.Clear();

                ReportDataSource rds = new ReportDataSource("DataSetProduccionProductos", listaReporte);

                this.reportViewer1.LocalReport.ReportPath = rutaReporte;
                this.reportViewer1.LocalReport.DataSources.Add(rds);

                this.reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el reporte: " + ex.Message + "\n\n" + ex.StackTrace,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}