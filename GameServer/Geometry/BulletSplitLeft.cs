using GameServer.Models;
namespace GameServer.Geometry
{
    sealed class BulletSplitLeft : BulletSplit
    {
        public BulletSplitLeft(Bullet bullet) : base(bullet)
        {

        }
        public override void adjustDirection()
        {
            bullet.direction.swap();
        }

        public override void adjustPosition()
        {
            bullet.position.swap();
            bullet.position.invert();
        }
    }
}
