using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace memoLibrary
{
    public class SinglePlayer : INotifyPropertyChanged
    {
        List<Card> Cards = new List<Card> // I might have to move Images to lib project
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

        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        private int Counter { get; set; } = 0;
        private string PreviousCard { get; set; } = String.Empty;
        private string CurrentCard { get; set; } = String.Empty;

        private int _MoveCounter;

        public int MoveCounter
        {
            get { return _MoveCounter; }
            set
            {
                _MoveCounter = value;
                PropertyChanged(this, new PropertyChangedEventArgs("MoveCounter"));
            }
        }

        public string TmpPath { get; set; } = @"pack://application:,,,/Images/Template.jpg";

        #region Delegates
        // These are all Action
        public delegate void AdjustButtonStatus(int i, bool flag);
        public delegate void UpdateImageSource(int i, string path);
        public delegate void DisplayVictoryMessage(string message);

        #endregion 

        public SinglePlayer(AdjustButtonStatus adjustButtonStatus, UpdateImageSource updateImageSource)
        {
            Cards.Shuffle<Card>();

            //reset board
            Reset(updateImageSource);

            //unlock buttons
            EnableUndiscoveredButtons(adjustButtonStatus);

            MoveCounter = 0;

            //resetboard
            //unblockbuttons
            //reset score (done by creating new object and renewing the binding)
            //should it prepare the labels as well?

            // this will reset
        }

        public bool SPButton(int i, AdjustButtonStatus adjustButtonStatus, 
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
                            item.Discover_Player1 = true;
                            Reset(updateImageSource);
                        }
                    }
                    if (Victory(displayVictoryMessage))
                        return true;
                }
                MoveCounter++;
                EnableUndiscoveredButtons(adjustButtonStatus);
            }
            PreviousCard = CurrentCard;
            return false;
        }

        public void Reset(UpdateImageSource updateImageSource)
        {
            for (int i = 0; i < Cards.Count; i++)
            {
                if (Cards[i].Discover_Player1)
                {
                    updateImageSource(i, Cards[i].Image_Path_Player1); //Images[i].Source = Cards[i].img_p1; 
                    // Change Image (i) to image discover of player (1)
                    //adjustButtonStatus(i, false); // disable button
                }
                else
                {
                    // Update Card Image to TMP
                    updateImageSource(i, TmpPath);
                }
            }
        }

        bool Victory(DisplayVictoryMessage displayVictoryMessage)
        {
            int score = 0;
            foreach (var item in Cards)
            {
                if (item.Discover_Player1)
                    score++;
            }
            if (score == 16)
            {
                displayVictoryMessage($"Congratulations \nYou have won in {MoveCounter} moves.");                
                return true;
            }
            return false;
        }

        public void EnableUndiscoveredButtons(AdjustButtonStatus adjustButtonStatus)
        {
            for (int i = 0; i < Cards.Count; i++)
            {
                if (Cards[i].Discover_Player1 == false)
                {
                    adjustButtonStatus(i, true);
                }
            }
        }


    }
}
