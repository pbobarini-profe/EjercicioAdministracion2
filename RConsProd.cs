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
            List<ConsumoProduccion> listaOriginal = NConsProd.Get();
            var listaParaReporte = listaOriginal.Select(cp => new
            {
                id = cp.id,
                consumoInsumoId = cp.consumoInsumo.id,
                produccionProductoId = cp.produccionProducto.id,
                observaciones = cp.observaciones
            }).ToList();
            ReportDataSource rds = new ReportDataSource("DataSet1", listaParaReporte);
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(rds);
            reportViewer1.LocalReport.ReportEmbeddedResource = "EjercicioAdministracion2.RConsProd.rdlc";
            reportViewer1.RefreshReport();
            this.reportViewer1.RefreshReport();
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }
    }
}
