namespace WizardsSpellbook.Core.Domain.Letters
{
    public class Letter
    {
        public char Character { get; private set; }

        public Letter(char character) => Character = character;
    }
}
