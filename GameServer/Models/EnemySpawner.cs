using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace GameServer.Models
{
    public class EnemySpawner
    {
        public Game game { get; set; }

        private Thread spawnThread;

        public EnemySpawner(Game game)
        {
            this.game = game;
        }

        public void enable()
        {
            new Thread(() =>
            {
                while (true)
                {
                    if (game.enemyCount() < Config.MAXENEMIES)
                        game.addEnemy(new Enemy(game, new Position(0, 0)));
                    Thread.Sleep(Config.ENEMYSPAWNSPEED);
                }
            }).Start();
        }

        public void disable()
        {
            if(spawnThread != null)
            {
                spawnThread.Abort();
            }
            spawnThread = null;
        }
    }
}
