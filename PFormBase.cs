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
    public partial class PFormBase : Form
    {
        public PFormBase()
        {
            InitializeComponent();
        }

        private void PFormBase_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'practicaDataSet.Productos' Puede moverla o quitarla según sea necesario.
            this.productosTableAdapter.Fill(this.practicaDataSet.Productos);

        }
    }
}
