using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using GameServer.Geometry;
using System.Collections;

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
                P1 = gamePlayer;
                gamePlayer.color = Color.Red;
                gamePlayer.position = Position.P1InitialPosition();
                IMoveStrategy strategy = new P1MoveStrategy();
                gamePlayer.moveLeft = new MoveLeftCommand(strategy, gamePlayer.position);
                gamePlayer.moveRight = new MoveRightCommand(strategy, gamePlayer.position);
            }
            else if (P2 == null)
            {
                P2 = gamePlayer;
                gamePlayer.color = Color.Green;
                gamePlayer.position = Position.P2InitialPosition();
                IMoveStrategy strategy = new P2MoveStrategy();
                gamePlayer.moveLeft = new MoveLeftCommand(strategy, gamePlayer.position);
                gamePlayer.moveRight = new MoveRightCommand(strategy, gamePlayer.position);
            }
            else if (P3 == null)
            {
                P3 = gamePlayer;
                gamePlayer.color = Color.Blue;
                gamePlayer.position = Position.P3InitialPosition();
                IMoveStrategy strategy = new P3MoveStrategy();
                gamePlayer.moveLeft = new MoveLeftCommand(strategy, gamePlayer.position);
                gamePlayer.moveRight = new MoveRightCommand(strategy, gamePlayer.position);
            }
            else if (P4 == null)
            {
                P4 = gamePlayer;
                gamePlayer.color = Color.Purple;
                gamePlayer.position = Position.P4InitialPosition();
                IMoveStrategy strategy = new P4MoveStrategy();
                gamePlayer.moveLeft = new MoveLeftCommand(strategy, gamePlayer.position);
                gamePlayer.moveRight = new MoveRightCommand(strategy, gamePlayer.position);
            }
            gamePlayer.game = game;
        }


        public Position getDirection(GamePlayer player)
        {
            if (player.Equals(P1))
            {
                return Position.DirectionTop();
            }
            if (player.Equals(P2))
            {
                return Position.DirectionLeft();
            }
            if (player.Equals(P3))
            {
                return Position.DirectionBottom();
            }
            if (player.Equals(P4))
            {
                return Position.DirectionRight();
            }
            return null;
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
            if (P1 != null)
                yield return P1;
            if (P2 != null)
                yield return P2;
            if (P3 != null)
                yield return P3;
            if (P4 != null)
                yield return P4;
        }

        public void SendMessage(string message)
        {
            foreach (GamePlayer player in getPlayers())
            
                player.sendMessage(message);
            }
        }
    }
}
