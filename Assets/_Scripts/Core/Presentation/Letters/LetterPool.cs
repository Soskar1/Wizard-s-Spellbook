using System.Collections.Generic;
using WizardsSpellbook.Core.Domain.Letters;

namespace WizardsSpellbook.Core.Presentation.Letters
{
    public class LetterPool
    {
        private Dictionary<Letter, LetterPresenter> _letterPool;

        public LetterPool()
        {
            _letterPool = new Dictionary<Letter, LetterPresenter>();
        }

        public void Add(Letter letter, LetterPresenter presenter)
        {
            _letterPool.Add(letter, presenter);
        }

        public void Remove(Letter letter)
        {
            _letterPool.Remove(letter);
        }

        public LetterPresenter GetPresenter(Letter letter)
        {
            return _letterPool[letter];
        }
    }
}
