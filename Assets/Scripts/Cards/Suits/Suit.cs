using System;
using DG.Tweening;
using UnityEngine;

namespace HOD.Cards.Suits
{
    [Serializable]
    public class Suit
    {
        private const float SWITCH_TIME = 0.2f;

        [SerializeField] private Sprite _suitSprite;
        [SerializeField] private Sprite _backSprite;

        private Card _card;

        private bool _currentState;

        public void SetCard(Card card)
        {
            if (_card != null)
                _card.OnShowStateChanged -= Show;

            _card = card;

            if (_card == null)
                return;

            _card.OnShowStateChanged += Show;

            _currentState = false;
            Show(false, false);
        }

        private void Show(bool state, bool withAnimation)
        {
            if (_currentState == state)
                return;

            _currentState = state;

            if (withAnimation)
                CreateStateSequence(state).Play();
            else
                SetRendererSprite(state);
        }

        private Sequence CreateStateSequence(bool state)
        {
            Sequence sequence = DOTween.Sequence();

            sequence.Append(_card.transform.DOScaleX(0, SWITCH_TIME / 2));
            sequence.AppendCallback(() => SetRendererSprite(state));
            sequence.Append(_card.transform.DOScaleX(1, SWITCH_TIME / 2));
            return sequence;
        }

        private void SetRendererSprite(bool state) => _card.SpriteRenderer.sprite = state ? _suitSprite : _backSprite;
    }
}
