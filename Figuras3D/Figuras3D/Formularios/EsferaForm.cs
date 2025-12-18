using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Figuras3D.Clases;

namespace Figuras3D.Formularios
{
    public partial class EsferaForm : Form
    {
        private Esfera esfera;
        private Point lastMousePos;
        private bool isRotating = false;
        private bool isMoving = false;
        private MotorRenderizado motorRenderizado;
        private Color colorOriginalFigura;
        private Button btnCamara;
        private Button btnPausar;
        private ContextMenuStrip menuCamara;
        private bool animacionPausada = false;

        public EsferaForm()
        {
            InitializeComponent();
            
            // Inicializar esfera con material por defecto
            esfera = new Esfera("Esfera Roja", 20);
            esfera.ColorFigura = Color.Red;
            esfera.Material = Material.CrearPlastico(Color.Red);
            esfera.Iluminacion = Iluminacion.CrearIluminacionEstudio();
            
            // GUARDAR color original
            colorOriginalFigura = Color.Red;
            
            // Inicializar motor de renderizado
            motorRenderizado = new MotorRenderizado();
            
            panelVisualizacion.DoubleBuffered(true);

            // Eventos del mouse
            panelVisualizacion.MouseDown += PanelVisualizacion_MouseDown;
            panelVisualizacion.MouseMove += PanelVisualizacion_MouseMove;
            panelVisualizacion.MouseUp += PanelVisualizacion_MouseUp;
            panelVisualizacion.MouseWheel += PanelVisualizacion_MouseWheel;
            
            // Inicializar controles
            InicializarControles();
            
            // Crear botones flotantes usando helper
            CrearBotonesFlotantes();
            
            // Aplicar decoraciones visuales
            AplicarDecoracionesVisuales();
        }
        
        private void AplicarDecoracionesVisuales()
        {
            // Agregar líneas separadoras visuales dentro de groupBoxTransformacion
            AgregarSeparador(groupBoxTransformacion, 104); // Entre Posición y Rotación
            AgregarSeparador(groupBoxTransformacion, 186); // Entre Rotación y Escala
            
            // Aplicar efecto hover a los botones
            AplicarEfectoHoverBoton(btnColor, Color.FromArgb(245, 245, 245), Color.White);
            AplicarEfectoHoverBoton(btnReiniciar, Color.FromArgb(220, 220, 220), Color.FromArgb(230, 230, 230));
        }
        
        private void AgregarSeparador(GroupBox grupo, int posicionY)
        {
            Panel separador = new Panel
            {
                Height = 1,
                Width = grupo.Width - 30,
                Left = 15,
                Top = posicionY,
                BackColor = Color.FromArgb(200, 200, 200),
                Anchor = AnchorStyles.Left | AnchorStyles.Right
            };
            grupo.Controls.Add(separador);
        }
        
        private void AplicarEfectoHoverBoton(Button boton, Color colorHover, Color colorNormal)
        {
            boton.MouseEnter += (s, e) => boton.BackColor = colorHover;
            boton.MouseLeave += (s, e) => boton.BackColor = colorNormal;
        }

        private void InicializarControles()
        {
            // Configurar ComboBox de Textura
            comboTextura.SelectedIndexChanged += ComboBoxTextura_SelectedIndexChanged;
            comboTextura.SelectedIndex = 8; // Sólido por defecto
            
            
            // Configurar ComboBox de Material
            comboMaterial.SelectedIndex = 0; // Plástico por defecto
            comboMaterial.SelectedIndexChanged += ComboBoxMaterial_SelectedIndexChanged;
            
            // Configurar ComboBox de Ambiente (Hora del día) - solo cambia el fondo
            // Agregar opción "Seleccione..." al inicio si no existe
            if (comboAmbiente.Items.Count > 0 && comboAmbiente.Items[0].ToString() != "Seleccione...")
            {
                comboAmbiente.Items.Insert(0, "Seleccione...");
            }
            comboAmbiente.SelectedIndex = 0; // Mostrar "Seleccione..." por defecto
            comboAmbiente.SelectedIndexChanged += ComboBoxAmbiente_SelectedIndexChanged;
            
            // Configurar TrackBar de Intensidad de Luz del FONDO
            trackBarBrillo.Minimum = 30;  // Mínimo brillo del fondo
            trackBarBrillo.Maximum = 100; // Máximo brillo del fondo
            trackBarBrillo.Value = 70;    // Valor predeterminado
            trackBarBrillo.Scroll += TrackBarBrilloFondo_Scroll;
            label11.Text = $"Brillo Fondo: {trackBarBrillo.Value}%";
        }

