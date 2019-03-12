using System;
using System.Windows.Media.Imaging;

namespace memoLibrary
{

    public class Card
    {
        public string Name { get; set; }
        public BitmapImage Image_Highlight { get; set; }
        public BitmapImage Image_Player1 { get; set; }
        public BitmapImage Image_Player2 { get; set; }
        public string Image_Path { get; set; }
        public string Image_Path_Player1 { get; set; }
        public string Image_Path_Player2 { get; set; }


        public bool Discover_Player1 { get; set; }
        public bool Discover_Player2 { get; set; }

        public Card(string name, string path)
        {
            Name = name;
            Image_Path = path;
            Image_Path_Player1 = path.ExtendPath("_p1");
            Image_Path_Player2 = path.ExtendPath("_p2");

            Image_Highlight = new BitmapImage(new Uri(path.ExtendPath("_highlight")));
            Image_Player1 = new BitmapImage(new Uri(path.ExtendPath("_p1")));
            Image_Player2 = new BitmapImage(new Uri(path.ExtendPath("_p2")));
        }
    }
}
