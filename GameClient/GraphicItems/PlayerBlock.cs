using GameServer;
using GameServer.Geometry;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClient.GraphicItems
{
    class PlayerBlock : GraphicItem
    {
        Bitmap buffer { get; set; }

        Color color { get; set; }

        Position position { get; set; }
        public PlayerBlock(Bitmap buffer, Color color, Position position)
        {
            this.buffer = buffer;
            this.color = color;
            this.position = position;
        }
        public void Draw()
        {
            using (var g = Graphics.FromImage(buffer))
            {
                int x = Proportion.ClientSize(position.x) - Proportion.ClientSize(Config.PLAYERSIZE / 2);
                int y = Proportion.ClientSize(position.y) - Proportion.ClientSize(Config.PLAYERSIZE / 2);
                int size = Proportion.ClientSize(Config.PLAYERSIZE);
                g.FillRectangle(new SolidBrush(color), x, y, size, size);
            }
        }
    }
}
