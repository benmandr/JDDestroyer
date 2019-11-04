using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameServer.Models
{
    public class EnemySpawner
    {
        public Mover mover;

        public Thread spawnThread;

        public EnemySpawner(Mover mover)
        {
            this.mover = mover;
        }


        public void Start()
        {
            new Thread(() =>
            {
                while (true)
                {
                    if (mover.GetEnemies().Count < Config.MAXENEMIES)
                    {
                        int[] types = { RedEnemy.TYPE, GreenEnemy.TYPE, BlueEnemy.TYPE };
                        Random rand = new Random();

                        int index = rand.Next(types.Length);
                        Enemy newEnemy = EnemyFactory.getInstance().getEnemy(types[index]);
                        if (newEnemy != null)
                        {
                            EnemyAdapter adapter = new EnemyAdapter(newEnemy);
                            mover.addItem(adapter);
                        }
                    }

                    Thread.Sleep(Config.ENEMYSPAWNSPEED);
                }

            }).Start();
        }
    }
}
