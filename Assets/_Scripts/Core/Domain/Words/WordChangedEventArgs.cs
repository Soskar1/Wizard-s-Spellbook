using System;
using WizardsSpellbook.Core.Domain.Letters;

namespace WizardsSpellbook.Core.Domain.Words
{
    public class WordChangedEventArgs : EventArgs
    {
        public WordOperationType OperationType { get; }
        public Letter[] Letters { get; }
        public bool WordIsValid { get; }

        public WordChangedEventArgs(WordOperationType operationType, Letter[] letters, bool wordIsValid)
        {
            OperationType = operationType;
            Letters = letters;
            WordIsValid = wordIsValid;
        }
    }
}
