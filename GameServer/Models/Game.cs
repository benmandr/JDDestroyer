
using System;
using SuperWebSocket;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace GameServer.Models
{
    class Game
    {
        GamePlayer P1 { get; set; }
        GamePlayer P2 { get; set; }
        GamePlayer P3 { get; set; }
        GamePlayer P4 { get; set; }

        List<Enemy> enemies = new List<Enemy>();

        public Game(GamePlayer gamePlayer)
        {
            gamePlayer.game = this;
            gamePlayer.position = new Position(0, 0);
            P1 = gamePlayer;
            enemySpawner();
        }

        public void addPlayer(GamePlayer gamePlayer)
        {
            if (P1 == null)
                P1 = gamePlayer;
            else if (P2 == null)
                P2 = gamePlayer;
            else if (P3 == null)
                P3 = gamePlayer;
            else if (P4 == null)
                P4 = gamePlayer;

            gamePlayer.position = new Position(0, 50);
            gamePlayer.game = this;
            sendMessage(gamePlayer.player.name + " has just joined", gamePlayer);
        }

        public void removePlayer(GamePlayer leavingPlayer)
        {
            sendMessage(leavingPlayer.player.name + " disconnected", leavingPlayer);
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
            new Thread(() =>
            {
                while (true)
                {
                    if (enemies.Count < 1)
                        enemies.Add(new Enemy(this, new Position(0, 0)));
                    Thread.Sleep(5000);
                }
            }).Start();
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
    }
}
