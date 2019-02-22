using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Memo
{
    class Card
    {
        public string name;
        public BitmapImage img;
        public BitmapImage img_highlight;

        public bool discovered = false;

        public Card(string name, string path)
        {
            this.name = name;
            img = new BitmapImage(new Uri(path));
            img_highlight = new BitmapImage(new Uri(path.ExtendPath("_highlight")));
        }
    }
}
