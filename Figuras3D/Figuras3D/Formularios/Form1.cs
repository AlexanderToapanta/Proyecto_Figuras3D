using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Figuras3D
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            // Configurar el fondo degradado del MDI
            this.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void cuboToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormularioFigura(new Formularios.CuboForm(), "Cubo");
        }

        private void esferaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormularioFigura(new Formularios.EsferaForm(), "Esfera");
        }

        private void cilindroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormularioFigura(new Formularios.CilindroForm(), "Cilindro");
        }

        private void conoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormularioFigura(new Formularios.ConoForm(), "Cono");
        }

        private void pirámideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormularioFigura(new Formularios.PiramideForm(), "Pirámide");
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro que desea salir?", "Confirmar salida",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        /// <summary>
        /// Abre un formulario hijo y oculta el panel de bienvenida
        /// </summary>
        private void AbrirFormularioFigura(Form formulario, string nombreFigura)
        {
            // Ocultar panel de bienvenida
            panelBienvenida.Visible = false;

            // Configurar formulario hijo
            formulario.MdiParent = this;
            formulario.WindowState = FormWindowState.Maximized;
            formulario.Show();

            // Actualizar status bar
            toolStripStatusLabel1.Text = $"Visualizando: {nombreFigura} | Use el mouse para interactuar";
        }

        /// <summary>
        /// Evento que se dispara cuando se activa un formulario hijo MDI
        /// </summary>
        private void Form1_MdiChildActivate(object sender, EventArgs e)
        {
            if (this.ActiveMdiChild == null)
            {
                // No hay formularios hijos activos, mostrar panel de bienvenida
                panelBienvenida.Visible = true;
                toolStripStatusLabel1.Text = "Listo | Seleccione una figura del menú para comenzar";
            }
            else
            {
                // Hay un formulario hijo activo, ocultar panel de bienvenida
                panelBienvenida.Visible = false;
            }
        }

        /// <summary>
        /// Dibuja un ícono 3D personalizado en el PictureBox
        /// </summary>
        private void pictureBoxLogo_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Dibujar un cubo 3D isométrico como logo
            int centerX = pictureBoxLogo.Width / 2;
            int centerY = pictureBoxLogo.Height / 2;
            int size = 50;

            // Definir puntos del cubo isométrico
            Point[] frontFace = new Point[]
            {
                new Point(centerX, centerY - size),           // Top
                new Point(centerX + size, centerY - size/2),  // Right
                new Point(centerX + size, centerY + size/2),  // Bottom-right
                new Point(centerX, centerY + size)            // Bottom
            };

            Point[] leftFace = new Point[]
            {
                new Point(centerX, centerY - size),           // Top
                new Point(centerX - size, centerY - size/2),  // Left
                new Point(centerX - size, centerY + size/2),  // Bottom-left
                new Point(centerX, centerY + size)            // Bottom
            };

            Point[] topFace = new Point[]
            {
                new Point(centerX, centerY - size),           // Top
                new Point(centerX + size, centerY - size/2),  // Right
                new Point(centerX, centerY),                  // Center
                new Point(centerX - size, centerY - size/2)   // Left
            };

            // Dibujar caras con colores diferentes
            using (Brush brushFront = new SolidBrush(Color.FromArgb(200, 0, 120, 215)))
            using (Brush brushLeft = new SolidBrush(Color.FromArgb(200, 0, 90, 160)))
            using (Brush brushTop = new SolidBrush(Color.FromArgb(200, 100, 180, 255)))
            using (Pen pen = new Pen(Color.White, 2))
            {
                // Cara izquierda
                g.FillPolygon(brushLeft, leftFace);
                g.DrawPolygon(pen, leftFace);

                // Cara frontal
                g.FillPolygon(brushFront, frontFace);
                g.DrawPolygon(pen, frontFace);

                // Cara superior
                g.FillPolygon(brushTop, topFace);
                g.DrawPolygon(pen, topFace);
            }
        }
    }
}