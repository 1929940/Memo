using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace memoLibrary
{
    public class SinglePlayer : INotifyPropertyChanged
    {
        public virtual event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        internal string PreviousCard { get; set; }
        internal string CurrentCard { get; set; }

        internal int Counter { get; set; }

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

        internal List<Card> Cards = new List<Card> // I might have to move Images to lib project
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

        public string TemplatePath { get; set; } = @"pack://application:,,,/Images/Template.jpg";

        #region Delegates
        // These are all Action
        public delegate void AdjustButtonStatus(int i, bool flag);
        public delegate void UpdateImageSource(int i, string path);
        public delegate void DisplayVictoryMessage(string message);
        #endregion 
        /// <summary>
        /// Creates a new SinglePlayer game
        /// </summary>
        /// <param name="adjustButtonStatus">Enable/Disable turning over a card</param>
        /// <param name="updateImageSource">Changes the source of an image</param>
        public SinglePlayer(AdjustButtonStatus adjustButtonStatus, UpdateImageSource updateImageSource)
        {
            Cards.Shuffle<Card>();

            Reset(updateImageSource);

            EnableUndiscoveredButtons(adjustButtonStatus);

        }
        /// <summary>
        /// Turns over the specified card
        /// </summary>
        /// <param name="i">Specified Card</param>
        /// <param name="adjustButtonStatus">Enable/Disable turning over a card</param>
        /// <param name="updateImageSource">Changes the source of an image</param>
        /// <param name="displayVictoryMessage">Dictates what function to call to display victory message</param>
        /// <returns></returns>
        public virtual bool PlayButton(int i, AdjustButtonStatus adjustButtonStatus, 
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
                    foreach (var card in Cards)
                    {
                        if (CurrentCard == card.Name)
                        {
                            card.Discover_Player1 = true;
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

        /// <summary>
        /// Turn's back over any card that has not found its match. 
        /// </summary>
        public virtual void Reset(UpdateImageSource updateImageSource)
        {
            for (int i = 0; i < Cards.Count; i++)
            {
                if (Cards[i].Discover_Player1)
                {
                    updateImageSource(i, Cards[i].Image_Path_Player1);  
                }
                else
                {
                    updateImageSource(i, TemplatePath);
                }
            }
        }
        /// <summary>
        /// Checks for Victory conditions
        /// </summary>
        /// <param name="displayVictoryMessage">Dictates what function to call to display victory message</param>
        /// <returns></returns>
        internal virtual bool Victory(DisplayVictoryMessage displayVictoryMessage)
        {
            int score = 0;
            foreach (var card in Cards)
            {
                if (card.Discover_Player1)
                    score++;
            }
            if (score == 16)
            {
                displayVictoryMessage($"Congratulations \nYou have won in {MoveCounter} moves.");                
                return true;
            }
            return false;
        }
        /// <summary>
        /// Enables turning over every card still in play
        /// </summary>
        /// <param name="adjustButtonStatus">Enable/Disable turning over a card</param>
        public virtual void EnableUndiscoveredButtons(AdjustButtonStatus adjustButtonStatus)
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
