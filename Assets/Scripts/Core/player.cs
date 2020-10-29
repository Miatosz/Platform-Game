using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core
{
    public class player : MonoBehaviour
    {
        public static int Level = 1;

        public static int Score = 0;
        public static int Lifes = 3;
        public static bool HasSilverKey = false;
        public static bool HasGoldKey = false;



        // private void Awake()
        // {
        //     DontDestroyOnLoad(this);
        // }


        public static void NextLevel()
        {
            HasSilverKey = false;
            HasGoldKey = false;
            Level++;
            SceneManager.LoadScene("level" + Level);
        }
    }

}
