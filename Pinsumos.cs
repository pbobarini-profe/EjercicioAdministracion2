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
    public partial class PInsumos : Form
    {
        List<Insumos> insumos = new List<Insumos>();
        Insumos insumoSeleccionado = new Insumos();

        public PInsumos()
        {
            InitializeComponent();
        }

        private void PInsumos_Load(object sender, EventArgs e)
        {
            insumos = NInsumos.Get();
            insumosBindingSource.DataSource = insumos;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (tbDescripcion.Text != "")
                {
                    string descripcion = tbDescripcion.Text;
                    Insumos ins = new Insumos
                    {
                        descripcion = descripcion
                    };
                    NInsumos.Create(ins);
                    insumos.Add(ins);
                    insumosBindingSource.DataSource = null;
                    insumosBindingSource.DataSource = insumos;
                    MessageBox.Show("Registro agregado", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tbDescripcion.Clear();
                }
                else
                {
                    throw new Exception("No puede tener campos vacios.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                if (tbDescripcion.Text != "")
                {
                    string descripcion = tbDescripcion.Text;
                    insumoSeleccionado.descripcion = descripcion;
                    NInsumos.Update(insumoSeleccionado);
                    Insumos ins = insumos.Find(i => i.id == insumoSeleccionado.id);
                    ins.descripcion = insumoSeleccionado.descripcion;
                    insumosBindingSource.DataSource = null;
                    insumosBindingSource.DataSource = insumos;
                    MessageBox.Show("Registro modificado", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    throw new Exception("No puede tener campos vacios.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show(
                    "Desea borrar el registro?",
                    "Confirmacion Eliminar",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                    );
                if (result == DialogResult.Yes)
                {
                    NInsumos.Delete(insumoSeleccionado.id);
                    MessageBox.Show("Registro eliminado", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    insumos.RemoveAll(i => i.id == insumoSeleccionado.id);
                    insumosBindingSource.DataSource = null;
                    insumosBindingSource.DataSource = insumos;
                    tbDescripcion.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvInsumos_SelectionChanged(object sender, EventArgs e)
        {
            insumoSeleccionado = (Insumos)insumosBindingSource.Current;
            if (insumoSeleccionado != null)
            {
                tbDescripcion.Text = insumoSeleccionado.descripcion;
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string busqueda = tbBuscar.Text;
            insumosBindingSource.DataSource = null;
            insumosBindingSource.DataSource = insumos.Where(i => i.descripcion.Contains(busqueda));
        }

        private void btnMostrarTodos_Click(object sender, EventArgs e)
        {
            insumosBindingSource.DataSource = null;
            insumosBindingSource.DataSource = insumos;
        }
    }
}