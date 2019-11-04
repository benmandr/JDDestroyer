using GameServer.Models;
using System.Drawing;
using System;
using GameServer.Geometry;
using System.Collections.Generic;
namespace GameServer.Messages
{
    public class EnemiesDataMessage
    {
        public const int TYPE = 6;
        public List<EnemyDummy> enemiesList { get; set; }

        public EnemiesDataMessage(List<EnemyDummy> enemies)
        {
            enemiesList = enemies;
        }
    }

    public class EnemyDummy
    {
        public int type { get; set; }
        public Position position { get; set; }

        public EnemyDummy() {
        }
        public EnemyDummy(Enemy enemy)
        {
            position = enemy.position;
            type = enemy.getType;
        }
    }
}
