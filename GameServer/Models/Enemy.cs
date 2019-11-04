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

        public Enemy(Position position)
        {
            this.position = position;
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
            Console.WriteLine("Position before rand" + position.ToString());
            Position currentPosition = position;
            string[] moves = { "subtractX", "subtractY", "addX", "addY" };
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
