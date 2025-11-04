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
    public partial class PProveedores : Form
    {
        List<Proveedores> listaProveedores = new List<Proveedores>();
        Proveedores proveedorSeleccionado = new Proveedores();
        public PProveedores()
        {
            InitializeComponent();
        }

        private void PProveedores_Load(object sender, EventArgs e)
        {
            listaProveedores = NProveedores.Get();
            proveedoresBindingSource.DataSource = listaProveedores;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (tbNombre.Text != "" && tbCuit.Text != "")
                {
                    string nombre = tbNombre.Text;
                    string cuit = tbCuit.Text;
                    Proveedores nuevoProv = new Proveedores
                    {
                        nombreCompleto = nombre,
                        identificacionTributaria = cuit
                    };
                    NProveedores.Create(nuevoProv);
                    listaProveedores.Add(nuevoProv);
                    proveedoresBindingSource.DataSource = null;
                    proveedoresBindingSource.DataSource = listaProveedores;
                    MessageBox.Show("Proveedor agregado con exito", "EXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    tbNombre.Clear();
                    tbCuit.Clear();
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

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (tbNombre.Text != "" && tbCuit.Text != "")
                {
                    string nombre = tbNombre.Text;
                    string cuit = tbCuit.Text;
                    proveedorSeleccionado.nombreCompleto = nombre;
                    proveedorSeleccionado.identificacionTributaria = cuit;
                    NProveedores.UpDate(proveedorSeleccionado);
                    Proveedores provActualizado = listaProveedores.Find(p => p.id == proveedorSeleccionado.id);
                    provActualizado.nombreCompleto = proveedorSeleccionado.nombreCompleto;
                    provActualizado.identificacionTributaria = proveedorSeleccionado.identificacionTributaria;

                    proveedoresBindingSource.DataSource = null;
                    proveedoresBindingSource.DataSource = listaProveedores;
                    MessageBox.Show("Proveedor modificado con exito", "EXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                if (proveedorSeleccionado == null || proveedorSeleccionado.id == 0)
                {
                    MessageBox.Show("Seleccione un proveedor válido.", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                DialogResult resultado = MessageBox.Show("¿Está seguro que desea eliminar el proveedor seleccionado?", "ADVERTENCIA", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (resultado == DialogResult.Yes)
                {
                    NProveedores.Delete(proveedorSeleccionado.id);
                    listaProveedores.RemoveAll(p => p.id == proveedorSeleccionado.id);
                    proveedoresBindingSource.DataSource = null;
                    proveedoresBindingSource.DataSource = listaProveedores;
                    MessageBox.Show("Proveedor eliminado con exito", "EXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error:{ex.Message}", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            proveedorSeleccionado = (Proveedores)proveedoresBindingSource.Current;
            if (proveedorSeleccionado != null)
            {
                tbNombre.Text = proveedorSeleccionado.nombreCompleto;
                tbCuit.Text = proveedorSeleccionado.identificacionTributaria;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string nombreFiltro = tbFiltrar.Text.ToLower();
            proveedoresBindingSource.DataSource = null;
            proveedoresBindingSource.DataSource = listaProveedores.Where(p => p.nombreCompleto.ToLower().Contains(nombreFiltro) || p.identificacionTributaria.ToLower().Contains(nombreFiltro)).ToList();

            tbFiltrar.Clear();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            proveedoresBindingSource.DataSource = null;
            proveedoresBindingSource.DataSource = listaProveedores;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            PReporte pReporte = new PReporte(proveedorSeleccionado);
            pReporte.Show();
        }
    }
}
