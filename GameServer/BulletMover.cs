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

        public BulletMover() {
            bullets = new List<Bullet>();
        }

        public void start()
        {
                moveThread = new Thread(() =>
                {
                    while (true)
                    {
                        bullets.ForEach(x => x.Move());
                        Thread.Sleep(Config.BULLETSPEED);
                    }

                });
                moveThread.Start();
        }
    }
}
