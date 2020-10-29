using UnityEngine;

namespace Sounds
{
    public class SFXPlayer  : MonoBehaviour
    {
        public static SFXPlayer sfx;

        public AudioSource Key;
        public AudioSource Coin;

        private void Awake()
        {
            if (sfx != null)
            {
                GameObject.Destroy(sfx);
            }
            else
            {
                sfx = this;
            }
      
            DontDestroyOnLoad(this);
        }

        public void PlayCoin()
        {
            Coin.Play();
        }

        public void PlayKey()
        {
            Key.Play();
        }
    }
}

