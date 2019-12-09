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
        public const int TYPE = 9;
        public List<IBullet> bulletList { get; set; }

        public BulletsDataMessage(List<IBullet> bullets)
        {
            bulletList = bullets;
        }
    }
}
