using System;

namespace WizardsSpellbook.Core.Domain.Letters
{
    public class BookChangedEventArgs : EventArgs
    {
        public int[] PositionsInBook { get; private set; }
        public Letter[] Letters { get; private set; }
        public BookOperationType BookOperationType { get; private set; }

        public BookChangedEventArgs(int[] positionsInBook, Letter[] letters, BookOperationType operationType)
        {
            PositionsInBook = positionsInBook;
            Letters = letters;
            BookOperationType = operationType;
        }
    }
}
