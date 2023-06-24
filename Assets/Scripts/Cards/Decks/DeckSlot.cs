using System;
using UnityEngine;

namespace HOD.Cards.Decks
{
    [Serializable]
    public struct DeckSlot
    {
        [SerializeField] private Transform _transform;
        [SerializeField] private int _layer;

        public int Layer => _layer;
        public Transform Transform => _transform;
    }
}
