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
    public partial class PTipoComprobante : Form
    {
        List<TipoComprobante> listaTC = new List<TipoComprobante>();
        TipoComprobante TCSeleccionado = new TipoComprobante();
        public PTipoComprobante()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (tbTC.Text != "")
                {
                    string TipoComp = tbTC.Text;
                    TipoComprobante nuevotc = new TipoComprobante
                    {
                        description = TipoComp,
                    };
                    NTipoComprobantes.Create(nuevotc);
                    listaTC.Add(nuevotc);
                    tipoComprobanteBindingSource.DataSource = null;
                    tipoComprobanteBindingSource.DataSource = listaTC;
                    MessageBox.Show("Tipo de Comprobante agregado con exito", "EXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    tbTC.Clear();
                }
                else
                {
                    MessageBox.Show("Debe completar el campo", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error:{ex.Message}", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PTipoComprobante_Load(object sender, EventArgs e)
        {

            listaTC = NTipoComprobantes.Get();
            tipoComprobanteBindingSource.DataSource = listaTC;
        }

        private void button2_Click(object sender, EventArgs e)
        {

            try
            {
                if (tbTC.Text != "")
                {
                    string tcomp = tbTC.Text;
                    TCSeleccionado.description = tcomp;
                    NTipoComprobantes.UpDate(TCSeleccionado);
                    TipoComprobante tcActualizado = listaTC.Find(p => p.id == TCSeleccionado.id);
                    tcActualizado.description = TCSeleccionado.description;


                    tipoComprobanteBindingSource.DataSource = null;
                    tipoComprobanteBindingSource.DataSource = listaTC;
                    MessageBox.Show("Tipo de Comprobante modificado con exito", "EXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Debe completar todos los campos", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error:{ex.Message}", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (TCSeleccionado == null || TCSeleccionado.id == 0)
                {
                    MessageBox.Show("Seleccione un tipo de Comprobante válido.", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                DialogResult resultado = MessageBox.Show("¿Está seguro que desea eliminar el Tipo de Comprobante seleccionado?", "ADVERTENCIA", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (resultado == DialogResult.Yes)
                {
                    NTipoComprobantes.Delete(TCSeleccionado.id);
                    listaTC.RemoveAll(p => p.id == TCSeleccionado.id);
                    tipoComprobanteBindingSource.DataSource = null;
                    tipoComprobanteBindingSource.DataSource = listaTC;
                    
                    MessageBox.Show("Tipo de Comprobante eliminado con exito", "EXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error:{ex.Message}", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            TCSeleccionado = (TipoComprobante)tipoComprobanteBindingSource.Current;
            if (TCSeleccionado != null)
            {
                tbTC.Text = TCSeleccionado.description;
            }
        }
    }
}
