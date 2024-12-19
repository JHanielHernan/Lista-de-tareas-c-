using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Aplication_Tasks
{
    public partial class list_task : UserControl
    {
        public static string Title {  get; set; }
        public static string Content { get; set; }
        public static string Status { get; set; }

        private Timer cronometro;
        private DateTime fechaObjetivo;

        public list_task(string title,string content, DateTime fechaObjetivo, string status)
        {
            InitializeComponent();
            Title = title;
            Content = content;
            lbl_title.Text = title;
            this.fechaObjetivo = fechaObjetivo;
            InicializarCronometro();
            Status = status;
            cb_status.Text = status;
            Change_Color();
        }

        void Change_Color()
        {
            if (Status == "filled" || cb_status.Text == "filled") this.panel1.BackColor = Color.Green;
            else if (Status == "earring" || cb_status.Text == "earring") this.panel1.BackColor = Color.OrangeRed;
            else if (Status == "incomplete" || cb_status.Text == "incomplete") this.panel1.BackColor = Color.Red;
        }

        private void list_task_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Content, Title);
        }
        private void InicializarCronometro()
        {
            // Configura el Timer
            cronometro = new Timer();
            cronometro.Interval = 1000; // 1 segundo
            cronometro.Tick += cronometr_Tick;
            cronometro.Start();

            lblTiempo.Text = CalcularTiempoRestante();
        }
        private string CalcularTiempoRestante()
        {
            TimeSpan tiempoRestante = fechaObjetivo - DateTime.Now;

            if (tiempoRestante.TotalSeconds <= 0)
            {
                return null; // El tiempo ha llegado
            }

            // Formatea el tiempo restante como: "Días HH:mm:ss"
            return $"{tiempoRestante.Days} días {tiempoRestante.Hours:D2}:{tiempoRestante.Minutes:D2}:{tiempoRestante.Seconds:D2}";
        }

        private void cronometr_Tick(object sender, EventArgs e)
        {
            // Calcula el tiempo restante
            string tiempoRestante = CalcularTiempoRestante();

            if (string.IsNullOrEmpty(tiempoRestante))
            {
                cronometro.Stop();
                Notificacion_mensaje(Title,Content);
                cb_status.Enabled = false;
            }
            else
            {
                lblTiempo.Text = tiempoRestante;
            }
        }
        void Notificacion_mensaje(string title, string text)
        {
            NotifyIcon notificacion = new NotifyIcon();
            notificacion.Icon = SystemIcons.Information;
            notificacion.Visible = true;
            notificacion.BalloonTipTitle = title;
            notificacion.BalloonTipText = text;
            notificacion.BalloonTipIcon = ToolTipIcon.Info;
            notificacion.ShowBalloonTip(3000);
        }

        private void list_task_DoubleClick(object sender, EventArgs e)
        {
            MessageBox.Show(Content, Title);
        }

        private void cb_status_TextChanged(object sender, EventArgs e)
        {
            Change_Color();
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Content, Title);

        }
    }
}
