using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Models
{
    class EnemyFactory
    {
        private static Dictionary<int, Enemy> enemyStore = new Dictionary<int, Enemy>();
        private static EnemyFactory instance = new EnemyFactory();
    
        public EnemyFactory()
        {
            fillStore();
        }

        public static EnemyFactory getInstance()
        {
            return instance;
        }

        private static Position getRandomInMiddle() {
            Random random = new Random();
            double min = Config.CORNERSIZE + Config.ENEMYSIZE / 2;
            double max = Config.INNERSQUARESIZE + Config.CORNERSIZE - Config.ENEMYSIZE / 2;


            double x = random.NextDouble() * (max - min) + min;
            double y = random.NextDouble() * (max - min) + min;

            return new Position(x, y);
        }

        public Enemy getEnemy(int enemyType)
        {
            Position position = getRandomInMiddle();
            //Enemy clonedEnemy = (Enemy)enemyStore[enemyType].deepClone(); //Deep copy
            Enemy clonedEnemy = (Enemy)enemyStore[enemyType].Clone(); //Shallow copy
            clonedEnemy.setPosition(position);
            return clonedEnemy;
        }

        private void fillStore()
        {
            enemyStore.Add(RedEnemy.TYPE, new RedEnemy(getRandomInMiddle()));
            enemyStore.Add(GreenEnemy.TYPE, new GreenEnemy(getRandomInMiddle()));
            enemyStore.Add(BlueEnemy.TYPE, new BlueEnemy(getRandomInMiddle()));
        }
    }
}
