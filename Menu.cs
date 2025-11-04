using EjercicioAdministracion2;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EjercicioClinica
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void btnInsumos_Click(object sender, EventArgs e)
        {
            PInsumos formInsumos = new PInsumos();
            formInsumos.Show();
        }

        private void btnConsumos_Click(object sender, EventArgs e)
        {
            PConsumosInsumos formularioConsumos = new PConsumosInsumos();
            formularioConsumos.Show();
        }

        private void btnReporte_Click(object sender, EventArgs e)
        {
            FrmReporteInsumos formReporte = new FrmReporteInsumos();
            formReporte.Show();
        }
    }
}
