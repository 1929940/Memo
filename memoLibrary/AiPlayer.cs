using System;
using System.Collections.Generic;
using System.Linq;


namespace memoLibrary
{
    public class AiPlayer : MultiPlayer
    {
        public delegate void WriteMessage(string message);
        public event WriteMessage WriteMessageEvent;

        private int AiMemoryLimit { get; set; } = 3;
        List<int> AiMemory = new List<int>();

        Random random = new Random();

        public enum AiDifficulties { Easy, Moderate, Hard}

        /// <summary>
        /// Creates a new game vs an AI controlled player 
        /// </summary>
        /// <param name="adjustButtonStatus">Enable/Disable turning over a card</param>
        /// <param name="updateImageSource">Changes the source of an image</param>
        public AiPlayer(AdjustButtonStatus adjustButtonStatus, UpdateImageSource updateImageSource) : base(adjustButtonStatus, updateImageSource)
        {
        }

        public override bool PlayButton(int i, 
            AdjustButtonStatus adjustButtonStatus, 
            UpdateImageSource updateImageSource, 
            DisplayVictoryMessage displayVictoryMessage)
        {
            AddToAiMemory(i);
            Counter++;
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
                            PlayerOneScore++;
                            Reset(updateImageSource);
                        }
                    }
                    RemoveFromAiMemory(); // this goes into the if and takes on NAME param and it should work as well
                    if (Victory(displayVictoryMessage))
                        return true;
                }
                // AI turn begins
                #region AiTurn
                Counter = 0;
                Player = 2;

                WriteMessageEvent("Press OK to begin ai's turn");

                Reset(updateImageSource);
                if (AiMemory.Count > 0)
                {
                    if (!CheckAiMemoryForPair(updateImageSource, displayVictoryMessage))
                    {
                        int tmp = AiPlayButton(AiPlaysRandomButton(), true, updateImageSource, displayVictoryMessage);
                        if (!CheckAiMemoryForMatch(tmp,updateImageSource, displayVictoryMessage))
                        {
                            AiPlayButton(AiPlaysRandomButton(), true, updateImageSource, displayVictoryMessage);
                        }
                    }
                }
                else
                {
                    AiPlayButton(AiPlaysRandomButton(), true, updateImageSource, displayVictoryMessage);
                    AiPlayButton(AiPlaysRandomButton(), true, updateImageSource, displayVictoryMessage);
                }

                #endregion
                Counter = 0;
                Player = 1;
                EnableUndiscoveredButtons(adjustButtonStatus);

                if (Victory(displayVictoryMessage))
                    return true;

                WriteMessageEvent("Press OK to begin player's turn");
                Reset(updateImageSource);

            }
            PreviousCard = CurrentCard;
            return false;
        }
        /// <summary>
        /// Simulates Ai turning over a specified card
        /// </summary>
        /// <param name="i">Specified card</param>
        /// <param name="playedRandom">Set true if Ai has just played a random card</param>
        /// <param name="updateImageSource">Changes the source of an image</param>
        /// <param name="displayVictoryMessage">Dictates what function to call to display victory message</param>
        /// <returns>Returns the index of the card it just played</returns>
        public virtual int AiPlayButton(int i, bool playedRandom,
            UpdateImageSource updateImageSource, 
            DisplayVictoryMessage displayVictoryMessage)
        {
            if (playedRandom)
            {
                AddToAiMemory(i); // Should edit list when playing randoms only
            }
            Counter++;
            // It never reaches 3
            //if (Counter == 3)
            //{
            //    Counter = 1;
            //    Reset(updateImageSource);
                
            //}
            updateImageSource(i, Cards[i].Image_Path);
            CurrentCard = Cards[i].Name;
            if (Counter == 2)
            {
                if (CurrentCard == PreviousCard)
                {
                    foreach (var item in Cards)
                    {
                        if (CurrentCard == item.Name)
                        {
                            item.Discover_Player2 = true;
                            PlayerTwoScore++;
                            Reset(updateImageSource);
                        }
                    }
                    RemoveFromAiMemory();
                }
            }
            PreviousCard = CurrentCard;
            return i;
        }

        /// <summary>
        /// Simulates Ai turning over a random selectable card
        /// </summary>
        /// <returns>Returns what card it turned over</returns>
        public virtual int AiPlaysRandomButton()
        {
            List<int> tmp = new List<int>();

            for (int i = 0; i < 16; i++)
            {
                if (!(Cards[i].Discover_Player1 || Cards[i].Discover_Player2))
                {
                    tmp.Add(i);
                }
                //    if(generatorTMP < 16) tmpList.Remove(generatorTMP);
                // whats the point of this if?

            }
            return tmp[random.Next(tmp.Count)];
        }

        /// <summary>
        /// Searches if in a list of a few previously played cards is a match 
        /// </summary>
        /// <param name="updateImageSource">Changes the source of an image</param>
        /// <param name="displayVictoryMessage">Dictates what function to call to display victory message</param>
        /// <returns>Returns true if it found a match</returns>
        public virtual bool CheckAiMemoryForPair(
            UpdateImageSource updateImageSource, 
            DisplayVictoryMessage displayVictoryMessage)
        {
            for (int i = 0; i < AiMemory.Count; i++)
            {
                for (int j = i+1; j < AiMemory.Count; j++)
                {
                    if ((i != j) && 
                        (Cards[AiMemory[i]].Name == Cards[AiMemory[j]].Name))
                    {
                        AiPlayButton(AiMemory[i], false, updateImageSource, displayVictoryMessage);
                        AiPlayButton(AiMemory[j], false, updateImageSource, displayVictoryMessage);
                        return true;
                    }

                }
            }
            return false;
        }

        /// <summary>
        /// Searches memory for a match to the previously turned over card
        /// </summary>
        /// <param name="previouslyPlayedCard">Index of a card that has just been turned over</param>
        /// <param name="updateImageSource">Changes the source of an image</param>
        /// <param name="displayVictoryMessage">Dictates what function to call to display victory message</param>
        /// <returns>Returns true if it found a match</returns>
        public virtual bool CheckAiMemoryForMatch(int previouslyPlayedCard,
            UpdateImageSource updateImageSource, 
            DisplayVictoryMessage displayVictoryMessage)
        {
            for (int i = 0; i < AiMemory.Count; i++)
            {
                if ((AiMemory[i] != previouslyPlayedCard) &&
                    (Cards[AiMemory[i]] == Cards[previouslyPlayedCard]))
                {
                    AiPlayButton(AiMemory[i], false, updateImageSource, displayVictoryMessage);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Simulates memory by adding to a list indexes of previously played cards
        /// </summary>
        /// <param name="value">Index of a card</param>
        public virtual void AddToAiMemory(int value)
        {
            foreach (var i in AiMemory)
            {
                if (i == value) return;
            }
            if (AiMemory.Count == AiMemoryLimit)
            {
                AiMemory.Remove(AiMemory.Last<int>());
            }
            AiMemory.Insert(0, value);
        }

        /// <summary>
        /// Filters out discovered matches from the 'memory'
        /// </summary>
        public virtual void RemoveFromAiMemory()
        {
            // Filter List for Discovered

            // Would be better if i pass the name of the card to find and remove?

            List<int> tmp = new List<int>();
            foreach (var item in AiMemory)
            {
                if ((Cards[item].Discover_Player1) || (Cards[item].Discover_Player2))
                {
                    tmp.Add(item);
                }
            }
            foreach (var item in tmp)
            {
                AiMemory.Remove(item);
            }
        }

        /// <summary>
        /// Allows to pick one of three difficulty settings
        /// </summary>
        /// <param name="difficulty">Specifies difficulty setting</param>
        public virtual void ChangeAiDifficulty(AiDifficulties difficulty)
        {
            if (difficulty == AiDifficulties.Easy)
            {
                AiMemoryLimit = 2;
            }
            else if (difficulty == AiDifficulties.Moderate)
            {
                AiMemoryLimit = 3;
            }
            else
            {
                AiMemoryLimit = 5;
            }
        }
    }
}
