using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Assertions;

namespace HOD.Cards.Decks
{
    [Serializable]
    public class DeckView
    {
        private const float CARD_MOVE_TIME = 0.3f;
        private const int NEGATIVE_CARD_LAYER = -1;

        [SerializeField] private DeckCardsShowState _showState;
        [SerializeField] private DeckSlot[] _slots;

        public int MaxCountOfVisibleCards => _slots.Length;
        public DeckSlot LastSlot => _slots[MaxCountOfVisibleCards - 1];

        private Deck _deck;

        public void SetDeck(Deck deck)
        {
            Assert.IsTrue(_showState != DeckCardsShowState.None);

            if (_deck != null)
                Unsubscribe();

            _deck = deck;

            if (_deck == null)
                return;

            Subscribe();
            UpdateSlots(false);
        }

        private void Unsubscribe()
        {
            _deck.OnCardAdded -= (c) => UpdateSlots();
            _deck.OnCardRemoved -= (c) => UpdateSlots();
        }

        private void Subscribe()
        {
            _deck.OnCardAdded += (c) => UpdateSlots();
            _deck.OnCardRemoved += (c) => UpdateSlots();
        }

        private void UpdateSlots(bool withAnimation = true)
        {
            List<Card> cards = _deck.Cards.ToList();

            for (int i = 0; i < cards.Count; i++)
            {
                int index = MaxCountOfVisibleCards - (cards.Count - i);
                UpdateCard(cards[i], index >= 0, index < 0 ? _slots[0] : _slots[index], withAnimation);
            }
        }

        private void UpdateCard(Card card, bool state, DeckSlot slot, bool withAnimation)
        {
            if (!state)
                card.transform.position = slot.Transform.position;
            else
                card.transform.DOMove(slot.Transform.position, CARD_MOVE_TIME);

            card.transform.parent = slot.Transform;
            card.SpriteRenderer.sortingOrder = state ? slot.Layer : NEGATIVE_CARD_LAYER;

            card.Show(state && GetSlotShowState(slot), withAnimation);
        }

        private bool GetSlotShowState(DeckSlot slot) => _showState == DeckCardsShowState.AllCardsIsOpened || (_showState == DeckCardsShowState.OnlyFrontCardIsOpened && slot.Equals(LastSlot));
    }
}
