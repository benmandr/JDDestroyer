using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using GameServer.Messages;
using System.Drawing;

using GameServer.Geometry;
namespace GameServer.Models
{
    public class GamePlayer
    {
        public Player player { get; set; }
        public Position position { get; set; }
        public Color color { get; set; }

        [JsonIgnore]
        public MoveLeftCommand moveLeft { get; set; }
        [JsonIgnore]
        public MoveRightCommand moveRight { get; set; }
        [JsonIgnore]
        public GameFacade game { get; set; }
        public long score { get; set; }


        public void Shoot()
        {
            Bullet bullet = new Bullet(this);
            if(game.bulletMover != null)
            {
                game.bulletMover.bullets.Add(bullet);
                game.bulletMover.notify();
            }
        }

        public GamePlayer(Player player)
        {
            this.player = player;
            score = 0;
        }

        public void sendMessage(string message)
        {
            player.sendMessage(message);
        }

        public bool MoveRight()
        {
            moveRight.Execute();
            if (Position.PlayerOutOfBounds(position))
            {
                moveRight.Undo();
                return false;
            }
            return true;
        }

        public bool MoveLeft()
        {
            moveLeft.Execute();
            if (Position.PlayerOutOfBounds(position))
            {
                moveLeft.Undo();
                return false;
            }
            return true;
        }

        public void PositionChanged() {
            PositionChangedMessage message = new PositionChangedMessage();
            message.position = position;
            message.playerId = player.id;

            SocketMessage socketMessage = new SocketMessage();
            socketMessage.type = PositionChangedMessage.TYPE;
            socketMessage.data = JsonConvert.SerializeObject(message);
            game.notifier.sendMessage(JsonConvert.SerializeObject(socketMessage), game.gamePlayers.getPlayers());
        }

        public bool Equals(GamePlayer obj)
        {
            if (obj == null)
            {
                return false;
            }
            return player.id == obj.player.id;
        }
    }
}
