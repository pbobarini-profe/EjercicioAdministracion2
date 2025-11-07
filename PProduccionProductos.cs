
using Modelos;
using Negocio;
using System;
using System.Windows.Forms;

namespace Presentacion
{
    public partial class PProduccionProductos : Form
    {
        private NProduccionProductos negocioProduccion = new NProduccionProductos();
        private NProductos negocioProductos = new NProductos();
        private int idSeleccionado = 0;

        public PProduccionProductos()
        {
            InitializeComponent();
        }

        private void PProduccionProductos_Load(object sender, EventArgs e)
        {
            CargarGrilla();
            CargarComboProductos();
            LimpiarCampos();
            dtpFecha.Value = DateTime.Now;
        }
        private void btnReporte_Click(object sender, EventArgs e)
        {
            try
            {
                PProduccionProductosReporte formReporte = new PProduccionProductosReporte();
                formReporte.ShowDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al abrir el reporte: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CargarComboProductos()
        {
            try
            {
                cmbProducto.DataSource = null;
                cmbProducto.DataSource = negocioProductos.Listar();
                cmbProducto.DisplayMember = "descripcion";
                cmbProducto.ValueMember = "id";
                cmbProducto.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar productos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarGrilla()
        {
            try
            {
                dgvProduccion.DataSource = null;
                var lista = negocioProduccion.Listar();

                var listaVista = new System.Collections.Generic.List<object>();
                foreach (var item in lista)
                {
                    listaVista.Add(new
                    {
                        id = item.id,
                        fecha = item.fecha.ToString("dd/MM/yyyy"),
                        producto = item.producto.descripcion,
                        producto_id = item.producto.id,
                        cantidad = item.cantidad
                    });
                }

                dgvProduccion.DataSource = listaVista;
                dgvProduccion.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                if (dgvProduccion.Columns["producto_id"] != null)
                {
                    dgvProduccion.Columns["producto_id"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimpiarCampos()
        {
            txtId.Clear();
            dtpFecha.Value = DateTime.Now;
            cmbProducto.SelectedIndex = -1;
            txtCantidad.Clear();
            idSeleccionado = 0;
            dtpFecha.Focus();
        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbProducto.SelectedValue == null)
                {
                    MessageBox.Show("Debe seleccionar un producto", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                ProduccionProductos produccion = new ProduccionProductos
                {
                    fecha = dtpFecha.Value,
                    producto = new Productos { id = Convert.ToInt32(cmbProducto.SelectedValue) },
                    cantidad = Convert.ToDecimal(txtCantidad.Text)
                };

                string resultado = negocioProduccion.Insertar(produccion);
                MessageBox.Show(resultado, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (resultado.Contains("correctamente"))
                {
                    CargarGrilla();
                    LimpiarCampos();
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("La cantidad debe ser un número válido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                if (idSeleccionado == 0)
                {
                    MessageBox.Show("Debe seleccionar un registro de la grilla", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (cmbProducto.SelectedValue == null)
                {
                    MessageBox.Show("Debe seleccionar un producto", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                ProduccionProductos produccion = new ProduccionProductos
                {
                    id = idSeleccionado,
                    fecha = dtpFecha.Value,
                    producto = new Productos { id = Convert.ToInt32(cmbProducto.SelectedValue) },
                    cantidad = Convert.ToDecimal(txtCantidad.Text)
                };

                string resultado = negocioProduccion.Actualizar(produccion);
                MessageBox.Show(resultado, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (resultado.Contains("correctamente"))
                {
                    CargarGrilla();
                    LimpiarCampos();
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("La cantidad debe ser un número válido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (idSeleccionado == 0)
                {
                    MessageBox.Show("Debe seleccionar un registro de la grilla", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult confirmacion = MessageBox.Show("¿Está seguro de eliminar este registro?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (confirmacion == DialogResult.Yes)
                {
                    string resultado = negocioProduccion.Eliminar(idSeleccionado);
                    MessageBox.Show(resultado, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (resultado.Contains("correctamente"))
                    {
                        CargarGrilla();
                        LimpiarCampos();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void dgvProduccion_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow fila = dgvProduccion.Rows[e.RowIndex];
                    idSeleccionado = Convert.ToInt32(fila.Cells["id"].Value);
                    txtId.Text = fila.Cells["id"].Value.ToString();


                    string fechaStr = fila.Cells["fecha"].Value.ToString();
                    dtpFecha.Value = DateTime.ParseExact(fechaStr, "dd/MM/yyyy", null);


                    int productoId = Convert.ToInt32(fila.Cells["producto_id"].Value);
                    cmbProducto.SelectedValue = productoId;

                    txtCantidad.Text = fila.Cells["cantidad"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al seleccionar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}