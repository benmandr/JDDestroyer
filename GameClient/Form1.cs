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
        Bitmap FrontBuffer;
        //Score management
        int bestScore = 0; //SERVER VAR
        string name = "Lohas"; //SERVER VAR
        string scoreTextBox;
        //Player
        Point PlayerPos;
        int playerSpeed = 10;
        int PlayerSize;


        public Form1()
        {
            InitializeComponent();
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
           // Go full screen with fixed square size
            Text = "JDDestroyer";
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;
            int ScreenHeight = (int)(Screen.PrimaryScreen.Bounds.Height - (Screen.PrimaryScreen.Bounds.Height * 0.1));
            ClientSize = new Size(ScreenHeight, ScreenHeight);
            //Set up the updating
            Timer GameTimer = new Timer();
            GameTimer.Interval = 10;
            GameTimer.Tick += new EventHandler(Tick);
            GameTimer.Start();
            Load += new EventHandler(Init);
            Paint += new PaintEventHandler(FormPaint);
            this.KeyDown += new KeyEventHandler(Form1_KeyDown);
        }

        //Controls
        void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            Console.WriteLine("Player POS" + PlayerPos.X);
            Console.WriteLine("Barrier:" + ClientSize.Width * 0.25);
            if (e.KeyCode == Keys.Left && PlayerPos.X > ClientSize.Width * 0.25)
                PlayerPos.X -= 1 * playerSpeed;
            else if (e.KeyCode == Keys.Right && PlayerPos.X + PlayerSize < ClientSize.Width * 0.75)
                PlayerPos.X += 1 * playerSpeed;
            else if (e.KeyCode == Keys.Up)
                bestScore += 1;
        }

        //Update paint
        void FormPaint(object sender, PaintEventArgs e)
        {
            if(BackBuffer != null)
            {
                e.Graphics.DrawImageUnscaled(BackBuffer, Point.Empty);
                e.Graphics.DrawImageUnscaled(FrontBuffer, Point.Empty);
            }
        }

        void Init(object sender, EventArgs ev)
        {
            if (BackBuffer != null)
            {
                BackBuffer.Dispose();
                FrontBuffer.Dispose();
            }
            if (PlayerPos != null)
            {
                PlayerPos = new Point(0,0);
            }
            BackBuffer = new Bitmap(ClientSize.Width,ClientSize.Height);
            FrontBuffer = new Bitmap(ClientSize.Width, ClientSize.Height);
            PlayerPos = new Point((int)(ClientSize.Width * 0.26), ClientSize.Height);
            PlayerSize = ClientSize.Width / 8;
            using (var e = Graphics.FromImage(BackBuffer))
            {
                int length = ClientSize.Width;
                //Main canvas color
                e.FillRectangle(Brushes.WhiteSmoke, new Rectangle(0, 0, length, length));
                //Middle square
                Rectangle middleSquare = new Rectangle(length / 4, length / 4, length / 2, length / 2);
                e.FillRectangle(Brushes.Gray, middleSquare);
                //Add border
                Pen borderPen = new Pen(Color.FromArgb(105, 105, 105), 4);
                borderPen.Alignment = System.Drawing.Drawing2D.PenAlignment.Inset;
                e.DrawRectangle(borderPen, middleSquare);
                //Corner squares
                int cornerCord = (int)(length * 0.75);
                e.FillRectangle(Brushes.Black, new Rectangle(0, 0, length / 4, length / 4));
                e.FillRectangle(Brushes.Black, new Rectangle(0, cornerCord, length / 4, length / 4));
                e.FillRectangle(Brushes.Black, new Rectangle(cornerCord, 0, length / 4, length / 4));
                e.FillRectangle(Brushes.Black, new Rectangle(cornerCord, cornerCord, length / 4, length / 4));
                //Score title
                e.DrawString("Best score:", new Font("Comic Sans MS", 18), Brushes.White, (float)(length * 0.75), 0);
            }
        }

        void Draw()
        {
            if (BackBuffer != null)
            {
                using (var graphic = Graphics.FromImage(FrontBuffer))
                {
                    graphic.Clear(Color.Transparent);
                    //Update the score
                    graphic.DrawString(scoreTextBox, new Font("Comic Sans MS", 18), Brushes.White, (float)(ClientSize.Width * 0.75), 28);
                    graphic.FillRectangle(Brushes.Green, PlayerPos.X, PlayerPos.Y - PlayerSize, PlayerSize, PlayerSize);
                    graphic.DrawString("P1", new Font("Comic Sans MS", 30), Brushes.White, (int)(PlayerPos.X + 20), PlayerPos.Y - 70);
                }
                Invalidate();
            }
        }

        void Tick(object sender, EventArgs e)
        {
            scoreTextBox = name + bestScore.ToString();
            Draw();
        }
    }
}
