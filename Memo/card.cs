using System;
using System.Collections.Generic;
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
        public bool discovered = false;

        public Card(string name, string path)
        {
            this.name = name;
            this.img = new BitmapImage(new Uri(path));
        }
    }
}