        #region Eventos de Mouse para Interacción 3D

        private void PanelVisualizacion_MouseDown(object sender, MouseEventArgs e)
        {
            lastMousePos = e.Location;
            if (e.Button == MouseButtons.Left)
            {
                isRotating = true;
                panelVisualizacion.Cursor = Cursors.Hand;
            }
            else if (e.Button == MouseButtons.Right)
            {
                isMoving = true;
                panelVisualizacion.Cursor = Cursors.SizeAll;
            }
        }

        private void PanelVisualizacion_MouseMove(object sender, MouseEventArgs e)
        {
            if (isRotating)
            {
                int deltaX = e.X - lastMousePos.X;
                int deltaY = e.Y - lastMousePos.Y;
                float nuevaRotY = esfera.Rotacion.Y + deltaX * 0.5f;
                float nuevaRotX = esfera.Rotacion.X + deltaY * 0.5f;
                esfera.Rotacion = new Point3D(nuevaRotX, nuevaRotY, esfera.Rotacion.Z);
                numericRotacionX.Value = (decimal)(nuevaRotX % 360);
                numericRotacionY.Value = (decimal)(nuevaRotY % 360);
                panelVisualizacion.Invalidate();
                lastMousePos = e.Location;
            }
            else if (isMoving)
            {
                int deltaX = e.X - lastMousePos.X;
                int deltaY = e.Y - lastMousePos.Y;
                float escalaMovimiento = 0.01f;
                float nuevaPosX = esfera.Posicion.X + deltaX * escalaMovimiento;
                float nuevaPosY = esfera.Posicion.Y - deltaY * escalaMovimiento;
                esfera.Posicion = new Point3D(nuevaPosX, nuevaPosY, esfera.Posicion.Z);
                numericPosX.Value = (decimal)nuevaPosX;
                numericPosY.Value = (decimal)nuevaPosY;
                panelVisualizacion.Invalidate();
                lastMousePos = e.Location;
            }
        }

        private void PanelVisualizacion_MouseUp(object sender, MouseEventArgs e)
        {
            isRotating = false;
            isMoving = false;
            panelVisualizacion.Cursor = Cursors.Default;
        }

        private void PanelVisualizacion_MouseWheel(object sender, MouseEventArgs e)
        {
            float cambioEscala = e.Delta > 0 ? 0.1f : -0.1f;
            float nuevaEscalaX = Math.Max(0.1f, esfera.Escala.X + cambioEscala);
            float nuevaEscalaY = Math.Max(0.1f, esfera.Escala.Y + cambioEscala);
            float nuevaEscalaZ = Math.Max(0.1f, esfera.Escala.Z + cambioEscala);
            esfera.Escala = new Point3D(nuevaEscalaX, nuevaEscalaY, nuevaEscalaZ);
            numericEscalaX.Value = (decimal)nuevaEscalaX;
            numericEscalaY.Value = (decimal)nuevaEscalaY;
            numericEscalaZ.Value = (decimal)nuevaEscalaZ;
            panelVisualizacion.Invalidate();
        }

        #endregion

        #region Renderizado con Motor Avanzado

        private void panelVisualizacion_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(panelVisualizacion.BackColor);
            
            // Usar motor de renderizado avanzado
            motorRenderizado.RenderizarFigura(g, esfera, panelVisualizacion.Width, panelVisualizacion.Height);
            
            // Dibujar instrucciones
            using (Font font = new Font("Segoe UI", 9, FontStyle.Bold))
            using (Brush textBrush = new SolidBrush(Color.White))
            using (Brush bgBrush = new SolidBrush(Color.FromArgb(150, 0, 0, 0)))
            {
                string instrucciones = "Click Izq: Rotar | Click Der: Mover | Rueda: Zoom";
                SizeF size = g.MeasureString(instrucciones, font);
                g.FillRectangle(bgBrush, 5, 5, size.Width + 10, size.Height + 10);
                g.DrawString(instrucciones, font, textBrush, 10, 10);
            }
        }

        #endregion

        #region Eventos de Controles de Transformación

        private void numericPos_ValueChanged(object sender, EventArgs e)
        {
            esfera.Posicion = new Point3D(
                (float)numericPosX.Value,
                (float)numericPosY.Value,
                (float)numericPosZ.Value
            );
            panelVisualizacion.Invalidate();
        }

