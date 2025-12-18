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
    public partial class EsferaForm : Form
    {
        private Esfera esfera;
        private Point lastMousePos;
        private bool isRotating = false;
        private bool isMoving = false;

        public EsferaForm()
        {
            InitializeComponent();
            esfera = new Esfera("Esfera Roja", 20);
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

                float nuevaRotY = esfera.Rotacion.Y + deltaX * 0.5f;
                float nuevaRotX = esfera.Rotacion.X + deltaY * 0.5f;

                esfera.Rotacion = new Point3D(nuevaRotX, nuevaRotY, esfera.Rotacion.Z);

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
                float nuevaPosX = esfera.Posicion.X + deltaX * escalaMovimiento;
                float nuevaPosY = esfera.Posicion.Y - deltaY * escalaMovimiento;

                esfera.Posicion = new Point3D(nuevaPosX, nuevaPosY, esfera.Posicion.Z);

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

            float nuevaEscalaX = Math.Max(0.1f, esfera.Escala.X + cambioEscala);
            float nuevaEscalaY = Math.Max(0.1f, esfera.Escala.Y + cambioEscala);
            float nuevaEscalaZ = Math.Max(0.1f, esfera.Escala.Z + cambioEscala);

            esfera.Escala = new Point3D(nuevaEscalaX, nuevaEscalaY, nuevaEscalaZ);

            numericEscalaX.Value = (decimal)nuevaEscalaX;
            numericEscalaY.Value = (decimal)nuevaEscalaY;
            numericEscalaZ.Value = (decimal)nuevaEscalaZ;

            panelVisualizacion.Invalidate();
        }

        private void panelVisualizacion_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            var vertices = esfera.ObtenerVerticesTransformados();
            var caras = esfera.ObtenerCaras();

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

                using (Brush brush = new SolidBrush(Color.FromArgb(150, esfera.ColorFigura)))
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
            esfera.Posicion = new Point3D(
                (float)numericPosX.Value,
                (float)numericPosY.Value,
                (float)numericPosZ.Value
            );
            panelVisualizacion.Invalidate();
        }

        private void numericRotacion_ValueChanged(object sender, EventArgs e)
        {
            esfera.Rotacion = new Point3D(
                (float)numericRotacionX.Value,
                (float)numericRotacionY.Value,
                (float)numericRotacionZ.Value
            );
            panelVisualizacion.Invalidate();
        }

        private void numericEscala_ValueChanged(object sender, EventArgs e)
        {
            esfera.Escala = new Point3D(
                (float)numericEscalaX.Value,
                (float)numericEscalaY.Value,
                (float)numericEscalaZ.Value
            );
            panelVisualizacion.Invalidate();
        }

        private void btnColor_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = esfera.ColorFigura;
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                esfera.ColorFigura = colorDialog1.Color;
                panelVisualizacion.Invalidate();
            }
        }

        private void btnReiniciar_Click(object sender, EventArgs e)
        {
            esfera.Posicion = new Point3D(0, 0, 0);
            esfera.Rotacion = new Point3D(0, 0, 0);
            esfera.Escala = new Point3D(1, 1, 1);
            esfera.ColorFigura = Color.Red;

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
                esfera.Rotacion = new Point3D(
                    esfera.Rotacion.X + 0.3f,
                    esfera.Rotacion.Y + 0.5f,
                    esfera.Rotacion.Z
                );

                numericRotacionX.Value = (decimal)(esfera.Rotacion.X % 360);
                numericRotacionY.Value = (decimal)(esfera.Rotacion.Y % 360);

                panelVisualizacion.Invalidate();
            }
        }
    }
}