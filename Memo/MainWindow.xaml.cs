﻿using System;
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
using System.IO;

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
            new Card("Arrow",       @"pack://application:,,,/Images/Arrow.jpg" ),
            new Card("Arrow",       @"pack://application:,,,/Images/Arrow.jpg" ),
            new Card("Ball",        @"pack://application:,,,/Images/Ball.jpg"),
            new Card("Ball",        @"pack://application:,,,/Images/Ball.jpg"),
            new Card("Four-Star",   @"pack://application:,,,/Images/Four-Star.jpg"),
            new Card("Four-Star",   @"pack://application:,,,/Images/Four-Star.jpg"),
            new Card("Hearth",      @"pack://application:,,,/Images/Hearth.jpg"),
            new Card("Hearth",      @"pack://application:,,,/Images/Hearth.jpg"),
            new Card("Lightning",   @"pack://application:,,,/Images/Lightning.jpg"),
            new Card("Lightning",   @"pack://application:,,,/Images/Lightning.jpg"),
            new Card("Pentagon",    @"pack://application:,,,/Images/Pentagon.jpg"),
            new Card("Pentagon",    @"pack://application:,,,/Images/Pentagon.jpg"),
            new Card("Star",        @"pack://application:,,,/Images/Star.jpg"),
            new Card("Star",        @"pack://application:,,,/Images/Star.jpg"),
            new Card("Triangle",    @"pack://application:,,,/Images/Triangle.jpg"),
            new Card("Triangle",    @"pack://application:,,,/Images/Triangle.jpg"),
        };
        BitmapImage tmp = new BitmapImage(new Uri(@"pack://application:,,,/Images/Template.jpg"));
        int moveCounter = 0;
        int playerOneScore = 0;
        int playerTwoScore = 0;
        int counter = 0;
        int generatorTMP = 16;
        string cur = String.Empty;
        string prev = String.Empty;
        enum GameModeSettings { SP, MP, AI };
        GameModeSettings GameMode = GameModeSettings.AI;   // Add GUI for Mode Selection
        int player = 1;


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
            string path = Directory.GetCurrentDirectory();
            if (GameMode == GameModeSettings.SP)
            {
                right_one_lbl.Content = "Moves: ";
                right_two_lbl.Content = moveCounter;
            }
            else
            {
                left_one_lbl.Content = "Now Playing:";
                left_two_lbl.Content = player;

                mid_one_lbl.Content = "P1 Score: ";
                mid_two_lbl.Content = playerOneScore;

                right_one_lbl.Content = "P2 Score: ";
                right_two_lbl.Content = playerTwoScore;
            }
        }

        void Reset()
        {
            for (int i = 0; i < Images.Count; i++)
            {
                if (Cards[i].discovered)
                {
                    Images[i].Source = Cards[i].img;
                }
                else if (Cards[i].discovered_p1)
                {
                    Images[i].Source = Cards[i].img_p1;
                }
                else if (Cards[i].discovered_p2)
                {
                    Images[i].Source = Cards[i].img_p2;
                }
                else
                {
                    Images[i].Source = tmp;
                }
            }
        }
        bool Victory()
        {
            int victoryCounter = 0;
            int scoreP1 = 0;
            int scoreP2 = 0;
            foreach (var item in Cards)
            {
                if (item.discovered)
                {
                    victoryCounter++;
                }
                else if (item.discovered_p1)
                {
                    victoryCounter++;
                    scoreP1++;
                }
                else if (item.discovered_p2)
                {
                    victoryCounter++;
                    scoreP2++;
                }
            }
            if (victoryCounter == 16)
            {
                if (scoreP1 > scoreP2) MessageBox.Show("Congrats player one, you have WON");
                else if (scoreP2 > scoreP1) MessageBox.Show("Congrats player two, you have WON");
                else
                {
                    if (GameMode == GameModeSettings.SP) MessageBox.Show("Congratz, you have won");
                    else MessageBox.Show("Contrangts, a draw");
                }
                ResetGame();
                if (GameMode == GameModeSettings.SP)
                {
                    moveCounter = 0;
                    right_two_lbl.Content = moveCounter;
                }
                else
                {
                    playerOneScore = 0;
                    playerTwoScore = 0;
                    mid_two_lbl.Content = playerOneScore;
                    right_two_lbl.Content = playerTwoScore;
                }
                return true;
            }
            return false;
        }
        void ResetGame()
        {
            foreach (var item in Cards) 
            {
                item.discovered = false;
                item.discovered_p1 = false;
                item.discovered_p2 = false;
            }
            moveCounter = 0;
            Cards.Shuffle<Card>();
            Reset();
        }
        void PressButton(int i)
        {
            if (GameMode == GameModeSettings.SP) SPButton(i);
            else if (GameMode == GameModeSettings.MP) MPButton(i);
            else if (GameMode == GameModeSettings.AI) AIButton(i);
        }
        void SPButton(int i)
        {
            counter++;
            if (counter == 3)
            {
                counter = 1;
                Reset();
            }
            Images[i].Source = Cards[i].img_highlight;
            cur = Cards[i].name;
            if (counter == 2)
            {
                if (cur == prev)
                {
                    foreach (var item in Cards)
                    {
                        if (cur == item.name)
                        {
                            item.discovered = true;
                            Debug.WriteLine("Discovered a pair");
                        }
                    }
                }
                moveCounter++;
                right_two_lbl.Content = moveCounter;
            }
            prev = cur;
            Victory();
        }
        void MPButton(int i)
        {
            counter++;
            if (counter == 3)
            {
                counter = 1;
                Reset();
            }
            Images[i].Source = Cards[i].img_highlight;
            cur = Cards[i].name;
            if (counter == 2)
            {
                if (cur == prev)
                {
                    foreach (var item in Cards)
                    {
                        if (cur == item.name)
                        {
                            if (player == 1) // if player == 0 consider this SP?
                            {
                                item.discovered_p1 = true;
                                playerOneScore++;
                                mid_two_lbl.Content = playerOneScore;
                                Reset();
                            }
                            else if (player == 2)
                            {
                                item.discovered_p2 = true;
                                playerTwoScore++;
                                right_two_lbl.Content = playerTwoScore;
                                Reset();
                            }
                        }
                    }
                }
                player = (player == 1) ? 2 : 1;
                left_two_lbl.Content = player;
            }
            prev = cur;
            Victory();
        }
        void AIButton(int i) 
        {
            counter++;
            Images[i].Source = Cards[i].img_highlight; 
            cur = Cards[i].name;
            if (counter == 2)
            {
                if (cur == prev)
                {
                    foreach (var item in Cards)
                    {
                        if (cur == item.name)
                        {
                         item.discovered_p1 = true;
                         playerOneScore++;
                         mid_two_lbl.Content = playerOneScore;
                         Reset();
                        }
                    }
                }
                counter = 0;
                if (Victory()) return; 
                player = 2;
                left_two_lbl.Content = player;
                MessageBox.Show("Press OK to ::begin:: AI turn", "Need a more elegant solution");
                Reset();
                AIPressButton(AIPlaysRandom());
                AIPressButton(AIPlaysRandom());
                counter = 0;
                player = 1;
                left_two_lbl.Content = player;
                if (Victory()) return;
                MessageBox.Show("Press OK to ::end:: AI turn", "Need a more elegant solution");
                Reset();


            }
            prev = cur;
        }
        void AIPressButton(int i)
        {
            counter++;
            if (counter == 3)
            {
                counter = 1;
                Reset();
            }
            Images[i].Source = Cards[i].img_highlight;
            cur = Cards[i].name;
            if (counter == 2)
            {
                if (cur == prev)
                {
                    foreach (var item in Cards)
                    {
                        if (cur == item.name)
                        {
                         item.discovered_p2 = true;
                         playerTwoScore++;
                         right_two_lbl.Content = playerTwoScore;
                         Reset();
                        }
                    }
                }
            }
            prev = cur;
            Victory();
        }
        int AIPlaysRandom()
        {
            List<int> tmpList = new List<int>();

            for (int i = 0; i < 16; i++)
            {
                if (!(Cards[i].discovered || Cards[i].discovered_p1 || Cards[i].discovered_p2))
                {
                    tmpList.Add(i);
                }
            }           
            if(generatorTMP < 16) tmpList.Remove(generatorTMP);
            Random random = new Random();
            System.Threading.Thread.Sleep(100);
            int returnVal = tmpList[random.Next(tmpList.Count)];
            generatorTMP = returnVal;
            return returnVal;
        }
        void TestRandom()
        {
            for (int i = 0; i < 10; i++) // od 0 do 9 Carty sa odkryte, ich nie powinno losowac
            {
                Cards[i].discovered = true;
            }
            Debug.WriteLine("Generating Using method");
            for (int i = 0; i < 100; i++)
            {
                System.Threading.Thread.Sleep(10); // prevents from generating the same number
                Debug.WriteLine(i + ":                            " + AIPlaysRandom() + Environment.NewLine);
            }
        }

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
    }
}
