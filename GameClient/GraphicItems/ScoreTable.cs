using GameServer;
using GameServer.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClient.GraphicItems
{
    class ScoreTable : GraphicItem
    {
        Bitmap buffer { get; set; }
        GamePlayers players { get; set; }

        private readonly object scoreTableLock;


        public ScoreTable(Bitmap buffer, GamePlayers players)
        {
            this.buffer = buffer;
            this.players = players;
            scoreTableLock = new object();
        }

        public void Draw()
        {
            using (var g = Graphics.FromImage(buffer))
            {
                string font = "Comic Sans MS";

                int mainSquareY = Proportion.ClientSize(Config.CORNERSIZE) / 2;
                int mainSquareHeight = Proportion.ClientSize(Config.INNERSQUARESIZE) + Proportion.ClientSize(Config.CORNERSIZE);
                Rectangle mainScoreSquare = new Rectangle(0, mainSquareY, Proportion.windowWidth, mainSquareHeight);
                g.FillRectangle(new SolidBrush(Color.FromArgb(230, Color.Black)), mainScoreSquare);

                int h1FontSize = Proportion.windowWidth / 25;
                float titleX = (float)(Proportion.windowWidth / 2 - 75);
                float titleY = (float)(Proportion.ClientSize(Config.CORNERSIZE) / 1.5);
                g.DrawString("High Scores", new Font(font, h1FontSize), Brushes.White, titleX, titleY);

                int pFontSize = Proportion.windowWidth / 40;
                float tHeadX1 = 20;
                float tHeadY1 = (float)Proportion.ClientSize(Config.CORNERSIZE);
                g.DrawString("Player", new Font(font, pFontSize), Brushes.White, tHeadX1, tHeadY1);

                float tHeadX2 = (float)(Proportion.windowWidth - 60);
                float tHeadY2 = (float)Proportion.ClientSize(Config.CORNERSIZE);
                g.DrawString("Score", new Font(font, pFontSize), Brushes.White, tHeadX2, tHeadY2);

                int index = 0;
                int positionReduce = 20;
                lock (scoreTableLock)
                {
                    foreach (GamePlayer player in players.getPlayers())
                    {
                        float playerNameX = 20;
                        float playerNameY = (float)(Proportion.ClientSize(Config.CORNERSIZE) + positionReduce);
                        string playerName = "Player" + index;
                        string playerScore = player.score.ToString();
                        g.DrawString(playerName, new Font(font, pFontSize), Brushes.White, playerNameX, playerNameY);

                        float playerScoreX = (float)(Proportion.windowWidth - 60);
                        float playerScoreY = (float)(Proportion.ClientSize(Config.CORNERSIZE) + positionReduce);
                        g.DrawString(playerScore, new Font(font, pFontSize), Brushes.White, playerScoreX, playerScoreY);

                        index++;
                        positionReduce += positionReduce;
                    }
                }
            }
        }
    }
}
