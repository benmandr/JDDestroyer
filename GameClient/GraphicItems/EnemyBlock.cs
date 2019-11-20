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
    class EnemyBlock : GraphicItem
    {
        Bitmap buffer { get; set; }

        Color enemyColor { get; set; }

        Position position { get; set; }
        public EnemyBlock(Bitmap buffer, Color enemyColor, Position position)
        {
            this.buffer = buffer;
            this.enemyColor = enemyColor;
            this.position = position;
        }
        public void Draw()
        {
            using (var g = Graphics.FromImage(buffer))
            {
                int x = Proportion.ClientSize(position.x) - Proportion.ClientSize(Config.ENEMYSIZE / 2);
                int y = Proportion.ClientSize(position.y) - Proportion.ClientSize(Config.ENEMYSIZE / 2);
                int size = Proportion.ClientSize(Config.ENEMYSIZE);
                g.FillRectangle(new SolidBrush(enemyColor), x, y, size, size);
            }
        }
    }
}
