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

        private void proveedoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PProveedores pProveedores = new PProveedores();
            pProveedores.Show();
        }

        private void tipoDeComprobanetesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PTipoComprobante pTipoComprobante = new PTipoComprobante();
            pTipoComprobante.Show();
        }
    }
}
