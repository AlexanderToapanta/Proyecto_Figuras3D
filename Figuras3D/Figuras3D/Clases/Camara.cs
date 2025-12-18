using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Figuras3D.Clases
{

    public enum TipoCamara
    {
        Libre,          // Vista libre controlada por el usuario
        Frontal,        // Vista frontal (frente)
        Trasera,        // Vista trasera (atrás)
        Superior,       // Vista desde arriba
        Inferior,       // Vista desde abajo
        Lateral,        // Vista lateral derecha
        LateralIzq,     // Vista lateral izquierda
        Isometrica,     // Vista isométrica (perspectiva 3D)
        Perspectiva     // Vista en perspectiva diagonal
    }

    public class Camara
    {
        public Point3D Posicion { get; set; }
        public Point3D Rotacion { get; set; }
        public float Zoom { get; set; }
        public TipoCamara Tipo { get; set; }

        public Camara()
        {
            Posicion = new Point3D(0, 0, 5);
            Rotacion = new Point3D(0, 0, 0);
            Zoom = 1.0f;
            Tipo = TipoCamara.Libre;
        }

        public Camara(Point3D posicion, Point3D rotacion, float zoom, TipoCamara tipo)
        {
            Posicion = posicion;
            Rotacion = rotacion;
            Zoom = zoom;
            Tipo = tipo;
        }

        public static Camara CrearCamaraFrontal()
        {
            return new Camara(
                new Point3D(0, 0, 5),
                new Point3D(0, 0, 0),
                1.0f,
                TipoCamara.Frontal
            );
        }

        public static Camara CrearCamaraTrasera()
        {
            return new Camara(
                new Point3D(0, 0, -5),
                new Point3D(0, 180, 0),
                1.0f,
                TipoCamara.Trasera
            );
        }

        public static Camara CrearCamaraSuperior()
        {
            return new Camara(
                new Point3D(0, 5, 0),
                new Point3D(90, 0, 0),
                1.0f,
                TipoCamara.Superior
            );
        }

        public static Camara CrearCamaraInferior()
        {
            return new Camara(
                new Point3D(0, -5, 0),
                new Point3D(-90, 0, 0),
                1.0f,
                TipoCamara.Inferior
            );
        }

        public static Camara CrearCamaraLateral()
        {
            return new Camara(
                new Point3D(5, 0, 0),
                new Point3D(0, 90, 0),
                1.0f,
                TipoCamara.Lateral
            );
        }

        public static Camara CrearCamaraLateralIzquierda()
        {
            return new Camara(
                new Point3D(-5, 0, 0),
                new Point3D(0, -90, 0),
                1.0f,
                TipoCamara.LateralIzq
            );
        }

        public static Camara CrearCamaraIsometrica()
        {
            return new Camara(
                new Point3D(3, 3, 3),
                new Point3D(35.264f, 45, 0), // Ángulos isométricos estándar
                1.0f,
                TipoCamara.Isometrica
            );
        }

        public static Camara CrearCamaraPerspectiva()
        {
            return new Camara(
                new Point3D(4, 2, 4),
                new Point3D(20, 45, 0),
                1.0f,
                TipoCamara.Perspectiva
            );
        }

        public static Camara CrearCamaraLibre()
        {
            return new Camara(
                new Point3D(0, 0, 5),
                new Point3D(0, 0, 0),
                1.0f,
                TipoCamara.Libre
            );
        }

        public Camara Clonar()
        {
            return new Camara(
                new Point3D(Posicion.X, Posicion.Y, Posicion.Z),
                new Point3D(Rotacion.X, Rotacion.Y, Rotacion.Z),
                Zoom,
                Tipo
            );
        }

        public string ObtenerNombre()
        {
            switch (Tipo)
            {
                case TipoCamara.Libre: return "Libre";
                case TipoCamara.Frontal: return "Frontal";
                case TipoCamara.Trasera: return "Trasera";
                case TipoCamara.Superior: return "Superior";
                case TipoCamara.Inferior: return "Inferior";
                case TipoCamara.Lateral: return "Lateral Der.";
                case TipoCamara.LateralIzq: return "Lateral Izq.";
                case TipoCamara.Isometrica: return "Isométrica";
                case TipoCamara.Perspectiva: return "Perspectiva";
                default: return "Desconocida";
            }
        }
    }
}
