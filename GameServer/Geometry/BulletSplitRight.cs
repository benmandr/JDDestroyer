using GameServer.Models;
namespace GameServer.Geometry
{
    sealed class BulletSplitRight : BulletSplit
    {
        public BulletSplitRight(Bullet bullet) : base(bullet)
        {

        }
        public override void adjustDirection()
        {
            bullet.direction.swap();
            bullet.direction.negative();
        }

        public override void adjustPosition()
        {
            bullet.position.swap();
        }
    }
}
