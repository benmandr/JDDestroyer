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
            Console.WriteLine("ORIGINAL: " + GetHashCode());
            return this.MemberwiseClone();
        }

        public object deepClone()
        {
            Console.WriteLine("Hash code of original obj:" + GetHashCode().ToString());
            Enemy deepClone = (Enemy)this.MemberwiseClone();
            deepClone.position = this.position;
            return deepClone;
        }

        ~Enemy()
        {
            // Your code
            if (moveThread != null)
                moveThread.Abort();
        }
    }
}
