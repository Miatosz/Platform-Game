using UnityEngine;

namespace Core
{
    public class HouseController : MonoBehaviour
    {
        public Animator animator;


        public void OpenDoor()
        {
            animator.SetBool("doorOpen", true);
        }
    }
}

