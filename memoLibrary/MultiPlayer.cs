using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace memoLibrary
{
    public class MultiPlayer : SinglePlayer
    {
        // I should not be implementing this again
        public override event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };


        private int _Player = 1;

        public int Player
        {
            get { return _Player; }
            set
            {
                _Player = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Player"));
            }
        }

        private int _PlayerOneScore;

        public int PlayerOneScore
        {
            get { return _PlayerOneScore; }
            set
            {
                _PlayerOneScore = value;
                PropertyChanged(this, new PropertyChangedEventArgs("PlayerOneScore"));
            }
        }

        private int _PlayerTwoScore;

        public int PlayerTwoScore
        {
            get { return _PlayerTwoScore; }
            set
            {
                _PlayerTwoScore = value;
                PropertyChanged(this, new PropertyChangedEventArgs("PlayerTwoScore"));
            }
        }

        public MultiPlayer(AdjustButtonStatus adjustButtonStatus, UpdateImageSource updateImageSource) : base(adjustButtonStatus, updateImageSource)
        {
        }

        public override bool PlayButton(int i, AdjustButtonStatus adjustButtonStatus,
            UpdateImageSource updateImageSource,
            DisplayVictoryMessage displayVictoryMessage)
        {
            Counter++;
            if (Counter == 3)
            {
                Counter = 1;
                Reset(updateImageSource);
            }
            updateImageSource(i, Cards[i].Image_Path);
            CurrentCard = Cards[i].Name;
            adjustButtonStatus(i, false);
            if (Counter == 2)
            {
                if (CurrentCard == PreviousCard)
                {
                    foreach (var item in Cards)
                    {
                        if (CurrentCard == item.Name)
                        {
                            if (Player == 1)
                            {
                                item.Discover_Player1 = true;
                                PlayerOneScore++;
                            }
                            else
                            {
                                item.Discover_Player2 = true;
                                PlayerTwoScore++;
                            }
                            Reset(updateImageSource);
                        }
                    }
                    if (Victory(displayVictoryMessage))
                        return true;
                }
                Player = (Player == 1) ? 2 : 1;
                EnableUndiscoveredButtons(adjustButtonStatus);
            }
            PreviousCard = CurrentCard;
            return false;
        }


        public override void Reset(UpdateImageSource updateImageSource)
        {
            for (int i = 0; i < Cards.Count; i++)
            {
                if (Cards[i].Discover_Player1)
                {
                    updateImageSource(i, Cards[i].Image_Path_Player1); 
                }
                else if (Cards[i].Discover_Player2)
                {
                    updateImageSource(i, Cards[i].Image_Path_Player2);
                }
                else
                {
                    updateImageSource(i, TmpPath);
                }
            }
        }

        internal override bool Victory(DisplayVictoryMessage displayVictoryMessage)
        {
            if (PlayerOneScore + PlayerTwoScore == 16)
            {
                if (PlayerOneScore > PlayerTwoScore)
                {
                    displayVictoryMessage($"Congratulations \nPlayer One has won.\nWith the score {PlayerOneScore} points vs {PlayerTwoScore} points");
                }
                else if (PlayerTwoScore > PlayerOneScore)
                {
                    displayVictoryMessage($"Congratulations \nPlayer Two has won.\nWith the score {PlayerTwoScore} points vs {PlayerOneScore} points");
                }
                else
                {
                    displayVictoryMessage($"Conglatunations\nIts a draw\nBoth Players have scored 8 points");
                }
                return true;
            }
            return false;
        }

        public override void EnableUndiscoveredButtons(AdjustButtonStatus adjustButtonStatus)
        {
            for (int i = 0; i < Cards.Count; i++)
            {
                //if (Cards[i].Discover_Player1 == false)
                //{
                //    adjustButtonStatus(i, true);
                //}
                //else if(Cards[i].Discover_Player2 == false)
                //{
                //    adjustButtonStatus(i, true);
                //}
                if (!(Cards[i].Discover_Player1 || Cards[i].Discover_Player2))
                {
                    adjustButtonStatus(i, true);
                }


            }
        }

    }
}