        private void numericRotacion_ValueChanged(object sender, EventArgs e)
        {
            esfera.Rotacion = new Point3D(
                (float)numericRotacionX.Value,
                (float)numericRotacionY.Value,
                (float)numericRotacionZ.Value
            );
            panelVisualizacion.Invalidate();
        }

        private void numericEscala_ValueChanged(object sender, EventArgs e)
        {
            esfera.Escala = new Point3D(
                (float)numericEscalaX.Value,
                (float)numericEscalaY.Value,
                (float)numericEscalaZ.Value
            );
            panelVisualizacion.Invalidate();
        }

        private void ComboBoxTextura_SelectedIndexChanged(object sender, EventArgs e)
        {
            TipoTextura tipoTextura = (TipoTextura)comboTextura.SelectedIndex;
            esfera.Material.Textura = tipoTextura;
            panelVisualizacion.Invalidate();
        }

        private void ComboBoxMaterial_SelectedIndexChanged(object sender, EventArgs e)
        {
            // GUARDAR la textura actual y propiedades antes de cambiar material
            TipoTextura texturaActual = esfera.Material.Textura;
            Color colorSecundario = esfera.Material.ColorSecundario;
            float escalaTextura = esfera.Material.EscalaTextura;
            
            // Aplicar material predefinido según selección
            switch (comboMaterial.SelectedIndex)
            {
                case 0: // Plástico
                    esfera.Material = Material.CrearPlastico(colorOriginalFigura);
                    esfera.Material.ColorDifuso = colorOriginalFigura;
                    break;
                case 1: // Metálico
                    esfera.Material = Material.CrearMetalico(colorOriginalFigura);
                    esfera.Material.ColorDifuso = colorOriginalFigura;
                    break;
                case 2: // Vidrio
                    esfera.Material = Material.CrearVidrio(colorOriginalFigura);
                    esfera.Material.ColorDifuso = colorOriginalFigura;
                    break;
                case 3: // Oro
                    esfera.Material = Material.CrearOro(colorOriginalFigura);
                    // El oro usa su propio color dorado, NO restauramos el original
                    break;
            }
            
            // RESTAURAR la textura y propiedades que tenía antes
            esfera.Material.Textura = texturaActual;
            esfera.Material.ColorSecundario = colorSecundario;
            esfera.Material.EscalaTextura = escalaTextura;
            
            panelVisualizacion.Invalidate();
        }

        private void ComboBoxAmbiente_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Si está en "Seleccione...", no hacer nada (mantener fondo predeterminado)
            if (comboAmbiente.SelectedIndex == 0)
            {
                // Mantener el fondo inicial (azul cielo día)
                panelVisualizacion.BackColor = Color.FromArgb(135, 206, 250);
                AplicarBrilloAlFondo();
                panelVisualizacion.Invalidate();
                return;
            }
            
            // Cambiar SOLO el color de fondo del panel según la hora del día
            // Restar 1 al índice porque ahora "Seleccione..." ocupa el índice 0
            switch (comboAmbiente.SelectedIndex - 1)
            {
                case 0: // Día - Fondo azul cielo brillante
                    panelVisualizacion.BackColor = Color.FromArgb(135, 206, 250);
                    break;

                case 1: // Tarde - Fondo beige cálido
                    panelVisualizacion.BackColor = Color.FromArgb(255, 235, 205);
                    break;

                case 2: // Noche - Fondo azul oscuro nocturno
                    panelVisualizacion.BackColor = Color.FromArgb(25, 25, 112);
                    break;

                case 3: // Amanecer - Amarillo suave difuminado
                    panelVisualizacion.BackColor = Color.FromArgb(255, 250, 205);
                    break;

                case 4: // Atardecer - Naranja-amarillo cálido difuminado
                    panelVisualizacion.BackColor = Color.FromArgb(255, 200, 124);
                    break;
            }
            
