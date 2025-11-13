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
    public partial class PCompras : Form
    {
        //public Proveedores proveedores;
        //public List<DtoCompras> dtoCompras = new List<DtoCompras> ();
        public PCompras()
        {
            InitializeComponent();
        }
        List<Compras> compras = new List<Compras>();
        Compras compraSeleccionada = new Compras();

        private void PCompras_Load(object sender, EventArgs e)
        {
            compras = NCompras.Get();
            comprasBindingSource.DataSource = compras;  
            //comprasBindingSource.DataSource = dtoCompras;
            //proveedoresBindingSource.DataSource = NProveedores.Get();
        }

        private void tbDescripcion_TextChanged(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (tbProveedor.Text != "" && tbComprobante.Text != "" && tbPuntoVenta.Text != "" && tbNumero.Text != "" && tbNeto.Text != "" && tbIva.Text != "" && tbGravado.Text != "" && tbTributos.Text != "" && tbUsuario.Text != "")
                {
                    DateTime fecha = tbFecha.Value;
                    int prov = int.Parse(tbProveedor.Text);
                    int tipoComp = int.Parse(tbComprobante.Text);
                    string puntoVenta = tbPuntoVenta.Text;
                    string numero = tbNumero.Text;
                    decimal netoTotal = decimal.Parse(tbNeto.Text.Replace(",", "."), System.Globalization.CultureInfo.InvariantCulture);
                    decimal ivaTotal = decimal.Parse(tbIva.Text.Replace(",", "."), System.Globalization.CultureInfo.InvariantCulture);
                    decimal noGravado = decimal.Parse(tbGravado.Text.Replace(",", "."), System.Globalization.CultureInfo.InvariantCulture);
                    decimal otrosTributos = decimal.Parse(tbTributos.Text.Replace(",", "."), System.Globalization.CultureInfo.InvariantCulture);
                    int usu = int.Parse(tbUsuario.Text);
                    Proveedores proveedor = new Proveedores { id = prov };
                    TipoComprobante tipoComprobante = new TipoComprobante { id = tipoComp };
                    Usuarios usuario = new Usuarios { id = usu};

                    Compras c = new Compras
                    {
                        fecha = fecha,
                        proveedor = proveedor,
                        tipoComprobante = tipoComprobante,
                        puntoVenta = puntoVenta,
                        numero = numero,
                        netoTotal = netoTotal,
                        ivaTotal = ivaTotal,
                        noGravado = noGravado,
                        otrosTributos = otrosTributos,
                        usuario = usuario,

                    };
                    NCompras.Create(c);
                    compras.Add(c);
                    comprasBindingSource.DataSource = null;
                    comprasBindingSource.DataSource = compras;
                    MessageBox.Show("Registro agregado", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    throw new Exception("No puede tener campos vacios");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show("¿Desea borrar el registro?", "Confirmacion Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    NCompras.Delete(compraSeleccionada.id);
                    MessageBox.Show("Registro eliminado", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    compras.RemoveAll(c => c.id == compraSeleccionada.id);
                    comprasBindingSource.DataSource = null;
                    comprasBindingSource.DataSource = compras;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            try
            {
                
                if (tbProveedor.Text != "" && tbComprobante.Text != "" &&
                    tbPuntoVenta.Text != "" && tbNumero.Text != "" && tbNeto.Text != "" && tbIva.Text != "" &&
                    tbGravado.Text != "" && tbTributos.Text != "" && tbUsuario.Text != "")
                {
                    


                    DateTime fecha = tbFecha.Value;
                    int proveedor = int.Parse(tbProveedor.Text);
                    int tipoComprobante = int.Parse(tbComprobante.Text);
                    string puntoVenta = tbPuntoVenta.Text;
                    string numero = tbNumero.Text;
                    decimal netoTotal = decimal.Parse(tbNeto.Text.Replace(",", "."), System.Globalization.CultureInfo.InvariantCulture);
                    decimal ivaTotal = decimal.Parse(tbIva.Text.Replace(",", "."), System.Globalization.CultureInfo.InvariantCulture);
                    decimal noGravado = decimal.Parse(tbGravado.Text.Replace(",", "."), System.Globalization.CultureInfo.InvariantCulture);
                    decimal otrosTributos = decimal.Parse(tbTributos.Text.Replace(",", "."), System.Globalization.CultureInfo.InvariantCulture);
                    int usuario = int.Parse(tbUsuario.Text);

                    compraSeleccionada.fecha = fecha;
                    compraSeleccionada.proveedor = new Proveedores { id = proveedor };
                    compraSeleccionada.tipoComprobante = new TipoComprobante { id = tipoComprobante };
                    compraSeleccionada.puntoVenta = puntoVenta;
                    compraSeleccionada.numero = numero;
                    compraSeleccionada.netoTotal = netoTotal;
                    compraSeleccionada.ivaTotal = ivaTotal;
                    compraSeleccionada.noGravado = noGravado;
                    compraSeleccionada.otrosTributos = otrosTributos;
                    compraSeleccionada.usuario = new Usuarios { id = usuario };

                    NCompras.Update(compraSeleccionada,compraSeleccionada.id);
                    Compras compra = compras.Find(c => c.id == compraSeleccionada.id);
                    compra.fecha = fecha;
                    compra.proveedor = compraSeleccionada.proveedor;
                    compra.tipoComprobante = compraSeleccionada.tipoComprobante;
                    compra.puntoVenta=puntoVenta;
                    compra.numero = numero; 
                    compra.netoTotal = netoTotal;
                    compra.ivaTotal = ivaTotal;
                    compra.noGravado = noGravado;
                    compra.otrosTributos= otrosTributos;
                    compra.usuario = compraSeleccionada.usuario;
                   
                    comprasBindingSource.DataSource = null;
                    comprasBindingSource.DataSource = compras;
                    

                    MessageBox.Show("Registro actualizado", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    throw new Exception("No puede tener campos vacíos");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_SelectionChanged_1(object sender, EventArgs e)
        {

            compraSeleccionada = (Compras)comprasBindingSource.Current;
            if (compraSeleccionada != null)
            {
                tbFecha.Value = compraSeleccionada.fecha;
                tbProveedor.Text = compraSeleccionada.proveedor.id.ToString();
                tbComprobante.Text = compraSeleccionada.tipoComprobante.id.ToString();
                tbPuntoVenta.Text = compraSeleccionada.puntoVenta;
                tbNumero.Text = compraSeleccionada.numero;
                tbNeto.Text = compraSeleccionada.netoTotal.ToString();
                tbIva.Text = compraSeleccionada.ivaTotal.ToString();
                tbGravado.Text = compraSeleccionada.noGravado.ToString();
                tbTributos.Text = compraSeleccionada.otrosTributos.ToString();
                tbUsuario.Text = compraSeleccionada.usuario.id.ToString();

            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            PComprasReporte reporteCompras = new PComprasReporte(compras); 
            reporteCompras.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (tbID.Text != "")
                {
                    int idBuscado = int.Parse(tbID.Text);
                    var comp = compras.FirstOrDefault(c => c.id == idBuscado);

                    if (comp != null)
                        comprasBindingSource.DataSource = new List<Compras> { comp };
                    else
                        MessageBox.Show("No se encontró una compra con ese ID.");
                }
                else
                {
                    MessageBox.Show("Ingrese un ID.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Ingrese un id válido", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            tbID.Text = String.Empty;
            comprasBindingSource.DataSource = compras;
            
        }
    }
}
