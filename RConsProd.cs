using Microsoft.Reporting.WinForms;
using Modelos;
using Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EjercicioAdministracion2
{
    public partial class RConsProd : Form
    {
        public RConsProd()
        {
            InitializeComponent();
        }

        private void RConsProd_Load(object sender, EventArgs e)
        {
            try
            {
                List<A_DtoRConsProd> lista = NConsProd.GetReporte();

                ReportDataSource rds = new ReportDataSource("DataSet", lista);

                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.LocalReport.DataSources.Add(rds);
                reportViewer1.LocalReport.ReportEmbeddedResource = "EjercicioAdministracion2.RConsProd.rdlc";

                reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al generar el reporte: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }
    }
}
