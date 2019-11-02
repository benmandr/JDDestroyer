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
        private static Map<int, Enemy> enemyMap
        public static EnemyFactory getInstance()
        {
            return instance;
        }

        private static Position getRandomInMiddle() {
            Random random = new Random();
            double min = Config.CORNERSIZE + Config.ENEMYSIZE / 2;
            double max = Config.INNERSQUARESIZE + Config.CORNERSIZE - Config.ENEMYSIZE/2;


            double x = random.NextDouble() * (max - min) + min;
            double y = random.NextDouble() * (max - min) + min;

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
