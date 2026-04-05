using WizardsSpellbook.Core.Domain.Letters;
using WizardsSpellbook.Core.Domain.Words;

namespace WizardsSpellbook.Core.Application.Words
{
    public class WordBuilder
    {
        private readonly Word _word;

        public WordBuilder(Word word)
        {
            _word = word;
        }

        public void MoveLetter(Letter letter)
        {
            if (_word.Contains(letter))
            {
                MoveToBook(letter);
            }
            else
            {
                MoveToWord(letter);
            }
        }

        private void MoveToWord(Letter letter)
        {
            _word.AddLetter(letter);
        }

        private void MoveToBook(Letter letter)
        {

        }
    }
}
