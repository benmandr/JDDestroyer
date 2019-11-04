using System.Threading;

namespace GameServer.Models
{
    public class Bullet
    {
      //  private static long bulletIncremental = 1;

     //   private long id { get; set; }

        private Position position { get; set; }
         
        
        public Bullet()
        {

        }

        public Bullet(GamePlayer gamePlayer, BulletMover mover)
        {
        //    id = bulletIncremental++;
            position = (Position)gamePlayer.position.Clone();
            mover.bullets.Add(this);
        }

        public void Move()
        {
            position.addY(1);
        }
    }
}
