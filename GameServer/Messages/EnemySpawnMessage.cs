using GameServer.Models;
using System.Drawing;
using System;
namespace GameServer.Messages
{
    public class EnemySpawnMessage
    {
        public const int TYPE = 6;

        public EnemySpawnMessage(Position position, int type)
        {
            this.position = position;
            this.type = type;
        }

        public EnemySpawnMessage() { }

        public int type { get; set; }
        public Position position { get; set; }
    }
}
