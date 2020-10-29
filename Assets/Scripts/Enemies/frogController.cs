using UnityEngine;

namespace Enemies
{
    public class frogController : MonoBehaviour
    {
        private GameObject _enemy;
    
  
        private Vector2 movement;
    
        public bool moveRight = true;
        private Vector3 _startPosition;

  
        private Vector2 startPosition;
        private Vector2 newPosition;
        private Vector2 oldPosition;
        private float _lockPos = 0;

        [SerializeField] private float speed = 3;
        [SerializeField] private int maxDistance = 2;

        void Start()
        {
            startPosition = transform.position;
            newPosition = transform.position;
        }

        void Update()
        {
            transform.rotation = Quaternion.Euler(_lockPos, _lockPos, _lockPos);
            oldPosition.x = transform.position.x;
            newPosition.x = startPosition.x + (maxDistance * Mathf.Sin(Time.time * speed));
            transform.position = newPosition;

            if (transform.position.x >= oldPosition.x)
            {
                GetComponent<SpriteRenderer>().flipX = true;
                Debug.Log("right");
            }else if (transform.position.x <= oldPosition.x)
            {
                GetComponent<SpriteRenderer>().flipX = false;
                Debug.Log("left");

            }
        
      
        }
    }

}
