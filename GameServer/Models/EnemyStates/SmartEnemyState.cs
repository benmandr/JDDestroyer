using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameServer.Geometry;
using System.Reflection;

namespace GameServer.Models.EnemyStates
{
    class SmartEnemyState : EnemyState
    {
        private static Random randomInstance = new Random();
        private static Position[] directionList = {
            Position.DirectionRight(),
            Position.DirectionBottom(),
            Position.DirectionRight(),
            Position.DirectionBottom(),
            Position.DirectionRight(),
            Position.DirectionBottom(),
            Position.DirectionRight(),
            Position.DirectionBottom(),
            Position.DirectionLeft(),
            Position.DirectionBottom(),
            Position.DirectionLeft(),
            Position.DirectionBottom(),
            Position.DirectionLeft(),
            Position.DirectionBottom(),
            Position.DirectionLeft(),
            Position.DirectionBottom(),
            Position.DirectionLeft(),
            Position.DirectionTop(),
            Position.DirectionLeft(),
            Position.DirectionTop(),
            Position.DirectionLeft(),
            Position.DirectionTop(),
            Position.DirectionLeft(),
            Position.DirectionTop(),
            Position.DirectionRight(),
            Position.DirectionTop(),
            Position.DirectionRight(),
            Position.DirectionTop(),
            Position.DirectionRight(),
            Position.DirectionTop(),
            Position.DirectionRight(),
            Position.DirectionTop()
        };

        public SmartEnemyState()
        {
            nextStep = randomInstance.Next(0, directionList.Length - 1);
        }
        private int nextStep;

        public bool Walk(Enemy context)
        {
            long currentTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            if (currentTime - context.lastMoveTime > Config.ENEMYMOVERATE)
            {
                Position copiedPosition = (Position)context.position.Clone();
                Position movePos = (Position)directionList[nextStep].Clone();
                movePos.multiply(Config.ENEMYMOVESPEED * 2);
                copiedPosition.add(movePos);

                Bounds enemyBounds = new Bounds(copiedPosition, Config.ENEMYSIZE);
                if (!Bounds.InnerSquare().InBounds(enemyBounds))
                {
                    movePos.negative();
                    context.position.add(movePos);
                } else
                {
                    context.position = copiedPosition;
                }
                context.lastMoveTime = currentTime;
                nextStep++;
                if (nextStep >= directionList.Length)
                {
                    nextStep = 0;
                }
            }
            return true;
        }
    }
}
