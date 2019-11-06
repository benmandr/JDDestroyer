using GameServer.Models;

namespace GameServer
{
    public class EnemyAdapter : IMovable
    {

        public Enemy enemy { get; set; }
        public EnemyAdapter(Enemy enemy)
        {
            this.enemy = enemy;
        }


        public bool Move()
        {
            return enemy.Walk();
        }
    }
}