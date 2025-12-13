using System;
using System.Drawing;
using System.Collections.Generic;

namespace Figuras3D
{
    /// <summary>
    /// CLASE BASE PARA TODAS LAS FIGURAS 3D
    /// Esta clase debe ser usada por la interfaz para:
    /// 1. Crear figuras: new Cube(), new Sphere(), etc.
    /// 2. Cambiar posición: figura.Position = new Point3D(x, y, z)
    /// 3. Cambiar rotación: figura.Rotation = new Point3D(angX, angY, angZ)
    /// 4. Cambiar escala: figura.Scale = new Point3D(sx, sy, sz)
    /// 5. Cambiar color: figura.Color = Color.Red
    /// 6. Seleccionar figura: figura.IsSelected = true
    /// </summary>
    public abstract class Figura3D
    {
        // Propiedades públicas que la interfaz puede modificar
        public string Nombre { get; set; }
        public Point3D Posicion { get; set; } = new Point3D(0, 0, 0);
        public Point3D Rotacion { get; set; } = new Point3D(0, 0, 0);
        public Point3D Escala { get; set; } = new Point3D(1, 1, 1);
        public Color ColorFigura { get; set; } = Color.LightBlue;
        public bool EstaSeleccionada { get; set; } = false;

        // Datos internos de la figura (la interfaz NO necesita tocarlos)
        protected List<Point3D> vertices = new List<Point3D>();
        protected List<int[]> caras = new List<int[]>();

        /// <summary>
        /// Constructor: cada figura debe tener un nombre
        /// </summary>
        public Figura3D(string nombre = "Figura")
        {
            this.Nombre = nombre;
            // Cada figura hija generará su propia geometría
            GenerarGeometria();
        }

        /// <summary>
        /// Método ABSTRACTO: cada figura implementa su forma aquí
        /// La interfaz NO necesita llamar este método
        /// </summary>
        public abstract void GenerarGeometria();

        /// <summary>
        /// Obtiene los vértices transformados (con posición, rotación y escala aplicados)
        /// La interfaz puede usar esto para dibujar
        /// </summary>
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

        /// <summary>
        /// Obtiene las caras de la figura
        /// Cada cara es un arreglo de índices que apuntan a los vértices
        /// La interfaz usa esto para saber cómo conectar los puntos
        /// </summary>
        public List<int[]> ObtenerCaras()
        {
            return caras;
        }
    }

    /// <summary>
    /// CLASE AUXILIAR Point3D para coordenadas 3D
    /// La interfaz usa esto para: new Point3D(x, y, z)
    /// </summary>
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