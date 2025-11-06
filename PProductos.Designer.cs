namespace Presentacion
{
    partial class PProductos
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblId;
        private System.Windows.Forms.Label lblDescripcion;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.Button btnInsertar;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.DataGridView dgvProductos;
        private System.Windows.Forms.GroupBox gbDatos;
        private System.Windows.Forms.GroupBox gbListado;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblId = new System.Windows.Forms.Label();
            this.lblDescripcion = new System.Windows.Forms.Label();
            this.txtId = new System.Windows.Forms.TextBox();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.btnInsertar = new System.Windows.Forms.Button();
            this.btnActualizar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.dgvProductos = new System.Windows.Forms.DataGridView();
            this.gbDatos = new System.Windows.Forms.GroupBox();
            this.gbListado = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductos)).BeginInit();
            this.gbDatos.SuspendLayout();
            this.gbListado.SuspendLayout();
            this.SuspendLayout();

            
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.Location = new System.Drawing.Point(250, 20);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(200, 26);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Gestión de Productos";

            
            this.gbDatos.Controls.Add(this.lblId);
            this.gbDatos.Controls.Add(this.txtId);
            this.gbDatos.Controls.Add(this.lblDescripcion);
            this.gbDatos.Controls.Add(this.txtDescripcion);
            this.gbDatos.Controls.Add(this.btnInsertar);
            this.gbDatos.Controls.Add(this.btnActualizar);
            this.gbDatos.Controls.Add(this.btnEliminar);
            this.gbDatos.Controls.Add(this.btnLimpiar);
            this.gbDatos.Location = new System.Drawing.Point(20, 60);
            this.gbDatos.Name = "gbDatos";
            this.gbDatos.Size = new System.Drawing.Size(350, 250);
            this.gbDatos.TabIndex = 1;
            this.gbDatos.TabStop = false;
            this.gbDatos.Text = "Datos del Producto";

            
            this.lblId.AutoSize = true;
            this.lblId.Location = new System.Drawing.Point(20, 30);
            this.lblId.Name = "lblId";
            this.lblId.Size = new System.Drawing.Size(21, 13);
            this.lblId.TabIndex = 0;
            this.lblId.Text = "ID:";

           
            this.txtId.Enabled = false;
            this.txtId.Location = new System.Drawing.Point(120, 27);
            this.txtId.Name = "txtId";
            this.txtId.Size = new System.Drawing.Size(200, 20);
            this.txtId.TabIndex = 1;

           
            this.lblDescripcion.AutoSize = true;
            this.lblDescripcion.Location = new System.Drawing.Point(20, 70);
            this.lblDescripcion.Name = "lblDescripcion";
            this.lblDescripcion.Size = new System.Drawing.Size(66, 13);
            this.lblDescripcion.TabIndex = 2;
            this.lblDescripcion.Text = "Descripción:";

          
            this.txtDescripcion.Location = new System.Drawing.Point(120, 67);
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(200, 20);
            this.txtDescripcion.TabIndex = 3;

          
            this.btnInsertar.Location = new System.Drawing.Point(20, 120);
            this.btnInsertar.Name = "btnInsertar";
            this.btnInsertar.Size = new System.Drawing.Size(75, 30);
            this.btnInsertar.TabIndex = 4;
            this.btnInsertar.Text = "Insertar";
            this.btnInsertar.UseVisualStyleBackColor = true;

          
            this.btnActualizar.Location = new System.Drawing.Point(110, 120);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(75, 30);
            this.btnActualizar.TabIndex = 5;
            this.btnActualizar.Text = "Actualizar";
            this.btnActualizar.UseVisualStyleBackColor = true;

          
            this.btnEliminar.Location = new System.Drawing.Point(200, 120);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(75, 30);
            this.btnEliminar.TabIndex = 6;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = true;

         
            this.btnLimpiar.Location = new System.Drawing.Point(20, 170);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(75, 30);
            this.btnLimpiar.TabIndex = 7;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = true;

            
            this.gbListado.Controls.Add(this.dgvProductos);
            this.gbListado.Location = new System.Drawing.Point(390, 60);
            this.gbListado.Name = "gbListado";
            this.gbListado.Size = new System.Drawing.Size(450, 450);
            this.gbListado.TabIndex = 2;
            this.gbListado.TabStop = false;
            this.gbListado.Text = "Listado de Productos";

           
            this.dgvProductos.AllowUserToAddRows = false;
            this.dgvProductos.AllowUserToDeleteRows = false;
            this.dgvProductos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProductos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvProductos.Location = new System.Drawing.Point(3, 16);
            this.dgvProductos.Name = "dgvProductos";
            this.dgvProductos.ReadOnly = true;
            this.dgvProductos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProductos.Size = new System.Drawing.Size(444, 431);
            this.dgvProductos.TabIndex = 0;

            
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(860, 530);
            this.Controls.Add(this.gbListado);
            this.Controls.Add(this.gbDatos);
            this.Controls.Add(this.lblTitulo);
            this.Name = "PProductos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Productos";
            this.Load += new System.EventHandler(this.PProductos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductos)).EndInit();
            this.gbDatos.ResumeLayout(false);
            this.gbDatos.PerformLayout();
            this.gbListado.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}