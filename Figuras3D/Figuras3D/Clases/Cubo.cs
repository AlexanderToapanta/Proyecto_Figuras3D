using System;
using System.Drawing;
using System.Collections.Generic;

namespace Figuras3D
{
    /// <summary>
    /// CLASE CUBO - Figura 3D básica
    /// Cómo usar desde la interfaz:
    /// 1. Crear: Cubo cubo = new Cubo("MiCubo");
    /// 2. Tamaño por defecto: 1x1x1 (centrado en origen)
    /// 3. Se puede cambiar tamaño con Escala
    /// </summary>
    public class Cubo : Figura3D
    {
        /// <summary>
        /// Constructor del cubo
        /// </summary>
        public Cubo(string nombre = "Cubo") : base(nombre)
        {
            ColorFigura = Color.Blue; // Color por defecto
        }

        /// <summary>
        /// Genera la geometría del cubo (8 vértices, 12 caras triangulares)
        /// </summary>
        public override void GenerarGeometria()
        {
            // Limpiar listas
            vertices.Clear();
            caras.Clear();

            // Definir los 8 vértices del cubo (centrado en origen)
            // Cada vértice tiene coordenadas X, Y, Z
            vertices.Add(new Point3D(-0.5f, -0.5f, 0.5f));  // 0: Frente abajo izquierda
            vertices.Add(new Point3D(0.5f, -0.5f, 0.5f));   // 1: Frente abajo derecha
            vertices.Add(new Point3D(0.5f, 0.5f, 0.5f));    // 2: Frente arriba derecha
            vertices.Add(new Point3D(-0.5f, 0.5f, 0.5f));   // 3: Frente arriba izquierda

            vertices.Add(new Point3D(-0.5f, -0.5f, -0.5f)); // 4: Atrás abajo izquierda
            vertices.Add(new Point3D(0.5f, -0.5f, -0.5f));  // 5: Atrás abajo derecha
            vertices.Add(new Point3D(0.5f, 0.5f, -0.5f));   // 6: Atrás arriba derecha
            vertices.Add(new Point3D(-0.5f, 0.5f, -0.5f));  // 7: Atrás arriba izquierda

            // Definir las 12 caras triangulares (2 triángulos por cada cara del cubo)
            // Cada arreglo son 3 índices que forman un triángulo

            // Cara FRONTAL (2 triángulos)
            caras.Add(new int[] { 0, 1, 2 }); // Triángulo 1
            caras.Add(new int[] { 0, 2, 3 }); // Triángulo 2

            // Cara TRASERA (2 triángulos)
            caras.Add(new int[] { 5, 4, 7 }); // Triángulo 1
            caras.Add(new int[] { 5, 7, 6 }); // Triángulo 2

            // Cara IZQUIERDA (2 triángulos)
            caras.Add(new int[] { 4, 0, 3 }); // Triángulo 1
            caras.Add(new int[] { 4, 3, 7 }); // Triángulo 2

            // Cara DERECHA (2 triángulos)
            caras.Add(new int[] { 1, 5, 6 }); // Triángulo 1
            caras.Add(new int[] { 1, 6, 2 }); // Triángulo 2

            // Cara SUPERIOR (2 triángulos)
            caras.Add(new int[] { 3, 2, 6 }); // Triángulo 1
            caras.Add(new int[] { 3, 6, 7 }); // Triángulo 2

            // Cara INFERIOR (2 triángulos)
            caras.Add(new int[] { 4, 5, 1 }); // Triángulo 1
            caras.Add(new int[] { 4, 1, 0 }); // Triángulo 2
        }
    }
}