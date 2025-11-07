using EjercicioAdministracion2;
using Modelos;
using Negocio;
using System;
using System.Windows.Forms;

namespace Presentacion
{
    public partial class PProductos : Form
    {
        private NProductos negocioProductos = new NProductos();
        private int idSeleccionado = 0;

        public PProductos()
        {
            InitializeComponent();
        }

        private void PProductos_Load(object sender, EventArgs e)
        {
            CargarGrilla();
            LimpiarCampos();
        }

        private void CargarGrilla()
        {
            try
            {
                dgvProductos.DataSource = null;
                dgvProductos.DataSource = negocioProductos.Listar();
                dgvProductos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimpiarCampos()
        {
            txtId.Clear();
            txtDescripcion.Clear();
            idSeleccionado = 0;
            txtDescripcion.Focus();
        }
        private void btnReporte_Click(object sender, EventArgs e)
        {
            try
            {
                PProductosReporte formReporte = new PProductosReporte();
                formReporte.ShowDialog(); 
                                          
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al abrir el reporte: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnInsertar_Click(object sender, EventArgs e)
        {
            try
            {
                Productos producto = new Productos
                {
                    descripcion = txtDescripcion.Text.Trim()
                };

                string resultado = negocioProductos.Insertar(producto);
                MessageBox.Show(resultado, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (resultado.Contains("correctamente"))
                {
                    CargarGrilla();
                    LimpiarCampos();
                }
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
                    MessageBox.Show("Debe seleccionar un producto de la grilla", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Productos producto = new Productos
                {
                    id = idSeleccionado,
                    descripcion = txtDescripcion.Text.Trim()
                };

                string resultado = negocioProductos.Actualizar(producto);
                MessageBox.Show(resultado, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (resultado.Contains("correctamente"))
                {
                    CargarGrilla();
                    LimpiarCampos();
                }
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
                    MessageBox.Show("Debe seleccionar un producto de la grilla", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult confirmacion = MessageBox.Show("¿Está seguro de eliminar este producto?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (confirmacion == DialogResult.Yes)
                {
                    string resultado = negocioProductos.Eliminar(idSeleccionado);
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

        private void dgvProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow fila = dgvProductos.Rows[e.RowIndex];
                    idSeleccionado = Convert.ToInt32(fila.Cells["id"].Value);
                    txtId.Text = fila.Cells["id"].Value.ToString();
                    txtDescripcion.Text = fila.Cells["descripcion"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al seleccionar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}