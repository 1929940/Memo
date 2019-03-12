using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace memoLibrary
{
    public class SinglePlayer
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
        private int Counter { get; set; } = 0;
        private string PreviousCard { get; set; } = String.Empty;
        private string CurrentCard { get; set; } = String.Empty;
        public int MoveCounter { get; set; } = 0;
        public string TmpPath { get; set; } = @"pack://application:,,,/Images/Template.jpg";

        #region Delegates
        // These are all Action
        public delegate void AdjustButtonStatus(int i, bool flag);
        public delegate void UpdateLabels(string msg);
        public delegate void UpdateImageSource(int i, string path);
        public delegate void DisplayVictoryMessage(string message);

        #endregion 

        public SinglePlayer()
        {
            Cards.Shuffle<Card>();
        }

        public void SPButton(int i, AdjustButtonStatus adjustButtonStatus, 
            UpdateLabels updateLabels, 
            UpdateImageSource updateImageSource,
            DisplayVictoryMessage displayVictoryMessage)
        {
            Counter++;
            if (Counter == 3)
            {
                Counter = 1;
                Reset(adjustButtonStatus, updateImageSource);
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
                            Reset(adjustButtonStatus, updateImageSource);
                        }
                    }
                    Victory(displayVictoryMessage);
                }
                MoveCounter++; // NotifyOnChange
                updateLabels(MoveCounter.ToString()); //NotifyOnChange
                EnableUndiscoveredButtons(adjustButtonStatus);
            }
            PreviousCard = CurrentCard;

        }

        public void Reset(AdjustButtonStatus adjustButtonStatus, UpdateImageSource updateImageSource)
        {
            for (int i = 0; i < Cards.Count; i++)
            {
                if (Cards[i].Discover_Player1)
                {
                    updateImageSource(i, Cards[i].Image_Path_Player1); //Images[i].Source = Cards[i].img_p1; 
                    // Change Image (i) to image discover of player (1)
                    adjustButtonStatus(i, false); // disable button
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
                displayVictoryMessage("You have won, congratulations");                
                return true;
            }
            return false;
        }

        void EnableUndiscoveredButtons(AdjustButtonStatus adjustButtonStatus)
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
