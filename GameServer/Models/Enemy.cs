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

        //[JsonIgnore]
        //private 

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
            Position currentPosition = position;
            string[] moves = { "subtractX", "subtractY", "addX", "addY" };
            Console.WriteLine(moves[randomInstance.Next(0, moves.Length - 1)]);
            MethodInfo moveMethod = currentPosition.GetType().GetMethod(moves[randomInstance.Next(0, moves.Length)]);
            moveMethod.Invoke(currentPosition, new object[] { 1 });
            //TODO: Bounds, TimeIntervals on move
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
