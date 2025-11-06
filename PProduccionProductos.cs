using System;
using System.Windows.Forms;
using Negocio;
using Modelos;

namespace EjercicioAdministracion2
{
    public partial class FormProduccionProductos : PFormBase
    {
        private ProduccionProductosNegocio produccionNegocio;
        private ProductosNegocio productosNegocio;
        private ProduccionProductos produccionSeleccionada;

        public FormProduccionProductos()
        {
            InitializeComponent();
            produccionNegocio = new ProduccionProductosNegocio();
            productosNegocio = new ProductosNegocio();
            produccionSeleccionada = null;

            ConfigurarEventos();
        }

        private void ConfigurarEventos()
        {
            button1.Text = "Agregar";
            button2.Text = "Baja";
            button3.Text = "Modificar";
            button4.Text = "Filtrar";
            button5.Text = "Desfiltrar";

            button1.Click += btnAgregar_Click;
            button2.Click += btnBaja_Click;
            button3.Click += btnModificar_Click;
            button4.Click += btnFiltrar_Click;
            button5.Click += btnDesfiltrar_Click;
            dataGridView1.SelectionChanged += dgv_SelectionChanged;
            this.Load += FormProduccion_Load;
        }

        private void FormProduccion_Load(object sender, EventArgs e)
        {
            this.Text = "Gestión de Producción de Productos";
            ConfigurarDataGridView();
            CargarProducciones();
            LimpiarCampos();
        }

        private void ConfigurarDataGridView()
        {
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;

            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Id",
                HeaderText = "ID",
                DataPropertyName = "id",
                Width = 50
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "NombreProducto",
                HeaderText = "Producto",
                Width = 200
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Cantidad",
                HeaderText = "Cantidad",
                DataPropertyName = "cantidad",
                Width = 100
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Fecha",
                HeaderText = "Fecha Producción",
                DataPropertyName = "fecha",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy HH:mm" }
            });

            // Evento para mostrar el nombre del producto
            dataGridView1.CellFormatting += (s, e) =>
            {
                if (e.ColumnIndex == dataGridView1.Columns["NombreProducto"].Index && e.RowIndex >= 0)
                {
                    var produccion = dataGridView1.Rows[e.RowIndex].DataBoundItem as ProduccionProductos;
                    if (produccion != null && produccion.producto != null)
                    {
                        e.Value = produccion.producto.Nombre;
                    }
                }
            };
        }

        private void CargarProducciones()
        {
            try
            {
                var resultado = produccionNegocio.ObtenerTodas();

                if (resultado.exito)
                {
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = resultado.producciones;
                }
                else
                {
                    MessageBox.Show(resultado.mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar producciones: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                using (FormProduccionDetalle form = new FormProduccionDetalle(productosNegocio))
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        var resultado = produccionNegocio.Insertar(form.ProduccionActual);

                        if (resultado.exito)
                        {
                            MessageBox.Show(resultado.mensaje, "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            CargarProducciones();
                            LimpiarCampos();
                        }
                        else
                        {
                            MessageBox.Show(resultado.mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar producción: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                if (produccionSeleccionada == null)
                {
                    MessageBox.Show("Debe seleccionar una producción de la grilla.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                decimal cantidadAnterior = produccionSeleccionada.cantidad;

                using (FormProduccionDetalle form = new FormProduccionDetalle(productosNegocio, produccionSeleccionada))
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        var resultado = produccionNegocio.Actualizar(form.ProduccionActual, cantidadAnterior);

                        if (resultado.exito)
                        {
                            MessageBox.Show(resultado.mensaje, "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            CargarProducciones();
                            LimpiarCampos();
                        }
                        else
                        {
                            MessageBox.Show(resultado.mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar producción: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBaja_Click(object sender, EventArgs e)
        {
            try
            {
                if (produccionSeleccionada == null)
                {
                    MessageBox.Show("Debe seleccionar una producción de la grilla.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult confirmacion = MessageBox.Show(
                    $"¿Está seguro que desea eliminar la producción de '{produccionSeleccionada.producto.Nombre}'?\n" +
                    $"Se restarán {produccionSeleccionada.cantidad} unidades del stock.",
                    "Confirmar eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (confirmacion == DialogResult.Yes)
                {
                    var resultado = produccionNegocio.Eliminar(produccionSeleccionada.id);

                    if (resultado.exito)
                    {
                        MessageBox.Show(resultado.mensaje, "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarProducciones();
                        LimpiarCampos();
                    }
                    else
                    {
                        MessageBox.Show(resultado.mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar producción: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            try
            {
                // Filtro por fechas usando tbMin y tbMax
                DateTime fechaInicio, fechaFin;

                if (DateTime.TryParse(tbMin.Text, out fechaInicio) && DateTime.TryParse(tbMax.Text, out fechaFin))
                {
                    var resultado = produccionNegocio.ObtenerPorRangoFechas(fechaInicio, fechaFin);

                    if (resultado.exito)
                    {
                        dataGridView1.DataSource = null;
                        dataGridView1.DataSource = resultado.producciones;
                        MessageBox.Show(resultado.mensaje, "Filtro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show(resultado.mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Ingrese fechas válidas en formato dd/MM/yyyy", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al filtrar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDesfiltrar_Click(object sender, EventArgs e)
        {
            tbMin.Clear();
            tbMax.Clear();
            CargarProducciones();
        }

        private void dgv_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                produccionSeleccionada = (ProduccionProductos)dataGridView1.SelectedRows[0].DataBoundItem;
                CargarDatosProduccion();
            }
        }

        private void CargarDatosProduccion()
        {
            if (produccionSeleccionada != null)
            {
                tbDescripcion.Text = produccionSeleccionada.producto?.Nombre ?? "";
                tbPrecio.Text = produccionSeleccionada.cantidad.ToString();
            }
        }

        private void LimpiarCampos()
        {
            tbDescripcion.Clear();
            tbPrecio.Clear();
            produccionSeleccionada = null;

            if (dataGridView1.Rows.Count > 0)
                dataGridView1.ClearSelection();
        }
    }
}