using System;
using System.Collections.Generic;

namespace WizardsSpellbook.Core.Domain.Words
{
    public class WordDictionary
    {
        private readonly HashSet<string> _words;

        public WordDictionary(IEnumerable<string> words)
        {
            _words = new HashSet<string>(words, StringComparer.OrdinalIgnoreCase);
        }

        public bool FindWord(string word) => _words.Contains(word);
    }
}
