using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Figuras3D.Clases
{
    internal class MotorRenderizado
    {
        public float EscalaProyeccion { get; set; } = 200f;
        public float FactorPerspectiva { get; set; } = 0.3f;
        public bool UsarIluminacion { get; set; } = true;
        public bool UsarTexturas { get; set; } = true;
        public bool OrdenarCarasPorProfundidad { get; set; } = true;
        public bool DibujarBordes { get; set; } = true;
        public Color ColorBordes { get; set; } = Color.FromArgb(100, 255, 255, 255);
        public bool UsarBackfaceCulling { get; set; } = true;
        
        public Color ColorMalla { get; set; } = Color.FromArgb(255, 0, 255, 0); 
        public float GrosorMalla { get; set; } = 2.0f;
        
        public Camara CamaraActiva { get; set; } = Camara.CrearCamaraLibre();


        public void RenderizarFigura(Graphics g, Figura3D figura, int anchoPanel, int altoPanel)
        {
            if (figura == null) return;

            g.SmoothingMode = SmoothingMode.AntiAlias;

            List<Point3D> vertices3D = figura.ObtenerVerticesTransformados();
            List<int[]> caras = figura.ObtenerCaras();

            List<PointF> vertices2D = ProyectarVertices(vertices3D, anchoPanel, altoPanel);

            bool esMalla3D = figura.Material.Textura == TipoTextura.Malla3D;
            bool esSoloMalla = figura.Material.Textura == TipoTextura.SoloMalla;
            
            if (esSoloMalla)
            {
                DibujarMalla3D(g, caras, vertices2D);
                if (figura.EstaSeleccionada)
                {
                    DibujarSeleccion(g, vertices2D);
                }
                return;
            }

            List<CaraRenderizada> carasParaRenderizar = new List<CaraRenderizada>();

            foreach (int[] cara in caras)
            {
                if (cara.Length < 3) continue;

                Point3D v1 = vertices3D[cara[0]];
                Point3D v2 = vertices3D[cara[1]];
                Point3D v3 = vertices3D[cara[2]];

                Point3D normal = Iluminacion.CalcularNormalCara(v1, v2, v3);

                float profundidad = (v1.Z + v2.Z + v3.Z) / 3f;

                bool esVisible = true;

                if (UsarBackfaceCulling)
                {
                    Point3D centro = new Point3D(
                        (v1.X + v2.X + v3.X) / 3f,
                        (v1.Y + v2.Y + v3.Y) / 3f,
                        (v1.Z + v2.Z + v3.Z) / 3f
                    );

                    Point3D vectorCamara = new Point3D(
                        0 - centro.X,
                        0 - centro.Y,
                        5 - centro.Z
                    );

                    float productoPunto = normal.X * vectorCamara.X +
                                         normal.Y * vectorCamara.Y +
                                         normal.Z * vectorCamara.Z;

                    esVisible = productoPunto > 0;
                }

                if (esVisible)
                {
                    CaraRenderizada caraInfo = new CaraRenderizada
                    {
                        Indices = cara,
                        Normal = normal,
                        Profundidad = profundidad,
                        CentroSuperficie = new Point3D(
                            (v1.X + v2.X + v3.X) / 3f,
                            (v1.Y + v2.Y + v3.Y) / 3f,
                            (v1.Z + v2.Z + v3.Z) / 3f
                        )
                    };

                    carasParaRenderizar.Add(caraInfo);
                }
            }

            // Ordenar caras por profundidad (pintar las más lejanas primero)
            if (OrdenarCarasPorProfundidad)
            {
                carasParaRenderizar = carasParaRenderizar.OrderBy(c => c.Profundidad).ToList();
            }

            // Renderizar cada cara
            foreach (CaraRenderizada caraInfo in carasParaRenderizar)
            {
                RenderizarCara(g, figura, caraInfo, vertices2D);
            }

            // Si la textura es Malla3D, dibujar el wireframe sobre el sólido
            if (esMalla3D)
            {
                DibujarMalla3D(g, caras, vertices2D);
            }

            // Dibujar selección si está seleccionada
            if (figura.EstaSeleccionada)
            {
                DibujarSeleccion(g, vertices2D);
            }
        }

        private void RenderizarCara(Graphics g, Figura3D figura, CaraRenderizada caraInfo, List<PointF> vertices2D)
        {
            PointF[] puntos2D = caraInfo.Indices.Select(i => vertices2D[i]).ToArray();

            Color colorBase;
            if (UsarIluminacion && figura.Iluminacion != null && figura.Iluminacion.Habilitada)
            {
                colorBase = figura.Iluminacion.CalcularColorIluminado(
                    figura.Material,
                    caraInfo.Normal,
                    caraInfo.CentroSuperficie
                );
            }
            else
            {
                colorBase = Color.FromArgb(
                    (int)(figura.Material.Opacidad * 255),
                    figura.Material.ColorDifuso
                );
            }

            // Aplicar textura si está habilitada
            if (UsarTexturas && figura.Material.Textura != TipoTextura.Solido)
            {
                // Calcular rectángulo de la cara para texturas
                Rectangle boundingRect = ObtenerRectanguloDelimitador(puntos2D);
                using (Brush brushTextura = GestorTexturas.ObtenerBrushTextura(figura.Material, boundingRect))
                {
                    // Dibujar la textura
                    g.FillPolygon(brushTextura, puntos2D);
                }
                
                int alphaOverlay = (int)(figura.Material.Opacidad * 255 * 0.5f); // 50% de opacidad para el overlay
                using (SolidBrush overlayBrush = new SolidBrush(Color.FromArgb(alphaOverlay, colorBase)))
                {
                    g.FillPolygon(overlayBrush, puntos2D);
                }
            }
            else
            {
                // Sin textura, usar solo el color con iluminación
                using (SolidBrush brush = new SolidBrush(colorBase))
                {
                    g.FillPolygon(brush, puntos2D);
                }
            }

            // Dibujar bordes
            if (DibujarBordes)
            {
                using (Pen pen = new Pen(ColorBordes, 1))
                {
                    g.DrawPolygon(pen, puntos2D);
                }
            }
        }

        private List<PointF> ProyectarVertices(List<Point3D> vertices3D, int ancho, int alto)
        {
            List<PointF> vertices2D = new List<PointF>();
            float centroX = ancho / 2f;
            float centroY = alto / 2f;

            foreach (Point3D v in vertices3D)
            {
                // Proyección perspectiva
                float factor = 1f / (1f + v.Z * FactorPerspectiva);
                float x2D = centroX + v.X * EscalaProyeccion * factor;
                float y2D = centroY - v.Y * EscalaProyeccion * factor;
                vertices2D.Add(new PointF(x2D, y2D));
            }

            return vertices2D;
        }

        private Rectangle ObtenerRectanguloDelimitador(PointF[] puntos)
        {
            if (puntos.Length == 0)
                return new Rectangle(0, 0, 1, 1);

            float minX = puntos[0].X;
            float maxX = puntos[0].X;
            float minY = puntos[0].Y;
            float maxY = puntos[0].Y;

            foreach (PointF p in puntos)
            {
                if (p.X < minX) minX = p.X;
                if (p.X > maxX) maxX = p.X;
                if (p.Y < minY) minY = p.Y;
                if (p.Y > maxY) maxY = p.Y;
            }

            int x = (int)minX;
            int y = (int)minY;
            int w = (int)(maxX - minX);
            int h = (int)(maxY - minY);

            // Asegurar dimensiones mínimas
            if (w < 1) w = 1;
            if (h < 1) h = 1;

            return new Rectangle(x, y, w, h);
        }

        private void DibujarSeleccion(Graphics g, List<PointF> vertices2D)
        {
            if (vertices2D.Count == 0) return;

            // Encontrar límites
            float minX = vertices2D[0].X, maxX = vertices2D[0].X;
            float minY = vertices2D[0].Y, maxY = vertices2D[0].Y;

            foreach (PointF p in vertices2D)
            {
                if (p.X < minX) minX = p.X;
                if (p.X > maxX) maxX = p.X;
                if (p.Y < minY) minY = p.Y;
                if (p.Y > maxY) maxY = p.Y;
            }

            // Dibujar rectángulo de selección
            using (Pen pen = new Pen(Color.Yellow, 2))
            {
                pen.DashStyle = DashStyle.Dash;
                g.DrawRectangle(pen, minX - 5, minY - 5, maxX - minX + 10, maxY - minY + 10);
            }
        }

        public void DibujarInformacionDebug(Graphics g, Figura3D figura, int x, int y)
        {
            using (Font font = new Font("Consolas", 9))
            using (Brush brush = new SolidBrush(Color.White))
            using (Brush bgBrush = new SolidBrush(Color.FromArgb(180, 0, 0, 0)))
            {
                string info = $"Figura: {figura.Nombre}\n" +
                             $"Pos: ({figura.Posicion.X:F1}, {figura.Posicion.Y:F1}, {figura.Posicion.Z:F1})\n" +
                             $"Rot: ({figura.Rotacion.X:F0}°, {figura.Rotacion.Y:F0}°, {figura.Rotacion.Z:F0}°)\n" +
                             $"Escala: ({figura.Escala.X:F2}, {figura.Escala.Y:F2}, {figura.Escala.Z:F2})\n" +
                             $"Material: Brillo={figura.Material.Brillo:F0}\n" +
                             $"Textura: {figura.Material.Textura}\n" +
                             $"Iluminación: {(figura.Iluminacion.Habilitada ? "ON" : "OFF")}";

                SizeF size = g.MeasureString(info, font);
                g.FillRectangle(bgBrush, x, y, size.Width + 10, size.Height + 10);
                g.DrawString(info, font, brush, x + 5, y + 5);
            }
        }

        private void DibujarMalla3D(Graphics g, List<int[]> caras, List<PointF> vertices2D)
        {
            using (Pen penMalla = new Pen(ColorMalla, GrosorMalla))
            {
                // Para evitar dibujar la misma arista dos veces, usamos un HashSet
                HashSet<string> aristasDibujadas = new HashSet<string>();

                foreach (int[] cara in caras)
                {
                    if (cara.Length < 3) continue;

                    // Dibujar cada arista de la cara
                    for (int i = 0; i < cara.Length; i++)
                    {
                        int v1Idx = cara[i];
                        int v2Idx = cara[(i + 1) % cara.Length];

                        // Crear un identificador único para la arista (orden no importa)
                        string aristaKey = v1Idx < v2Idx ?
                            $"{v1Idx}-{v2Idx}" : $"{v2Idx}-{v1Idx}";

                        // Si la arista no ha sido dibujada, dibujarla
                        if (!aristasDibujadas.Contains(aristaKey))
                        {
                            aristasDibujadas.Add(aristaKey);

                            // Verificar que los índices son válidos
                            if (v1Idx >= 0 && v1Idx < vertices2D.Count &&
                                v2Idx >= 0 && v2Idx < vertices2D.Count)
                            {
                                g.DrawLine(penMalla, vertices2D[v1Idx], vertices2D[v2Idx]);
                            }
                        }
                    }
                }
            }
        }
    }

    internal class CaraRenderizada
    {
        public int[] Indices { get; set; }
        public Point3D Normal { get; set; }
        public float Profundidad { get; set; }
        public Point3D CentroSuperficie { get; set; }
    }
}
