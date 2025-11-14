using Microsoft.Reporting.WebForms;
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
    public partial class wreport : Form
    {
        List<DetalleCompras> lista;
        public wreport(List<DetalleCompras> detalles)
        {
            InitializeComponent();
            lista = detalles;
        }

        private void wreport_Load(object sender, EventArgs e)
        {
           reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", lista));
            this.reportViewer1.RefreshReport();
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }
    }
}
