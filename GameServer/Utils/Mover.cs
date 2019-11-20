using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using GameServer.Models;
namespace GameServer
{
    public class Mover
    {
        public List<IMovable> items;
        public List<IMovable> newItems = new List<IMovable>();

        private readonly object x = new object();

        private Thread moveThread = null;
        public List<GamePlayerObserver> observers = new List<GamePlayerObserver>();

        public Mover()
        {
            items = new List<IMovable>();
        }

        public List<Bullet> GetBullets()
        {
            lock (x)
            {
                return items.FindAll(x => x is BulletAdapter).Select(x => (BulletAdapter)x).Select(x => x.bullet).ToList();
            }
        }

        public List<Enemy> GetEnemies()
        {
            lock (x)
            {
                return items.FindAll(x => x is EnemyAdapter).Select(x => (EnemyAdapter)x).Select(x => x.enemy).ToList();
            }
        }

        public GoldenTooth GetGoldenTooth()
        {
            lock (x)
            {
                return (GoldenTooth)items.Find(x => x is GoldenTooth);
            }
        }

        public void addObserver(GamePlayerObserver observer)
        {
            observers.Add(observer);

        }

        public void Start()
        {
            moveThread = new Thread(() =>
            {
                while (true)
                {
                    lock (x)
                    {
                        if (items.Count > 0)
                        {
                            items.RemoveAll(item => !item.Move());
                            notify();
                        }
                        foreach(IMovable item in newItems)
                        {
                            items.Add(item);
                        }
                        newItems.Clear();
                    }
                    Thread.Sleep(Config.FRAMESPEED);
                }

            });
            moveThread.Start();
        }

        public void addNew(IMovable item)
        {
            newItems.Add(item);
        }

        public void addItem(IMovable item)
        {
            items.Add(item);
            notify();
        }

        public void notify()
        {
            List<Bullet> bullets = GetBullets();
            observers.ForEach(x => x.BulletListChange(bullets));

            List<Enemy> enemies = GetEnemies();
            observers.ForEach(x => x.EnemyListChange(enemies));

            GoldenTooth goldenTooth = GetGoldenTooth();
            if(goldenTooth != null)
                observers.ForEach(x => x.GoldenToothPosition(goldenTooth));
        }
    }
}
