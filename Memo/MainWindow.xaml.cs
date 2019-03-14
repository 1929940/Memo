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

        #region Delegate Methods
        private void AdjustButtonStatusWPF(int i, bool flag)
        {
            Buttons[i].IsEnabled = flag;
        }
        private void UpdateImageSourceWPF(int i, string path)
        {
            Images[i].Source = new BitmapImage(new Uri(path));
        }
        private void DisplayVictoryMessageWPF(string message)
        {
            MessageBox.Show(message);
        }
        public void Print(string msg)
        {
            MessageBox.Show(msg);
        }
        #endregion


        private void NewGame(AiPlayer.AiDifficulties difficulty, GameModesProcessor.Modes modes)
        {
            Image empty = null;

            AIEasy.Icon = (difficulty == AiPlayer.AiDifficulties.Easy) ? dot : empty;
            AIMedium.Icon = (difficulty == AiPlayer.AiDifficulties.Moderate) ? dot : empty;
            AIHard.Icon = (difficulty == AiPlayer.AiDifficulties.Hard) ? dot : empty;

            GameModesProcessor.Setting = modes;
            SP = GameModesProcessor.CreateGame(AdjustButtonStatusWPF, UpdateImageSourceWPF);
            ConfigureDisplayWPF();
            if (SP is AiPlayer)
            {
                (SP as AiPlayer).WriteMessageEvent += Print;
                (SP as AiPlayer).ChangeAiDifficulty(AiPlayer.AiDifficulties.Hard);
            }
        }

        public void Binding(Label labelName, string propName)
        {
            Binding myBinding = new Binding();
            myBinding.Source = SP;
            myBinding.Path = new PropertyPath(propName);
            myBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            BindingOperations.SetBinding(labelName, Label.ContentProperty, myBinding);
        }

        private void ConfigureDisplayWPF()
        {
            if (GameModesProcessor.Setting == GameModesProcessor.Modes.SP)
            {
                left_one_lbl.Content = String.Empty;
                left_two_lbl.Content = String.Empty;

                mid_one_lbl.Content = String.Empty;
                mid_two_lbl.Content = String.Empty;

                right_one_lbl.Content = "Moves: ";
                Binding(right_two_lbl, "MoveCounter");

                VsNone.Icon = dot;
                VsAnother.Icon = "";
                VsAI.Icon = "";
            }
            else if (GameModesProcessor.Setting == GameModesProcessor.Modes.MP)
            {
                left_one_lbl.Content = "Now Playing:";
                Binding(left_two_lbl, "Player");

                mid_one_lbl.Content = "P1 Score: ";
                Binding(mid_two_lbl, "PlayerOneScore");

                right_one_lbl.Content = "P2 Score: ";
                Binding(right_two_lbl, "PlayerTwoScore");

                VsNone.Icon = "";
                VsAnother.Icon = dot;
                VsAI.Icon = "";
            }
            else
            {
                left_one_lbl.Content = "Now Playing:";
                Binding(left_two_lbl, "Player");

                mid_one_lbl.Content = "P1 Score: ";
                Binding(mid_two_lbl, "PlayerOneScore");

                right_one_lbl.Content = "P2 Score: ";
                Binding(right_two_lbl, "PlayerTwoScore");

                VsNone.Icon = "";
                VsAnother.Icon = "";
                VsAI.Icon = dot;
            }
        }


        void PressButton(int i)
        {
             if (SP.PlayButton(i,AdjustButtonStatusWPF, UpdateImageSourceWPF, DisplayVictoryMessageWPF))
            {
                NewGame(AiPlayer.AiDifficulties.Easy, GameModesProcessor.Setting);
            }
        }

        // Debuging Methods
        //void TestRandom()
        //{
        //    for (int i = 0; i < 10; i++) // od 0 do 9 Carty sa odkryte, ich nie powinno losowac
        //    {
        //        Cards[i].discovered_p1 = true;
        //    }
        //    for (int i = 0; i < 100; i++)
        //    {
        //        System.Threading.Thread.Sleep(10); // prevents from generating the same number
        //    }
        //}
        //void DisplayerAML()
        //{
        //    Debug.Write("AML contains: ");
        //    foreach (var item in ArtificialMemoryList)
        //    {
        //        Debug.Write(item.ToString() + ", ");
        //    }
        //    Debug.WriteLine("");
        //    foreach (var item in ArtificialMemoryList)
        //    {
        //        Debug.Write(Cards[item].name + ", ");
        //    }
        //    Debug.WriteLine("");
        //}
        #region Buttons
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
        #endregion

        #region MenuEvents
        private void MenuNewGame_Click(object sender, RoutedEventArgs e)
        {
            NewGame(AiPlayer.AiDifficulties.Easy, GameModesProcessor.Setting);
        }

        private void MenuQuitGame_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void VsNone_Click(object sender, RoutedEventArgs e)
        {
            NewGame(AiPlayer.AiDifficulties.Easy, GameModesProcessor.Modes.SP);
        }

        private void VsAnother_Click(object sender, RoutedEventArgs e)
        {
            NewGame(AiPlayer.AiDifficulties.Easy, GameModesProcessor.Modes.MP);
        }

        private void VsAI_Click(object sender, RoutedEventArgs e)
        {
            NewGame(AiPlayer.AiDifficulties.Easy, GameModesProcessor.Modes.AI);
        }

        private void AIEasy_Click(object sender, RoutedEventArgs e)
        {
            NewGame(AiPlayer.AiDifficulties.Easy, GameModesProcessor.Modes.AI);
        }

        private void AIMedium_Click(object sender, RoutedEventArgs e)
        {
            NewGame(AiPlayer.AiDifficulties.Moderate, GameModesProcessor.Modes.AI);
        }

        private void AIHard_Click(object sender, RoutedEventArgs e)
        {
            NewGame(AiPlayer.AiDifficulties.Hard, GameModesProcessor.Modes.AI);
        }
        #endregion
    }
}

