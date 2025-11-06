using EjercicioAdministracion2;
using Modelos;
using Negocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace EjercicioClinica
{
    public partial class Menu : Form
    {
        public List<DetalleVentas> DetVentas = new List<DetalleVentas>();
        private Ventas venta;

        public Menu()
        {
            InitializeComponent();
            venta = null;
        }

        public Menu(Ventas ventaSeleccionada)
        {
            InitializeComponent();
            venta = ventaSeleccionada;
        }

        private void Menu_Load(object sender, EventArgs e)
        {
            List<Ventas> ventas = NVentas.GetAll();
            ComboBoxVentas.DataSource = ventas;
            ComboBoxVentas.DisplayMember = "numero";
            ComboBoxVentas.ValueMember = "id";

            productosBindingSource.DataSource = NProducto.GetAll();

            List<Usuarios> usuarios = NUsuario.GetAll();
            ComboBoxUsuarios.DataSource = usuarios;
            ComboBoxUsuarios.DisplayMember = "nombre";
            ComboBoxUsuarios.ValueMember = "id";

            if (venta != null)
            {
                ComboBoxVentas.SelectedValue = venta.id;
                DetVentas = NDetalleVenta.GetById(venta);
                detalleVentasBindingSource.DataSource = DetVentas;
            }
            else
            {
                DetVentas = NDetalleVenta.GetAll();
                detalleVentasBindingSource.DataSource = DetVentas;
            }
        }

        private void comboBoxVentas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ComboBoxVentas.SelectedItem != null)
            {
                venta = (Ventas)ComboBoxVentas.SelectedItem;

                DetVentas = NDetalleVenta.GetById(venta);
                detalleVentasBindingSource.DataSource = DetVentas;
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (tbCantidad.Text != "" && tbPrecio.Text != "" && ComboBoxVentas.SelectedItem != null && ComboBoxUsuarios.SelectedItem != null)
                {
                    Productos producto = (Productos)comboBox1.SelectedItem;
                    Ventas ventaSeleccionada = (Ventas)ComboBoxVentas.SelectedItem;
                    Usuarios usuarioSeleccionado = (Usuarios)ComboBoxUsuarios.SelectedItem;
                    decimal cantidad = decimal.Parse(tbCantidad.Text);
                    decimal precio = decimal.Parse(tbPrecio.Text);

                    DetalleVentas detalle = new DetalleVentas
                    {
                        venta = ventaSeleccionada,
                        producto = producto,
                        cantidad = cantidad,
                        precioUnitario = precio,
                        usuario = usuarioSeleccionado 
                    };

                    NDetalleVenta.Create(detalle);
                    DetVentas.Add(detalle);
                    detalleVentasBindingSource.DataSource = null;
                    detalleVentasBindingSource.DataSource = DetVentas;

                    MessageBox.Show("Detalle de venta agregado correctamente.", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    throw new Exception("Debe completar todos los campos y seleccionar venta, producto y usuario.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (detalleVentasBindingSource.Current is DetalleVentas detalleSeleccionado)
            {
                DialogResult result = MessageBox.Show(
                    "¿Seguro que desea eliminar este detalle?",
                    "Confirmar eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    NDetalleVenta.Delete(detalleSeleccionado.id);
                    DetVentas.Remove(detalleSeleccionado);
                    detalleVentasBindingSource.DataSource = null;
                    detalleVentasBindingSource.DataSource = DetVentas;
                }
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            if (detalleVentasBindingSource.Current is DetalleVentas detalleSeleccionado)
            {
                if (tbCantidad.Text != "" && tbPrecio.Text != "")
                {
                    detalleSeleccionado.cantidad = decimal.Parse(tbCantidad.Text);
                    detalleSeleccionado.precioUnitario = decimal.Parse(tbPrecio.Text);
                    NDetalleVenta.Update(detalleSeleccionado);

                    detalleVentasBindingSource.DataSource = null;
                    detalleVentasBindingSource.DataSource = DetVentas;

                    MessageBox.Show("Detalle actualizado correctamente.", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Complete los campos de cantidad y precio.", "Atención",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                decimal min = 0;
                decimal max = decimal.MaxValue;

                if (!string.IsNullOrWhiteSpace(tbMin.Text))
                    min = decimal.Parse(tbMin.Text.Replace(",", "."));
                if (!string.IsNullOrWhiteSpace(tbMax.Text))
                    max = decimal.Parse(tbMax.Text.Replace(",", "."));

                detalleVentasBindingSource.DataSource = null;
                detalleVentasBindingSource.DataSource = DetVentas
                    .Where(d => d.precioUnitario >= min && d.precioUnitario <= max)
                    .ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al filtrar precios: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                tbMin.Text = "";
                tbMax.Text = "";

                detalleVentasBindingSource.DataSource = null;
                detalleVentasBindingSource.DataSource = DetVentas;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al restablecer el filtro: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (ComboBoxVentas.SelectedItem != null)
            {
                Ventas ventaSeleccionada = (Ventas)ComboBoxVentas.SelectedItem;
                PDetalleVentaReporte reporte = new PDetalleVentaReporte(ventaSeleccionada);
                reporte.Show();
            }
            else
            {
                MessageBox.Show("Debe seleccionar una venta para generar el reporte.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }



        }

    }
}
