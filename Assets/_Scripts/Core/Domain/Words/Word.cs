using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using WizardsSpellbook.Core.Domain.Letters;

namespace WizardsSpellbook.Core.Domain.Words
{
    public class Word : INotifyCollectionChanged, IDisposable
    {
        private readonly ObservableCollection<Letter> _letters;

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public Word()
        {
            _letters = new ObservableCollection<Letter>();

            _letters.CollectionChanged += HandleCollectionChanged;
        }

        private void HandleCollectionChanged(object sender, NotifyCollectionChangedEventArgs args) => CollectionChanged?.Invoke(sender, args);

        public void AddLetter(Letter letter) => _letters.Add(letter);
        public void RemoveLetter(Letter letter) => _letters.Remove(letter);
        public bool Contains(Letter letter) => _letters.Contains(letter);

        public void Dispose()
        {
            _letters.CollectionChanged -= HandleCollectionChanged;
        }
    }
}
