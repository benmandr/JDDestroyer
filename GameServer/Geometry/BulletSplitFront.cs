using GameServer.Models;
namespace GameServer.Geometry
{
    sealed class BulletSplitFront : BulletSplit
    {
        public BulletSplitFront(Bullet bullet) : base(bullet)
        {

        }
        public override void adjustDirection()
        {
        }

        public override void adjustPosition()
        {
            bullet.position.invert();
        }
    }
}
