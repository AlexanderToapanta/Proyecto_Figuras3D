using System;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics; // Agregar para Debug
using Figuras3D;

namespace Figuras3D.Formularios
{
    public partial class FormTestFiguras : Form
    {
        // Lista de todas las figuras en la escena
        private List<Figura3D> figuras = new List<Figura3D>();

        // Figura actualmente seleccionada
        private Figura3D figuraSeleccionada = null;

        // Variables para control de cámara
        private Point mouseAnterior;
        private float rotacionCamaraX = 0;
        private float rotacionCamaraY = 0;
        private float zoomCamara = 50.0f;

        // Colores disponibles
        private Color[] colores = {
            Color.Blue, Color.Red, Color.Green,
            Color.Orange, Color.Purple, Color.Cyan,
            Color.Yellow, Color.Magenta, Color.Lime
        };

        // Random para colores aleatorios
        private Random random = new Random();

        // Para depuración
        private string mensajeError = "";

        public FormTestFiguras()
        {
            // Configurar DoubleBuffered ANTES de InitializeComponent
            this.DoubleBuffered = true;
            InitializeComponent();
            CrearFigurasIniciales();
        }

        private void FormTestFiguras_Load(object sender, EventArgs e)
        {
            // Configurar para mejor rendimiento
            SetStyle(ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint, true);

            // Mostrar información de depuración
            MostrarInfoFiguras();
        }

        private void MostrarInfoFiguras()
        {
            Debug.WriteLine("=== INFORMACIÓN DE FIGURAS ===");
            foreach (var figura in figuras)
            {
                Debug.WriteLine($"\n{figura.Nombre}:");
                Debug.WriteLine($"  Vértices: {figura.ObtenerVerticesTransformados().Count}");
                Debug.WriteLine($"  Caras: {figura.ObtenerCaras().Count}");

                // Verificar índices de caras
                var vertices = figura.ObtenerVerticesTransformados();
                var caras = figura.ObtenerCaras();
                int errores = 0;

                for (int i = 0; i < caras.Count; i++)
                {
                    foreach (var indice in caras[i])
                    {
                        if (indice < 0 || indice >= vertices.Count)
                        {
                            errores++;
                            Debug.WriteLine($"  ERROR en cara {i}: índice {indice} fuera de rango (0-{vertices.Count - 1})");
                        }
                    }
                }

                if (errores > 0)
                {
                    Debug.WriteLine($"  TOTAL ERRORES: {errores}");
                }
            }
        }

        private void CrearFigurasIniciales()
        {
            // Crear un ejemplo de cada figura
            CrearCuboInicial();
            CrearEsferaInicial();
            CrearCilindroInicial();
            CrearConoInicial();
            CrearPiramideInicial();

            // Seleccionar la primera figura
            if (figuras.Count > 0)
            {
                SeleccionarFigura(figuras[0]);
            }

            MostrarInfoFiguras();
        }

        private void CrearCuboInicial()
        {
            Cubo cubo = new Cubo("Cubo Ejemplo");
            cubo.ColorFigura = colores[0];
            cubo.Posicion = new Point3D(-6, 0, 0);
            figuras.Add(cubo);
        }

        private void CrearEsferaInicial()
        {
            Esfera esfera = new Esfera("Esfera Ejemplo", 8); // Reducir segmentos para depuración
            esfera.ColorFigura = colores[1];
            esfera.Posicion = new Point3D(-3, 0, 0);
            figuras.Add(esfera);
        }

        private void CrearCilindroInicial()
        {
            Cilindro cilindro = new Cilindro("Cilindro Ejemplo", 8); // Reducir segmentos
            cilindro.ColorFigura = colores[2];
            cilindro.Posicion = new Point3D(0, 0, 0);
            figuras.Add(cilindro);
        }

        private void CrearConoInicial()
        {
            Cono cono = new Cono("Cono Ejemplo", 8); // Reducir segmentos
            cono.ColorFigura = colores[3];
            cono.Posicion = new Point3D(3, 0, 0);
            figuras.Add(cono);
        }

