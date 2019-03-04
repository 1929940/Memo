using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Memo
{
    public class Card // Perhaps I should make another card class, a child of this, for player 1 and 2.
    {
        public string name;
        public BitmapImage img_highlight;
        public BitmapImage img_p1;
        public BitmapImage img_p2;

        public bool discovered_p1 = false;
        public bool discovered_p2 = false;

        public Card(string name, string path)
        {
            this.name = name;
            img_highlight = new BitmapImage(new Uri(path.ExtendPath("_highlight")));
            img_p1 = new BitmapImage(new Uri(path.ExtendPath("_p1")));
            img_p2 = new BitmapImage(new Uri(path.ExtendPath("_p2")));
        }
    }
}
