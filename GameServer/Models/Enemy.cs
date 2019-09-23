using System;
using System.Threading;
namespace GameServer.Models
{
    class Enemy
    {
        public Position position { get; set; }
        private Game game;


        public Enemy(Game game, Position position)
        {
            this.game = game;
            this.position = position;
            sendMessage("Enemy spawned at [" + position.x + ", " + position.y + "]");
            enemyMove();
        }

        public void setPosition(Position position)
        {
            this.position = position;
            sendMessage("Enemy position changed [" + position.x + ", " + position.y + "]");
        }

        public void enemyMove()
        {
            new Thread(() =>
            {
                while (true)
                {
                    Thread.Sleep(100);
                    setPosition(position.addX(5));
                    Thread.Sleep(100);
                    setPosition(position.addY(5));
                    Thread.Sleep(100);
                    setPosition(position.addX(-5));
                    Thread.Sleep(100);
                    setPosition(position.addY(-5));
                }
            }).Start();
        }

        public void sendMessage(string message)
        {
            game.sendMessage(message);
        }
    }
}
