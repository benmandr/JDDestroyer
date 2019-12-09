using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameServer.Messages;
using Newtonsoft.Json;

namespace GameServer.Models
{
    public class GamePlayerObserver
    {
        GamePlayer gamePlayer { get; set; }

        public GamePlayerObserver(GamePlayer gamePlayer)
        {
            this.gamePlayer = gamePlayer;
        }

        public void BulletListChange(List<IBullet> bullets)
        {
            BulletsDataMessage messageData = new BulletsDataMessage(bullets);

            SocketMessage message = new SocketMessage();
            message.type = BulletsDataMessage.TYPE;
            message.data = JsonConvert.SerializeObject(messageData);

            gamePlayer.sendMessage(JsonConvert.SerializeObject(message));
        }

        public void EnemyListChange(List<IEnemy> enemies)
        {
            EnemiesDataMessage messageData = new EnemiesDataMessage(enemies.Select(x => new EnemyDummy(x)).ToList());

            SocketMessage message = new SocketMessage();
            message.type = EnemiesDataMessage.TYPE;
            message.data = JsonConvert.SerializeObject(messageData);

            gamePlayer.sendMessage(JsonConvert.SerializeObject(message));
        }

        public void GoldenToothPosition(GoldenTooth goldenTooth)
        {
            GoldenToothMessage messageData = new GoldenToothMessage(goldenTooth);

            SocketMessage message = new SocketMessage();
            message.type = GoldenToothMessage.TYPE;
            message.data = JsonConvert.SerializeObject(messageData);

            gamePlayer.sendMessage(JsonConvert.SerializeObject(message));
        }

        public void PlayerListChange(GameFacade game)
        {
            SocketMessage gameMessage = new SocketMessage();
            gameMessage.type = GameDataMessage.TYPE;
            gameMessage.data = JsonConvert.SerializeObject(game);
            //Console.WriteLine(JsonConvert.SerializeObject(gameMessage));

            gamePlayer.sendMessage(JsonConvert.SerializeObject(gameMessage));
        }
    }
}
