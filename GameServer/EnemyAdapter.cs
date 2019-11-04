using GameServer.Models;

namespace GameServer
{
    public class EnemyAdapter : ToMovable
    {

        private Enemy enemy;
        public EnemyAdapter(Enemy enemy) {
            this.enemy = enemy;
        }
        

        public void move()
        {
            this.enemy.walk();
        }
    }
}