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
    public partial class PComprasReporte : Form
    {
        public List<Compras> lista;   
        public PComprasReporte(List<Compras> compras)
        {
            InitializeComponent();
            lista = compras;
        }

        private void PComprasReporte_Load(object sender, EventArgs e)
        {                   
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1",lista));                         
            this.reportViewer1.RefreshReport();
        }
    }
}
