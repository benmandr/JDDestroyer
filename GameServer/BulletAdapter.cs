using GameServer.Models;

namespace GameServer
{
    public class BulletAdapter : IMovable
    {
        public IBullet bullet { get; set; }
        public BulletAdapter(IBullet bullet) {
            this.bullet = bullet;
        }
        
        public bool Move()
        {
            return bullet.Fly();
        }
    }
}