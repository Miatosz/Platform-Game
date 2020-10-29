using Core;
using UnityEngine;

namespace Hud
{
    public class HudController : MonoBehaviour
    {
        [SerializeField] public Sprite[] spritesLoads ;

        private void Start()
        {
            GetComponent<SpriteRenderer>().sprite = spritesLoads[3];
        }

        public void ChangeHud()
        { 
            GetComponent<SpriteRenderer>().sprite = spritesLoads[player.Lifes];
        }

        public void SilverKeyHUD()
        {
            GameObject.Find("silverkeyHUD").GetComponent<SpriteRenderer>().enabled = true;
        }
    
        public void GoldenKeyHUD()
        {
            GameObject.Find("goldenkeyHUD").GetComponent<SpriteRenderer>().enabled = true;
        }
    
    
    
    }
}

