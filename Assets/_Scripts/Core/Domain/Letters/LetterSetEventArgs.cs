using System;

namespace WizardsSpellbook.Core.Domain.Letters
{
    public class LetterSetEventArgs : EventArgs
    {
        public int Index { get; private set; }
        public Letter Letter { get; private set; }

        public LetterSetEventArgs(int index, Letter letter)
        {
            Index = index;
            Letter = letter;
        }
    }
}
