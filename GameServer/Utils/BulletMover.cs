using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using GameServer.Models;
namespace GameServer
{
    public class BulletMover
    {
        public List<Bullet> bullets;


        private Thread moveThread = null;
        List<GamePlayerEnemyObserver> observers = new List<GamePlayerEnemyObserver>();

        public BulletMover() {
            bullets = new List<Bullet>();
        }

        public void addObserver (GamePlayerEnemyObserver observer)
        {
            observers.Add(observer);
        }

        public void Start()
        {
                moveThread = new Thread(() =>
                {
                    while (true)
                    {
                        bullets.ForEach(x => x.Fly());
                        notify();
                        Thread.Sleep(Config.BULLETSPEED);
                    }

                });
                moveThread.Start();
        }

        public void notify()
        {
            observers.ForEach(x => x.bulletListChange(bullets));
        }
    }
}