            // Aplicar el brillo actual del trackBar al nuevo color
            AplicarBrilloAlFondo();
            VisualizacionFiguras.AjustarColorBoton(btnCamara, panelVisualizacion.BackColor);
            VisualizacionFiguras.AjustarColorBoton(btnPausar, panelVisualizacion.BackColor);
            panelVisualizacion.Invalidate();
        }

        private void TrackBarBrilloFondo_Scroll(object sender, EventArgs e)
        {
            // Ajustar el brillo del color de fondo
            label11.Text = $"Brillo Fondo: {trackBarBrillo.Value}%";
            AplicarBrilloAlFondo();
            panelVisualizacion.Invalidate();
        }

        private void AplicarBrilloAlFondo()
        {
            // Obtener el color base del fondo actual
            Color colorBase = ObtenerColorBaseAmbiente();
            
            // Calcular el factor de brillo (0.3 a 1.0)
            float factorBrillo = trackBarBrillo.Value / 100f;
            
            // Aplicar el factor de brillo al color
            int r = (int)(colorBase.R * factorBrillo);
            int g = (int)(colorBase.G * factorBrillo);
            int b = (int)(colorBase.B * factorBrillo);
            
            // Asegurar que los valores estén en rango válido
            r = Math.Max(0, Math.Min(255, r));
            g = Math.Max(0, Math.Min(255, g));
            b = Math.Max(0, Math.Min(255, b));
            
            panelVisualizacion.BackColor = Color.FromArgb(r, g, b);
            
            // Ajustar color de los botones según el nuevo fondo
            if (btnCamara != null)
            {
                VisualizacionFiguras.AjustarColorBoton(btnCamara, panelVisualizacion.BackColor);
            }
            if (btnPausar != null)
            {
                VisualizacionFiguras.AjustarColorBoton(btnPausar, panelVisualizacion.BackColor);
            }
        }

        private Color ObtenerColorBaseAmbiente()
        {
            // Devolver el color base según el ambiente seleccionado
            // Si está en "Seleccione..." (índice 0), devolver color de Día por defecto
            if (comboAmbiente.SelectedIndex == 0)
            {
                return Color.FromArgb(135, 206, 250); // Día por defecto
            }
            
            // Restar 1 al índice porque "Seleccione..." ocupa el índice 0
            switch (comboAmbiente.SelectedIndex - 1)
            {
                case 0: return Color.FromArgb(135, 206, 250); // Día - Azul cielo
                case 1: return Color.FromArgb(255, 235, 205); // Tarde - Beige cálido
                case 2: return Color.FromArgb(25, 25, 112);   // Noche - Azul oscuro
                case 3: return Color.FromArgb(255, 250, 205); // Amanecer - Amarillo suave difuminado
                case 4: return Color.FromArgb(255, 200, 124); // Atardecer - Naranja-amarillo difuminado
                default: return Color.FromArgb(135, 206, 250); // Por defecto Día
            }
        }

        #endregion

        #region Eventos de Botones

        private void btnColor_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = esfera.Material.ColorDifuso;
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                // GUARDAR el nuevo color como original
                colorOriginalFigura = colorDialog1.Color;
                
                esfera.ColorFigura = colorDialog1.Color;
                esfera.Material.ColorDifuso = colorDialog1.Color;
                panelVisualizacion.Invalidate();
            }
        }

        private void btnReiniciar_Click(object sender, EventArgs e)
        {
            // Reiniciar transformaciones
            esfera.Posicion = new Point3D(0, 0, 0);
            esfera.Rotacion = new Point3D(0, 0, 0);
            esfera.Escala = new Point3D(1, 1, 1);
            
            // Reiniciar color y material DE LA FIGURA
            colorOriginalFigura = Color.Red;
            esfera.ColorFigura = Color.Red;
            esfera.Material = Material.CrearPlastico(Color.Red);
            
            // Actualizar controles numéricos
            numericPosX.Value = 0;
            numericPosY.Value = 0;
            numericPosZ.Value = 0;
            numericRotacionX.Value = 0;
            numericRotacionY.Value = 0;
            numericRotacionZ.Value = 0;
            numericEscalaX.Value = 1;
            numericEscalaY.Value = 1;
            numericEscalaZ.Value = 1;
            
            // Reiniciar ComboBoxes
            comboTextura.SelectedIndex = 0; // Sólido
            comboMaterial.SelectedIndex = 0; // Plástico
            comboAmbiente.SelectedIndex = 0; // "Seleccione..."
            
            // Reiniciar brillo del fondo
            trackBarBrillo.Value = 70;
            label11.Text = $"Brillo Fondo: {trackBarBrillo.Value}%";
            
            // Restablecer color de fondo a Día por defecto
            panelVisualizacion.BackColor = Color.FromArgb(135, 206, 250);
            AplicarBrilloAlFondo();
            
            panelVisualizacion.Invalidate();
        }

        #endregion

        #region Timer de Animación

        private void timer1_Tick(object sender, EventArgs e)
        {
            // No animar si está pausado o si el usuario está interactuando
            if (animacionPausada || isRotating || isMoving)
            {
                return;
            }
            
            esfera.Rotacion = new Point3D(
                esfera.Rotacion.X + 0.3f,
                esfera.Rotacion.Y + 0.5f,
                esfera.Rotacion.Z
            );
            
            numericRotacionX.Value = (decimal)(esfera.Rotacion.X % 360);
            numericRotacionY.Value = (decimal)(esfera.Rotacion.Y % 360);
            
            panelVisualizacion.Invalidate();
        }

        #endregion
        
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        #region Botones Flotantes de Cámara y Pausa

        private void CrearBotonesFlotantes()
        {
            // Crear botón de pausa usando el helper
            btnPausar = VisualizacionFiguras.CrearBotonPausar(panelVisualizacion, BtnPausar_Click);
            
            // Crear botón de cámara usando el helper
            btnCamara = VisualizacionFiguras.CrearBotonCamara(panelVisualizacion, BtnCamara_Click);
            
            // Crear menú de cámara usando el helper
            menuCamara = VisualizacionFiguras.CrearMenuCamara(ItemCamara_Click);
            
            // Ajustar posición cuando el panel cambie de tamaño
            panelVisualizacion.Resize += (s, e) =>
            {
                VisualizacionFiguras.ReposicionarBotones(btnCamara, btnPausar, panelVisualizacion);
            };
            
            // Ajustar colores según el fondo
            VisualizacionFiguras.AjustarColorBoton(btnCamara, panelVisualizacion.BackColor);
            VisualizacionFiguras.AjustarColorBoton(btnPausar, panelVisualizacion.BackColor);
        }

        private void BtnPausar_Click(object sender, EventArgs e)
        {
            animacionPausada = !animacionPausada;
            
            // Cambiar el ícono según el estado usando fuentes Webdings
            if (animacionPausada)
            {
                btnPausar.Text = "4";  // Símbolo de play en Webdings
            }
            else
            {
                btnPausar.Text = ";";  // Símbolo de pausa en Webdings
            }
        }

        private void BtnCamara_Click(object sender, EventArgs e)
        {
            // Mostrar el menú debajo del botón
            menuCamara.Show(btnCamara, new Point(0, btnCamara.Height));
        }

        private void ItemCamara_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            if (item == null) return;
            
            TipoCamara tipoCamara = (TipoCamara)item.Tag;
            CambiarCamara(tipoCamara);
        }

        private void CambiarCamara(TipoCamara tipo)
        {
            // Crear la cámara según el tipo seleccionado
            switch (tipo)
            {
                case TipoCamara.Libre:
                    motorRenderizado.CamaraActiva = Camara.CrearCamaraLibre();
                    break;
                case TipoCamara.Frontal:
                    motorRenderizado.CamaraActiva = Camara.CrearCamaraFrontal();
                    esfera.Rotacion = new Point3D(0, 0, 0);
                    break;
                case TipoCamara.Trasera:
                    motorRenderizado.CamaraActiva = Camara.CrearCamaraTrasera();
                    esfera.Rotacion = new Point3D(0, 180, 0);
                    break;
                case TipoCamara.Superior:
                    motorRenderizado.CamaraActiva = Camara.CrearCamaraSuperior();
                    esfera.Rotacion = new Point3D(90, 0, 0);
                    break;
                case TipoCamara.Inferior:
                    motorRenderizado.CamaraActiva = Camara.CrearCamaraInferior();
                    esfera.Rotacion = new Point3D(-90, 0, 0);
                    break;
                case TipoCamara.Lateral:
                    motorRenderizado.CamaraActiva = Camara.CrearCamaraLateral();
                    esfera.Rotacion = new Point3D(0, 90, 0);
                    break;
                case TipoCamara.LateralIzq:
                    motorRenderizado.CamaraActiva = Camara.CrearCamaraLateralIzquierda();
                    esfera.Rotacion = new Point3D(0, -90, 0);
                    break;
                case TipoCamara.Isometrica:
                    motorRenderizado.CamaraActiva = Camara.CrearCamaraIsometrica();
                    esfera.Rotacion = new Point3D(35.264f, 45, 0);
                    break;
                case TipoCamara.Perspectiva:
                    motorRenderizado.CamaraActiva = Camara.CrearCamaraPerspectiva();
                    esfera.Rotacion = new Point3D(20, 45, 0);
                    break;
            }
            
            // Actualizar controles numéricos
            numericRotacionX.Value = (decimal)(esfera.Rotacion.X % 360);
            numericRotacionY.Value = (decimal)(esfera.Rotacion.Y % 360);
            numericRotacionZ.Value = (decimal)(esfera.Rotacion.Z % 360);
            
            panelVisualizacion.Invalidate();
        }

        #endregion
    }
}