        private void CrearPiramideInicial()
        {
            Piramide piramide = new Piramide("Pirámide Ejemplo");
            piramide.ColorFigura = colores[4];
            piramide.Posicion = new Point3D(6, 0, 0);
            figuras.Add(piramide);
        }

        #region Event Handlers para botones

        private void btnCrearCubo_Click(object sender, EventArgs e)
        {
            Cubo cubo = new Cubo($"Cubo {figuras.Count + 1}");
            cubo.ColorFigura = ObtenerColorAleatorio();
            cubo.Posicion = new Point3D(0, 0, 0);
            figuras.Add(cubo);
            SeleccionarFigura(cubo);
            MostrarInfoFiguras();
            panelDibujo.Invalidate();
        }

        private void btnCrearEsfera_Click(object sender, EventArgs e)
        {
            Esfera esfera = new Esfera($"Esfera {figuras.Count + 1}", 8);
            esfera.ColorFigura = ObtenerColorAleatorio();
            esfera.Posicion = new Point3D(0, 0, 0);
            figuras.Add(esfera);
            SeleccionarFigura(esfera);
            MostrarInfoFiguras();
            panelDibujo.Invalidate();
        }

        private void btnCrearCilindro_Click(object sender, EventArgs e)
        {
            Cilindro cilindro = new Cilindro($"Cilindro {figuras.Count + 1}", 8);
            cilindro.ColorFigura = ObtenerColorAleatorio();
            cilindro.Posicion = new Point3D(0, 0, 0);
            figuras.Add(cilindro);
            SeleccionarFigura(cilindro);
            MostrarInfoFiguras();
            panelDibujo.Invalidate();
        }

        private void btnCrearCono_Click(object sender, EventArgs e)
        {
            Cono cono = new Cono($"Cono {figuras.Count + 1}", 8);
            cono.ColorFigura = ObtenerColorAleatorio();
            cono.Posicion = new Point3D(0, 0, 0);
            figuras.Add(cono);
            SeleccionarFigura(cono);
            MostrarInfoFiguras();
            panelDibujo.Invalidate();
        }

        private void btnCrearPiramide_Click(object sender, EventArgs e)
        {
            Piramide piramide = new Piramide($"Pirámide {figuras.Count + 1}");
            piramide.ColorFigura = ObtenerColorAleatorio();
            piramide.Posicion = new Point3D(0, 0, 0);
            figuras.Add(piramide);
            SeleccionarFigura(piramide);
            MostrarInfoFiguras();
            panelDibujo.Invalidate();
        }

        private void btnCambiarColor_Click(object sender, EventArgs e)
        {
            if (figuraSeleccionada != null)
            {
                figuraSeleccionada.ColorFigura = ObtenerColorAleatorio();
                panelDibujo.Invalidate();
            }
        }

        private void btnEliminarFigura_Click(object sender, EventArgs e)
        {
            if (figuraSeleccionada != null)
            {
                figuras.Remove(figuraSeleccionada);
                figuraSeleccionada = null;
                panelDibujo.Invalidate();
            }
        }

        private void btnLimpiarTodo_Click(object sender, EventArgs e)
        {
            figuras.Clear();
            figuraSeleccionada = null;
            panelDibujo.Invalidate();
        }

        private void btnRotarX_Click(object sender, EventArgs e)
        {
            RotarFigura(10, 0, 0);
        }

        private void btnRotarY_Click(object sender, EventArgs e)
        {
            RotarFigura(0, 10, 0);
        }

        private void btnRotarZ_Click(object sender, EventArgs e)
        {
            RotarFigura(0, 0, 10);
        }

        private void btnMoverX_Click(object sender, EventArgs e)
        {
            MoverFigura(0.5f, 0, 0);
        }

        private void btnMoverY_Click(object sender, EventArgs e)
        {
            MoverFigura(0, 0.5f, 0);
        }

