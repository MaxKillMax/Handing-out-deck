using UnityEngine;

namespace HOD.Sounds
{
    public class Sound : MonoBehaviour
    {
        // Need to add pull of sources
        [SerializeField] private AudioSource[] _audioSources;
        [SerializeField] private AudioClip _cardClip;

        public void PlayCardSound()
        {
            for (int i = 0; i < _audioSources.Length; i++)
            {
                if (_audioSources[i].isPlaying)
                    continue;

                _audioSources[i].clip = _cardClip;
                _audioSources[i].Play();
                break;
            }
        }
    }
}
