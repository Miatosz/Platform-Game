using UnityEngine;

namespace Sounds
{
    public class MusicPlayer : MonoBehaviour
    {
        public AudioSource Music;

        private void Start()
        {
            Music.Play();
        }
    }
}

