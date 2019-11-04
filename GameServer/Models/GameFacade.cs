using System;
using System.Collections.Generic;
using System.Threading;
using Newtonsoft.Json;
using System.Drawing;
using GameServer.Geometry;
using GameServer.Messages;

namespace GameServer.Models
{
    public class GameFacade
    {
        public string name { get; set; }

        public GamePlayers gamePlayers { get; set; }

        [JsonIgnore]
        public EnemySpawner enemySpawner { get; set; }

        [JsonIgnore]
        public Mover mover;

        public GameFacade()
        {
            gamePlayers = new GamePlayers();
            mover = new Mover();
            gamePlayers = new GamePlayers();
            enemySpawner = new EnemySpawner(mover);
        }

        public void StartGame()
        {
            name = "JdDestroyer";
            enemySpawner.Start();
            mover.Start();
        }

        public void AddPlayer(GamePlayer gamePlayer)
        {
            gamePlayers.addPlayer(gamePlayer, this);
            GamePlayerObserver observer = new GamePlayerObserver(gamePlayer);
            mover.addObserver(observer);


            PlayerDataMessage playerData = new PlayerDataMessage();
            playerData.position = gamePlayer.position;
            playerData.name = gamePlayer.player.name;
            playerData.id = gamePlayer.player.id;
            playerData.color = gamePlayer.color;

            SocketMessage playerDataMessage = new SocketMessage();
            playerDataMessage.type = PlayerDataMessage.TYPE;
            playerDataMessage.data = JsonConvert.SerializeObject(playerData);
            Console.WriteLine(JsonConvert.SerializeObject(playerDataMessage));
            gamePlayer.sendMessage(JsonConvert.SerializeObject(playerDataMessage));


            mover.observers.ForEach(x => x.PlayerListChange(this));

            
            mover.notify();
        }

        public void SendMessage(string message)
        {
            foreach(GamePlayer player in gamePlayers.getPlayers())
            {
                player.sendMessage(message);
            }
        }
    }
}
