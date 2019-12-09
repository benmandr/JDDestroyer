using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameServer.Geometry;
using System.Reflection;

namespace GameServer.Models.EnemyStates
{
    class AngryEnemyState : EnemyState
    {
        private static Random randomInstance = new Random();

        private static string[] moves = { "subtractX", "addX", "subtractY", "addY" };
        long startState = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;

        public bool Walk(Enemy context)
        {
            long currentTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            if (currentTime - context.lastMoveTime > Config.ENEMYMOVERATE/2) //Remove *3
            {
                context.lastMoveTime = currentTime;
                int x = 1;
                for (int i = randomInstance.Next(0, moves.Length); x < moves.Length; i = (i + 1) % moves.Length)
                {
                    Position copiedPosition = new Position(context.position.x, context.position.y);
                    MethodInfo moveCopy = copiedPosition.GetType().GetMethod(moves[i]);
                    copiedPosition = (Position)moveCopy.Invoke(copiedPosition, new object[] { Config.ENEMYMOVESPEED*2 });
                    Bounds enemyBounds = new Bounds(copiedPosition, Config.ENEMYSIZE);
                    if (Bounds.InnerSquare().InBounds(enemyBounds))
                    {
                        context.position = copiedPosition;
                    }
                    x++;
                }
            }

            return true;
        }
    }
}
