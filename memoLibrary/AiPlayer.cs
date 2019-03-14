using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace memoLibrary
{
    public class AiPlayer : MultiPlayer
    {

        public delegate void WriteMessage(string message);
        public event WriteMessage WriteMessageEvent;

        //I should have AI in a seperate class

        // props:
        // limit

        private int AiMemoryLimit { get; set; } = 3;

        public enum AiDifficulties { Easy, Moderate, Hard}
        //public AiDifficulties Dificulty = AiDifficulties.Moderate;


        // aml list - change to hashset

        List<int> AiMemory = new List<int>();


        // RNG generator

        Random random = new Random();

        // Prepare to change difficulty in here
        public AiPlayer(AdjustButtonStatus adjustButtonStatus, UpdateImageSource updateImageSource) : base(adjustButtonStatus, updateImageSource)
        {
        }
        public override bool PlayButton(int i, AdjustButtonStatus adjustButtonStatus, UpdateImageSource updateImageSource, DisplayVictoryMessage displayVictoryMessage)
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
                Player = 2; // InotifyOnChange Should Update This

                //TODO
                //Need to display a message
                //MessageBox.Show("Play")

                WriteMessageEvent("Press OK to begin ai's turn");

                //CW Play // ReadKey()

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
                //MessageBox Confirmation again
                WriteMessageEvent("Press OK to begin player's turn");
                Reset(updateImageSource);

            }
            PreviousCard = CurrentCard;
            return false;




            #region OldFunction
            //void AIButton(int i) 
            //{
            //    AddAml(i);
            //    counter++;
            //    Images[i].Source = Cards[i].img_highlight; 
            //    cur = Cards[i].name;
            //    Buttons[i].IsEnabled = false;
            //    if (counter == 2)
            //    {
            //        if (cur == prev)
            //        {
            //            foreach (var item in Cards)
            //            {
            //                if (cur == item.name)
            //                {
            //                 item.discovered_p1 = true;
            //                 playerOneScore++;
            //                 mid_two_lbl.Content = playerOneScore;
            //                 Reset();
            //                }
            //            }
            //            RemoveAml();
            //        }
            //        counter = 0;
            //        bool tmpVictory = Victory();
            //        if (tmpVictory) return; 

            //        // AI turn begins

            //        player = 2;
            //        left_two_lbl.Content = player;
            //        MessageBox.Show("Press OK to ::begin:: AI turn", "Need a more elegant solution");
            //        Reset();
            //        Debug.WriteLine("");
            //        if (ArtificialMemoryList.Count > 0)
            //        {
            //            if (!AICheckAMLforPair())
            //            {
            //                int tmp = AIPressButton(AIPlaysRandom(), true);
            //                if (!AICheckAMLforCard(tmp))
            //                {
            //                    AIPressButton(AIPlaysRandom(), true);
            //                }
            //            }
            //        }
            //        else
            //        {
            //            AIPressButton(AIPlaysRandom(), true);
            //            AIPressButton(AIPlaysRandom(), true);
            //        }

            //        EnableUndiscoveredButtons();
            //        counter = 0;
            //        player = 1;
            //        left_two_lbl.Content = player;
            //        tmpVictory = Victory();
            //        if (tmpVictory) return;
            //        MessageBox.Show("Press OK to ::end:: AI turn", "Need a more elegant solution");
            //        Reset();
            //    }
            //    prev = cur;
            //}
            #endregion
        }
        public virtual int AiPlayButton(int i, bool playedRandom,
            UpdateImageSource updateImageSource, DisplayVictoryMessage displayVictoryMessage)
        {
            if (playedRandom)
            {
                AddToAiMemory(i); // Should edit list when playing randoms only
            }
            Counter++;
            if (Counter == 3)
            {
                Counter = 1;
                Reset(updateImageSource);
            }
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


            #region OldFunction
            //int AIPressButton(int i, bool playedRandom)
            //{
            //    if (playedRandom)
            //    {
            //    AddAml(i); // Should edit list when playing randoms only
            //    }
            //    counter++;
            //    if (counter == 3)
            //    {
            //        counter = 1;
            //        Reset();
            //    }
            //    Images[i].Source = Cards[i].img_highlight;
            //    cur = Cards[i].name;
            //    if (counter == 2)
            //    {
            //        if (cur == prev)
            //        {
            //            foreach (var item in Cards)
            //            {
            //                if (cur == item.name)
            //                {
            //                 item.discovered_p2 = true;
            //                 playerTwoScore++;
            //                 right_two_lbl.Content = playerTwoScore;
            //                 Reset();
            //                }
            //            }
            //            RemoveAml();
            //        }
            //    }
            //    prev = cur;
            //    return i;
            //}
            #endregion
            return 0;
        }
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


            #region OldFunction
            //int AIPlaysRandom()
            //{
            //    List<int> tmpList = new List<int>();

            //    for (int i = 0; i < 16; i++)
            //    {
            //        if (!(Cards[i].discovered_p1 || Cards[i].discovered_p2))
            //        {
            //            tmpList.Add(i);
            //        }
            //    }           
            //    if(generatorTMP < 16) tmpList.Remove(generatorTMP);
            //    Random random = new Random();
            //    int returnVal = tmpList[random.Next(tmpList.Count)];
            //    generatorTMP = returnVal;
            //    Debug.WriteLine("AIPlaysRandom Method has generated: " + returnVal);
            //    return returnVal;
            //}
            #endregion
            //return 0;
        }
        public virtual bool CheckAiMemoryForPair(UpdateImageSource updateImageSource, DisplayVictoryMessage displayVictoryMessage)
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

            #region OldFunction
            //bool AICheckAMLforPair()
            //{
            //    for (int i = 0; i < ArtificialMemoryList.Count; i++)
            //    {
            //        for (int j = i+1; j < ArtificialMemoryList.Count; j++)
            //        {
            //            if ((i != j) && 
            //                (Cards[ArtificialMemoryList[i]].name == Cards[ArtificialMemoryList[j]].name))
            //            {
            //                Debug.WriteLine("AICheckAMLforPair method has found a pair:");
            //                Debug.WriteLine("Card One: {0}, {1}", ArtificialMemoryList[i], Cards[ArtificialMemoryList[i]].name);
            //                Debug.WriteLine("Card Two: {0}, {1}", ArtificialMemoryList[j], Cards[ArtificialMemoryList[j]].name);
            //                AIPressButton(ArtificialMemoryList[i], false);
            //                Debug.WriteLine("______ j:     " + j);
            //                Debug.WriteLine("_______ count: " + ArtificialMemoryList.Count);
            //                AIPressButton(ArtificialMemoryList[j], false);
            //                return true;
            //            }
            //        }
            //    }
            //    Debug.WriteLine("AICheckAMLforPair method has NOT found a pair");
            //    return false;
            //}
            #endregion
            //return false;
        }
        public virtual bool CheckAiMemoryForMatch(int previouslyPlayedCard,
            UpdateImageSource updateImageSource, DisplayVictoryMessage displayVictoryMessage)
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


            #region OldFunction
            //bool AICheckAMLforCard(int prevPlayedCardIndex)
            //{
            //    for (int i = 0; i < ArtificialMemoryList.Count; i++)
            //    {
            //        if ((ArtificialMemoryList[i] != prevPlayedCardIndex) && 
            //            (Cards[ArtificialMemoryList[i]] == Cards[prevPlayedCardIndex]))
            //        {
            //            AIPressButton(ArtificialMemoryList[i], false);
            //            return true;
            //        }
            //    }
            //    return false;
            //}
            #endregion
        }
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


            #region OldFunction
            //void AddAml(int value)
            //{
            //    foreach (var item in ArtificialMemoryList) //Should use hashset?
            //    {
            //        if (item == value) return;
            //    }
            //    if (ArtificialMemoryList.Count == limit)
            //    {
            //        ArtificialMemoryList.RemoveAt(limit-1);
            //    }
            //    ArtificialMemoryList.Insert(0, value);
            //}
            #endregion
        }
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

            #region OldFunction
            //void RemoveAml()
            //{
            //    List<int> tmpList = new List<int>();
            //    foreach (var item in ArtificialMemoryList)
            //    {
            //        if ((Cards[item].discovered_p1 == true) || (Cards[item].discovered_p2 == true))
            //        {
            //            tmpList.Add(item);
            //        }
            //    }
            //    foreach (var item in tmpList)
            //    {
            //        ArtificialMemoryList.Remove(item);
            //    }
            //}
            #endregion
        }
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
