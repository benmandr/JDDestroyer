using GameServer.Geometry;
using GameServer.Models;

namespace GameServer.Messages
{
    public class GoldenToothMessage
    {
        public const int TYPE = 10;
        public Position position { get; set; }

        public GoldenToothMessage() { }


        public GoldenToothMessage(GoldenTooth goldenTooth)
        {
            position = goldenTooth.position;
        }
    }
}
