using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading;
using GameServer.Messages;
using Newtonsoft.Json;
using GameServer.Geometry;

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

        public void enemyMove()
        {

            new Thread(() =>
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

        }

        public void notifyMovement()
        {
            EnemyMoveMessage messageData = new EnemyMoveMessage(position, this.GetHashCode().ToString());

            SocketMessage message = new SocketMessage();
            message.type = EnemyMoveMessage.TYPE;
            message.data = JsonConvert.SerializeObject(messageData);

            //gamePlayer.sendMessage(JsonConvert.SerializeObject(message));
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

    }
}
