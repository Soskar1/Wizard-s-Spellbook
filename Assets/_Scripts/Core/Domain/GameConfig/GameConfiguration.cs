namespace WizardsSpellbook.Core.Domain.GameConfig
{
    public class GameConfiguration
    {
        public int LettersInBook { get; private set; }

        public GameConfiguration(GameConfigurationData data)
        {
            LettersInBook = data.LettersInBook;
        }
    }
}
