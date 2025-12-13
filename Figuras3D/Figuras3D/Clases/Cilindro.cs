using System;
using System.Drawing;


namespace Figuras3D
{
    /// <summary>
    /// CLASE CILINDRO - Figura 3D básica
    /// </summary>
    public class Cilindro : Figura3D
    {
        private int segmentos; // Número de lados del cilindro

        /// <summary>
        /// Constructor del cilindro
        /// </summary>
        public Cilindro(string nombre = "Cilindro", int segmentos = 16) : base(nombre)
        {
            this.segmentos = Math.Max(3, segmentos); // Mínimo 3 lados (triángulo)
            ColorFigura = Color.Green; // Color por defecto
            GenerarGeometria();
        }

        /// <summary>
        /// Genera la geometría del cilindro
        /// </summary>
        public override void GenerarGeometria()
        {
            vertices.Clear();
            caras.Clear();

            float radio = 0.5f;
            float altura = 2.0f;
            float mitadAltura = altura / 2.0f;

            // Centro de la tapa superior
            vertices.Add(new Point3D(0, mitadAltura, 0)); // índice 0

            // Centro de la tapa inferior
            vertices.Add(new Point3D(0, -mitadAltura, 0)); // índice 1

            // Vértices de la tapa superior
            for (int i = 0; i < segmentos; i++)
            {
                float angulo = 2 * (float)Math.PI * i / segmentos;
                float x = radio * (float)Math.Cos(angulo);
                float z = radio * (float)Math.Sin(angulo);
                vertices.Add(new Point3D(x, mitadAltura, z)); // índices 2 a segmentos+1
            }

            // Vértices de la tapa inferior
            for (int i = 0; i < segmentos; i++)
            {
                float angulo = 2 * (float)Math.PI * i / segmentos;
                float x = radio * (float)Math.Cos(angulo);
                float z = radio * (float)Math.Sin(angulo);
                vertices.Add(new Point3D(x, -mitadAltura, z)); // índices segmentos+2 a 2*segmentos+1
            }

            // Caras de la tapa superior (triángulos desde el centro)
            for (int i = 0; i < segmentos; i++)
            {
                int actual = 2 + i;
                int siguiente = 2 + ((i + 1) % segmentos);
                caras.Add(new int[] { 0, actual, siguiente });
            }

            // Caras de la tapa inferior (triángulos hacia el centro)
            for (int i = 0; i < segmentos; i++)
            {
                int actual = 2 + segmentos + i;
                int siguiente = 2 + segmentos + ((i + 1) % segmentos);
                caras.Add(new int[] { 1, siguiente, actual }); // Orden invertido para que mire hacia afuera
            }

            // Caras laterales (rectángulos divididos en 2 triángulos)
            for (int i = 0; i < segmentos; i++)
            {
                int superiorActual = 2 + i;
                int superiorSiguiente = 2 + ((i + 1) % segmentos);
                int inferiorActual = 2 + segmentos + i;
                int inferiorSiguiente = 2 + segmentos + ((i + 1) % segmentos);

                // Primer triángulo del rectángulo lateral
                caras.Add(new int[] { superiorActual, inferiorActual, superiorSiguiente });
                // Segundo triángulo del rectángulo lateral
                caras.Add(new int[] { superiorSiguiente, inferiorActual, inferiorSiguiente });
            }
        }
    }
}