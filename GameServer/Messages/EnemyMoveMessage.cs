using System.Drawing;
using System;
using GameServer.Geometry;
namespace GameServer.Messages
{
    public class EnemyMoveMessage
    {
        public const int TYPE = 7;

        public EnemyMoveMessage(Position position, string hashcode)
        {
            this.position = position;
            this.hashcode = hashcode;
        }

        public EnemyMoveMessage() {
        }

        public string hashcode { get; set; }
        public Position position { get; set; }

    }
}
