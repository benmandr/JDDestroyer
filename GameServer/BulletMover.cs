using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using GameServer.Models;
using GameServer.Geometry;
namespace GameServer
{
    public class BulletMover
    {
        public List<Bullet> bullets;

        private readonly object x = new object();

        private Thread moveThread = null;
        private List<GamePlayerEnemyObserver> observers;
        public BulletMover(List<GamePlayerEnemyObserver>  observers)
        {
            this.observers = observers;
            bullets = new List<Bullet>();
        }

        public void addBullet(Bullet bullet)
        {
            lock (x)
            {
                bullets.Add(bullet);
            }
        }

        public void start()
        {
            moveThread = new Thread(() =>
            {
                while (true)
                {
                    lock (x)
                    {
                      //  Bounds OuterCircle = Bounds.MainSquare();
                      //  bullets.RemoveAll(x => !OuterCircle.inBounds(x.position));
                        bullets.ForEach(x => x.Fly());
                    }
                    observers.ForEach(x => x.bulletListChange(bullets));
                    Thread.Sleep(Config.BULLETSPEED);
                }

            });
            moveThread.Start();
        }
    }
}
