using Core;
using Hud;
using Sounds;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Controller
{
    public class PlayerMovement : MonoBehaviour
    {
        public float jumpForce = 5;
        public float speed = 2;
        
        
        public bool isMoving;
        public bool isCrouching;
        public bool isJumping;
        public bool isInAir;
        public bool isGrounded;
        public bool isOnLadder;
        public bool doorOpened;
        
        public Animator animator;
        
        private Rigidbody2D _rb;
        private Vector2 _playerPos;
        private float _lockPos = 0;
        public GameObject HUD;

        public GameObject currentRespawnPoint;
        
        void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            isMoving = false;
            doorOpened = false;
            isCrouching = false;
            isJumping = false;
            isGrounded = true;
            isInAir = false;
            isOnLadder = false;
        }

        private void Update()  
        {
            Debug.Log(player.Lifes);
            transform.rotation = Quaternion.Euler(_lockPos, _lockPos, _lockPos);
            move();
            animationsChange();
            
        }

        void keyCollect()
        {
          
        }
        
        
        void animationsChange()
        {
           
            if (isMoving)
            {
                animator.SetBool("move", true);
            }
            else
            {
                animator.SetBool("move", false);
            }

            if (isCrouching)
            {
                animator.SetBool("crouch", true);
            }
            else
            {
                animator.SetBool("crouch", false);
            }

            if (!isGrounded)
            {
                animator.SetBool("jump", true);
            }
            else
            {
                animator.SetBool("jump", false);

            }
        }
        
        void move()
        {

            if (isOnLadder)
            {
                float y = Input.GetAxisRaw("Vertical"); 
                float moveByY = y * speed; 
                _rb.velocity = new Vector2(_rb.velocity.x, moveByY);
            }
            
          

            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            { 
                GetComponent<SpriteRenderer>().flipX = true;
                isMoving = true;
            } 
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                GetComponent<SpriteRenderer>().flipX = false;
                isMoving = true;
            }


            if (Input.GetKeyDown(KeyCode.Space) && !isInAir)
            {
                isInAir = true;
                _rb.velocity = new Vector2(_rb.velocity.x, jumpForce);
                isJumping = true;
                isGrounded = false;
            }

            if (isGrounded || !isInAir)
            {
                if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
                GetComponent<SpriteRenderer>().flipX = false;

            } else if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
                GetComponent<SpriteRenderer>().flipX = true;

            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                isCrouching = true;
            }

            if (Input.GetKeyUp(KeyCode.A) ||
                Input.GetKeyUp(KeyCode.LeftArrow) ||
                Input.GetKeyUp(KeyCode.D) ||
                Input.GetKeyUp(KeyCode.RightArrow) ||
                Input.GetKeyUp(KeyCode.LeftControl))
            {
                isMoving = false;
                isCrouching = false;
            }
            
            
            
            
            
            
            float x = Input.GetAxisRaw("Horizontal"); 
            float moveByX = x * speed; 
            _rb.velocity = new Vector2(moveByX, _rb.velocity.y);

            
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.collider.tag == "tile")
            {
                isGrounded = true;
                isInAir = false;
            }

            switch (other.collider.tag)
            {
                case "lockedDoors":
                    if (player.HasSilverKey)
                    {
                        other.gameObject.GetComponent<TilemapCollider2D>().enabled = false;
                        other.gameObject.GetComponent<TilemapRenderer>().enabled = false;
                    }
                    break;
                case "tile":
                    isGrounded = true;
                    isInAir = false;
                    break;
                case "enemy":
                    Debug.Log("life");
                    player.Lifes--;
                    HUD.GetComponent<HudController>().ChangeHud();
                    break;
                case "hidden":
                    isGrounded = true;
                    isInAir = false;
                    break;
            }
        }

        void silverKeyCollect()
        {
            player.HasSilverKey = true;
            HUD.GetComponent<HudController>().SilverKeyHUD();
            SFXPlayer.sfx.PlayKey();
            
        }

        void bonusStars()
        {
            if (GameObject.Find("Secret Place") != null)
            {
                GameObject bonusPlace = GameObject.FindGameObjectWithTag("secretPlace");
                bonusPlace.GetComponent<TilemapRenderer>().enabled = false;
                bonusPlace.GetComponent<TilemapCollider2D>().enabled = false;
                // GameObject bonusStars = GameObject.Find("bonusStars");
                // bonusStars.SetActive(true); 
                GameObject[] bonusStars = GameObject.FindGameObjectsWithTag("star");
                foreach (var bonusStar in bonusStars)
                {
                    if (bonusStar.GetComponent<SpriteRenderer>().enabled == false)
                        bonusStar.GetComponent<SpriteRenderer>().enabled = true;
                }
            }
        }
        

        void goldKeyCollect()
        {
            player.HasGoldKey = true;
            HUD.GetComponent<HudController>().GoldenKeyHUD();
            SFXPlayer.sfx.PlayKey();
        }

        void respawn()
        {
            GetComponent<SpriteRenderer>().enabled = false;
            new WaitForSeconds(5);
            transform.position = new Vector3(currentRespawnPoint.transform.position.x,
                currentRespawnPoint.transform.position.y,
                0); 
            GetComponent<SpriteRenderer>().enabled = true;
        }

        void decreaseLifes()
        {
            player.Lifes--;
            HUD.GetComponent<HudController>().ChangeHud();

        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            switch (other.tag)
            {
                case "respawnPoint":
                    currentRespawnPoint = other.gameObject;
                    break;
                case "spikes":
                    decreaseLifes();
                    respawn();
                    break;
                case "outOfMap":
                    decreaseLifes();
                    respawn();
                    break;
                case "endLevel":
                    if (player.HasGoldKey)
                    {
                        Debug.Log("open");
                        GameObject go = GameObject.FindGameObjectWithTag("endLevel");
                        go.GetComponent<Animator>().SetBool("doorOpen", true);
                        doorOpened = true;
                    }
                    break;
                case "ladder":
                    isOnLadder = true;
                    break;
                case "portal":
                    if (doorOpened)
                    {
                        SFXPlayer.Destroy(SFXPlayer.sfx);
                        player.NextLevel();
                    }
                    break;
                case "star":
                    SFXPlayer.sfx.PlayCoin();
                    Destroy(other.gameObject);
                    player.Score += 10;
                    break;
                case "enemy":
                    decreaseLifes();
                    break;
                case "silver key":
                    silverKeyCollect();
                    Destroy(other.gameObject);
                    bonusStars();
                    break;
                case "gold key":
                    goldKeyCollect();
                    Destroy(other.gameObject);
                    break;
            }
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.tag != "crank") return;
            if (!Input.GetKeyDown(KeyCode.UpArrow)) return; 
            
            other.GetComponent<Animator>().SetBool("onOff", true);
            GameObject go = GameObject.Find("hidden");
            go.GetComponent<TilemapRenderer>().enabled = true;
            go.GetComponent<BoxCollider2D>().enabled = true;
            
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.tag == "ladder")
            {
                isOnLadder = false;
            }
        }
   
}

}