        private void btnMoverZ_Click(object sender, EventArgs e)
        {
            MoverFigura(0, 0, 0.5f);
        }

        private void btnEscalarMas_Click(object sender, EventArgs e)
        {
            EscalarFigura(0.1f);
        }

        #endregion

        #region Métodos auxiliares

        private Color ObtenerColorAleatorio()
        {
            return colores[random.Next(colores.Length)];
        }

        private void RotarFigura(float x, float y, float z)
        {
            if (figuraSeleccionada != null)
            {
                figuraSeleccionada.Rotacion = new Point3D(
                    figuraSeleccionada.Rotacion.X + x,
                    figuraSeleccionada.Rotacion.Y + y,
                    figuraSeleccionada.Rotacion.Z + z
                );
                panelDibujo.Invalidate();
            }
        }

        private void MoverFigura(float x, float y, float z)
        {
            if (figuraSeleccionada != null)
            {
                figuraSeleccionada.Posicion = new Point3D(
                    figuraSeleccionada.Posicion.X + x,
                    figuraSeleccionada.Posicion.Y + y,
                    figuraSeleccionada.Posicion.Z + z
                );
                panelDibujo.Invalidate();
            }
        }

        private void EscalarFigura(float factor)
        {
            if (figuraSeleccionada != null)
            {
                figuraSeleccionada.Escala = new Point3D(
                    figuraSeleccionada.Escala.X + factor,
                    figuraSeleccionada.Escala.Y + factor,
                    figuraSeleccionada.Escala.Z + factor
                );
                panelDibujo.Invalidate();
            }
        }

        private void SeleccionarFigura(Figura3D figura)
        {
            // Deseleccionar todas las figuras
            foreach (var f in figuras)
            {
                f.EstaSeleccionada = false;
            }

            // Seleccionar la nueva figura
            figura.EstaSeleccionada = true;
            figuraSeleccionada = figura;
            panelDibujo.Invalidate();
        }

        #endregion

        #region Eventos del panel de dibujo

        private void PanelDibujo_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.Clear(panelDibujo.BackColor);

            // Dibujar todas las figuras
            foreach (Figura3D figura in figuras)
            {
                DibujarFigura(g, figura);
            }

            // Dibujar información
            DibujarInformacion(g);

            // Dibujar ejes de referencia
            DibujarEjes(g);

            // Mostrar mensaje de error si existe
            if (!string.IsNullOrEmpty(mensajeError))
            {
                using (Font font = new Font("Consolas", 10, FontStyle.Bold))
                using (Brush brush = new SolidBrush(Color.Red))
                {
                    g.DrawString(mensajeError, font, brush, 10, panelDibujo.Height - 50);
                }
            }
        }

