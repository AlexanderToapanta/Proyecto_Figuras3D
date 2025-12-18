using System;
using System.Drawing;
using System.Collections.Generic;
using Figuras3D.Clases;

namespace Figuras3D
{
    public abstract class Figura3D
    {
        public string Nombre { get; set; }
        public Point3D Posicion { get; set; } = new Point3D(0, 0, 0);
        public Point3D Rotacion { get; set; } = new Point3D(0, 0, 0);
        public Point3D Escala { get; set; } = new Point3D(1, 1, 1);
        public Color ColorFigura { get; set; } = Color.LightBlue;
        public bool EstaSeleccionada { get; set; } = false;

        public Material Material { get; set; }
        public Iluminacion Iluminacion { get; set; }

        protected List<Point3D> vertices = new List<Point3D>();
        protected List<int[]> caras = new List<int[]>();

        public Figura3D(string nombre = "Figura")
        {
            this.Nombre = nombre;
            
            Material = new Material(Color.LightBlue);
            Iluminacion = Iluminacion.CrearIluminacionEstudio();
            GenerarGeometria();
        }

        public abstract void GenerarGeometria();

        public List<Point3D> ObtenerVerticesTransformados()
        {
            List<Point3D> verticesTransformados = new List<Point3D>();

            foreach (Point3D vertice in vertices)
            {
                // 1. Aplicar ESCALA
                Point3D v = new Point3D(
                    vertice.X * Escala.X,
                    vertice.Y * Escala.Y,
                    vertice.Z * Escala.Z
                );

                // 2. Aplicar ROTACIÓN en X
                float angX = Rotacion.X * (float)Math.PI / 180.0f;
                Point3D rx = new Point3D(
                    v.X,
                    v.Y * (float)Math.Cos(angX) - v.Z * (float)Math.Sin(angX),
                    v.Y * (float)Math.Sin(angX) + v.Z * (float)Math.Cos(angX)
                );

                // 3. Aplicar ROTACIÓN en Y
                float angY = Rotacion.Y * (float)Math.PI / 180.0f;
                Point3D ry = new Point3D(
                    rx.X * (float)Math.Cos(angY) + rx.Z * (float)Math.Sin(angY),
                    rx.Y,
                    -rx.X * (float)Math.Sin(angY) + rx.Z * (float)Math.Cos(angY)
                );

                // 4. Aplicar ROTACIÓN en Z
                float angZ = Rotacion.Z * (float)Math.PI / 180.0f;
                Point3D rz = new Point3D(
                    ry.X * (float)Math.Cos(angZ) - ry.Y * (float)Math.Sin(angZ),
                    ry.X * (float)Math.Sin(angZ) + ry.Y * (float)Math.Cos(angZ),
                    ry.Z
                );

                // 5. Aplicar POSICIÓN (traslación)
                Point3D final = new Point3D(
                    rz.X + Posicion.X,
                    rz.Y + Posicion.Y,
                    rz.Z + Posicion.Z
                );

                verticesTransformados.Add(final);
            }

            return verticesTransformados;
        }

        public List<int[]> ObtenerCaras()
        {
            return caras;
        }
    }

    public class Point3D
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public Point3D(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Point3D()
        {
            X = 0;
            Y = 0;
            Z = 0;
        }
    }

}