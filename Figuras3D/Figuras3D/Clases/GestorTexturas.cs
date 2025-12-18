using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Figuras3D.Clases
{
    internal class GestorTexturas
    {
        /// <summary>
        /// Obtiene un brush para una textura específica
        /// </summary>
        public static Brush ObtenerBrushTextura(Material material, Rectangle area)
        {
            switch (material.Textura)
            {
                case TipoTextura.Solido:
                    return new SolidBrush(Color.FromArgb(
                        (int)(material.Opacidad * 255),
                        material.ColorDifuso
                    ));

                case TipoTextura.Degradado:
                    return CrearDegradado(material, area);

                case TipoTextura.Cuadricula:
                    return CrearCuadricula(material);

                case TipoTextura.Rayas:
                    return CrearRayas(material, false);

                case TipoTextura.RayasVerticales:
                    return CrearRayas(material, true);

                case TipoTextura.Puntos:
                    return CrearPuntos(material);

                case TipoTextura.Marmol:
                    return CrearMarmol(material);

                case TipoTextura.Ladrillo:
                    return CrearLadrillo(material);

                default:
                    return new SolidBrush(material.ColorDifuso);
            }
        }

        /// <summary>
        /// Crea un degradado entre dos colores
        /// </summary>
        private static Brush CrearDegradado(Material material, Rectangle area)
        {
            if (area.Width <= 0 || area.Height <= 0)
            {
                return new SolidBrush(material.ColorDifuso);
            }

            try
            {
                LinearGradientBrush brush = new LinearGradientBrush(
                    area,
                    material.ColorDifuso,
                    material.ColorSecundario,
                    LinearGradientMode.Vertical
                );
                return brush;
            }
            catch
            {
                return new SolidBrush(material.ColorDifuso);
            }
        }

        /// <summary>
        /// Crea un patrón de cuadrícula
        /// </summary>
        private static Brush CrearCuadricula(Material material)
        {
            int tamano = (int)(20 * material.EscalaTextura);
            if (tamano < 4) tamano = 4;

            Bitmap bitmap = new Bitmap(tamano, tamano);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                // Fondo
                g.Clear(material.ColorDifuso);

                // Líneas de cuadrícula
                using (Pen pen = new Pen(material.ColorSecundario, 1))
                {
                    g.DrawLine(pen, 0, 0, tamano, 0);
                    g.DrawLine(pen, 0, 0, 0, tamano);
                }
            }

            TextureBrush textureBrush = new TextureBrush(bitmap);
            return textureBrush;
        }

        /// <summary>
        /// Crea un patrón de rayas
        /// </summary>
        private static Brush CrearRayas(Material material, bool vertical)
        {
            int ancho = (int)(20 * material.EscalaTextura);
            if (ancho < 4) ancho = 4;

            int width = vertical ? ancho : ancho * 2;
            int height = vertical ? ancho * 2 : ancho;

            Bitmap bitmap = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.Clear(material.ColorDifuso);

                using (SolidBrush brush = new SolidBrush(material.ColorSecundario))
                {
                    if (vertical)
                    {
                        g.FillRectangle(brush, 0, 0, ancho / 2, height);
                    }
                    else
                    {
                        g.FillRectangle(brush, 0, 0, width, ancho / 2);
                    }
                }
            }

            TextureBrush textureBrush = new TextureBrush(bitmap);
            return textureBrush;
        }

        /// <summary>
        /// Crea un patrón de puntos
        /// </summary>
        private static Brush CrearPuntos(Material material)
        {
            int tamano = (int)(15 * material.EscalaTextura);
            if (tamano < 4) tamano = 4;

            Bitmap bitmap = new Bitmap(tamano, tamano);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.Clear(material.ColorDifuso);
                g.SmoothingMode = SmoothingMode.AntiAlias;

                using (SolidBrush brush = new SolidBrush(material.ColorSecundario))
                {
                    int radio = tamano / 6;
                    if (radio < 1) radio = 1;
                    g.FillEllipse(brush, tamano / 4, tamano / 4, radio, radio);
                }
            }

            TextureBrush textureBrush = new TextureBrush(bitmap);
            return textureBrush;
        }

        /// <summary>
        /// Crea un efecto de mármol
        /// </summary>
        private static Brush CrearMarmol(Material material)
        {
            int tamano = (int)(60 * material.EscalaTextura);
            if (tamano < 20) tamano = 20;

            Bitmap bitmap = new Bitmap(tamano, tamano);
            Random rnd = new Random(42); // Seed fija para consistencia

            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.Clear(material.ColorDifuso);
                g.SmoothingMode = SmoothingMode.AntiAlias;

                // Crear venas de mármol
                using (Pen pen = new Pen(material.ColorSecundario, 2))
                {
                    for (int i = 0; i < 5; i++)
                    {
                        Point[] puntos = new Point[4];
                        for (int j = 0; j < 4; j++)
                        {
                            puntos[j] = new Point(
                                rnd.Next(0, tamano),
                                rnd.Next(0, tamano)
                            );
                        }
                        g.DrawCurve(pen, puntos, 0.5f);
                    }
                }
            }

            TextureBrush textureBrush = new TextureBrush(bitmap);
            return textureBrush;
        }

        /// <summary>
        /// Crea un patrón de ladrillos
        /// </summary>
        private static Brush CrearLadrillo(Material material)
        {
            int anchoLadrillo = (int)(40 * material.EscalaTextura);
            int altoLadrillo = (int)(20 * material.EscalaTextura);
            if (anchoLadrillo < 8) anchoLadrillo = 8;
            if (altoLadrillo < 4) altoLadrillo = 4;

            Bitmap bitmap = new Bitmap(anchoLadrillo, altoLadrillo * 2);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.Clear(material.ColorSecundario);

                using (SolidBrush brush = new SolidBrush(material.ColorDifuso))
                {
                    // Primera fila
                    g.FillRectangle(brush, 1, 1, anchoLadrillo - 2, altoLadrillo - 2);

                    // Segunda fila (desplazada)
                    g.FillRectangle(brush, 1, altoLadrillo + 1, anchoLadrillo / 2 - 2, altoLadrillo - 2);
                    g.FillRectangle(brush, anchoLadrillo / 2 + 1, altoLadrillo + 1,
                        anchoLadrillo / 2 - 2, altoLadrillo - 2);
                }
            }

            TextureBrush textureBrush = new TextureBrush(bitmap);
            return textureBrush;
        }

        /// <summary>
        /// Aplica un color base con variación para simular textura
        /// </summary>
        public static Color AplicarVariacionTextura(Material material, Point3D coordenada, Random rnd)
        {
            Color colorBase = material.ColorDifuso;

            switch (material.Textura)
            {
                case TipoTextura.Solido:
                    return colorBase;

                case TipoTextura.Cuadricula:
                    {
                        int escala = (int)(10 * material.EscalaTextura);
                        if (escala < 1) escala = 1;
                        bool enLinea = ((int)(coordenada.X * escala) % 10 == 0) ||
                                      ((int)(coordenada.Y * escala) % 10 == 0);
                        return enLinea ? material.ColorSecundario : colorBase;
                    }

                case TipoTextura.Rayas:
                    {
                        int escala = (int)(5 * material.EscalaTextura);
                        if (escala < 1) escala = 1;
                        bool enRaya = ((int)(coordenada.Y * escala) % 2 == 0);
                        return enRaya ? colorBase : material.ColorSecundario;
                    }

                case TipoTextura.RayasVerticales:
                    {
                        int escala = (int)(5 * material.EscalaTextura);
                        if (escala < 1) escala = 1;
                        bool enRaya = ((int)(coordenada.X * escala) % 2 == 0);
                        return enRaya ? colorBase : material.ColorSecundario;
                    }

                case TipoTextura.Puntos:
                    {
                        int escala = (int)(5 * material.EscalaTextura);
                        if (escala < 1) escala = 1;
                        float distX = (coordenada.X * escala) % 1;
                        float distY = (coordenada.Y * escala) % 1;
                        float dist = (float)Math.Sqrt(
                            (distX - 0.5f) * (distX - 0.5f) +
                            (distY - 0.5f) * (distY - 0.5f)
                        );
                        return dist < 0.3f ? material.ColorSecundario : colorBase;
                    }

                case TipoTextura.Marmol:
                    {
                        // Ruido Perlin simplificado
                        float ruido = (float)(Math.Sin(coordenada.X * 5) *
                                            Math.Cos(coordenada.Y * 3) * 0.5 + 0.5);
                        int variacion = (int)(ruido * 40 - 20);
                        return Color.FromArgb(
                            Math.Max(0, Math.Min(255, colorBase.R + variacion)),
                            Math.Max(0, Math.Min(255, colorBase.G + variacion)),
                            Math.Max(0, Math.Min(255, colorBase.B + variacion))
                        );
                    }

                default:
                    return colorBase;
            }
        }
    }
}
