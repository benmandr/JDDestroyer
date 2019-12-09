using GameServer.Models;

namespace GameServer
{
    public class EnemyAdapter : IMovable
    {

        public IEnemy enemy { get; set; }
        public EnemyAdapter(IEnemy enemy)
        {
            this.enemy = enemy;
        }


        public bool Move()
        {
            return enemy.Walk();
        }
    }
}