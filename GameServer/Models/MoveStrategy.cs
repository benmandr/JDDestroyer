using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Models
{
    public interface IMoveStrategy
    {
        void MoveRight(Position position);
        void MoveLeft(Position position);
    }

    public class P1MoveStrategy : IMoveStrategy
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

    public class P2MoveStrategy : IMoveStrategy
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

    public class P3MoveStrategy : IMoveStrategy
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

    public class P4MoveStrategy : IMoveStrategy
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
