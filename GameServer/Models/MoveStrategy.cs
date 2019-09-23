using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Models
{
    interface IMoveStrategy
    {
        void MoveRight(Position position);
        void MoveLeft(Position position);
    }

    class P1MoveStrategy : IMoveStrategy
    {
        public void MoveRight(Position position)
        {
            position.addX(Config.MOVESPEED);
        }
        public void MoveLeft(Position position)
        {
            position.subtractX(Config.MOVESPEED);
        }
    }

    class P2MoveStrategy : IMoveStrategy
    {
        public void MoveRight(Position position)
        {
            position.subtractY(Config.MOVESPEED);
        }
        public void MoveLeft(Position position)
        {
            position.addY(Config.MOVESPEED);
        }
    }

    class P3MoveStrategy : IMoveStrategy
    {
        public void MoveRight(Position position)
        {
            position.subtractX(Config.MOVESPEED);
        }
        public void MoveLeft(Position position)
        {
            position.addX(Config.MOVESPEED);
        }
    }

    class P4MoveStrategy : IMoveStrategy
    {
        public void MoveRight(Position position)
        {
            position.addY(Config.MOVESPEED);
        }
        public void MoveLeft(Position position)
        {
            position.subtractY(Config.MOVESPEED);
        }
    }
}
