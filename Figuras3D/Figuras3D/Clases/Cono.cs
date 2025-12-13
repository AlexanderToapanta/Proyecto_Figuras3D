using System;
using System.Drawing;

namespace Figuras3D
{
    /// <summary>
    /// CLASE CONO - Figura 3D básica
    /// </summary>
    public class Cono : Figura3D
    {
        private int segmentos; // Número de lados del cono

        /// <summary>
        /// Constructor del cono
        /// </summary>
        public Cono(string nombre = "Cono", int segmentos = 16) : base(nombre)
        {
            this.segmentos = Math.Max(3, segmentos); // Mínimo 3 lados
            ColorFigura = Color.Orange; // Color por defecto
            GenerarGeometria(); // ← generar geometría ahora que 'segmentos' está inicializado
        }

        public override void GenerarGeometria()
        {
            vertices.Clear();
            caras.Clear();

            float radio = 0.5f;
            float altura = 2.0f;
            float mitadAltura = altura / 2.0f;

            // Vértice superior (punta del cono)
            vertices.Add(new Point3D(0, mitadAltura, 0)); // índice 0

            // Centro de la base
            vertices.Add(new Point3D(0, -mitadAltura, 0)); // índice 1

            // Vértices de la base
            for (int i = 0; i < segmentos; i++)
            {
                float angulo = 2 * (float)Math.PI * i / segmentos;
                float x = radio * (float)Math.Cos(angulo);
                float z = radio * (float)Math.Sin(angulo);
                vertices.Add(new Point3D(x, -mitadAltura, z)); // índices 2 a segmentos+1
            }

            // Caras laterales
            for (int i = 0; i < segmentos; i++)
            {
                int actual = 2 + i;
                int siguiente = 2 + ((i + 1) % segmentos);
                caras.Add(new int[] { 0, siguiente, actual });
            }

            // Caras de la base
            for (int i = 0; i < segmentos; i++)
            {
                int actual = 2 + i;
                int siguiente = 2 + ((i + 1) % segmentos);
                caras.Add(new int[] { 1, actual, siguiente });
            }
        }
    }
}