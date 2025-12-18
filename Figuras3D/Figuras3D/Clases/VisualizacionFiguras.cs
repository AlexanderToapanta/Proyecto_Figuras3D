using System;
using System.Drawing;
using System.Windows.Forms;

namespace Figuras3D.Clases
{
    /// <summary>
    /// Clase auxiliar para gestionar la visualización de figuras 3D con botones flotantes de cámara y pausa
    /// </summary>
    public static class VisualizacionFiguras
    {
        /// <summary>
        /// Crea un botón de cámara flotante y lo agrega al panel
        /// </summary>
        public static Button CrearBotonCamara(Panel panel, EventHandler onClickMenu)
        {
            Button btnCamara = new Button
            {
                Size = new Size(45, 45),
                BackColor = Color.FromArgb(200, 60, 60, 60),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Wingdings", 18F, FontStyle.Bold),
                Text = "N", // Símbolo de cámara en Wingdings
                Cursor = Cursors.Hand,
                Tag = "CameraButton"
            };
            
            btnCamara.FlatAppearance.BorderSize = 0;
            btnCamara.FlatAppearance.MouseOverBackColor = Color.FromArgb(220, 80, 80, 80);
            btnCamara.FlatAppearance.MouseDownBackColor = Color.FromArgb(240, 100, 100, 100);
            
            btnCamara.Location = new Point(panel.Width - btnCamara.Width - 10, 10);
            btnCamara.Click += onClickMenu;
            
            panel.Controls.Add(btnCamara);
            btnCamara.BringToFront();
            
            return btnCamara;
        }

        /// <summary>
        /// Crea un botón de pausa flotante y lo agrega al panel
        /// </summary>
        public static Button CrearBotonPausar(Panel panel, EventHandler onClick)
        {
            Button btnPausar = new Button
            {
                Size = new Size(45, 45),
                BackColor = Color.FromArgb(200, 60, 60, 60),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Webdings", 16F, FontStyle.Bold),
                Text = ";", // Símbolo de pausa en Webdings
                Cursor = Cursors.Hand,
                Tag = "PauseButton"
            };
            
            btnPausar.FlatAppearance.BorderSize = 0;
            btnPausar.FlatAppearance.MouseOverBackColor = Color.FromArgb(220, 80, 80, 80);
            btnPausar.FlatAppearance.MouseDownBackColor = Color.FromArgb(240, 100, 100, 100);
            
            btnPausar.Location = new Point(panel.Width - btnPausar.Width - 65, 10);
            btnPausar.Click += onClick;
            
            panel.Controls.Add(btnPausar);
            btnPausar.BringToFront();
            
            return btnPausar;
        }

        /// <summary>
        /// Crea un menú contextual de cámaras
        /// </summary>
        public static ContextMenuStrip CrearMenuCamara(EventHandler onItemClick)
        {
            ContextMenuStrip menuCamara = new ContextMenuStrip
            {
                BackColor = Color.FromArgb(250, 250, 250),
                Font = new Font("Segoe UI", 9.5F),
                ShowImageMargin = false,
                RenderMode = ToolStripRenderMode.Professional
            };
            
            menuCamara.Renderer = new ToolStripProfessionalRenderer(new CustomColorTable());
            
            string[] opciones = {
                "Libre",
                "Frontal",
                "Trasera",
                "Superior",
                "Inferior",
                "Lateral Derecha",
                "Lateral Izquierda",
                "Isométrica",
                "Perspectiva"
            };
            
            for (int i = 0; i < opciones.Length; i++)
            {
                ToolStripMenuItem item = new ToolStripMenuItem(opciones[i])
                {
                    Tag = (TipoCamara)i,
                    Font = new Font("Segoe UI", 9.5F),
                    ForeColor = Color.FromArgb(50, 50, 50),
                    Padding = new Padding(8, 4, 8, 4)
                };
                
                item.Click += onItemClick;
                menuCamara.Items.Add(item);
                
                if (i == 0 || i == 4 || i == 6)
                {
                    menuCamara.Items.Add(new ToolStripSeparator());
                }
            }
            
            return menuCamara;
        }

        /// <summary>
        /// Ajusta el color del botón según el brillo del fondo
        /// </summary>
        public static void AjustarColorBoton(Button boton, Color fondo)
        {
            if (boton == null) return;
            
            int brillo = (int)((fondo.R * 0.299) + (fondo.G * 0.587) + (fondo.B * 0.114));
            
            if (brillo > 128)
            {
                boton.BackColor = Color.FromArgb(200, 40, 40, 40);
                boton.ForeColor = Color.White;
                boton.FlatAppearance.MouseOverBackColor = Color.FromArgb(220, 60, 60, 60);
            }
            else
            {
                boton.BackColor = Color.FromArgb(200, 220, 220, 220);
                boton.ForeColor = Color.FromArgb(40, 40, 40);
                boton.FlatAppearance.MouseOverBackColor = Color.FromArgb(220, 240, 240, 240);
            }
        }

        /// <summary>
        /// Reposiciona los botones flotantes cuando el panel cambia de tamaño
        /// </summary>
        public static void ReposicionarBotones(Button btnCamara, Button btnPausar, Panel panel)
        {
            if (btnCamara != null)
            {
                btnCamara.Location = new Point(panel.Width - btnCamara.Width - 10, 10);
            }
            
            if (btnPausar != null)
            {
                btnPausar.Location = new Point(panel.Width - btnPausar.Width - 65, 10);
            }
        }

        private class CustomColorTable : ProfessionalColorTable
        {
            public override Color MenuItemSelected
            {
                get { return Color.FromArgb(230, 240, 250); }
            }

            public override Color MenuItemSelectedGradientBegin
            {
                get { return Color.FromArgb(230, 240, 250); }
            }

            public override Color MenuItemSelectedGradientEnd
            {
                get { return Color.FromArgb(220, 235, 250); }
            }

            public override Color MenuItemBorder
            {
                get { return Color.FromArgb(180, 200, 220); }
            }

            public override Color MenuBorder
            {
                get { return Color.FromArgb(160, 160, 160); }
            }
        }
    }
}
