

namespace memoLibrary
{
    public static class GameModesProcessor
    {
        public enum Modes { SP, MP , AI}
        public static Modes Setting = Modes.SP;

        public delegate void ConfigureDisplayForMode();
        //What if we did a static constructor that got us all the nessesary delegates?

        public static SinglePlayer CreateGame(
            SinglePlayer.AdjustButtonStatus adjustButtonStatus, 
            SinglePlayer.UpdateImageSource updateImageSource)
        {
            SinglePlayer output;

            if (Setting == Modes.SP)
            {
                output = new SinglePlayer(adjustButtonStatus, updateImageSource);
            }
            else if(Setting == Modes.MP)
            {
                output = new MultiPlayer(adjustButtonStatus, updateImageSource);
            }
            else
            {
                output = new AiPlayer(adjustButtonStatus, updateImageSource);
            }
            return output;
        }

    }
}
