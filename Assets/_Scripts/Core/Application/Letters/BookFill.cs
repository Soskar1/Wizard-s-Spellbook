using WizardsSpellbook.Core.Application.Words;
using WizardsSpellbook.Core.Domain.Letters;

namespace WizardsSpellbook.Core.Application.Letters
{
    public class BookFill
    {
        private readonly LetterInventory _letterInventory;
        private readonly Book _book;
        private readonly WordBuilder _wordBuilder;

        public BookFill(LetterInventory inventory, Book book, WordBuilder wordBuilder)
        {
            _letterInventory = inventory;
            _book = book;
            _wordBuilder = wordBuilder;
        }

        public void Fill()
        {
            for (var i = 0; i < _book.MaxSize; ++i)
            {
                var letter = _book.GetLetter(i);

                if (letter == null)
                {
                    letter = _letterInventory.GetLetter();
                    _book.SetLetter(i, letter);
                }
            }
        }

        public void Refill()
        {
            _wordBuilder.Clear();
            _book.Clear();

            Fill();
        }
    }
}