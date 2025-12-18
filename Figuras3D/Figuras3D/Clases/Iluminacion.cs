using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Figuras3D.Clases
{
    public class Iluminacion
    {
        public Point3D DireccionLuz { get; set; } = new Point3D(0.5f, -0.7f, 0.5f);

        public Color ColorLuz { get; set; } = Color.White;

        public float IntensidadLuz { get; set; } = 1.8f;
      
        public Color ColorAmbiente { get; set; } = Color.FromArgb(70, 70, 75);

        public float IntensidadAmbiente { get; set; } = 0.6f;

        public Point3D PosicionCamara { get; set; } = new Point3D(0, 0, 5);

        public bool Habilitada { get; set; } = true;

        public float Contraste { get; set; } = 1.5f;

        public Iluminacion()
        {
            NormalizarDireccionLuz();
        }

        private void NormalizarDireccionLuz()
        {
            float magnitud = (float)Math.Sqrt(
                DireccionLuz.X * DireccionLuz.X +
                DireccionLuz.Y * DireccionLuz.Y +
                DireccionLuz.Z * DireccionLuz.Z
            );

            if (magnitud > 0)
            {
                DireccionLuz = new Point3D(
                    DireccionLuz.X / magnitud,
                    DireccionLuz.Y / magnitud,
                    DireccionLuz.Z / magnitud
                );
            }
        }

        public void EstablecerDireccionLuz(float x, float y, float z)
        {
            DireccionLuz = new Point3D(x, y, z);
            NormalizarDireccionLuz();
        }

        public Color CalcularColorIluminado(Material material, Point3D normal, Point3D posicionSuperficie)
        {
            if (!Habilitada)
            {
                return material.ColorDifuso;
            }

            // Normalizar el vector normal
            normal = NormalizarVector(normal);

            // 1. COMPONENTE AMBIENTAL (base mínima de luz)
            float ambR = ColorAmbiente.R * IntensidadAmbiente * material.FactorAmbiente;
            float ambG = ColorAmbiente.G * IntensidadAmbiente * material.FactorAmbiente;
            float ambB = ColorAmbiente.B * IntensidadAmbiente * material.FactorAmbiente;

            // 2. COMPONENTE DIFUSA (Ley de Lambert) - MEJORADA
            float productoPunto = Math.Max(0, -(
                normal.X * DireccionLuz.X +
                normal.Y * DireccionLuz.Y +
                normal.Z * DireccionLuz.Z
            ));

            // Aplicar contraste EXTREMO para diferencias dramáticas
            productoPunto = (float)Math.Pow(productoPunto, 1.0f / Contraste);

            float difR = material.ColorDifuso.R * productoPunto * IntensidadLuz * material.FactorDifuso;
            float difG = material.ColorDifuso.G * productoPunto * IntensidadLuz * material.FactorDifuso;
            float difB = material.ColorDifuso.B * productoPunto * IntensidadLuz * material.FactorDifuso;

            // 3. COMPONENTE ESPECULAR (Reflexión de Phong) - ULTRA INTENSIFICADA
            float espR = 0, espG = 0, espB = 0;

            if (productoPunto > 0)
            {
                // Vector de reflexión
                Point3D reflexion = CalcularReflexion(DireccionLuz, normal);

                // Vector hacia la cámara
                Point3D vectorCamara = new Point3D(
                    PosicionCamara.X - posicionSuperficie.X,
                    PosicionCamara.Y - posicionSuperficie.Y,
                    PosicionCamara.Z - posicionSuperficie.Z
                );
                vectorCamara = NormalizarVector(vectorCamara);

                // Producto punto para especular
                float especular = Math.Max(0,
                    reflexion.X * vectorCamara.X +
                    reflexion.Y * vectorCamara.Y +
                    reflexion.Z * vectorCamara.Z
                );

                // Aplicar exponente de brillo
                especular = (float)Math.Pow(especular, material.Brillo);

                // MULTIPLICADOR AUMENTADO para brillos MÁS visibles
                float multiplicadorEspecular = 2.2f; // Antes era 1.5, ahora 2.2 para brillos más intensos

                espR = material.ColorEspecular.R * especular * IntensidadLuz * material.FactorEspecular * multiplicadorEspecular;
                espG = material.ColorEspecular.G * especular * IntensidadLuz * material.FactorEspecular * multiplicadorEspecular;
                espB = material.ColorEspecular.B * especular * IntensidadLuz * material.FactorEspecular * multiplicadorEspecular;
            }

            // Combinar todas las componentes con saturación mejorada
            int r = Math.Min(255, (int)(ambR + difR + espR));
            int g = Math.Min(255, (int)(ambG + difG + espG));
            int b = Math.Min(255, (int)(ambB + difB + espB));

            // Aplicar opacidad
            int alpha = (int)(material.Opacidad * 255);

            return Color.FromArgb(alpha, r, g, b);
        }

        /// <summary>
        /// Calcula el vector de reflexión
        /// </summary>
        private Point3D CalcularReflexion(Point3D incidente, Point3D normal)
        {
            float producto = 2 * (
                incidente.X * normal.X +
                incidente.Y * normal.Y +
                incidente.Z * normal.Z
            );

            return new Point3D(
                incidente.X - producto * normal.X,
                incidente.Y - producto * normal.Y,
                incidente.Z - producto * normal.Z
            );
        }

        private Point3D NormalizarVector(Point3D v)
        {
            float magnitud = (float)Math.Sqrt(v.X * v.X + v.Y * v.Y + v.Z * v.Z);

            if (magnitud > 0.0001f)
            {
                return new Point3D(v.X / magnitud, v.Y / magnitud, v.Z / magnitud);
            }

            return new Point3D(0, 0, 0);
        }


        public static Point3D CalcularNormalCara(Point3D v1, Point3D v2, Point3D v3)
        {
            // Vectores de los lados del triángulo
            Point3D u = new Point3D(v2.X - v1.X, v2.Y - v1.Y, v2.Z - v1.Z);
            Point3D v = new Point3D(v3.X - v1.X, v3.Y - v1.Y, v3.Z - v1.Z);

            // Producto cruzado
            Point3D normal = new Point3D(
                u.Y * v.Z - u.Z * v.Y,
                u.Z * v.X - u.X * v.Z,
                u.X * v.Y - u.Y * v.X
            );

            // Normalizar
            float magnitud = (float)Math.Sqrt(
                normal.X * normal.X +
                normal.Y * normal.Y +
                normal.Z * normal.Z
            );

            if (magnitud > 0.0001f)
            {
                return new Point3D(
                    normal.X / magnitud,
                    normal.Y / magnitud,
                    normal.Z / magnitud
                );
            }

            return new Point3D(0, 0, 1);
        }


        public static Iluminacion CrearIluminacionPerfecta()
        {
            return new Iluminacion()
            {
                DireccionLuz = new Point3D(0.4f, -0.6f, 0.7f),
                ColorLuz = Color.FromArgb(255, 255, 255),
                IntensidadLuz = 2.0f,        // MUY INTENSA
                ColorAmbiente = Color.FromArgb(90, 90, 95),
                IntensidadAmbiente = 0.65f,  // Alta luz ambiente
                Contraste = 1.6f             // Contraste muy alto
            };
        }

        public static Iluminacion CrearIluminacionEstudio()
        {
            return new Iluminacion()
            {
                DireccionLuz = new Point3D(0.5f, -0.6f, 0.6f),
                ColorLuz = Color.FromArgb(255, 255, 250),
                IntensidadLuz = 1.8f,
                ColorAmbiente = Color.FromArgb(85, 85, 90),
                IntensidadAmbiente = 0.6f,
                Contraste = 1.5f
            };
        }

        public static Iluminacion CrearIluminacionDramatica()
        {
            return new Iluminacion()
            {
                DireccionLuz = new Point3D(0.8f, -0.4f, 0.4f),
                ColorLuz = Color.White,
                IntensidadLuz = 2.5f,        // EXTREMADAMENTE INTENSA
                ColorAmbiente = Color.FromArgb(25, 25, 30),
                IntensidadAmbiente = 0.3f,   // Muy poca luz ambiente
                Contraste = 2.0f             // Contraste MÁXIMO
            };
        }
    }
}
