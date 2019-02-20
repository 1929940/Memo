using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Memo
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Image> Images;
        List<Card> Cards = new List<Card>
        {
            new Card("Arrow",@"C:\Users\Użytkownik\source\repos\Memo\Memo\Resources\Icons\Arrow.jpg" ),
            new Card("Arrow",@"C:\Users\Użytkownik\source\repos\Memo\Memo\Resources\Icons\Arrow.jpg" ),
            new Card("Ball",@"C:\Users\Użytkownik\source\repos\Memo\Memo\Resources\Icons\Ball.jpg"),
            new Card("Ball",@"C:\Users\Użytkownik\source\repos\Memo\Memo\Resources\Icons\Ball.jpg"),
            new Card("Four-Star",@"C:\Users\Użytkownik\source\repos\Memo\Memo\Resources\Icons\Four-Star.jpg"),
            new Card("Four-Star",@"C:\Users\Użytkownik\source\repos\Memo\Memo\Resources\Icons\Four-Star.jpg"),
            new Card("Hexa",@"C:\Users\Użytkownik\source\repos\Memo\Memo\Resources\Icons\Hexa.jpg"),
            new Card("Hexa",@"C:\Users\Użytkownik\source\repos\Memo\Memo\Resources\Icons\Hexa.jpg"),
            new Card("Lightning",@"C:\Users\Użytkownik\source\repos\Memo\Memo\Resources\Icons\Lightning.jpg"),
            new Card("Lightning",@"C:\Users\Użytkownik\source\repos\Memo\Memo\Resources\Icons\Lightning.jpg"),
            new Card("Pentagon",@"C:\Users\Użytkownik\source\repos\Memo\Memo\Resources\Icons\Pentagon.jpg"),
            new Card("Pentagon",@"C:\Users\Użytkownik\source\repos\Memo\Memo\Resources\Icons\Pentagon.jpg"),
            new Card("Star",@"C:\Users\Użytkownik\source\repos\Memo\Memo\Resources\Icons\Star.jpg"),
            new Card("Star",@"C:\Users\Użytkownik\source\repos\Memo\Memo\Resources\Icons\Star.jpg"),
            new Card("Triangle",@"C:\Users\Użytkownik\source\repos\Memo\Memo\Resources\Icons\Triangle.jpg"),
            new Card("Triangle",@"C:\Users\Użytkownik\source\repos\Memo\Memo\Resources\Icons\Triangle.jpg"),
        };
        BitmapImage tmp = new BitmapImage(new Uri(@"C:\Users\Użytkownik\source\repos\Memo\Memo\Resources\Icons\Template.jpg"));
        int counter = 0;
        string cur = String.Empty;
        string prev = String.Empty;


        public MainWindow()
        {
            InitializeComponent();
            Images = new List<Image>(16)
            {
                Image_1x1,Image_1x2,Image_1x3,Image_1x4,
                Image_2x1,Image_2x2,Image_2x3,Image_2x4,
                Image_3x1,Image_3x2,Image_3x3,Image_3x4,
                Image_4x1,Image_4x2,Image_4x3,Image_4x4,
            };
            Cards.Shuffle<Card>();
            Reset();
        }

        void Reset()
        {
            for (int i = 0; i < Images.Count; i++)
            {
                if (Cards[i].discovered)
                {
                    Images[i].Source = Cards[i].img;
                }
                else
                {
                    Images[i].Source = tmp;
                }
            }
        }
        void Victory()
        {
            int victoryCounter = 0;
            foreach (var item in Cards)
            {
                if (item.discovered)
                    victoryCounter++;
            }
            if (victoryCounter == 16)
            {
                MessageBox.Show("Contrangts, you have won");
                ResetGame();
            }

        }
        void ResetGame()
        {
            foreach (var item in Cards) 
            {
                item.discovered = false;
            }
            Cards.Shuffle<Card>();
            Reset();
        }
        void PressButton(int i)
        {
            counter++;
            if (counter == 3)
            {
                counter = 1;
                Reset();
            }
            Images[i].Source = Cards[i].img;
            cur = Cards[i].name;
            if (counter == 2)
            {
                if (cur == prev)
                {
                    foreach (var item in Cards)
                    {
                        if (cur == item.name)
                            item.discovered = true;
                    }
                }
            }
            prev = cur;
            Victory();
        }


        private void Button_1x1_Click(object sender, RoutedEventArgs e)
        {
            PressButton(0);
        }

        private void Button_1x2_Click(object sender, RoutedEventArgs e)
        {
            PressButton(1);
        }

        private void Button_1x3_Click(object sender, RoutedEventArgs e)
        {
            PressButton(2);
        }

        private void Button_1x4_Click(object sender, RoutedEventArgs e)
        {
            PressButton(3);
        }

        private void Button_2x1_Click(object sender, RoutedEventArgs e)
        {
            PressButton(4);
        }

        private void Button_2x2_Click(object sender, RoutedEventArgs e)
        {
            PressButton(5);
        }

        private void Button_2x3_Click(object sender, RoutedEventArgs e)
        {
            PressButton(6);
        }

        private void Button_2x4_Click(object sender, RoutedEventArgs e)
        {
            PressButton(7);
        }

        private void Button_3x1_Click(object sender, RoutedEventArgs e)
        {
            PressButton(8);
        }

        private void Button_3x2_Click(object sender, RoutedEventArgs e)
        {
            PressButton(9);
        }

        private void Button_3x3_Click(object sender, RoutedEventArgs e)
        {
            PressButton(10);
        }

        private void Button_3x4_Click(object sender, RoutedEventArgs e)
        {
            PressButton(11);
        }

        private void Button_4x1_Click(object sender, RoutedEventArgs e)
        {
            PressButton(12);
        }

        private void Button_4x2_Click(object sender, RoutedEventArgs e)
        {
            PressButton(13);
        }

        private void Button_4x3_Click(object sender, RoutedEventArgs e)
        {
            PressButton(14);
        }

        private void Button_4x4_Click(object sender, RoutedEventArgs e)
        {
            PressButton(15);
        }
    }
}
