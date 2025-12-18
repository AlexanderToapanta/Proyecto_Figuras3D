using System;
using System.Drawing;
using System.Collections.Generic;

namespace Figuras3D
{

    public class Piramide : Figura3D
    {

        public Piramide(string nombre = "Pirámide") : base(nombre)
        {
            ColorFigura = Color.Purple; // Color por defecto
        }

        public override void GenerarGeometria()
        {
            vertices.Clear();
            caras.Clear();

            float tamBase = 1.0f;
            float altura = 2.0f;
            float mitadBase = tamBase / 2.0f;
            float mitadAltura = altura / 2.0f;

            vertices.Add(new Point3D(-mitadBase, -mitadAltura, mitadBase));   
            vertices.Add(new Point3D(mitadBase, -mitadAltura, mitadBase));    
            vertices.Add(new Point3D(mitadBase, -mitadAltura, -mitadBase));   
            vertices.Add(new Point3D(-mitadBase, -mitadAltura, -mitadBase));  

            vertices.Add(new Point3D(0, mitadAltura, 0)); 

            caras.Add(new int[] { 0, 1, 2 }); 
            caras.Add(new int[] { 0, 2, 3 }); 

            caras.Add(new int[] { 0, 4, 1 }); 
            caras.Add(new int[] { 1, 4, 2 }); 
            caras.Add(new int[] { 2, 4, 3 }); 
            caras.Add(new int[] { 3, 4, 0 }); 
        }
    }
}