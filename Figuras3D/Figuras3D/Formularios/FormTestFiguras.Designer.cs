namespace Figuras3D.Formularios
{
    partial class FormTestFiguras
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.panelDibujo = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panelControles = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnCrearCubo = new System.Windows.Forms.Button();
            this.btnCrearEsfera = new System.Windows.Forms.Button();
            this.btnCrearCilindro = new System.Windows.Forms.Button();
            this.btnCrearCono = new System.Windows.Forms.Button();
            this.btnCrearPiramide = new System.Windows.Forms.Button();
            this.btnCambiarColor = new System.Windows.Forms.Button();
            this.btnEliminarFigura = new System.Windows.Forms.Button();
            this.btnLimpiarTodo = new System.Windows.Forms.Button();
            this.btnRotarX = new System.Windows.Forms.Button();
            this.btnRotarY = new System.Windows.Forms.Button();
            this.btnRotarZ = new System.Windows.Forms.Button();
            this.btnMoverX = new System.Windows.Forms.Button();
            this.btnMoverY = new System.Windows.Forms.Button();
            this.btnMoverZ = new System.Windows.Forms.Button();
            this.btnEscalarMas = new System.Windows.Forms.Button();
            this.lblInstrucciones = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panelControles.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelDibujo
            // 
            this.panelDibujo.BackColor = System.Drawing.Color.Black;
            this.panelDibujo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDibujo.Location = new System.Drawing.Point(0, 0);
            this.panelDibujo.Name = "panelDibujo";
            this.panelDibujo.Size = new System.Drawing.Size(700, 700);
            this.panelDibujo.TabIndex = 0;
            this.panelDibujo.Paint += new System.Windows.Forms.PaintEventHandler(this.PanelDibujo_Paint);
            this.panelDibujo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PanelDibujo_MouseDown);
            this.panelDibujo.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PanelDibujo_MouseMove);
            this.panelDibujo.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.PanelDibujo_MouseWheel);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panelDibujo);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panelControles);
            this.splitContainer1.Size = new System.Drawing.Size(1000, 700);
            this.splitContainer1.SplitterDistance = 700;
            this.splitContainer1.TabIndex = 1;
            // 
            // panelControles
            // 
            this.panelControles.BackColor = System.Drawing.Color.LightGray;
            this.panelControles.Controls.Add(this.flowLayoutPanel1);
            this.panelControles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControles.Location = new System.Drawing.Point(0, 0);
            this.panelControles.Name = "panelControles";
            this.panelControles.Size = new System.Drawing.Size(296, 700);
            this.panelControles.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Controls.Add(this.btnCrearCubo);
            this.flowLayoutPanel1.Controls.Add(this.btnCrearEsfera);
            this.flowLayoutPanel1.Controls.Add(this.btnCrearCilindro);
            this.flowLayoutPanel1.Controls.Add(this.btnCrearCono);
            this.flowLayoutPanel1.Controls.Add(this.btnCrearPiramide);
            this.flowLayoutPanel1.Controls.Add(this.btnCambiarColor);
            this.flowLayoutPanel1.Controls.Add(this.btnEliminarFigura);
            this.flowLayoutPanel1.Controls.Add(this.btnLimpiarTodo);
            this.flowLayoutPanel1.Controls.Add(this.btnRotarX);
            this.flowLayoutPanel1.Controls.Add(this.btnRotarY);
            this.flowLayoutPanel1.Controls.Add(this.btnRotarZ);
            this.flowLayoutPanel1.Controls.Add(this.btnMoverX);
            this.flowLayoutPanel1.Controls.Add(this.btnMoverY);
            this.flowLayoutPanel1.Controls.Add(this.btnMoverZ);
            this.flowLayoutPanel1.Controls.Add(this.btnEscalarMas);
            this.flowLayoutPanel1.Controls.Add(this.lblInstrucciones);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(10);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(296, 700);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // btnCrearCubo
            // 
            this.btnCrearCubo.BackColor = System.Drawing.Color.SteelBlue;
            this.btnCrearCubo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCrearCubo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCrearCubo.ForeColor = System.Drawing.Color.White;
            this.btnCrearCubo.Location = new System.Drawing.Point(13, 13);
            this.btnCrearCubo.Name = "btnCrearCubo";
            this.btnCrearCubo.Size = new System.Drawing.Size(200, 40);
            this.btnCrearCubo.TabIndex = 0;
            this.btnCrearCubo.Text = "Crear Cubo";
            this.btnCrearCubo.UseVisualStyleBackColor = false;
            this.btnCrearCubo.Click += new System.EventHandler(this.btnCrearCubo_Click);
            // 
            // btnCrearEsfera
            // 
            this.btnCrearEsfera.BackColor = System.Drawing.Color.SteelBlue;
            this.btnCrearEsfera.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCrearEsfera.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCrearEsfera.ForeColor = System.Drawing.Color.White;
            this.btnCrearEsfera.Location = new System.Drawing.Point(13, 59);
            this.btnCrearEsfera.Name = "btnCrearEsfera";
            this.btnCrearEsfera.Size = new System.Drawing.Size(200, 40);
            this.btnCrearEsfera.TabIndex = 1;
            this.btnCrearEsfera.Text = "Crear Esfera";
            this.btnCrearEsfera.UseVisualStyleBackColor = false;
            this.btnCrearEsfera.Click += new System.EventHandler(this.btnCrearEsfera_Click);
            // 
            // btnCrearCilindro
            // 
            this.btnCrearCilindro.BackColor = System.Drawing.Color.SteelBlue;
            this.btnCrearCilindro.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCrearCilindro.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCrearCilindro.ForeColor = System.Drawing.Color.White;
            this.btnCrearCilindro.Location = new System.Drawing.Point(13, 105);
            this.btnCrearCilindro.Name = "btnCrearCilindro";
            this.btnCrearCilindro.Size = new System.Drawing.Size(200, 40);
            this.btnCrearCilindro.TabIndex = 2;
            this.btnCrearCilindro.Text = "Crear Cilindro";
            this.btnCrearCilindro.UseVisualStyleBackColor = false;
            this.btnCrearCilindro.Click += new System.EventHandler(this.btnCrearCilindro_Click);
            // 
            // btnCrearCono
            // 
            this.btnCrearCono.BackColor = System.Drawing.Color.SteelBlue;
            this.btnCrearCono.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCrearCono.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCrearCono.ForeColor = System.Drawing.Color.White;
            this.btnCrearCono.Location = new System.Drawing.Point(13, 151);
            this.btnCrearCono.Name = "btnCrearCono";
            this.btnCrearCono.Size = new System.Drawing.Size(200, 40);
            this.btnCrearCono.TabIndex = 3;
            this.btnCrearCono.Text = "Crear Cono";
            this.btnCrearCono.UseVisualStyleBackColor = false;
            this.btnCrearCono.Click += new System.EventHandler(this.btnCrearCono_Click);
            // 
            // btnCrearPiramide
            // 
            this.btnCrearPiramide.BackColor = System.Drawing.Color.SteelBlue;
            this.btnCrearPiramide.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCrearPiramide.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCrearPiramide.ForeColor = System.Drawing.Color.White;
            this.btnCrearPiramide.Location = new System.Drawing.Point(13, 197);
            this.btnCrearPiramide.Name = "btnCrearPiramide";
            this.btnCrearPiramide.Size = new System.Drawing.Size(200, 40);
            this.btnCrearPiramide.TabIndex = 4;
            this.btnCrearPiramide.Text = "Crear Pirámide";
            this.btnCrearPiramide.UseVisualStyleBackColor = false;
            this.btnCrearPiramide.Click += new System.EventHandler(this.btnCrearPiramide_Click);
            // 
            // btnCambiarColor
            // 
            this.btnCambiarColor.BackColor = System.Drawing.Color.SteelBlue;
            this.btnCambiarColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCambiarColor.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCambiarColor.ForeColor = System.Drawing.Color.White;
            this.btnCambiarColor.Location = new System.Drawing.Point(13, 243);
            this.btnCambiarColor.Name = "btnCambiarColor";
            this.btnCambiarColor.Size = new System.Drawing.Size(200, 40);
            this.btnCambiarColor.TabIndex = 5;
            this.btnCambiarColor.Text = "Cambiar Color";
            this.btnCambiarColor.UseVisualStyleBackColor = false;
            this.btnCambiarColor.Click += new System.EventHandler(this.btnCambiarColor_Click);
            // 
            // btnEliminarFigura
            // 
            this.btnEliminarFigura.BackColor = System.Drawing.Color.SteelBlue;
            this.btnEliminarFigura.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEliminarFigura.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEliminarFigura.ForeColor = System.Drawing.Color.White;
            this.btnEliminarFigura.Location = new System.Drawing.Point(13, 289);
            this.btnEliminarFigura.Name = "btnEliminarFigura";
            this.btnEliminarFigura.Size = new System.Drawing.Size(200, 40);
            this.btnEliminarFigura.TabIndex = 6;
            this.btnEliminarFigura.Text = "Eliminar Figura";
            this.btnEliminarFigura.UseVisualStyleBackColor = false;
            this.btnEliminarFigura.Click += new System.EventHandler(this.btnEliminarFigura_Click);
            // 
            // btnLimpiarTodo
            // 
            this.btnLimpiarTodo.BackColor = System.Drawing.Color.SteelBlue;
            this.btnLimpiarTodo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimpiarTodo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLimpiarTodo.ForeColor = System.Drawing.Color.White;
            this.btnLimpiarTodo.Location = new System.Drawing.Point(13, 335);
            this.btnLimpiarTodo.Name = "btnLimpiarTodo";
            this.btnLimpiarTodo.Size = new System.Drawing.Size(200, 40);
            this.btnLimpiarTodo.TabIndex = 7;
            this.btnLimpiarTodo.Text = "Limpiar Todo";
            this.btnLimpiarTodo.UseVisualStyleBackColor = false;
            this.btnLimpiarTodo.Click += new System.EventHandler(this.btnLimpiarTodo_Click);
            // 
            // btnRotarX
            // 
            this.btnRotarX.BackColor = System.Drawing.Color.SteelBlue;
            this.btnRotarX.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRotarX.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRotarX.ForeColor = System.Drawing.Color.White;
            this.btnRotarX.Location = new System.Drawing.Point(13, 381);
            this.btnRotarX.Name = "btnRotarX";
            this.btnRotarX.Size = new System.Drawing.Size(200, 40);
            this.btnRotarX.TabIndex = 8;
            this.btnRotarX.Text = "Rotar +X";
            this.btnRotarX.UseVisualStyleBackColor = false;
            this.btnRotarX.Click += new System.EventHandler(this.btnRotarX_Click);
            // 
            // btnRotarY
            // 
            this.btnRotarY.BackColor = System.Drawing.Color.SteelBlue;
            this.btnRotarY.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRotarY.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRotarY.ForeColor = System.Drawing.Color.White;
            this.btnRotarY.Location = new System.Drawing.Point(13, 427);
            this.btnRotarY.Name = "btnRotarY";
            this.btnRotarY.Size = new System.Drawing.Size(200, 40);
            this.btnRotarY.TabIndex = 9;
            this.btnRotarY.Text = "Rotar +Y";
            this.btnRotarY.UseVisualStyleBackColor = false;
            this.btnRotarY.Click += new System.EventHandler(this.btnRotarY_Click);
            // 
            // btnRotarZ
            // 
            this.btnRotarZ.BackColor = System.Drawing.Color.SteelBlue;
            this.btnRotarZ.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRotarZ.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRotarZ.ForeColor = System.Drawing.Color.White;
            this.btnRotarZ.Location = new System.Drawing.Point(13, 473);
            this.btnRotarZ.Name = "btnRotarZ";
            this.btnRotarZ.Size = new System.Drawing.Size(200, 40);
            this.btnRotarZ.TabIndex = 10;
            this.btnRotarZ.Text = "Rotar +Z";
            this.btnRotarZ.UseVisualStyleBackColor = false;
            this.btnRotarZ.Click += new System.EventHandler(this.btnRotarZ_Click);
            // 
            // btnMoverX
            // 
            this.btnMoverX.BackColor = System.Drawing.Color.SteelBlue;
            this.btnMoverX.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMoverX.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMoverX.ForeColor = System.Drawing.Color.White;
            this.btnMoverX.Location = new System.Drawing.Point(13, 519);
            this.btnMoverX.Name = "btnMoverX";
            this.btnMoverX.Size = new System.Drawing.Size(200, 40);
            this.btnMoverX.TabIndex = 11;
            this.btnMoverX.Text = "Mover +X";
            this.btnMoverX.UseVisualStyleBackColor = false;
            this.btnMoverX.Click += new System.EventHandler(this.btnMoverX_Click);
            // 
            // btnMoverY
            // 
            this.btnMoverY.BackColor = System.Drawing.Color.SteelBlue;
            this.btnMoverY.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMoverY.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMoverY.ForeColor = System.Drawing.Color.White;
            this.btnMoverY.Location = new System.Drawing.Point(13, 565);
            this.btnMoverY.Name = "btnMoverY";
            this.btnMoverY.Size = new System.Drawing.Size(200, 40);
            this.btnMoverY.TabIndex = 12;
            this.btnMoverY.Text = "Mover +Y";
            this.btnMoverY.UseVisualStyleBackColor = false;
            this.btnMoverY.Click += new System.EventHandler(this.btnMoverY_Click);
            // 
            // btnMoverZ
            // 
            this.btnMoverZ.BackColor = System.Drawing.Color.SteelBlue;
            this.btnMoverZ.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMoverZ.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMoverZ.ForeColor = System.Drawing.Color.White;
            this.btnMoverZ.Location = new System.Drawing.Point(13, 611);
            this.btnMoverZ.Name = "btnMoverZ";
            this.btnMoverZ.Size = new System.Drawing.Size(200, 40);
            this.btnMoverZ.TabIndex = 13;
            this.btnMoverZ.Text = "Mover +Z";
            this.btnMoverZ.UseVisualStyleBackColor = false;
            this.btnMoverZ.Click += new System.EventHandler(this.btnMoverZ_Click);
            // 
            // btnEscalarMas
            // 
            this.btnEscalarMas.BackColor = System.Drawing.Color.SteelBlue;
            this.btnEscalarMas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEscalarMas.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEscalarMas.ForeColor = System.Drawing.Color.White;
            this.btnEscalarMas.Location = new System.Drawing.Point(13, 657);
            this.btnEscalarMas.Name = "btnEscalarMas";
            this.btnEscalarMas.Size = new System.Drawing.Size(200, 40);
            this.btnEscalarMas.TabIndex = 14;
            this.btnEscalarMas.Text = "Escalar +";
            this.btnEscalarMas.UseVisualStyleBackColor = false;
            this.btnEscalarMas.Click += new System.EventHandler(this.btnEscalarMas_Click);
            // 
            // lblInstrucciones
            // 
            this.lblInstrucciones.AutoSize = false;
            this.lblInstrucciones.Location = new System.Drawing.Point(13, 703);
            this.lblInstrucciones.Name = "lblInstrucciones";
            this.lblInstrucciones.Size = new System.Drawing.Size(280, 100);
            this.lblInstrucciones.TabIndex = 15;
            this.lblInstrucciones.Text = "INSTRUCCIONES:\r\n1. Click en panel para seleccionar figura\r\n2. Arrastrar mouse par" +
    "a rotar cámara\r\n3. Rueda mouse para zoom\r\n4. Usar botones para transformaciones";
            this.lblInstrucciones.Padding = new System.Windows.Forms.Padding(10);
            // 
            // FormTestFiguras
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1000, 700);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FormTestFiguras";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Prueba de Figuras 3D - Sistema Gráfico";
            this.Load += new System.EventHandler(this.FormTestFiguras_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panelControles.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panelDibujo;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panelControles;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnCrearCubo;
        private System.Windows.Forms.Button btnCrearEsfera;
        private System.Windows.Forms.Button btnCrearCilindro;
        private System.Windows.Forms.Button btnCrearCono;
        private System.Windows.Forms.Button btnCrearPiramide;
        private System.Windows.Forms.Button btnCambiarColor;
        private System.Windows.Forms.Button btnEliminarFigura;
        private System.Windows.Forms.Button btnLimpiarTodo;
        private System.Windows.Forms.Button btnRotarX;
        private System.Windows.Forms.Button btnRotarY;
        private System.Windows.Forms.Button btnRotarZ;
        private System.Windows.Forms.Button btnMoverX;
        private System.Windows.Forms.Button btnMoverY;
        private System.Windows.Forms.Button btnMoverZ;
        private System.Windows.Forms.Button btnEscalarMas;
        private System.Windows.Forms.Label lblInstrucciones;
    }
}