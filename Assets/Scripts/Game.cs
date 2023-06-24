using HOD.Cards;
using HOD.Cards.Decks;
using HOD.Sounds;
using UnityEngine;
using UnityEngine.SceneManagement;

// Handing out deck (HOD)
namespace HOD
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private Sound _sound;
        [SerializeField] private Deck _handoutDeck;
        [SerializeField] private Deck _endDeck;

        private void Awake()
        {
            _handoutDeck.OnClicked += HandoutCardToEndDeck;
        }

        private void HandoutCardToEndDeck()
        {
            if (!_handoutDeck.TryGetCard(out Card card))
                return;

            _sound.PlayCardSound();
            _endDeck.AddToFront(card);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                Restart();

            if (Input.GetKeyDown(KeyCode.Escape))
                Quit();
        }

        private static void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private static void Quit()
        {
            Application.Quit();
        }
    }
}
