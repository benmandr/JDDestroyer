﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Models.EnemyStates
{
    class StateChangeSmart : StateChange
    {
        protected override void InitiateState(IEnemy enemy)
        {
            enemy.state = new SmartEnemyState();
        }
    }
}
