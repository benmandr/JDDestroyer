using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Utils
{
    public class MoverIterator : IMovableIterator
    {
        public Mover mover;
        private int current = 0;
        private bool removed = false;
        public MoverIterator(Mover mover)
        {
            this.mover = mover;
        }

        public IMovable Current()
        {
            return mover[current];
        }

        public IMovable First()
        {
            if(mover.items.Count == 0)
                return null;
            current = 0;
            return mover[0];
        }

        public bool IsDone()
        {
            return current >= mover.items.Count;
        }

        public IMovable Next()
        {
            if(!removed)
                current++;
            removed = false;
            if (!IsDone())
                return mover[current];
            else
                return null;
        }

        public IMovable Remove()
        {
            if (IsDone())
            {
                return null;
            }

            IMovable item = Current();
            mover.items.RemoveAt(current);
            removed = true;
            return item;
        }
    }
}
