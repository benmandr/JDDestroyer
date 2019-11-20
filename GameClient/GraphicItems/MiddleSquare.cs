using GameServer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClient.GraphicItems
{
    class MiddleSquare : GraphicItem
    {
        Bitmap buffer { get; set; }
        public MiddleSquare(Bitmap buffer)
        {
            this.buffer = buffer;
        }

        public void Draw()
        {
            using (var g = Graphics.FromImage(buffer))
            {
                int cornerSize = Proportion.ClientSize(Config.CORNERSIZE);
                int innerSize = Proportion.ClientSize(Config.INNERSQUARESIZE);
                Rectangle middleSquare = new Rectangle(cornerSize, cornerSize, innerSize, innerSize);
                g.FillRectangle(Brushes.Gray, middleSquare);
                Pen borderPen = new Pen(Color.FromArgb(105, 105, 105), 4)
                {
                    Alignment = System.Drawing.Drawing2D.PenAlignment.Inset
                };
                g.DrawRectangle(borderPen, middleSquare);
            }
        }
    }
}
