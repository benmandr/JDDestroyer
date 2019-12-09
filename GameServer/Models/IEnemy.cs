using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using GameServer.Geometry;
using Newtonsoft.Json;

namespace GameServer.Models
{

    public interface IEnemy
    {
        Position position { get; set; }
        [JsonIgnore]
        EnemyStates.EnemyState state { get; set; }
        long lastMoveTime { get; set; }

        int getType { get; }

        Color getColor();

        void setPosition(Position position);

        bool Walk();

        object Clone();

        object deepClone();
    }
}
