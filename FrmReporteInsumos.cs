using Microsoft.Reporting.WinForms;
using System;
using System.Data;
using System.Windows.Forms;

namespace EjercicioAdministracion2
{
    public partial class FrmReporteInsumos : Form
    {
        public FrmReporteInsumos()
        {
            InitializeComponent();
        }

        private void FrmReporteInsumos_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id", typeof(int));
            dt.Columns.Add("descripcion", typeof(string));
            dt.Columns.Add("totalConsumos", typeof(int));
            dt.Columns.Add("cantidadTotal", typeof(decimal));

            var insumos = Negocio.NInsumos.Get();
            foreach (var ins in insumos)
            {
                var consumos = Negocio.NConsumosInsumos.GetByInsumo(ins);
                int totalConsumos = consumos.Count;
                decimal cantidadTotal = 0;
                foreach (var c in consumos)
                {
                    cantidadTotal += c.cantidad;
                }
                dt.Rows.Add(ins.id, ins.descripcion, totalConsumos, cantidadTotal);
            }

            reportViewer1.LocalReport.ReportPath = "ReporteInsumos.rdlc";
            reportViewer1.LocalReport.DataSources.Clear();

            ReportDataSource rds = new ReportDataSource("DataSet1", dt);
            reportViewer1.LocalReport.DataSources.Add(rds);

            reportViewer1.RefreshReport();
        }
    }
}