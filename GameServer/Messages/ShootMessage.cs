using System.Collections.Generic;
using GameServer.Models;

namespace GameServer.Messages
{
    public class ShootMessage
    {
        public const int TYPE = 8;
    }

    public class BulletsDataMessage
    {
        public const int TYPE = 10;
        public List<Bullet> bulletList { get; set; }

        public BulletsDataMessage(List<Bullet> bullets)
        {
            bulletList = bullets;
        }
    }
}
