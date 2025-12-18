using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Figuras3D.Clases
{
    /// <summary>
    /// Tipos de vistas predefinidas de cámara
    /// </summary>
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

    /// <summary>
    /// Clase que representa una cámara con posición y rotación
    /// </summary>
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

        /// <summary>
        /// Crea una cámara con vista frontal
        /// </summary>
        public static Camara CrearCamaraFrontal()
        {
            return new Camara(
                new Point3D(0, 0, 5),
                new Point3D(0, 0, 0),
                1.0f,
                TipoCamara.Frontal
            );
        }

        /// <summary>
        /// Crea una cámara con vista trasera
        /// </summary>
        public static Camara CrearCamaraTrasera()
        {
            return new Camara(
                new Point3D(0, 0, -5),
                new Point3D(0, 180, 0),
                1.0f,
                TipoCamara.Trasera
            );
        }

        /// <summary>
        /// Crea una cámara con vista superior
        /// </summary>
        public static Camara CrearCamaraSuperior()
        {
            return new Camara(
                new Point3D(0, 5, 0),
                new Point3D(90, 0, 0),
                1.0f,
                TipoCamara.Superior
            );
        }

        /// <summary>
        /// Crea una cámara con vista inferior
        /// </summary>
        public static Camara CrearCamaraInferior()
        {
            return new Camara(
                new Point3D(0, -5, 0),
                new Point3D(-90, 0, 0),
                1.0f,
                TipoCamara.Inferior
            );
        }

        /// <summary>
        /// Crea una cámara con vista lateral derecha
        /// </summary>
        public static Camara CrearCamaraLateral()
        {
            return new Camara(
                new Point3D(5, 0, 0),
                new Point3D(0, 90, 0),
                1.0f,
                TipoCamara.Lateral
            );
        }

        /// <summary>
        /// Crea una cámara con vista lateral izquierda
        /// </summary>
        public static Camara CrearCamaraLateralIzquierda()
        {
            return new Camara(
                new Point3D(-5, 0, 0),
                new Point3D(0, -90, 0),
                1.0f,
                TipoCamara.LateralIzq
            );
        }

        /// <summary>
        /// Crea una cámara con vista isométrica
        /// </summary>
        public static Camara CrearCamaraIsometrica()
        {
            return new Camara(
                new Point3D(3, 3, 3),
                new Point3D(35.264f, 45, 0), // Ángulos isométricos estándar
                1.0f,
                TipoCamara.Isometrica
            );
        }

        /// <summary>
        /// Crea una cámara con vista en perspectiva
        /// </summary>
        public static Camara CrearCamaraPerspectiva()
        {
            return new Camara(
                new Point3D(4, 2, 4),
                new Point3D(20, 45, 0),
                1.0f,
                TipoCamara.Perspectiva
            );
        }

        /// <summary>
        /// Crea una cámara libre (controlada por el usuario)
        /// </summary>
        public static Camara CrearCamaraLibre()
        {
            return new Camara(
                new Point3D(0, 0, 5),
                new Point3D(0, 0, 0),
                1.0f,
                TipoCamara.Libre
            );
        }

        /// <summary>
        /// Clona la cámara actual
        /// </summary>
        public Camara Clonar()
        {
            return new Camara(
                new Point3D(Posicion.X, Posicion.Y, Posicion.Z),
                new Point3D(Rotacion.X, Rotacion.Y, Rotacion.Z),
                Zoom,
                Tipo
            );
        }

        /// <summary>
        /// Obtiene el nombre descriptivo de la cámara
        /// </summary>
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
