﻿using System;
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
        public Enemies enemySpawner { get; set; }

        [JsonIgnore]
        public Mover mover;

        public GameFacade()
        {
            gamePlayers = new GamePlayers();
            mover = new Mover();
            gamePlayers = new GamePlayers();
            enemySpawner = new Enemies(mover);
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
            mover.observers.ForEach(x => x.PlayerListChange(this));
            mover.notify();
        }

        public void SendMessage(string message)
        {
            gamePlayers.SendMessage(message);
        }
    }
}
