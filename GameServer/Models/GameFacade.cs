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
        public Enemies enemySpawner { get; set; }

        [JsonIgnore]
        public BulletMover bulletMover;

        public GameFacade()
        {
            gamePlayers = new GamePlayers();
            enemyObservers = new List<GamePlayerEnemyObserver>();
            bulletMover = new BulletMover();
            gamePlayers = new GamePlayers();
            enemySpawner = new Enemies();
            notifier = new Notifier();
        }

        public void StartGame()
        {
            name = "JdDestroyer";
            enemySpawner.Start(notifier, enemyObservers);
            bulletMover.Start();
        }

        public void AddPlayer(GamePlayer gamePlayer)
        {
            gamePlayers.addPlayer(gamePlayer, this);
            enemyObservers.Add(new GamePlayerEnemyObserver(gamePlayer));
        }
    }
}
