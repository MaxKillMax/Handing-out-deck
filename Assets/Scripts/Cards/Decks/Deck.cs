using System;
using System.Collections.Generic;
using UnityEngine;

namespace HOD.Cards.Decks
{
    [RequireComponent(typeof(Collider2D))]
    public class Deck : MonoBehaviour
    {
        [SerializeField] private List<Card> _cards;
        [SerializeField] private DeckView _view;

        public event Action<Card> OnCardAdded;
        public event Action<Card> OnCardRemoved;
        public event Action OnClicked;

        public IEnumerable<Card> Cards => _cards;

        private void Awake()
        {
            _view.SetDeck(this);
        }

        private void OnDestroy()
        {
            _view.SetDeck(null);
        }

        public bool TryGetCard(out Card card)
        {
            card = default;

            if (_cards.Count == 0)
                return false;

            card = _cards[^1];
            _cards.Remove(card);
            OnCardRemoved?.Invoke(card);
            return true;
        }

        public void AddToFront(Card card)
        {
            _cards.Add(card);
            OnCardAdded?.Invoke(card);
        }

        private void OnMouseDown() => OnClicked?.Invoke();
    }
}
