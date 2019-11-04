using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Models
{
    public class GamePlayers
    {
        public GamePlayer P1 { get; set; }
        public GamePlayer P2 { get; set; }
        public GamePlayer P3 { get; set; }
        public GamePlayer P4 { get; set; }

        public void addPlayer(GamePlayer gamePlayer, GameFacade game)
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
            gamePlayer.game = game;
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
        public GamePlayer getPlayer(long id)
        {
            foreach (GamePlayer gamePlayer in getPlayers())
            {
                if (gamePlayer.player.id == id)
                    return gamePlayer;
            }
            return null;
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
