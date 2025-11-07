using Modelos;
using Negocio;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace EjercicioAdministracion2
{
    public partial class PProductosReporte : Form
    {
        private NProductos negocioProductos = new NProductos();

        public PProductosReporte()
        {
            InitializeComponent();
        }

        private void PProductosReporte_Load(object sender, EventArgs e)
        {
            try
            {
                List<Productos> listaProductos = negocioProductos.Listar();
                this.reportViewer1.LocalReport.DataSources.Clear();
                ReportDataSource rds = new ReportDataSource("DataSetProductos", listaProductos);
                this.reportViewer1.LocalReport.ReportPath = "ReporteProductos.rdlc";
                this.reportViewer1.LocalReport.DataSources.Add(rds);
                this.reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el reporte: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}