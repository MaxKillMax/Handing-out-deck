using System;
using HOD.Cards.Suits;
using UnityEngine;

namespace HOD.Cards
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Card : MonoBehaviour
    {
        [SerializeField] private Suit _suit;

        /// <summary>
        /// First boolean is new state. Second boolean declares need in animation
        /// </summary>
        public event Action<bool, bool> OnShowStateChanged;

        public bool State { get; private set; }
        public SpriteRenderer SpriteRenderer { get; private set; }

        private void Awake()
        {
            SpriteRenderer = GetComponent<SpriteRenderer>();

            _suit.SetCard(this);
        }

        private void OnDestroy()
        {
            _suit.SetCard(null);
        }

        public void Show(bool state, bool withAnimation = true)
        {
            OnShowStateChanged?.Invoke(state, withAnimation);
        }
    }
}
