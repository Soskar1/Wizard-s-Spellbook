using System.Collections.Generic;
using System.Linq;

namespace WizardsSpellbook.Core.Domain.Words
{
    public class WordDictionary
    {
        private ISet<string> _words;

        public WordDictionary(ISet<string> words)
        {
            _words = words.Select(word => word.ToUpperInvariant()).ToHashSet();
        }

        public bool FindWord(string word) => _words.Contains(word);
    }
}