        private void DibujarFigura(Graphics g, Figura3D figura)
        {
            try
            {
                // Obtener vértices transformados
                List<Point3D> vertices = figura.ObtenerVerticesTransformados();
                List<int[]> caras = figura.ObtenerCaras();

                // DEBUG: Contar caras válidas
                int carasDibujadas = 0;
                int erroresIndices = 0;

                // Preparar pincel y lápiz
                using (Brush brush = new SolidBrush(figura.ColorFigura))
                using (Pen pen = new Pen(figura.EstaSeleccionada ? Color.Yellow : Color.Black,
                                         figura.EstaSeleccionada ? 3 : 1))
                {
                    // Dibujar cada cara (triángulo)
                    for (int i = 0; i < caras.Count; i++)
                    {
                        int[] cara = caras[i];

                        if (cara.Length >= 3)
                        {
                            // Verificar índices
                            bool indicesValidos = true;
                            for (int j = 0; j < 3; j++)
                            {
                                if (cara[j] < 0 || cara[j] >= vertices.Count)
                                {
                                    indicesValidos = false;
                                    erroresIndices++;
                                    break;
                                }
                            }

                            if (indicesValidos)
                            {
                                Point3D p1 = vertices[cara[0]];
                                Point3D p2 = vertices[cara[1]];
                                Point3D p3 = vertices[cara[2]];

                                // Verificar si los puntos son visibles
                                if (p1.Z > -100 && p2.Z > -100 && p3.Z > -100)
                                {
                                    // Convertir 3D a 2D
                                    PointF punto1 = Proyectar3Da2D(p1);
                                    PointF punto2 = Proyectar3Da2D(p2);
                                    PointF punto3 = Proyectar3Da2D(p3);

                                    // Crear polígono
                                    PointF[] puntos = { punto1, punto2, punto3 };

                                    // Dibujar relleno
                                    g.FillPolygon(brush, puntos);

                                    // Dibujar bordes
                                    g.DrawPolygon(pen, puntos);

                                    carasDibujadas++;
                                }
                            }
                        }
                    }
                }

                // Dibujar nombre de la figura
                Point3D centro = figura.Posicion;
                PointF puntoCentro = Proyectar3Da2D(centro);

                using (Font font = new Font("Arial", 10))
                using (Brush textBrush = new SolidBrush(Color.White))
                {
                    g.DrawString($"{figura.Nombre} ({carasDibujadas} caras)", font, textBrush,
                                puntoCentro.X - 40, puntoCentro.Y - 20);
                }

                // DEBUG: Mostrar errores en consola
                if (erroresIndices > 0)
                {
                    Debug.WriteLine($"{figura.Nombre}: {erroresIndices} caras con índices inválidos");
                    mensajeError = $"ERROR: {figura.Nombre} tiene {erroresIndices} caras con índices inválidos";
                }
            }
            catch (Exception ex)
            {
                // Mostrar error en pantalla
                mensajeError = $"Error dibujando {figura.Nombre}: {ex.Message}";
                Debug.WriteLine(mensajeError);
                Debug.WriteLine(ex.StackTrace);
            }
        }

        private PointF Proyectar3Da2D(Point3D punto3D)
        {
            try
            {
                // Aplicar rotación de cámara
                Point3D puntoRotado = RotarPunto(punto3D, rotacionCamaraY, rotacionCamaraX);

                // Proyección perspectiva simple
                // Asegurar que Z no sea demasiado cerca de -200 (división por cero)
                float z = puntoRotado.Z + 200.0f;
                if (z < 10) z = 10; // Evitar división por número muy pequeño

                float factorPerspectiva = 200.0f / z;

                // Calcular coordenadas 2D
                float x = panelDibujo.Width / 2 + puntoRotado.X * zoomCamara * factorPerspectiva;
                float y = panelDibujo.Height / 2 - puntoRotado.Y * zoomCamara * factorPerspectiva;

                return new PointF(x, y);
            }
            catch (Exception)
            {
                return new PointF(-1000, -1000); // Fuera de pantalla si hay error
            }
        }

        private Point3D RotarPunto(Point3D punto, float anguloY, float anguloX)
        {
            try
            {
                // Convertir ángulos a radianes
                float radY = anguloY * (float)Math.PI / 180.0f;
                float radX = anguloX * (float)Math.PI / 180.0f;

                // Rotación en Y (alrededor del eje vertical)
                float cosY = (float)Math.Cos(radY);
                float sinY = (float)Math.Sin(radY);
                float x1 = punto.X * cosY - punto.Z * sinY;
                float z1 = punto.X * sinY + punto.Z * cosY;

                // Rotación en X (alrededor del eje horizontal)
                float cosX = (float)Math.Cos(radX);
                float sinX = (float)Math.Sin(radX);
                float y1 = punto.Y * cosX - z1 * sinX;
                float z2 = punto.Y * sinX + z1 * cosX;

                return new Point3D(x1, y1, z2);
            }
            catch (Exception)
            {
                return new Point3D(0, 0, 0);
            }
        }

