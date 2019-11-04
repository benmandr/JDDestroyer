using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameServer.Messages;
using Newtonsoft.Json;

namespace GameServer.Models
{
    public class GamePlayerEnemyObserver
    {
        GamePlayer gamePlayer { get; set; }

        public GamePlayerEnemyObserver(GamePlayer gamePlayer)
        {
            this.gamePlayer = gamePlayer;
        }
        public void enemySpawn(Enemy enemy)
        {
            EnemySpawnMessage messageData = new EnemySpawnMessage(enemy.position, enemy.getType);

            SocketMessage message = new SocketMessage();
            message.type = EnemySpawnMessage.TYPE;
            message.data = JsonConvert.SerializeObject(messageData);

            gamePlayer.sendMessage(JsonConvert.SerializeObject(message));
        }

        public void bulletList(List<Bullet> bullets)
        {
            BulletsDataMessage messageData = new BulletsDataMessage(bullets);

            SocketMessage message = new SocketMessage();
            message.type = BulletsDataMessage.TYPE;
            message.data = JsonConvert.SerializeObject(messageData);

            gamePlayer.sendMessage(JsonConvert.SerializeObject(message));
        }
    }
}
