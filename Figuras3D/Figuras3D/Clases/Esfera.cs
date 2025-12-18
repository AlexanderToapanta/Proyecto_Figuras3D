using System;
using System.Drawing;

namespace Figuras3D
{
    public class Esfera : Figura3D
    {
        private int segmentos;

        public Esfera(string nombre = "Esfera", int segmentos = 16) : base(nombre)
        {
            this.segmentos = Math.Max(8, segmentos);
            ColorFigura = Color.Red; 
            GenerarGeometria();
        }

        public override void GenerarGeometria()
        {
            vertices.Clear();
            caras.Clear();

            int anillos = segmentos / 2;
            int divisiones = segmentos;

            for (int i = 0; i <= anillos; i++)
            {
                double phi = Math.PI * i / anillos;

                for (int j = 0; j <= divisiones; j++)
                {
                    double theta = 2 * Math.PI * j / divisiones;

                    float x = (float)(Math.Sin(phi) * Math.Cos(theta));
                    float y = (float)(Math.Cos(phi));
                    float z = (float)(Math.Sin(phi) * Math.Sin(theta));

                    vertices.Add(new Point3D(x, y, z));
                }
            }

            for (int i = 0; i < anillos; i++)
            {
                for (int j = 0; j < divisiones; j++)
                {
                    int primero = i * (divisiones + 1) + j;
                    int segundo = primero + 1;
                    int tercero = primero + (divisiones + 1);
                    int cuarto = tercero + 1;

                    caras.Add(new int[] { primero, tercero, segundo });
                    caras.Add(new int[] { segundo, tercero, cuarto });
                }
            }
        }
    }
}