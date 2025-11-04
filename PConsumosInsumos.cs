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
    public partial class PConsumosInsumos : Form
    {
        List<ConsumosInsumos> consumos = new List<ConsumosInsumos>();
        ConsumosInsumos consumoSeleccionado = new ConsumosInsumos();
        List<Insumos> insumos = new List<Insumos>();

        public PConsumosInsumos()
        {
            InitializeComponent();
        }

        private void PConsumosInsumos_Load(object sender, EventArgs e)
        {
            insumos = NInsumos.Get();
            cbInsumos.DataSource = insumos;
            cbInsumos.DisplayMember = "descripcion";
            cbInsumos.ValueMember = "id";

            consumos = NConsumosInsumos.Get();
            consumosInsumosBindingSource.DataSource = consumos;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbInsumos.SelectedItem != null && tbCantidad.Text != "")
                {
                    DateTime fecha = dtpFecha.Value;
                    decimal cantidad = decimal.Parse(tbCantidad.Text.Replace(",", "."), System.Globalization.CultureInfo.InvariantCulture);
                    Insumos insumo = (Insumos)cbInsumos.SelectedItem;

                    ConsumosInsumos c = new ConsumosInsumos
                    {
                        fecha = fecha,
                        cantidad = cantidad,
                        insumo = insumo
                    };
                    NConsumosInsumos.Create(c);
                    consumos.Add(c);
                    consumosInsumosBindingSource.DataSource = null;
                    consumosInsumosBindingSource.DataSource = consumos;
                    MessageBox.Show("Registro agregado", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tbCantidad.Clear();
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
                if (cbInsumos.SelectedItem != null && tbCantidad.Text != "")
                {
                    DateTime fecha = dtpFecha.Value;
                    decimal cantidad = decimal.Parse(tbCantidad.Text.Replace(",", "."), System.Globalization.CultureInfo.InvariantCulture);
                    Insumos insumo = (Insumos)cbInsumos.SelectedItem;

                    consumoSeleccionado.fecha = fecha;
                    consumoSeleccionado.cantidad = cantidad;
                    consumoSeleccionado.insumo = insumo;

                    NConsumosInsumos.Update(consumoSeleccionado);
                    ConsumosInsumos c = consumos.Find(con => con.id == consumoSeleccionado.id);
                    c.fecha = consumoSeleccionado.fecha;
                    c.cantidad = consumoSeleccionado.cantidad;
                    c.insumo = consumoSeleccionado.insumo;
                    consumosInsumosBindingSource.DataSource = null;
                    consumosInsumosBindingSource.DataSource = consumos;
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
                    NConsumosInsumos.Delete(consumoSeleccionado.id);
                    MessageBox.Show("Registro eliminado", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    consumos.RemoveAll(c => c.id == consumoSeleccionado.id);
                    consumosInsumosBindingSource.DataSource = null;
                    consumosInsumosBindingSource.DataSource = consumos;
                    tbCantidad.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvConsumos_SelectionChanged(object sender, EventArgs e)
        {
            consumoSeleccionado = (ConsumosInsumos)consumosInsumosBindingSource.Current;
            if (consumoSeleccionado != null && consumoSeleccionado.insumo != null)
            {
                dtpFecha.Value = consumoSeleccionado.fecha;
                tbCantidad.Text = consumoSeleccionado.cantidad.ToString();
                cbInsumos.SelectedValue = consumoSeleccionado.insumo.id;
            }
        }

        private void btnFiltrarPorInsumo_Click(object sender, EventArgs e)
        {
            if (cbInsumos.SelectedItem != null)
            {
                Insumos insumo = (Insumos)cbInsumos.SelectedItem;
                consumosInsumosBindingSource.DataSource = null;
                consumosInsumosBindingSource.DataSource = consumos.Where(c => c.insumo.id == insumo.id);
            }
        }

        private void btnMostrarTodos_Click(object sender, EventArgs e)
        {
            consumosInsumosBindingSource.DataSource = null;
            consumosInsumosBindingSource.DataSource = consumos;
        }

        private void btnFiltrarPorFecha_Click(object sender, EventArgs e)
        {
            DateTime fechaInicio = dtpFechaInicio.Value.Date;
            DateTime fechaFin = dtpFechaFin.Value.Date.AddDays(1).AddSeconds(-1);
            consumosInsumosBindingSource.DataSource = null;
            consumosInsumosBindingSource.DataSource = consumos.Where(c => c.fecha >= fechaInicio && c.fecha <= fechaFin);
        }
    }
}