using System.Threading;
using System.Drawing;
using Newtonsoft.Json;
using System.Collections.Generic;
using GameServer.Geometry;

namespace GameServer.Models
{
    public class Bullet
    {
        //  private static long bulletIncremental = 1;

        //   private long id { get; set; }

        public Position position { get; set; }
        public Color color { get; set; }
        
        public Bullet()
        {

        }

        public Bullet(GamePlayer gamePlayer)
        {
            //    id = bulletIncremental++;
            position = (Position)gamePlayer.position.Clone();
            color = gamePlayer.color;
        }

        public void Fly()
        {
            position.subtractY(2);
        }
    }
}