        private void DibujarInformacion(Graphics g)
        {
            string info = $"Figuras en escena: {figuras.Count}\n" +
                         $"Cámara: RotX={rotacionCamaraX:F0}°, RotY={rotacionCamaraY:F0}°, Zoom={zoomCamara:F1}\n";

            if (figuraSeleccionada != null)
            {
                var vertices = figuraSeleccionada.ObtenerVerticesTransformados();
                var caras = figuraSeleccionada.ObtenerCaras();

                info += $"Figura seleccionada: {figuraSeleccionada.Nombre}\n" +
                       $"Vértices: {vertices.Count}, Caras: {caras.Count}\n" +
                       $"Posición: ({figuraSeleccionada.Posicion.X:F2}, " +
                       $"{figuraSeleccionada.Posicion.Y:F2}, " +
                       $"{figuraSeleccionada.Posicion.Z:F2})\n" +
                       $"Rotación: ({figuraSeleccionada.Rotacion.X:F0}°, " +
                       $"{figuraSeleccionada.Rotacion.Y:F0}°, " +
                       $"{figuraSeleccionada.Rotacion.Z:F0}°)\n" +
                       $"Escala: ({figuraSeleccionada.Escala.X:F2}, " +
                       $"{figuraSeleccionada.Escala.Y:F2}, " +
                       $"{figuraSeleccionada.Escala.Z:F2})";
            }

            using (Font font = new Font("Consolas", 9))
            using (Brush brush = new SolidBrush(Color.White))
            {
                g.DrawString(info, font, brush, 10, 10);
            }
        }

        private void DibujarEjes(Graphics g)
        {
            try
            {
                PointF origen = Proyectar3Da2D(new Point3D(0, 0, 0));

                // Eje X (Rojo)
                PointF finX = Proyectar3Da2D(new Point3D(3, 0, 0));
                using (Pen penRojo = new Pen(Color.Red, 2))
                {
                    g.DrawLine(penRojo, origen, finX);
                }
                g.DrawString("X", new Font("Arial", 10), Brushes.Red, finX);

                // Eje Y (Verde)
                PointF finY = Proyectar3Da2D(new Point3D(0, 3, 0));
                using (Pen penVerde = new Pen(Color.Green, 2))
                {
                    g.DrawLine(penVerde, origen, finY);
                }
                g.DrawString("Y", new Font("Arial", 10), Brushes.Green, finY);

                // Eje Z (Azul)
                PointF finZ = Proyectar3Da2D(new Point3D(0, 0, 3));
                using (Pen penAzul = new Pen(Color.Blue, 2))
                {
                    g.DrawLine(penAzul, origen, finZ);
                }
                g.DrawString("Z", new Font("Arial", 10), Brushes.Blue, finZ);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error dibujando ejes: {ex.Message}");
            }
        }

        private void PanelDibujo_MouseDown(object sender, MouseEventArgs e)
        {
            mouseAnterior = e.Location;

            // Intentar seleccionar figura
            if (e.Button == MouseButtons.Left)
            {
                // Seleccionar cíclicamente
                if (figuras.Count > 0)
                {
                    int indiceActual = figuraSeleccionada != null ?
                        figuras.IndexOf(figuraSeleccionada) : -1;

                    int nuevoIndice = (indiceActual + 1) % figuras.Count;
                    SeleccionarFigura(figuras[nuevoIndice]);
                }
            }
        }

        private void PanelDibujo_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                // Rotar cámara
                float deltaX = e.X - mouseAnterior.X;
                float deltaY = e.Y - mouseAnterior.Y;

                rotacionCamaraY += deltaX * 0.5f;
                rotacionCamaraX += deltaY * 0.5f;

                // Limitar rotación vertical
                rotacionCamaraX = Math.Max(-90, Math.Min(90, rotacionCamaraX));

                mouseAnterior = e.Location;
                panelDibujo.Invalidate();
            }
        }

        private void PanelDibujo_MouseWheel(object sender, MouseEventArgs e)
        {
            // Zoom con rueda del mouse
            zoomCamara += e.Delta * 0.05f; // Reducir velocidad de zoom
            zoomCamara = Math.Max(10, Math.Min(200, zoomCamara));
            panelDibujo.Invalidate();
        }

        #endregion
    }
}