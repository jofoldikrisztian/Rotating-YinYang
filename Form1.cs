using System;
using System.Drawing;
using System.Windows.Forms;

namespace RotatingPlane
{
    public partial class Rotating : Form
    {

        float angle;
        int size;
        Point center;
        Bitmap bmp;
        Graphics g;
        private readonly int edgeX, edgeY;

        public Rotating()
        {
            InitializeComponent();
            edgeX = ClientSize.Width - btnGo.Location.X;
            edgeY = ClientSize.Height - btnGo.Location.Y;
            angle = 0.0f;
        }

        private void RotationTimer_Tick(object sender, EventArgs e)
        {
            drawYinYang();
        }
        private void drawYinYang()
        {
            g = Graphics.FromImage(bmp);
            g.Clear(Color.White);
            pictureBox1.Image = bmp;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            g.TranslateTransform(center.X, center.Y);
            g.ScaleTransform(1.5F, 1.5F);
            g.RotateTransform(angle);

            g.FillPie(Brushes.Black, -size, -size, 2 * size, 2 * size, 90, -180);
            g.FillPie(Brushes.White, -size / 2, -size, size, size, 90, -180);
            g.FillPie(Brushes.Black, -size / 2, 0, size, size, 90, 180);

            g.DrawEllipse(new Pen(Color.Black, 3), -size, -size, 2 * size, 2 * size);
            g.FillEllipse(Brushes.Black, -15 * size / 100, -65 * size / 100, 30 * size / 100, 30 * size / 100);
            g.FillEllipse(Brushes.White, -15 * size / 100, 35 * size / 100, 30 * size / 100, 30 * size / 100);

            g.Dispose();

            if (angle == 360)
                angle = 0;
            else
                angle++;
        }
        private void Rotating_Paint(object sender, PaintEventArgs e)
        {
            bmp = new Bitmap(ClientSize.Width, ClientSize.Height);
            size = (int)(Math.Min(ClientSize.Width, ClientSize.Height) / 3.65);
            center = new Point(ClientSize.Width / 2, ClientSize.Height / 2);
        }
        private void Rotating_Resize(object sender, EventArgs e)
        {
            if (Created)
            {
                Refresh();
                btnGo.Location = new Point(ClientSize.Width - edgeX, ClientSize.Height - edgeY);
                Invalidate();
            }
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            if (RotationTimer.Enabled)
            {
                pictureBox1.Hide();
                RotationTimer.Stop();
                btnGo.Text = "Show";
            }
            else
            {
                RotationTimer.Start();
                pictureBox1.Show();
                btnGo.Text = "Hide";
            }
        }
    }
}
