using System;
using WizardsSpellbook.Core.Domain.Letters;

namespace WizardsSpellbook.Core.Domain.Words
{
    public class WordChangedEventArgs : EventArgs
    {
        public WordOperationType OperationType { get; }
        public Letter Letter { get; }
        public bool WordIsValid { get; }

        public WordChangedEventArgs(WordOperationType operationType, Letter letter, bool wordIsValid)
        {
            OperationType = operationType;
            Letter = letter;
            WordIsValid = wordIsValid;
        }
    }
}
