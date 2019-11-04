using System;
using System.Collections.Generic;
using System.Threading;
using GameServer.Utils;
using Newtonsoft.Json;

namespace GameServer.Models
{
    public class GameFacade
    {
        public string name { get; set; }
        public GamePlayers gamePlayers { get; set; }

        [JsonIgnore]
        public List<GamePlayerEnemyObserver> enemyObservers { get; set; }

        [JsonIgnore]
        public Notifier notifier { get; set; }

        [JsonIgnore]
        public Enemies enemies { get; set; }

        [JsonIgnore]
        public BulletMover bulletMover;

        public void createGame()
        {
            name = "JdDestroyer";
            enemies.spawnEnemies(notifier, enemyObservers);
            bulletMover.start();
        }

        public GameFacade()
        {
            gamePlayers = new GamePlayers();
            enemyObservers = new List<GamePlayerEnemyObserver>();
            bulletMover = new BulletMover();
            gamePlayers = new GamePlayers();
            enemies = new Enemies();
            notifier = new Notifier();
        }

        public void AddPlayer(GamePlayer gamePlayer)
        {
            GamePlayerEnemyObserver newObserver = gamePlayers.addPlayer(gamePlayer, this);
            enemyObservers.Add(newObserver);
        }
    }
}
