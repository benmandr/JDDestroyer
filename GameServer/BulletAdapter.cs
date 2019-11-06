using GameServer.Models;

namespace GameServer
{
    public class BulletAdapter : IMovable
    {
        public Bullet bullet { get; set; }
        public BulletAdapter(Bullet bullet) {
            this.bullet = bullet;
        }
        
        public bool Move()
        {
            return bullet.Fly();
        }
    }
}