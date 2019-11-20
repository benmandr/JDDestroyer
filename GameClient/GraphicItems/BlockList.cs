using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClient.GraphicItems
{
    class BlockList : GraphicItem
    {
        List<GraphicItem> blocks = new List<GraphicItem>();

        public void Add(GraphicItem item)
        {
            blocks.Add(item);
        }


        public void Draw()
        {
            foreach(GraphicItem item in blocks)
            {
                item.Draw();
            }
        }
    }
}
