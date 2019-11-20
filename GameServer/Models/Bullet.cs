﻿using System;
using System.Threading;
using System.Drawing;
using Newtonsoft.Json;
using System.Collections.Generic;
using GameServer.Geometry;
using GameServer.Models.EnemyStates;

namespace GameServer.Models
{
    public class Bullet : ICloneable
    {
        //  private static long bulletIncremental = 1;

        //   private long id { get; set; }

        public Position position { get; set; }
        public Color color { get; set; }
        [JsonIgnore]
        public bool split = false;
        [JsonIgnore]
        public bool inner = false;
        [JsonIgnore]
        public bool fullyInside = false;
        [JsonIgnore]
        public Position direction;
        [JsonIgnore]
        public Mover mover;

        public Bullet()
        {

        }

        public Bullet(GamePlayer gamePlayer, Position direction, Mover mover)
        {
            //    id = bulletIncremental++;
            position = (Position)gamePlayer.position.Clone();
            color = gamePlayer.color;
            this.direction = direction;
            this.mover = mover;
        }

        public bool Fly()
        {
            Enemy hitEnemy = mover.enemyHit(this);
            if (hitEnemy!= null)
            {
                hitEnemy.state = new AngryEnemyState();
                return false;
            }
            Position delta = (Position)direction.Clone();
            delta.multiply(Config.BULLETSPEED);
            position.add(delta);

            Bounds Square = Bounds.MainSquare();
            Bounds bulletBound = new Bounds(position, Config.BULLETWIDTH);
            if (!Square.intersects(bulletBound))
            {

                return false;
            }

            Bounds innerSquare = Bounds.InnerSquare();
            if (!split)
            {
                if (innerSquare.intersects(bulletBound))
                {
                    split = true;

                    Console.WriteLine("cloning bullet");

                    //to front
                    mover.addNew(new BulletAdapter((new BulletSplitFront(this)).splitBullet()));

                    //to right
                    mover.addNew(new BulletAdapter((new BulletSplitRight(this)).splitBullet()));

                    //to left
                    mover.addNew(new BulletAdapter((new BulletSplitLeft(this)).splitBullet()));

                    inner = true;
                }
            }

            if(inner && innerSquare.inBounds(bulletBound))
            {
                fullyInside = true;
            }

            if (inner && fullyInside && !innerSquare.inBounds(bulletBound))
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
