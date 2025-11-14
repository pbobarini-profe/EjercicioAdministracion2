using Modelos;
using Negocio;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace EjercicioAdministracion2
{
    public partial class PFormBase : Form
    {
        private nDetalleCompra objNegocio = new nDetalleCompra();
        List<DetalleCompras> detalleCompras=new List<DetalleCompras>();
        
        public PFormBase()
        {
            InitializeComponent();
        }



        private void CargarDetallesEnGrid()
        {
            try
            {
                detalleCompras = nDetalleCompra.Get();
                detalleComprasBindingSource3.DataSource = detalleCompras;


            }
            catch (Exception ex)
            {
               MessageBox.Show("Error al cargar los datos en el grid: " + ex.Message, "Error de Carga", MessageBoxButtons.OK, MessageBoxIcon.Error);
           }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(tbCompra.Text) ||
                    string.IsNullOrWhiteSpace(tbCantidad.Text) ||
                    string.IsNullOrWhiteSpace(tbInsumo.Text) ||
                    string.IsNullOrWhiteSpace(tbPrecio.Text) ||
                    string.IsNullOrWhiteSpace(tbUsuario.Text))
                {
                    MessageBox.Show("Complete todos los campos.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DetalleCompras detalle = new DetalleCompras
                {
                    compra = new Compras { id = int.Parse(tbCompra.Text) },
                    cantidad = decimal.Parse(tbCantidad.Text),
                    insumo = new Insumos { id = int.Parse(tbInsumo.Text) },
                    precioUnitario = decimal.Parse(tbPrecio.Text),
                    usuario = new Usuarios { id = int.Parse(tbUsuario.Text) }
                };

                
                objNegocio.Create(detalle);
                CargarDetallesEnGrid();


                MessageBox.Show("Detalle agregado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (FormatException)
            {
                MessageBox.Show("Asegúrese de que todos los campos numéricos (ID's, Cantidad, Precio) tengan un formato numérico válido.", "Error de Formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar detalle: " + ex.Message, "Error de Guardado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void PFormBase_Load(object sender, EventArgs e)
        {
            //CargarDetallesEnGrid();
            detalleCompras =nDetalleCompra.Get();
            detalleComprasBindingSource3.DataSource = detalleCompras;
            

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {

                if (string.IsNullOrWhiteSpace(tbDetalleCompra.Text))
                {
                    MessageBox.Show("Debe seleccionar o ingresar el ID del detalle a eliminar (tbIDDetalle).");
                    return;
                }

                int id = int.Parse(tbDetalleCompra.Text);

                objNegocio.Delete(id);
                detalleComprasBindingSource3.DataSource = null;

                detalleComprasBindingSource3.DataSource = detalleCompras;
                CargarDetallesEnGrid();
                MessageBox.Show("Detalle eliminado correctamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar: " + ex.Message);
            }
            

        }

        private void button3_Click(object sender, EventArgs e)
        {

            try
            {

                if (string.IsNullOrWhiteSpace(tbDetalleCompra.Text))
                {
                    MessageBox.Show("Debe seleccionar o ingresar el ID del detalle a modificar (tbIDDetalle).");
                    return;
                }


                DetalleCompras detallenuevo = new DetalleCompras
                {

                    id = int.Parse(tbDetalleCompra.Text),


                    compra = new Compras { id = int.Parse(tbCompra.Text) },

                    cantidad = decimal.Parse(tbCantidad.Text),
                    insumo = new Insumos { id = int.Parse(tbInsumo.Text) },
                    precioUnitario = decimal.Parse(tbPrecio.Text),
                    usuario = new Usuarios { id = int.Parse(tbUsuario.Text) }
                };

                objNegocio.Update(detallenuevo);
                detalleComprasBindingSource3.DataSource = null;

                detalleComprasBindingSource3.DataSource = detalleCompras;
                CargarDetallesEnGrid();
                MessageBox.Show("Detalle modificado correctamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar: " + ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(tbCompra.Text))
                {
                    MessageBox.Show("Ingrese un ID de Compra para filtrar.");
                    return;
                }

                int compraId = int.Parse(tbCompra.Text);

                //var lista = objNegocio.GetAll();
                //dataGridView1.DataSource = lista.Where(x => x.compra.id == compraId).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al filtrar: " + ex.Message);
            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && detalleComprasBindingSource3.Current != null)
            {
                DetalleCompras detalleSeleccionado = (DetalleCompras)detalleComprasBindingSource3.Current;


                tbDetalleCompra.Text = detalleSeleccionado.id.ToString();


                tbCompra.Text = detalleSeleccionado.compra.id.ToString();
                tbCantidad.Text = detalleSeleccionado.cantidad.ToString();
                tbInsumo.Text = detalleSeleccionado.insumo.id.ToString();
                tbPrecio.Text = detalleSeleccionado.precioUnitario.ToString();
                tbUsuario.Text = detalleSeleccionado.usuario.id.ToString();
            }
        }


        private void button5_Click_1(object sender, EventArgs e)
        {
            //CargarDetallesEnGrid();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            wreport reporte= new wreport(detalleCompras);
            reporte.Show();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
