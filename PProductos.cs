using System;
using System.Windows.Forms;
using Negocio;
using Modelos;

namespace EjercicioAdministracion2
{
    public partial class FormProductos : PFormBase
    {
        private ProductosNegocio productosNegocio;
        private Producto productoSeleccionado;

        public FormProductos()
        {
            InitializeComponent();
            productosNegocio = new ProductosNegocio();
            productoSeleccionado = null;

            // Configurar eventos de los controles heredados
            ConfigurarEventos();
        }

        private void ConfigurarEventos()
        {
            // Configurar textos de botones
            button1.Text = "Agregar";
            button2.Text = "Baja";
            button3.Text = "Modificar";
            button4.Text = "Filtrar";
            button5.Text = "Desfiltrar";

            // Asignar eventos
            button1.Click += btnAgregar_Click;
            button2.Click += btnBaja_Click;
            button3.Click += btnModificar_Click;
            button4.Click += btnFiltrar_Click;
            button5.Click += btnDesfiltrar_Click;
            dataGridView1.SelectionChanged += dgv_SelectionChanged;
            this.Load += FormProductos_Load;
        }

        private void FormProductos_Load(object sender, EventArgs e)
        {
            this.Text = "Gestión de Productos";
            ConfigurarDataGridView();
            ConfigurarCampos();
            CargarProductos();
            LimpiarCampos();
        }

        private void ConfigurarCampos()
        {
            // Configurar labels y campos para productos
            // tbMin y tbMax serán filtros
            // tbDescripcion y tbPrecio se usarán para entrada de datos

            // Necesitamos agregar más controles para todos los campos
            // Como heredamos de PFormBase, solo usamos los que ya existen
            // Puedes agregar más en el Designer si es necesario
        }

        private void ConfigurarDataGridView()
        {
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;

            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "IdProducto",
                HeaderText = "ID",
                DataPropertyName = "IdProducto",
                Width = 50
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Nombre",
                HeaderText = "Nombre",
                DataPropertyName = "Nombre",
                Width = 150
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Descripcion",
                HeaderText = "Descripción",
                DataPropertyName = "Descripcion",
                Width = 200
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "PrecioUnitario",
                HeaderText = "Precio",
                DataPropertyName = "PrecioUnitario",
                Width = 80,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" }
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Stock",
                HeaderText = "Stock",
                DataPropertyName = "Stock",
                Width = 70
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "StockMinimo",
                HeaderText = "Stock Mín.",
                DataPropertyName = "StockMinimo",
                Width = 80
            });
            dataGridView1.Columns.Add(new DataGridViewCheckBoxColumn
            {
                Name = "Activo",
                HeaderText = "Activo",
                DataPropertyName = "Activo",
                Width = 60
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "FechaCreacion",
                HeaderText = "Fecha",
                DataPropertyName = "FechaCreacion",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" }
            });
        }

        private void CargarProductos()
        {
            try
            {
                var resultado = productosNegocio.ObtenerTodos();

                if (resultado.exito)
                {
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = resultado.productos;
                }
                else
                {
                    MessageBox.Show(resultado.mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar productos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                // Usar un formulario de diálogo para capturar todos los datos
                using (FormProductoDetalle form = new FormProductoDetalle())
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        var resultado = productosNegocio.Insertar(form.ProductoActual);

                        if (resultado.exito)
                        {
                            MessageBox.Show(resultado.mensaje, "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            CargarProductos();
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
                MessageBox.Show("Error al agregar producto: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                if (productoSeleccionado == null)
                {
                    MessageBox.Show("Debe seleccionar un producto de la grilla.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (FormProductoDetalle form = new FormProductoDetalle(productoSeleccionado))
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        var resultado = productosNegocio.Actualizar(form.ProductoActual);

                        if (resultado.exito)
                        {
                            MessageBox.Show(resultado.mensaje, "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            CargarProductos();
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
                MessageBox.Show("Error al modificar producto: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBaja_Click(object sender, EventArgs e)
        {
            try
            {
                if (productoSeleccionado == null)
                {
                    MessageBox.Show("Debe seleccionar un producto de la grilla.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult confirmacion = MessageBox.Show(
                    $"¿Está seguro que desea eliminar el producto '{productoSeleccionado.Nombre}'?",
                    "Confirmar eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (confirmacion == DialogResult.Yes)
                {
                    var resultado = productosNegocio.Eliminar(productoSeleccionado.IdProducto);

                    if (resultado.exito)
                    {
                        MessageBox.Show(resultado.mensaje, "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarProductos();
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
                MessageBox.Show("Error al eliminar producto: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            try
            {
                string filtro = tbMin.Text.Trim();

                if (string.IsNullOrWhiteSpace(filtro))
                {
                    CargarProductos();
                    return;
                }

                var resultado = productosNegocio.BuscarPorNombre(filtro);

                if (resultado.exito)
                {
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = resultado.productos;
                }
                else
                {
                    MessageBox.Show(resultado.mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            CargarProductos();
        }

        private void dgv_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                productoSeleccionado = (Producto)dataGridView1.SelectedRows[0].DataBoundItem;
                CargarDatosProducto();
            }
        }

        private void CargarDatosProducto()
        {
            if (productoSeleccionado != null)
            {
                tbDescripcion.Text = productoSeleccionado.Nombre;
                tbPrecio.Text = productoSeleccionado.PrecioUnitario.ToString();
            }
        }

        private void LimpiarCampos()
        {
            tbDescripcion.Clear();
            tbPrecio.Clear();
            productoSeleccionado = null;

            if (dataGridView1.Rows.Count > 0)
                dataGridView1.ClearSelection();
        }
    }
}