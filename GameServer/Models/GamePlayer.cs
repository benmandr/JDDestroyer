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
        [JsonIgnore]
        public long lastShootTime { get; set; }

        public void Shoot()
        {

            long currentTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            if (game.mover != null && currentTime - lastShootTime > Config.SHOOTINGRATE)
            {
                removeScore(Config.SHOOTSCOST);
                game.mover.addItem(new BulletAdapter(new Bullet(game.gamePlayers, this, game.gamePlayers.getDirection(this), game.mover)));
                lastShootTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;

            }
        }

        public GamePlayer(Player player)
        {
            this.player = player;
            score = 0;
            lastShootTime = 0;
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

            game.SendMessage(JsonConvert.SerializeObject(socketMessage));
        }

        public bool Equals(GamePlayer obj)
        {
            if (obj == null)
            {
                return false;
            }
            return player.id == obj.player.id;
        }

        public void addScore(long score)
        {
            this.score += score;

            PlayerScoreMessage message = new PlayerScoreMessage(game.gamePlayers);

            SocketMessage socketMessage = new SocketMessage();
            socketMessage.type = PlayerScoreMessage.TYPE;
            socketMessage.data = JsonConvert.SerializeObject(message);
            game.SendMessage(JsonConvert.SerializeObject(socketMessage));
        }

        public void removeScore(long score)
        {
            this.score -= score;
            if (this.score < 0)
                this.score = 0;

            PlayerScoreMessage message = new PlayerScoreMessage(game.gamePlayers);

            SocketMessage socketMessage = new SocketMessage();
            socketMessage.type = PlayerScoreMessage.TYPE;
            socketMessage.data = JsonConvert.SerializeObject(message);
            game.SendMessage(JsonConvert.SerializeObject(socketMessage));
        }
    }
}
