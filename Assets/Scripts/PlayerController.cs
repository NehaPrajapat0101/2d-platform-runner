using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public BoxCollider2D boxCollider;
    public float speed;
    public float deltaSpeed;
    public float jumpForce;
    public float deltaJump;
    public float normalGravity;
    public float fallGravity;
    public float jumpGravity;
    public bool isInvincible;
    public float invincibleTime = 1f;

    private Vector2 standingSize;
    private Vector2 standingOffset;
    private Rigidbody2D rb2D;

    [SerializeField] private Vector2 crouchSize;
    [SerializeField] private Vector2 crouchOffset;
    [SerializeField] private Vector2 jumpingSize;
    [SerializeField] private Vector2 jumpingOffset;
    [SerializeField] private ScoreController scoreController;
    [SerializeField] private HeartController heartController;
    [SerializeField] private GameObject starting;

    private bool isCrouching = false;
    private bool isJumping = false;
    private bool hasGun = false;

    void Awake()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        standingSize = boxCollider.size;
        standingOffset = boxCollider.offset;
        isInvincible = false;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        speed = PlayerSpeed();
        PlyaerHasGun();
        PlayerLocomotion(horizontal);
        PlayerCrouch();
        PlayerJump();
        PlayerAttack();
        MovePlayer(horizontal, Speed());
        HandleGravity();
        HandleCollider();
    }

    float PlayerSpeed()
    {
        if (Input.GetKey(KeyCode.LeftShift))
            return 8;
        else
            return 5;
    }

    void MovePlayer(float horizontal, float speed)
    {
        // horizontal movement
        float currentSpeed = horizontal * Speed();
        //Vector3 position = transform.position;
        //position.x += horizontal * Speed() * Time.deltaTime;
        //transform.position = position;
        rb2D.velocity = new Vector2(currentSpeed, rb2D.velocity.y);

        // vertical movement
        if(Input.GetKeyDown(KeyCode.Space))
        {
            //rb2D.AddForce(Vector2.up * JumpForce(), ForceMode2D.Impulse);
            rb2D.velocity = new Vector2(rb2D.velocity.x, JumpForce());
        }
        
    }

    float JumpForce()
    {
        //if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Space) ||
        //    Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKey(KeyCode.Space))
        if(Input.GetKey(KeyCode.LeftShift))
        {
            //Debug.Log("shift + space");
            return jumpForce + deltaJump;
        }
        else
            return jumpForce;
    }

    float Speed()
    {
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.D) ||
            Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKey(KeyCode.D))
        {
            //Debug.Log("Shift + d");
            return speed + deltaSpeed;
        }
        else
            return speed;
    }

    void PlayerLocomotion(float horizontal)
    {
        bool isRunning = Input.GetKey(KeyCode.LeftShift);

        Vector3 scale = transform.localScale;
        if(horizontal < 0)
        {
            scale.x = -1f * Mathf.Abs(scale.x);
        }
        else if(horizontal > 0)
        {
            scale.x = Mathf.Abs(scale.x);
        }

        transform.localScale = scale;

        if(isRunning && Mathf.Abs(horizontal) > 0)
        {
            animator.SetFloat("Speed", 0.3f);
        }
        else if(!isRunning && Mathf.Abs(horizontal) > 0)
        {
            animator.SetFloat("Speed", 0.1f);
        }
        else
        {
            animator.SetFloat("Speed", 0f);
        }
    }

    void PlayerCrouch()
    {
        float move = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl))
        {
            isCrouching = !isCrouching;
        }

        if(move != 0)
        {
            isCrouching = false;
        }

        animator.SetBool("isCrouching", isCrouching);

    }

    void PlayerJump()
    {

        if (Input.GetKeyDown(KeyCode.Space) && isJumping == false)
        {
            isJumping = true;
            animator.SetBool("isJumping", true);
        }
        else
        {
            animator.SetBool("isJumping", false);
        }

        isJumping = false;

    }

    void PlyaerHasGun()
    {

        if(Input.GetKeyDown(KeyCode.Q))
        {
            hasGun = !hasGun;
            animator.SetBool("hasGun", hasGun);
        }

    }

    void PlayerAttack()
    {
        if(Input.GetKeyDown(KeyCode.J) || Input.GetMouseButton(0))
        {
            animator.SetTrigger("Attack");
        }
    }

    internal void TakeDamage()
    {
        if(isInvincible)
        {
            Debug.Log("Player is invincible.");
            return;
        }

        StartCoroutine(InvincibleCoroutine());
        
    }

    void AfterDeath()
    {
        scoreController.ResetScore();
        scoreController.RefreshUI();
        LevelReload();
    }

    void LevelReload()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(currentSceneIndex);
        
    }

    void HandleGravity()
    {
        if (rb2D.velocity.y > 0) // jump 
            rb2D.gravityScale = jumpGravity;
        else if (rb2D.velocity.y < 0)  // fall
            rb2D.gravityScale = fallGravity;
        else // normal walking or running
            rb2D.gravityScale = normalGravity;
    }

    void HandleCollider()
    {
        if (isCrouching)
        {
            //Debug.Log("crouch collider");
            boxCollider.size = crouchSize;
            boxCollider.offset = crouchOffset;
        }
        else if(isJumping)
        {
            boxCollider.size = jumpingSize;
            boxCollider.offset = jumpingOffset;
        }
        else
        {
            boxCollider.size = standingSize;
            boxCollider.offset = standingOffset;
        }
    }

    public void PickupKey()
    {
        scoreController.IncreaseScore(10);
        //Debug.Log("Key collected");
    }

    private IEnumerator InvincibleCoroutine()
    {
        Debug.Log("Invincible Coroutine fxn called");
        bool playerDead = heartController.DecreaseHearts();

        if (playerDead)
        {
            animator.SetTrigger("Die");
            yield break;
        }

        animator.SetTrigger("Hurt");
        isInvincible = true;

        yield return new WaitForSeconds(invincibleTime);

        Debug.Log("After yeild return");
        
        StartCoroutine(PlayerAtStart());
    }

    IEnumerator PlayerAtStart()
    {
        Vector3 position = transform.position;
        position = starting.transform.position;
        transform.position = position;

        yield return new WaitForSeconds(1f);

        isInvincible = false;
    }

}
