using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameServer.Geometry;
namespace GameServer.Models
{
    public abstract class MoveCommand
    {
        public IMoveStrategy strategy { get; set; }
        public Position position { get; set; }

        public MoveCommand(IMoveStrategy strategy, Position position)
        {
            this.strategy = strategy;
            this.position = position;
        }

        abstract public void Execute();
        abstract public void Undo();
    }

    public class MoveLeftCommand : MoveCommand
    {
        public override void Execute()
        {
            strategy.MoveLeft(position);
        }

        public override void Undo()
        {
            strategy.MoveRight(position);
        }

        public MoveLeftCommand(IMoveStrategy strategy, Position position) : base(strategy, position)
        {

        }
    }

    public class MoveRightCommand : MoveCommand
    {
        public override void Execute()
        {
            strategy.MoveRight(position);
        }
        public override void Undo()
        {
            strategy.MoveLeft(position);
        }

        public MoveRightCommand(IMoveStrategy strategy, Position position) : base(strategy, position)
        {

        }
    }
}
