using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Utils
{
    public interface IMovableCollection
    {
        IMovableIterator createIterator();
    }
}
