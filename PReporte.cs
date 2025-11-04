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
    public partial class PReporte : Form
    {
        public Proveedores proveedorSeleccionado;
        public PReporte(Proveedores p)
        {
            InitializeComponent();
            proveedorSeleccionado = p;
        }

        private void PReporte_Load(object sender, EventArgs e)
        {
            List<DtoInforme> list;

            if (proveedorSeleccionado != null && proveedorSeleccionado.id > 0)
            {
                list = new List<DtoInforme>
                {
                    new DtoInforme
                    {
                        id = proveedorSeleccionado.id,
                        nombreCompleto = proveedorSeleccionado.nombreCompleto,
                        identificacionTributaria = proveedorSeleccionado.identificacionTributaria
                    }
                };
            }
            else
            {
                var proveedores = NProveedores.Get();
                list = proveedores.Select(p => new DtoInforme
                {
                    id = p.id,
                    nombreCompleto = p.nombreCompleto,
                    identificacionTributaria = p.identificacionTributaria
                }).ToList();
            }
            this.reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(
                new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", list)
            );
            this.reportViewer1.RefreshReport();
        }
    }
}
