using System;
using System.Drawing;
using System.Collections.Generic;

namespace Figuras3D
{
    /// <summary>
    /// CLASE PIRÁMIDE - Figura 3D básica
    /// Cómo usar desde la interfaz:
    /// 1. Crear: Piramide piramide = new Piramide("MiPiramide");
    /// 2. Por defecto: base cuadrada de 1x1, altura=2
    /// </summary>
    public class Piramide : Figura3D
    {
        /// <summary>
        /// Constructor de la pirámide
        /// </summary>
        public Piramide(string nombre = "Pirámide") : base(nombre)
        {
            ColorFigura = Color.Purple; // Color por defecto
        }

        /// <summary>
        /// Genera la geometría de la pirámide (base cuadrada)
        /// </summary>
        public override void GenerarGeometria()
        {
            vertices.Clear();
            caras.Clear();

            float tamBase = 1.0f;
            float altura = 2.0f;
            float mitadBase = tamBase / 2.0f;
            float mitadAltura = altura / 2.0f;

            // Vértices de la base (cuadrado en Y = -mitadAltura)
            vertices.Add(new Point3D(-mitadBase, -mitadAltura, mitadBase));   // 0: Frente izquierda
            vertices.Add(new Point3D(mitadBase, -mitadAltura, mitadBase));    // 1: Frente derecha
            vertices.Add(new Point3D(mitadBase, -mitadAltura, -mitadBase));   // 2: Atrás derecha
            vertices.Add(new Point3D(-mitadBase, -mitadAltura, -mitadBase));  // 3: Atrás izquierda

            // Vértice superior (punta de la pirámide)
            vertices.Add(new Point3D(0, mitadAltura, 0)); // 4: Punta

            // Base de la pirámide (2 triángulos que forman un cuadrado)
            caras.Add(new int[] { 0, 1, 2 }); // Primer triángulo de la base
            caras.Add(new int[] { 0, 2, 3 }); // Segundo triángulo de la base

            // Caras laterales (4 triángulos)
            caras.Add(new int[] { 0, 4, 1 }); // Cara frontal
            caras.Add(new int[] { 1, 4, 2 }); // Cara derecha
            caras.Add(new int[] { 2, 4, 3 }); // Cara trasera
            caras.Add(new int[] { 3, 4, 0 }); // Cara izquierda
        }
    }
}