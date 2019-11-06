using System;
using System.Threading;
using System.Drawing;
using Newtonsoft.Json;
using System.Collections.Generic;
using GameServer.Geometry;

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
                    Bullet front = deepClone();
                    front.position.invert();
                    mover.addNew(new BulletAdapter(front));

                    //to right
                    Bullet right = deepClone();
                    right.position.swap();
                    right.direction.swap();
                    right.direction.negative();
                    mover.addNew(new BulletAdapter(right));
                    //to left
                    Bullet left = deepClone();
                    left.position.swap();
                    left.position.invert();
                    left.direction.swap();
                    mover.addNew(new BulletAdapter(left));


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
