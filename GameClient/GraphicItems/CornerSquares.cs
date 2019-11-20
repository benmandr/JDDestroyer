using GameServer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClient.GraphicItems
{
    class CornerSquares : GraphicItem
    {
        Bitmap buffer { get; set; }
        public CornerSquares(Bitmap buffer)
        {
            this.buffer = buffer;
        }

        public void Draw()
        {
            using (var g = Graphics.FromImage(buffer))
            {
                int cornerCord = Proportion.ClientSize(Config.CORNERSIZE + Config.INNERSQUARESIZE);
                int cornerSize = Proportion.ClientSize(Config.CORNERSIZE);
                g.FillRectangle(Brushes.Black, new Rectangle(0, 0, cornerSize, cornerSize));
                g.FillRectangle(Brushes.Black, new Rectangle(0, cornerCord, cornerSize, cornerSize));
                g.FillRectangle(Brushes.Black, new Rectangle(cornerCord, 0, cornerSize, cornerSize));
                g.FillRectangle(Brushes.Black, new Rectangle(cornerCord, cornerCord, cornerSize, cornerSize));
            }
        }
    }
}
