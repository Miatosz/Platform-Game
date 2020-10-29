using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class Score : MonoBehaviour
    {
        // public GameObject player;
        public Text scoreText;
    
        private void Update()
        {
            scoreText.text = player.Score.ToString();
        }
    }
}

