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
using GameServer.Models.EnemyStates;

namespace GameServer.Models
{
    public abstract class Enemy : ICloneable, IEnemy
    {
        public Position position { get; set; }

        [JsonIgnore]
        public EnemyState state { get; set; }

        public long lastMoveTime { get; set; }

        public Enemy(Position position)
        {
            this.position = position;
            this.lastMoveTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            this.state = new SmartEnemyState();
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
            return state.Walk(this);
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
