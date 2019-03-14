using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using memoLibrary;

namespace Memo
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Image> Images;
        List<Button> Buttons;
        Image dot;

        SinglePlayer SP;


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
            Buttons = new List<Button>()
            {
                Button_1x1, Button_1x2, Button_1x3, Button_1x4,
                Button_2x1, Button_2x2, Button_2x3, Button_2x4,
                Button_3x1, Button_3x2, Button_3x3, Button_3x4,
                Button_4x1, Button_4x2, Button_4x3, Button_4x4,
            };

            dot = new Image
            {
                Source = new BitmapImage(new Uri(@"pack://application:,,,/Images/dot.png"))
            };

            NewGame(AiPlayer.AiDifficulties.Easy, GameModesProcessor.Setting);
        }





    }
}

