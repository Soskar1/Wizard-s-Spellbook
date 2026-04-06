using WizardsSpellbook.Core.Domain.Letters;

namespace WizardsSpellbook.Core.Application.Letters
{
    public class BookFill
    {
        private readonly LetterGenerator _letterGenerator;
        private readonly Book _book;

        public BookFill(LetterGenerator letterGenerator, Book book)
        {
            _letterGenerator = letterGenerator;
            _book = book;
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
    }
}