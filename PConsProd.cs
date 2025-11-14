using Modelos;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace EjercicioAdministracion2
{
    public partial class PConsProd : Form
    {
        List<A_DtoConsProd> consumosP = new List<A_DtoConsProd>();
        A_DtoConsProd consumoSeleccionado = null;

        public PConsProd()
        {
            InitializeComponent();
        }

        private void PConsProd_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'lP1DataSet.ConsumoProduccion' Puede moverla o quitarla según sea necesario.
            this.consumoProduccionTableAdapter.Fill(this.lP1DataSet.ConsumoProduccion);
            actualizarGrilla();
        }

        private void actualizarGrilla()
        {
            consumosP = NConsProd.GetDto();
            aDtoConsProdBindingSource.DataSource = null;
            aDtoConsProdBindingSource.DataSource = consumosP;
        }

        private void DataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (aDtoConsProdBindingSource.Current != null)
            {
                var seleccionado = (A_DtoConsProd)aDtoConsProdBindingSource.Current;
                txtConsInsu.Text = seleccionado.InsumoId.ToString();
                txtProdProd.Text = seleccionado.ProductoId.ToString();
                txtObserv.Text = seleccionado.Observaciones;
                consumoSeleccionado = seleccionado;
            }
            else
            {
                consumoSeleccionado = null;
                txtConsInsu.Clear();
                txtProdProd.Clear();
                txtObserv.Clear();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtConsInsu.Text != "" && txtProdProd.Text != "")
                {
                    ConsumoProduccion cp = new ConsumoProduccion()
                    {
                        consumoInsumo = new ConsumosInsumos { id = int.Parse(txtConsInsu.Text) },
                        produccionProducto = new ProduccionProductos { id = int.Parse(txtProdProd.Text) },
                        observaciones = txtObserv.Text
                    };

                    NConsProd.Create(cp);
                    actualizarGrilla();

                    MessageBox.Show("Registro agregado con éxito", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Debe completar los campos obligatorios", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"ERROR: {ex.Message}", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            txtConsInsu.Clear();
            txtProdProd.Clear();
            txtObserv.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (consumoSeleccionado == null)
                {
                    MessageBox.Show("Debe seleccionar un registro para eliminar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult result = MessageBox.Show("¿Está seguro de que desea eliminar el registro seleccionado?",
                    "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    NConsProd.Delete(consumoSeleccionado.Id);
                    actualizarGrilla();
                    MessageBox.Show("Registro eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    txtConsInsu.Clear();
                    txtProdProd.Clear();
                    txtObserv.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"ERROR: {ex.Message}", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (consumoSeleccionado == null)
                {
                    MessageBox.Show("Debe seleccionar un registro para modificar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (txtConsInsu.Text != "" && txtProdProd.Text != "")
                {
                    ConsumoProduccion cp = new ConsumoProduccion()
                    {
                        id = consumoSeleccionado.Id,
                        consumoInsumo = new ConsumosInsumos { id = int.Parse(txtConsInsu.Text) },
                        produccionProducto = new ProduccionProductos { id = int.Parse(txtProdProd.Text) },
                        observaciones = txtObserv.Text
                    };

                    NConsProd.Update(cp);
                    actualizarGrilla();

                    MessageBox.Show("Registro modificado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Debe completar los campos obligatorios.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"ERROR: {ex.Message}", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(txtFiltro.Text) && cbCampo.SelectedItem != null)
                {
                    string campo = cbCampo.SelectedItem.ToString();
                    string texto = txtFiltro.Text.ToLower();

                    var filtrados = consumosP.Where(x =>
                        (campo == "Producto" && x.Producto != null && x.Producto.ToLower().Contains(texto)) ||
                        (campo == "Insumo" && x.Insumo != null && x.Insumo.ToLower().Contains(texto)) ||
                        (campo == "Observaciones" && x.Observaciones != null && x.Observaciones.ToLower().Contains(texto)) ||
                        (campo == "ProductoId" && x.ProductoId.ToString().Contains(texto)) ||
                        (campo == "InsumoId" && x.InsumoId.ToString().Contains(texto))
                    ).ToList();

                    // 👉 ESTA LÍNEA FALTABA 👇
                    aDtoConsProdBindingSource.DataSource = filtrados;

                    if (filtrados.Count == 0)
                        MessageBox.Show("No se encontraron resultados para el filtro ingresado.", "Sin resultados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Seleccione un campo y escriba un texto para filtrar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"ERROR: {ex.Message}", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            actualizarGrilla();
            txtFiltro.Clear();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            RConsProd reporte = new RConsProd();
            reporte.Show();
        }
    }
}
