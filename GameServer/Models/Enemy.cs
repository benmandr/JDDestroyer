using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading;

namespace GameServer.Models
{
    public abstract class Enemy : ICloneable
    {
        public Position position { get; set; }

        public Enemy(Position position)
        {
            this.position = position;
        }
        Thread moveThread = null;
        public Enemy()
        {
        }
        abstract public Color getColor();
        abstract public int getType { get; }

        public void setPosition(Position position)
        {
            this.position = position;
        }

        public void enemyMove()
        {
            /*
            moveThread = new Thread(() =>
            {
                while (true)
                {
                    Thread.Sleep(Config.ENEMYMOVERATE);
                    setPosition(position.addX(Config.MOVESPEED));
                    Thread.Sleep(Config.ENEMYMOVERATE);
                    setPosition(position.addY(Config.MOVESPEED));
                    Thread.Sleep(Config.ENEMYMOVERATE);
                    setPosition(position.subtractX(Config.MOVESPEED));
                    Thread.Sleep(Config.ENEMYMOVERATE);
                    setPosition(position.subtractY(Config.MOVESPEED));
                }
            }).Start();
            */
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        ~Enemy()
        {
            // Your code
            if (moveThread != null)
                moveThread.Abort();
        }
    }
}
