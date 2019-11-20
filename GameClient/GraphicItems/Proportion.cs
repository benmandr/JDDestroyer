using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClient.GraphicItems
{
    class Proportion
    {
        public static int windowWidth { get; private set; }
        public static int ClientSize(double value)
        {
            return (int)(value * windowWidth / 100);
        }
    }
}
