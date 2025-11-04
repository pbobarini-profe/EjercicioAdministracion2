
using EjercicioAdministracion2;

namespace EjercicioClinica
{
    partial class Menu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()

        {
            this.btnInsumos = new System.Windows.Forms.Button();
            this.btnConsumos = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnInsumos
            // 
            this.btnInsumos.Location = new System.Drawing.Point(22, 35);
            this.btnInsumos.Name = "btnInsumos";
            this.btnInsumos.Size = new System.Drawing.Size(105, 37);
            this.btnInsumos.TabIndex = 0;
            this.btnInsumos.Text = "Ver insumos";
            this.btnInsumos.UseVisualStyleBackColor = true;
            this.btnInsumos.Click += new System.EventHandler(this.btnInsumos_Click);
            // 
            // btnConsumos
            // 
            this.btnConsumos.Location = new System.Drawing.Point(151, 35);
            this.btnConsumos.Name = "btnConsumos";
            this.btnConsumos.Size = new System.Drawing.Size(106, 45);
            this.btnConsumos.TabIndex = 1;
            this.btnConsumos.Text = "Ver Consumo Insumos";
            this.btnConsumos.UseVisualStyleBackColor = true;
            this.btnConsumos.Click += new System.EventHandler(this.btnConsumos_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(75, 106);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(130, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Generar Reporte";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnReporte_Click);
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(298, 141);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnConsumos);
            this.Controls.Add(this.btnInsumos);
            this.Name = "Menu";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnInsumos;
        private System.Windows.Forms.Button btnConsumos;
        private System.Windows.Forms.Button button1;
    }
}

