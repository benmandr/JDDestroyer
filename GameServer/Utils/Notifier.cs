using GameServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Utils
{
    public class Notifier
    {
        public void notifyPlayers(Enemy enemy, List<GamePlayerEnemyObserver> enemyObservers)
        {
            enemyObservers.ForEach(x => x.enemySpawn(enemy));
        }

        public void sendMessage(string msg, GamePlayer except, IEnumerable<GamePlayer> gamePlayers)
        {
            foreach (GamePlayer gamePlayer in gamePlayers)
            {
                if (gamePlayer != null && (except == null || except.player.session.SessionID != gamePlayer.player.session.SessionID))
                {
                    gamePlayer.sendMessage(msg);
                }
            }
        }

        public void sendMessage(string msg, IEnumerable<GamePlayer> gamePlayers)
        {
            sendMessage(msg, null, gamePlayers);
        }
    }
}
