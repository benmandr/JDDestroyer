using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameClient
{
    public partial class Form1 : Form
    {
        Bitmap BackBuffer;
        const int BallAxisSpeed = 2;

        Point BallPos = new Point(30, 30);
        Point BallSpeed = new Point(BallAxisSpeed, BallAxisSpeed);
        const int BallSize = 50;

        public Form1()
        {
            InitializeComponent();
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            //Go full screen with fixed square size
            Text = "JDDestroyer";
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;
            int ScreenHeight = (int)(Screen.PrimaryScreen.Bounds.Height - (Screen.PrimaryScreen.Bounds.Height * 0.1));
            ClientSize = new Size(ScreenHeight, ScreenHeight);
            Timer GameTimer = new Timer();
            GameTimer.Interval = 10;
            GameTimer.Tick += new EventHandler(Tick);
            GameTimer.Start();

            ResizeEnd += new EventHandler(CreateBackBuffer);
            Load += new EventHandler(CreateBackBuffer);
            KeyDown += new KeyEventHandler(Form1_KeyDown);
            Paint += new PaintEventHandler(FormPaint);
        }

        void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
                BallSpeed.X = -BallAxisSpeed;
            else if (e.KeyCode == Keys.Right)
                BallSpeed.X = BallAxisSpeed;
            else if (e.KeyCode == Keys.Up)
                BallSpeed.Y = -BallAxisSpeed; // Y axis is downwards so -ve is up.
            else if (e.KeyCode == Keys.Down)
                BallSpeed.Y = BallAxisSpeed;
        }

        //Draw the main canvas without any moving objects
        void FormPaint(object sender, PaintEventArgs e)
        {
            if(BackBuffer != null)
            {
                int length = ClientSize.Width;
                //Main canvas
                e.Graphics.DrawImageUnscaled(BackBuffer, Point.Empty);
                //Main canvas color
                e.Graphics.FillRectangle(new SolidBrush(Color.WhiteSmoke), new Rectangle(0, 0, length, length));
                //Middle square
                e.Graphics.DrawRectangle(new Pen(Color.DarkSlateGray), new Rectangle(length/4, length/4, length/2, length/2));
                //Corner squares
                SolidBrush cornerBrush = new SolidBrush(Color.Black);
                int cornerCord = (int)(length * 0.75);
                e.Graphics.FillRectangle(cornerBrush, new Rectangle(0,0, ClientSize.Width / 4, ClientSize.Height / 4));
                e.Graphics.FillRectangle(cornerBrush, new Rectangle(0, cornerCord, ClientSize.Width / 4, ClientSize.Height / 4));
                e.Graphics.FillRectangle(cornerBrush, new Rectangle(cornerCord, 0, ClientSize.Width / 4, ClientSize.Height / 4));
                e.Graphics.FillRectangle(cornerBrush, new Rectangle(cornerCord, cornerCord, ClientSize.Width / 4, ClientSize.Height / 4));
               
            }
        }

        void CreateBackBuffer(object sender, EventArgs e)
        {
            if (BackBuffer != null)
                BackBuffer.Dispose();
            BackBuffer = new Bitmap(Width,Height);
        }

        void Draw()
        {
            if(BackBuffer != null)
            {
                using (var graphic = Graphics.FromImage(BackBuffer))
                {
                    //InstantiateMainBlock(graphic);
                    graphic.Clear(Color.White);
                    //graphic.FillEllipse(Brushes.Black, BallPos.X - BallSize / 2, BallPos.Y - BallSize / 2, BallSize, BallSize);
                   
                }

                Invalidate();
            }
        }

        void Tick(object sender, EventArgs e)
        {
            //BallPos.X += BallSpeed.X;
            //BallPos.Y += BallSpeed.Y;
            Draw();
        }
    }
}
