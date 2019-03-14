using memoLibrary;
using System.Windows;

namespace Memo
{
    public partial class MainWindow : Window
    {
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
