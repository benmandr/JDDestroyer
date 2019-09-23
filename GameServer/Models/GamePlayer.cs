using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using GameServer.Messages;

namespace GameServer.Models
{
    class GamePlayer
    {
        public Player player { get; set; }
        public Position position { get; set; }
        public IMoveStrategy moveStrategy { get; set; }
        public Game game { get; set; }

        public long score { get; set; }


        public GamePlayer(Player player)
        {
            this.player = player;
            score = 0;
        }

        public void sendMessage(string message)
        {
            player.sendMessage(message);
        }

        public void MoveRight()
        {
            moveStrategy.MoveRight(position);
            PositionChanged();
        }

        public void MoveLeft()
        {
            moveStrategy.MoveLeft(position);
            PositionChanged();
        }

        public void PositionChanged() {
            PositionChangedMessage message = new PositionChangedMessage();
            message.position = position;
            message.playerId = player.id;
            game.sendMessage(JsonConvert.SerializeObject(message), this);
        }

        public bool Equals(GamePlayer obj)
        {
            if (obj == null)
            {
                return false;
            }
            return player.session.SessionID == obj.player.session.SessionID;
        }
    }
}
