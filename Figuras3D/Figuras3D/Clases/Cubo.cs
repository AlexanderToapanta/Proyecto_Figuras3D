using System;
using System.Drawing;
using System.Collections.Generic;

namespace Figuras3D
{

    public class Cubo : Figura3D
    {

        public Cubo(string nombre = "Cubo") : base(nombre)
        {
            ColorFigura = Color.Blue; 
        }

        public override void GenerarGeometria()
        {
            vertices.Clear();
            caras.Clear();

            vertices.Add(new Point3D(-0.5f, -0.5f, 0.5f));  // 0: Frente abajo izquierda
            vertices.Add(new Point3D(0.5f, -0.5f, 0.5f));   // 1: Frente abajo derecha
            vertices.Add(new Point3D(0.5f, 0.5f, 0.5f));    // 2: Frente arriba derecha
            vertices.Add(new Point3D(-0.5f, 0.5f, 0.5f));   // 3: Frente arriba izquierda

            vertices.Add(new Point3D(-0.5f, -0.5f, -0.5f)); // 4: Atrás abajo izquierda
            vertices.Add(new Point3D(0.5f, -0.5f, -0.5f));  // 5: Atrás abajo derecha
            vertices.Add(new Point3D(0.5f, 0.5f, -0.5f));   // 6: Atrás arriba derecha
            vertices.Add(new Point3D(-0.5f, 0.5f, -0.5f));  // 7: Atrás arriba izquierda


            // Cara FRONTAL (2 triángulos)
            caras.Add(new int[] { 0, 1, 2 });
            caras.Add(new int[] { 0, 2, 3 });  

            // Cara TRASERA (2 triángulos)
            caras.Add(new int[] { 5, 4, 7 }); 
            caras.Add(new int[] { 5, 7, 6 }); 

            // Cara IZQUIERDA (2 triángulos)
            caras.Add(new int[] { 4, 0, 3 }); 
            caras.Add(new int[] { 4, 3, 7 }); 

            // Cara DERECHA (2 triángulos)
            caras.Add(new int[] { 1, 5, 6 }); 
            caras.Add(new int[] { 1, 6, 2 }); 

            // Cara SUPERIOR (2 triángulos)
            caras.Add(new int[] { 3, 2, 6 }); 
            caras.Add(new int[] { 3, 6, 7 }); 

            // Cara INFERIOR (2 triángulos)
            caras.Add(new int[] { 4, 5, 1 }); 
            caras.Add(new int[] { 4, 1, 0 }); 
        }
    }
}