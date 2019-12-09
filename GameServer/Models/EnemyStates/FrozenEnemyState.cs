﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameServer.Geometry;
using System.Reflection;

namespace GameServer.Models.EnemyStates
{
    class FrozenEnemyState : EnemyState
    {
        public bool Walk(Enemy context)
        {
            return true;
        }
    }
}
