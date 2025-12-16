using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Figuras3D.Formularios
{
    public partial class PiramideForm : Form
    {
        private Piramide piramide;
        private Point lastMousePos;
        private bool isRotating = false;
        private bool isMoving = false;

        public PiramideForm()
        {
            InitializeComponent();
            piramide = new Piramide("Pirámide Morada");
            panelVisualizacion.DoubleBuffered(true);

            // Eventos del mouse
            panelVisualizacion.MouseDown += PanelVisualizacion_MouseDown;
            panelVisualizacion.MouseMove += PanelVisualizacion_MouseMove;
            panelVisualizacion.MouseUp += PanelVisualizacion_MouseUp;
            panelVisualizacion.MouseWheel += PanelVisualizacion_MouseWheel;
        }

        private void PanelVisualizacion_MouseDown(object sender, MouseEventArgs e)
        {
            lastMousePos = e.Location;

            if (e.Button == MouseButtons.Left)
            {
                isRotating = true;
                panelVisualizacion.Cursor = Cursors.Hand;
            }
            else if (e.Button == MouseButtons.Right)
            {
                isMoving = true;
                panelVisualizacion.Cursor = Cursors.SizeAll;
            }
        }

        private void PanelVisualizacion_MouseMove(object sender, MouseEventArgs e)
        {
            if (isRotating)
            {
                int deltaX = e.X - lastMousePos.X;
                int deltaY = e.Y - lastMousePos.Y;

                float nuevaRotY = piramide.Rotacion.Y + deltaX * 0.5f;
                float nuevaRotX = piramide.Rotacion.X + deltaY * 0.5f;

                piramide.Rotacion = new Point3D(nuevaRotX, nuevaRotY, piramide.Rotacion.Z);

                numericRotacionX.Value = (decimal)(nuevaRotX % 360);
                numericRotacionY.Value = (decimal)(nuevaRotY % 360);

                panelVisualizacion.Invalidate();
                lastMousePos = e.Location;
            }
            else if (isMoving)
            {
                int deltaX = e.X - lastMousePos.X;
                int deltaY = e.Y - lastMousePos.Y;

                float escalaMovimiento = 0.01f;
                float nuevaPosX = piramide.Posicion.X + deltaX * escalaMovimiento;
                float nuevaPosY = piramide.Posicion.Y - deltaY * escalaMovimiento;

                piramide.Posicion = new Point3D(nuevaPosX, nuevaPosY, piramide.Posicion.Z);

                numericPosX.Value = (decimal)nuevaPosX;
                numericPosY.Value = (decimal)nuevaPosY;

                panelVisualizacion.Invalidate();
                lastMousePos = e.Location;
            }
        }

        private void PanelVisualizacion_MouseUp(object sender, MouseEventArgs e)
        {
            isRotating = false;
            isMoving = false;
            panelVisualizacion.Cursor = Cursors.Default;
        }

        private void PanelVisualizacion_MouseWheel(object sender, MouseEventArgs e)
        {
            float cambioEscala = e.Delta > 0 ? 0.1f : -0.1f;

            float nuevaEscalaX = Math.Max(0.1f, piramide.Escala.X + cambioEscala);
            float nuevaEscalaY = Math.Max(0.1f, piramide.Escala.Y + cambioEscala);
            float nuevaEscalaZ = Math.Max(0.1f, piramide.Escala.Z + cambioEscala);

            piramide.Escala = new Point3D(nuevaEscalaX, nuevaEscalaY, nuevaEscalaZ);

            numericEscalaX.Value = (decimal)nuevaEscalaX;
            numericEscalaY.Value = (decimal)nuevaEscalaY;
            numericEscalaZ.Value = (decimal)nuevaEscalaZ;

            panelVisualizacion.Invalidate();
        }

        private void panelVisualizacion_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            var vertices = piramide.ObtenerVerticesTransformados();
            var caras = piramide.ObtenerCaras();

            List<PointF> vertices2D = new List<PointF>();
            float escalaProyeccion = 200;
            float centroX = panelVisualizacion.Width / 2;
            float centroY = panelVisualizacion.Height / 2;

            foreach (var v in vertices)
            {
                float factor = 1 / (1 + v.Z * 0.3f);
                float x2D = centroX + v.X * escalaProyeccion * factor;
                float y2D = centroY - v.Y * escalaProyeccion * factor;
                vertices2D.Add(new PointF(x2D, y2D));
            }

            foreach (var cara in caras)
            {
                PointF[] puntos = new PointF[]
                {
                    vertices2D[cara[0]],
                    vertices2D[cara[1]],
                    vertices2D[cara[2]]
                };

                using (Brush brush = new SolidBrush(Color.FromArgb(150, piramide.ColorFigura)))
                {
                    g.FillPolygon(brush, puntos);
                }
                g.DrawPolygon(Pens.White, puntos);
            }

            using (Font font = new Font("Segoe UI", 10))
            using (Brush textBrush = new SolidBrush(Color.White))
            {
                g.DrawString("Click Izq: Rotar | Click Der: Mover | Rueda: Zoom", font, textBrush, 10, 10);
            }
        }

        private void numericPos_ValueChanged(object sender, EventArgs e)
        {
            piramide.Posicion = new Point3D(
                (float)numericPosX.Value,
                (float)numericPosY.Value,
                (float)numericPosZ.Value
            );
            panelVisualizacion.Invalidate();
        }

        private void numericRotacion_ValueChanged(object sender, EventArgs e)
        {
            piramide.Rotacion = new Point3D(
                (float)numericRotacionX.Value,
                (float)numericRotacionY.Value,
                (float)numericRotacionZ.Value
            );
            panelVisualizacion.Invalidate();
        }

        private void numericEscala_ValueChanged(object sender, EventArgs e)
        {
            piramide.Escala = new Point3D(
                (float)numericEscalaX.Value,
                (float)numericEscalaY.Value,
                (float)numericEscalaZ.Value
            );
            panelVisualizacion.Invalidate();
        }

        private void btnColor_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = piramide.ColorFigura;
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                piramide.ColorFigura = colorDialog1.Color;
                panelVisualizacion.Invalidate();
            }
        }

        private void btnReiniciar_Click(object sender, EventArgs e)
        {
            piramide.Posicion = new Point3D(0, 0, 0);
            piramide.Rotacion = new Point3D(0, 0, 0);
            piramide.Escala = new Point3D(1, 1, 1);
            piramide.ColorFigura = Color.Purple;

            numericPosX.Value = 0;
            numericPosY.Value = 0;
            numericPosZ.Value = 0;
            numericRotacionX.Value = 0;
            numericRotacionY.Value = 0;
            numericRotacionZ.Value = 0;
            numericEscalaX.Value = 1;
            numericEscalaY.Value = 1;
            numericEscalaZ.Value = 1;

            panelVisualizacion.Invalidate();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!isRotating && !isMoving)
            {
                piramide.Rotacion = new Point3D(
                    piramide.Rotacion.X + 0.5f,
                    piramide.Rotacion.Y + 0.8f,
                    piramide.Rotacion.Z
                );

                numericRotacionX.Value = (decimal)(piramide.Rotacion.X % 360);
                numericRotacionY.Value = (decimal)(piramide.Rotacion.Y % 360);

                panelVisualizacion.Invalidate();
            }
        }
    }
}