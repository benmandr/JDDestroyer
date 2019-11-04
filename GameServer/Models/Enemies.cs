using GameServer.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameServer.Models
{
    public class Enemies
    {
        public List<Enemy> enemies { get; set; }

        public Thread spawnThread;

        public Enemies()
        {
            enemies = new List<Enemy>();
        }

        public void Start(Notifier notifier, List<GamePlayerEnemyObserver> observers)
        {
            new Thread(() =>
            {
                while (true)
                {
                    if (enemies.Count < Config.MAXENEMIES)
                    {
                        int[] types = { RedEnemy.TYPE, GreenEnemy.TYPE, BlueEnemy.TYPE };
                        Random rand = new Random();

                        int index = rand.Next(types.Length);
                        Enemy newEnemy = EnemyFactory.getInstance().getEnemy(types[index]);
                        if (newEnemy != null)
                        {
                            enemies.Add(newEnemy);
                            notifier.notifyPlayers(newEnemy, observers);
                            newEnemy.enemyMove();
                        }
                    }

                    Thread.Sleep(Config.ENEMYSPAWNSPEED);
                }

            }).Start();
        }
    }
}
