using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClient.GraphicItems
{
    class MainWindow : GraphicItem
    {
        Bitmap buffer { get; set; }
        public MainWindow(Bitmap buffer)
        {
            this.buffer = buffer;
        }

        public void Draw()
        {
            using (var g = Graphics.FromImage(buffer))
            {
                Console.WriteLine("draw bg");
                Rectangle main = new Rectangle(0, 0, Proportion.ClientSize(100), Proportion.windowWidth);
                g.FillRectangle(Brushes.WhiteSmoke, main);
            }
        }
    }
}
