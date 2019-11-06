using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading;
using GameServer.Messages;
using Newtonsoft.Json;
using GameServer.Geometry;
using System.Reflection;

namespace GameServer.Models
{
    public abstract class Enemy : ICloneable
    {
        public Position position { get; set; }

        [JsonIgnore]
        private static Random randomInstance = new Random();

        private long lastMoveTime;

        public Enemy(Position position)
        {
            this.position = position;
            this.lastMoveTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
        }
        public Enemy()
        {
        }
        abstract public Color getColor();
        abstract public int getType { get; }

        public void setPosition(Position position)
        {
            this.position = position;
        }

        public override string ToString()
        {
            return position.x + " : " + position.y;
        }

        public bool Walk()
        {
            long currentTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            if (currentTime - lastMoveTime > Config.ENEMYMOVERATE) //Remove *3
            {
                lastMoveTime = currentTime;
                string[] moves = { "subtractX", "addX", "subtractY", "addY" };
                int x = 1;
                for(int i = randomInstance.Next(0, moves.Length); x < moves.Length; i = (i + 1) % moves.Length)
                {
                    Position copiedPosition = new Position(position.x, position.y);
                    MethodInfo moveCopy = copiedPosition.GetType().GetMethod(moves[i]);
                    copiedPosition = (Position)moveCopy.Invoke(copiedPosition, new object[] { Config.ENEMYMOVESPEED });
                    Bounds enemyBounds = new Bounds(copiedPosition, Config.ENEMYSIZE);
                    if (Bounds.InnerSquare().inBounds(enemyBounds))
                    {
                        position = copiedPosition;
                    }
                    x++;
                }
            }
            return true;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public object deepClone()
        {
            Enemy deepClone = (Enemy)this.MemberwiseClone();
            deepClone.position = this.position;
            return deepClone;
        }

        public static Enemy createFromDummy(EnemyDummy dummy)
        {
            switch (dummy.type)
            {
                case GreenEnemy.TYPE:
                    return new GreenEnemy(dummy.position);
                case BlueEnemy.TYPE:
                    return new BlueEnemy(dummy.position);
                case RedEnemy.TYPE:
                    return new RedEnemy(dummy.position);
            }
            return null;
        }

    }
}
