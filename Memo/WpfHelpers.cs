using memoLibrary;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Memo
{
    public partial class MainWindow : Window
    {
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
            if (SP.PlayButton(i, AdjustButtonStatusWPF, UpdateImageSourceWPF, DisplayVictoryMessageWPF))
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
    }
}
