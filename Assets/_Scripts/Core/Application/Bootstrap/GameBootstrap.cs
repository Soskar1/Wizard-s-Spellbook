using Reflex.Attributes;
using UnityEngine;
using WizardsSpellbook.Core.Application.Letters;
using WizardsSpellbook.Core.Domain.Letters;

namespace WizardsSpellbook.Core.Application.Bootstrap
{
    public class GameBootstrap : MonoBehaviour
    {
        private AlphabetInventory _alphabetInventory;
        private LetterGenerator _letterGenerator;
        private Book _book;

        [Inject]
        public void Inject(LetterGenerator generator, AlphabetInventory inventory, Book book)
        {
            _letterGenerator = generator;
            _alphabetInventory = inventory;
            _book = book;
        }

        public void Start()
        {
            foreach (var letterRaw in "ABCDEFGIJKLMNOPQRSTUVWXYZ")
            {
                var letter = new Letter(letterRaw);
                _alphabetInventory.AddLetter(letter);
            }

            _letterGenerator.FillBook(_book);
        }
    }
}
