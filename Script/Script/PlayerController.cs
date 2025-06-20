using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rbody;
    Animator anim;

    bool walking, jumped, jumping, grounded = false;
    bool IsJump = false;
    bool IsIdle = false;
    bool IsRun = false;
    float speed = 3f, height = 20f, jumpTime, walkTime;
    int moveState;
    bool jumpRequested;
    private Vector3 originalScale;

    

    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        rbody.gravityScale = 2f;
        rbody.freezeRotation = true;
        Application.targetFrameRate = 60;
        Time.timeScale = 1f;
        originalScale = transform.localScale;
        Debug.Log($"PlayerController Start - gravityScale: {rbody.gravityScale}, height: {height}");
    }

    void MoveAnh(Vector3 dir)
    {
        walking = true;
        speed = Mathf.Clamp(speed, 3f, 80f);
        walkTime += Time.deltaTime;
        rbody.velocity = new Vector2(dir.x * speed, rbody.velocity.y);

        if (walkTime < 3f && walking)
        {
            speed += 0.025f;
        }
        else if (walkTime > 3f)
        {
            speed = 3f;
            walkTime = 0;
        }
    }

    public void JumpAnh()
    {
        

        if (jumping)
        {
            jumpTime += Time.deltaTime;
            if (jumpTime > 0.5f)
            {
                jumping = false;
                IsJump = false;
                anim.SetBool("IsJump", IsJump);
            }
        }

       
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("GroundAnh"))
        {
            grounded = true;
            jumped = false;
            jumping = false;
            IsJump = false;
            jumpTime = 10;
            anim.SetBool("IsJump", IsJump);
            Debug.Log("Grounded = true");
        }
       
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("GroundAnh"))
        {
            grounded = false;
            Debug.Log("Grounded = false");
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
        
    {
        if (collision.gameObject.CompareTag("RewardAnh"))
        {
            GameController.instance.AddScoreAnh(1);
            Destroy(collision.gameObject);
            Debug.Log("Ate Coin, score increased!");
        }
        else if (collision.gameObject.CompareTag("MushRoomANh"))
        {
            GameController.instance.AddScoreAnh(3);
            transform.localScale += new Vector3(0.5f, 0.5f, 0);
            Destroy(collision.gameObject);
            Debug.Log("Ate Reward, scale increased!");
        }
        else if (collision.gameObject.CompareTag("MysteryBoxAnh"))
        {
            GameController.instance.AddScoreAnh(3);
            Destroy(collision.gameObject);
            Debug.Log("Ate Mystery Box, score increased!");
        }

        
             if (collision.gameObject.CompareTag("ObstacleAnh"))
        {
            if (transform.localScale.x > originalScale.x || transform.localScale.y > originalScale.y)
            {
                transform.localScale = originalScale;
                Debug.Log("Shrink to original size!");
            }
            else
            {
                gameObject.SetActive(false);
                Time.timeScale = 0;
                GameController.instance.GameOverAnh();
                Debug.Log("Hit Obstacle, Player died!");
                
            }
        }
        
    }

    void StateAnh()
    {
        IsRun = false;
        switch (moveState)
        {
            case 1:
                IsRun = true;
                anim.SetBool("IsRun", IsRun);
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                MoveAnh(Vector3.right);
                break;

            case 2:
                IsRun = true;
                anim.SetBool("IsRun", IsRun);
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                MoveAnh(Vector3.left);
                break;

            default:
                walking = false;
                walkTime = 0;
                speed = 3f;
                rbody.velocity = new Vector2(0, rbody.velocity.y);
                anim.SetBool("IsRun", IsRun);
                break;
        }

        IsIdle = moveState == 0 && grounded && !IsJump;
        anim.SetBool("IsIdle", IsIdle);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            jumpRequested = true;
            Debug.Log("Space pressed for Jump");
        }
        StateAnh();
    }



    void FixedUpdate()
    {
        if (!Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow))
        {
            moveState = 0;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            moveState = 1;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveState = 2;
        }

        
        if (jumpRequested && grounded)
        {
            rbody.velocity = new Vector2(rbody.velocity.x, height); 
            grounded = false;
            IsJump = true;
            jumping = true;
            jumpTime = 0;
            anim.SetBool("IsJump", true);
            jumpRequested = false;
        }

        JumpAnh(); 
    }

}
