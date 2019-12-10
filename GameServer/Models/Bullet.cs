using System;
using System.Threading;
using System.Drawing;
using Newtonsoft.Json;
using System.Collections.Generic;
using GameServer.Geometry;
using GameServer.Models.EnemyStates;

namespace GameServer.Models
{
    public class Bullet : ICloneable, IBullet
    {
        public Position position { get; set; }
        public Color color { get; set; }
        [JsonIgnore]
        public bool split = false;
        [JsonIgnore]
        public bool hitTooth = false;
        [JsonIgnore]
        public bool inner = false;
        [JsonIgnore]
        public bool fullyInside = false;
        [JsonIgnore]
        public Position direction;
        [JsonIgnore]
        public Mover mover;
        [JsonIgnore]
        GamePlayers gamePlayers = null;
        [JsonIgnore]
        GamePlayer shooter = null;

        public Bullet()
        {

        }

        public Bullet(GamePlayers gamePlayers, GamePlayer gamePlayer, Position direction, Mover mover)
        {
            this.gamePlayers = gamePlayers;
            this.shooter = gamePlayer;
            position = (Position)gamePlayer.position.Clone();
            color = gamePlayer.color;
            this.direction = direction;
            this.mover = mover;
        }

        public bool Fly()
        {
            IEnemy hitEnemy = mover.enemyHit(this);
            if (hitEnemy!= null)
            {
                StateChange stateChanger = (new StateChangeSmart()).setNext(new StateChangeAngry()).setNext(new StateChangeFrozen()).setNext(new StateChangeDefault());

                new Thread(() =>
                {
                    shooter.addScore(Config.ENEMYHITSCORE);
                    stateChanger.ChangeState(hitEnemy);

                }).Start();
                return false;
            }

            GoldenTooth tooth = mover.GetGoldenTooth();
            if (tooth != null)
            {
                Bounds toothBounds = new Bounds(tooth.position, Config.GOLDENTOOTHSIZE);
                Bounds bulletBounds = new Bounds(position, Config.BULLETWIDTH);
                if (toothBounds.Intersects(bulletBounds))
                {
                    shooter.addScore(Config.GOLDENTOOTHSCORE);
                    return false;
                }
            }

            if(gamePlayers != null)
            {
                foreach(GamePlayer gamePlayer in gamePlayers.getPlayers())
                {
                    if (gamePlayer.Equals(shooter))
                        continue;
                    Bounds playerBounds = new Bounds(gamePlayer.position, Config.PLAYERSIZE);
                    Bounds bulletBounds = new Bounds(position, Config.BULLETWIDTH);
                    if (playerBounds.Intersects(bulletBounds))
                    {
                        gamePlayer.removeScore(Config.HITCOST);
                        shooter.addScore(Config.PLAYERHITSCORE);
                        return false;
                    }
                }
            }

            Position delta = (Position)direction.Clone();
            delta.multiply(Config.BULLETSPEED);
            position.add(delta);

            Bounds Square = Bounds.MainSquare();
            Bounds bulletBound = new Bounds(position, Config.BULLETWIDTH);
            if (!Square.Intersects(bulletBound))
            {

                return false;
            }

            Bounds innerSquare = Bounds.InnerSquare();
            if (!split)
            {
                if (innerSquare.Intersects(bulletBound))
                {
                    split = true;
                    //to front
                    mover.addNew(new BulletAdapter((new BulletSplitFront(this)).splitBullet()));

                    //to right
                    mover.addNew(new BulletAdapter((new BulletSplitRight(this)).splitBullet()));

                    //to left
                    mover.addNew(new BulletAdapter((new BulletSplitLeft(this)).splitBullet()));

                    inner = true;
                }
            }

            if(inner && innerSquare.InBounds(bulletBound))
            {
                fullyInside = true;
            }

            if (inner && fullyInside && !innerSquare.InBounds(bulletBound))
            {
                return false;
            }

            return true;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public Bullet deepClone()
        {
            Bullet clone = (Bullet)Clone();
            clone.position = (Position)position.Clone();
            clone.direction = (Position)direction.Clone();


            return clone;
        }
    }
}
