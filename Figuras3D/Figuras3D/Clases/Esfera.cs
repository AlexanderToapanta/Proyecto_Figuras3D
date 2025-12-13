using System;
using System.Drawing;

namespace Figuras3D
{
    /// <summary>
    /// CLASE ESFERA - Figura 3D básica
    /// </summary>
    public class Esfera : Figura3D
    {
        private int segmentos; // Número de divisiones horizontales y verticales

        /// <summary>
        /// Constructor de la esfera
        /// </summary>
        public Esfera(string nombre = "Esfera", int segmentos = 16) : base(nombre)
        {
            this.segmentos = Math.Max(8, segmentos); // Mínimo 8 segmentos
            ColorFigura = Color.Red; // Color por defecto
            GenerarGeometria();
        }

        /// <summary>
        /// Genera la geometría de la esfera
        /// </summary>
        public override void GenerarGeometria()
        {
            vertices.Clear();
            caras.Clear();

            // Número de anillos verticales y divisiones horizontales
            int anillos = segmentos / 2;
            int divisiones = segmentos;

            // Generar vértices
            for (int i = 0; i <= anillos; i++)
            {
                // Ángulo vertical (de 0 a 180 grados)
                double phi = Math.PI * i / anillos;

                for (int j = 0; j <= divisiones; j++)
                {
                    // Ángulo horizontal (de 0 a 360 grados)
                    double theta = 2 * Math.PI * j / divisiones;

                    // Calcular posición del vértice
                    float x = (float)(Math.Sin(phi) * Math.Cos(theta));
                    float y = (float)(Math.Cos(phi));
                    float z = (float)(Math.Sin(phi) * Math.Sin(theta));

                    vertices.Add(new Point3D(x, y, z));
                }
            }

            // Generar caras (triángulos)
            for (int i = 0; i < anillos; i++)
            {
                for (int j = 0; j < divisiones; j++)
                {
                    // Calcular índices de los 4 vértices de un cuadrilátero
                    int primero = i * (divisiones + 1) + j;
                    int segundo = primero + 1;
                    int tercero = primero + (divisiones + 1);
                    int cuarto = tercero + 1;

                    // Dividir cuadrilátero en 2 triángulos
                    caras.Add(new int[] { primero, tercero, segundo });
                    caras.Add(new int[] { segundo, tercero, cuarto });
                }
            }
        }
    }
}