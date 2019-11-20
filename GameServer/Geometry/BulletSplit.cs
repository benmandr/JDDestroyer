using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameServer.Models;

namespace GameServer.Geometry
{
    abstract class BulletSplit
    {
        protected Bullet bullet;

        public BulletSplit(Bullet bullet)
        {
            this.bullet = bullet;
        }

        abstract public void adjustDirection();
        abstract public void adjustPosition();

        public Bullet getBullet()
        {
            bullet = bullet.deepClone();

            adjustDirection();
            adjustPosition();

            return bullet;
        }
    }
}
