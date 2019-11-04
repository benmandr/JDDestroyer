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
        public P1MoveStrategy() {

        }
        public void MoveRight(Position position)
        {
            position.addX(Config.MOVESPEED);
            if (Position.PlayerOutOfBounds(position))
            {
                position.subtractX(Config.MOVESPEED);
            }
        }
        public void MoveLeft(Position position)
        {
            position.subtractX(Config.MOVESPEED);
            if (Position.PlayerOutOfBounds(position))
            {
                position.addX(Config.MOVESPEED);
            }
        }

    }

    public class P2MoveStrategy : IMoveStrategy
    {
        public P2MoveStrategy()
        {
        }
        public void MoveRight(Position position)
        {
            position.subtractY(Config.MOVESPEED);
            if (Position.PlayerOutOfBounds(position))
            {
                position.addY(Config.MOVESPEED);
            }
        }
        public void MoveLeft(Position position)
        {
            position.addY(Config.MOVESPEED);
            if (Position.PlayerOutOfBounds(position))
            {
                position.subtractY(Config.MOVESPEED);
            }
        }
    }

    public class P3MoveStrategy : IMoveStrategy
    {
        public P3MoveStrategy()
        {
        }
        public void MoveRight(Position position)
        {
            position.subtractX(Config.MOVESPEED);
            if (Position.PlayerOutOfBounds(position))
            {
                position.addX(Config.MOVESPEED);
            }
        }
        public void MoveLeft(Position position)
        {
            position.addX(Config.MOVESPEED);
            if (Position.PlayerOutOfBounds(position))
            {
                position.subtractX(Config.MOVESPEED);
            }
        }
    }

    public class P4MoveStrategy : IMoveStrategy
    {
        public P4MoveStrategy()
        {
        }
        public void MoveRight(Position position)
        {
            position.addY(Config.MOVESPEED);
            if (Position.PlayerOutOfBounds(position))
            {
                position.subtractY(Config.MOVESPEED);
            }
        }
        public void MoveLeft(Position position)
        {
            position.subtractY(Config.MOVESPEED);
            if (Position.PlayerOutOfBounds(position))
            {
                position.addY(Config.MOVESPEED);
            }
        }
    }
}
