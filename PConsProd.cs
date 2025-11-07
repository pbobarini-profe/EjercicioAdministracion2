//CREATE TABLE dbo.Productos ( id INT IDENTITY(1,1) NOT NULL PRIMARY KEY, descripcion NVARCHAR(200) NULL ); GO

//CREATE TABLE dbo.Insumos ( id INT IDENTITY(1,1) NOT NULL PRIMARY KEY, descripcion NVARCHAR(200) NULL ); GO

//CREATE TABLE dbo.ProduccionProductos ( id INT IDENTITY(1,1) NOT NULL PRIMARY KEY, fecha DATETIME2(0) NOT NULL, 
//productoId INT NOT NULL, cantidad DECIMAL(18,3) NOT NULL, 
//CONSTRAINT FK_ProduccionProductos_Productos FOREIGN KEY (productoId) 
//REFERENCES dbo.Productos(id) ); GO

//CREATE TABLE dbo.ConsumosInsumos ( id INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
//fecha DATETIME2(0) NOT NULL, cantidad DECIMAL(18,3) NOT NULL, insumoId INT NOT NULL, 
//CONSTRAINT FK_ConsumosInsumos_Insumos FOREIGN KEY (insumoId)
//REFERENCES dbo.Insumos(id) ); GO

//CREATE TABLE dbo.ConsumoProduccion ( id INT IDENTITY(1,1) NOT NULL PRIMARY KEY, consumoInsumoId INT NOT NULL, 
//produccionProductoId INT NOT NULL, observaciones NVARCHAR(MAX) NULL, 
//CONSTRAINT FK_ConsumoProduccion_ConsumosInsumos FOREIGN KEY (consumoInsumoId) REFERENCES dbo.ConsumosInsumos(id), 
//CONSTRAINT FK_ConsumoProduccion_ProduccionProductos 
//FOREIGN KEY (produccionProductoId)
//REFERENCES dbo.ProduccionProductos(id) ); GO

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
    public partial class PConsProd : Form
    {
        List<ConsumoProduccion> consumosP = new List<ConsumoProduccion>();
        ConsumoProduccion consumoSeleccionado = null;

        public PConsProd()
        {
            InitializeComponent();
        }

        private void PConsProd_Load(object sender, EventArgs e)
        {
            ConsumosInsumos consumoInsumo = new ConsumosInsumos();
            ProduccionProductos produccionProducto= new ProduccionProductos();
            consumosP = NConsProd.Get();
            consumoProduccionBindingSource.DataSource = null;
            consumoProduccionBindingSource.DataSource = consumosP;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtConsInsu.Text != "" && txtProdProd.Text != "")
                {
                    string observaciones = txtObserv.Text;
                    ConsumoProduccion cp = new ConsumoProduccion()
                    {
                        consumoInsumo = new ConsumosInsumos
                        {
                            id = int.Parse(txtConsInsu.Text)
                        },
                        produccionProducto = new ProduccionProductos
                        {
                            id = int.Parse(txtProdProd.Text)
                        },
                        observaciones = observaciones
                    };
                    NConsProd.Create(cp);
                    consumosP.Add(cp);
                    consumoProduccionBindingSource.DataSource = null;
                    consumoProduccionBindingSource.DataSource = consumosP;
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
                DialogResult result = MessageBox.Show("¿Está seguro de que desea eliminar el registro seleccionado?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    NConsProd.Delete(consumoSeleccionado.id);
                    consumosP.RemoveAll(cp => cp.id == consumoSeleccionado.id);
                    consumoProduccionBindingSource.DataSource = null;
                    consumoProduccionBindingSource.DataSource = consumosP;
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

        private void DataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (consumoProduccionBindingSource.Current != null)
            {
                consumoSeleccionado = (ConsumoProduccion)consumoProduccionBindingSource.Current;
                txtConsInsu.Text = consumoSeleccionado.consumoInsumo.id.ToString();
                txtProdProd.Text = consumoSeleccionado.produccionProducto.id.ToString();
                txtObserv.Text = consumoSeleccionado.observaciones;
            }
            else
            {
                consumoSeleccionado = null;
                txtConsInsu.Clear();
                txtProdProd.Clear();
                txtObserv.Clear();
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
                    consumoSeleccionado.consumoInsumo.id = int.Parse(txtConsInsu.Text);
                    consumoSeleccionado.produccionProducto.id = int.Parse(txtProdProd.Text);
                    consumoSeleccionado.observaciones = txtObserv.Text;
                    NConsProd.Update(consumoSeleccionado);
                    ConsumoProduccion cp = consumosP.Find(x => x.id == consumoSeleccionado.id);
                    if (cp != null)
                    {
                        cp.consumoInsumo.id = consumoSeleccionado.consumoInsumo.id;
                        cp.produccionProducto.id = consumoSeleccionado.produccionProducto.id;
                        cp.observaciones = consumoSeleccionado.observaciones;
                    }
                    consumoProduccionBindingSource.DataSource = null;
                    consumoProduccionBindingSource.DataSource = consumosP;
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
                if (txtFiltro.Text != "")
                {
                    int idFiltro = int.Parse(txtFiltro.Text);
                    consumoProduccionBindingSource.DataSource = null;
                    consumoProduccionBindingSource.DataSource = consumosP.Where(cp => cp.produccionProducto.id == idFiltro).ToList();
                }
                else
                {
                    MessageBox.Show("Ingrese un ID de Producción para filtrar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"ERROR: {ex.Message}", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            consumoProduccionBindingSource.DataSource = null;
            consumoProduccionBindingSource.DataSource = consumosP;
            txtFiltro.Clear();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            RConsProd reporte = new RConsProd();
            reporte.Show();
        }
    }
}