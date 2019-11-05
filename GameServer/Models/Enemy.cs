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

        public void Walk()
        {
            long currentTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            if (currentTime - lastMoveTime > Config.ENEMYMOVERATE * 3) //Remove *3
            {
                lastMoveTime = currentTime;
                string[] moves = { "subtractX", "subtractY", "addX", "addY" };
                string nextMove = moves[randomInstance.Next(0, moves.Length)];

                Position copiedPosition = new Position(position.x, position.y);
                MethodInfo moveCopy = copiedPosition.GetType().GetMethod(nextMove);
                copiedPosition = (Position)moveCopy.Invoke(copiedPosition, new object[] { 1 });
                if (Bounds.InnerSquare().enemyInBounds(copiedPosition))
                {
                    MethodInfo moveMethod = position.GetType().GetMethod(nextMove);
                    moveMethod.Invoke(position, new object[] { 1 });
                }
            }
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
