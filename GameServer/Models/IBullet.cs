using GameServer.Geometry;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Models
{
    public interface IBullet
    {
        Position position { get; set; }

        Color color { get; set; }

        bool Fly();

        object Clone();

        Bullet deepClone();

    }
}
