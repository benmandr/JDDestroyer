﻿using System;
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

        private Thread moveThread = null;
        public List<GamePlayerObserver> observers = new List<GamePlayerObserver>();

        public Mover()
        {
            items = new List<IMovable>();
        }

        public List<Bullet> GetBullets()
        {
            return items.FindAll(x => x is BulletAdapter).Select(x => (BulletAdapter)x).Select(x => x.bullet).ToList();
        }

        public List<Enemy> GetEnemies()
        {
            return items.FindAll(x => x is EnemyAdapter).Select(x => (EnemyAdapter)x).Select(x => x.enemy).ToList();
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
                    if(items.Count > 0)
                    {
                        items.ForEach(x => x.Move());
                        notify();
                    }
                    Thread.Sleep(Config.FRAMESPEED);
                }

            });
            moveThread.Start();
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
        }
    }
}
