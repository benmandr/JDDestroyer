using System;
using SuperWebSocket;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Newtonsoft.Json;

namespace GameServer.Models
{
    public class Game
    {
        public string name { get; set; }
        public GamePlayer P1 { get; set; }
        public GamePlayer P2 { get; set; }
        public GamePlayer P3 { get; set; }
        public GamePlayer P4 { get; set; }

        public List<Enemy> enemies { get; set; }


        [JsonIgnore]
        public List<GamePlayerEnemyObserver> enemyObservers { get; set; }
        [JsonIgnore]
        public Thread spawnThread;



        public Game(GamePlayer gamePlayer)
        {
            enemies = new List<Enemy>();
            enemyObservers = new List<GamePlayerEnemyObserver>();
            name = "JdDestroyer";
            gamePlayer.game = this;
            gamePlayer.position = Position.P1InitialPosition();
            gamePlayer.moveStrategy = new P1MoveStrategy();
            P1 = gamePlayer;
            //  spawner.enable();
            spawnEnemies();
            enemyObservers.Add(new GamePlayerEnemyObserver(gamePlayer));
        }

        public Game()
        {
        }

        public void addPlayer(GamePlayer gamePlayer)
        {
            if (P1 == null)
            {
                gamePlayer.moveStrategy = new P1MoveStrategy();
                P1 = gamePlayer;
                gamePlayer.position = Position.P1InitialPosition();
            }
            else if (P2 == null)
            {
                gamePlayer.moveStrategy = new P2MoveStrategy();
                P2 = gamePlayer;
                gamePlayer.position = Position.P2InitialPosition();
            }
            else if (P3 == null)
            {
                gamePlayer.moveStrategy = new P3MoveStrategy();
                P3 = gamePlayer;
                gamePlayer.position = Position.P3InitialPosition();
            }
            else if (P4 == null)
            {
                gamePlayer.moveStrategy = new P4MoveStrategy();
                P4 = gamePlayer;
                gamePlayer.position = Position.P4InitialPosition();
            }

            gamePlayer.game = this;

            enemyObservers.Add(new GamePlayerEnemyObserver(gamePlayer));
        }



        public void spawnEnemies()
        {
            Console.WriteLine(enemies.Count);
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
                            notifyPlayers(newEnemy);
                        }
                    }

                    Thread.Sleep(Config.ENEMYSPAWNSPEED);
                }

            }).Start();
        }
        public void notifyPlayers(Enemy enemy)
        {
            foreach (GamePlayerEnemyObserver observer in enemyObservers)
            {
                observer.update(enemy);
            }
        }



        public void removePlayer(GamePlayer leavingPlayer)
        {
            if (leavingPlayer.Equals(P1))
            {
                P1 = null;
                return;
            }
            if (leavingPlayer.Equals(P2))
            {
                P2 = null;
                return;
            }
            if (leavingPlayer.Equals(P3))
            {
                P3 = null;
                return;
            }
            if (leavingPlayer.Equals(P4))
            {
                P4 = null;
                return;
            }
        }

        public void sendMessage(string msg, GamePlayer except)
        {
            foreach (GamePlayer gamePlayer in getPlayers())
            {
                if (gamePlayer != null && (except == null || except.player.session.SessionID != gamePlayer.player.session.SessionID))
                {
                    gamePlayer.sendMessage(msg);
                }
            }
        }

        public bool isEmpty()
        {
            foreach (GamePlayer gamePlayer in getPlayers())
            {
                if (gamePlayer != null)
                {
                    return false;
                }
            }
            return true;
        }

        public void enemySpawner()
        {
        }

        public void sendMessage(string msg)
        {
            sendMessage(msg, null);
        }

        public IEnumerable<GamePlayer> getPlayers()
        {
            yield return P1;
            yield return P2;
            yield return P3;
            yield return P4;
        }

        public GamePlayer getPlayer(long id)
        {
            foreach (GamePlayer gamePlayer in getPlayers())
            {
                if (gamePlayer.player.id == id)
                    return gamePlayer;
            }
            return null;
        }
    }
}
