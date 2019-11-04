using GameServer.Models;

namespace GameServer
{
    public class BulletAdapter : ToMovable
    {
        private Bullet bullet;
        public BulletAdapter(Bullet bullet) {
            this.bullet = bullet;
        }
        
        public void move()
        {
            this.bullet.Fly();
        }
    }
}