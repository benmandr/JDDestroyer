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
    class GoldenToothBlock : GraphicItem
    {
        Bitmap buffer { get; set; }

        Position position { get; set; }
        public GoldenToothBlock(Bitmap buffer, Position position)
        {
            this.buffer = buffer;
            this.position = position;
        }
        public void Draw()
        {
            using (var g = Graphics.FromImage(buffer))
            {
                int x = Proportion.ClientSize(position.x) - Proportion.ClientSize(Config.GOLDENTOOTHSIZE / 2);
                int y = Proportion.ClientSize(position.y) - Proportion.ClientSize(Config.GOLDENTOOTHSIZE / 2);
                int size = Proportion.ClientSize(Config.GOLDENTOOTHSIZE);
                g.FillRectangle(new SolidBrush(Color.Gold),x, y, size, size);
            }
        }
    }
}
