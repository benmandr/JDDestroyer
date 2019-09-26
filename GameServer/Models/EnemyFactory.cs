using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Models
{
    class EnemyFactory
    {
        private static EnemyFactory instance = new EnemyFactory();

        public static EnemyFactory getInstance()
        {
            return instance;
        }

        private static Position getRandomInMiddle() {
            Random random = new Random();
            double x = random.NextDouble() * (Config.CORNERSIZE - Config.CORNERSIZE + Config.INNERSQUARESIZE) + Config.CORNERSIZE;
            double y = random.NextDouble() * (Config.CORNERSIZE - Config.CORNERSIZE + Config.INNERSQUARESIZE) + Config.CORNERSIZE;

            return new Position(x, y);
        }

        public Enemy getEnemy(int enemyType)
        {
            Position position = getRandomInMiddle();
            switch (enemyType)
            {
                case RedEnemy.TYPE:
                    return new RedEnemy(position);  //@todo
                case GreenEnemy.TYPE:
                    return new GreenEnemy(position);  //@todo
                case BlueEnemy.TYPE:
                    return new BlueEnemy(position);  //@todo
            }
            return null;
        }
    }
}
