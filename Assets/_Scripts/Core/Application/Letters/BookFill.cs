using WizardsSpellbook.Core.Application.Words;
using WizardsSpellbook.Core.Domain.Letters;

namespace WizardsSpellbook.Core.Application.Letters
{
    public class BookFill
    {
        private readonly LetterGenerator _letterGenerator;
        private readonly Book _book;
        private readonly WordBuilder _wordBuilder;

        public BookFill(LetterGenerator letterGenerator, Book book, WordBuilder wordBuilder)
        {
            _letterGenerator = letterGenerator;
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
                    letter = _letterGenerator.GenerateLetter();
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