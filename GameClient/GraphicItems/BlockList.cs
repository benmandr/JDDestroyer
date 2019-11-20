using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClient.GraphicItems
{
    class BlockList : GraphicItem
    {
        List<GraphicItem> blocks;

        public BlockList(List<GraphicItem> enemyBlocks)
        {
            this.blocks = enemyBlocks;
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
