using GameServer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClient.GraphicItems
{
    class PlayerScore : GraphicItem
    {
        private long score;
        Bitmap buffer { get; set; }
        public PlayerScore(Bitmap buffer, long score)
        {
            this.buffer = buffer;
            this.buffer = buffer;
            this.score = score;
        }
        public void Draw()
        {
            using (var g = Graphics.FromImage(buffer))
            {
                int fontSize = Proportion.windowWidth / 50;
                float xPosition = (float)(Proportion.windowWidth * 0.75);
                g.DrawString("Best score: " + score, new Font("Comic Sans MS", fontSize), Brushes.White, xPosition, 0);
            }
        }
    }
}
