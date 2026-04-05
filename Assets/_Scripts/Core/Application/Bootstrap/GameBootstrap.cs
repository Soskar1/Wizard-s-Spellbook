using Reflex.Attributes;
using UnityEngine;
using WizardsSpellbook.Core.Application.Letters;
using WizardsSpellbook.Core.Domain.Letters;
using WizardsSpellbook.Core.Domain.Words;

namespace WizardsSpellbook.Core.Application.Bootstrap
{
    public class GameBootstrap : MonoBehaviour
    {
        private AlphabetInventory _alphabetInventory;
        private LetterGenerator _letterGenerator;
        private Book _book;
        private Word _word;

        [Inject]
        public void Inject(LetterGenerator generator, AlphabetInventory inventory, Book book, Word word)
        {
            _letterGenerator = generator;
            _alphabetInventory = inventory;
            _book = book;
            _word = word;
        }

        public void Start()
        {
            foreach (var letter in "ABCDEFGIJKLMNOPQRSTUVWXYZ")
            {
                _alphabetInventory.AddLetter(letter);
            }

            _letterGenerator.FillBook(_book);
        }
    }
}
