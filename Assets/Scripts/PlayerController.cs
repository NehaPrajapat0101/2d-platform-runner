using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public BoxCollider2D boxCollider;
    public float speed;
    public float jumpForce;

    private Vector2 standingSize;
    private Vector2 standingOffset;
    private Rigidbody2D rb2D;

    [SerializeField] private Vector2 crouchSize;
    [SerializeField] private Vector2 crouchOffset;

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
        MovePlayer(horizontal, speed);
    }

    float PlayerSpeed()
    {
        if (Input.GetKey(KeyCode.LeftShift))
            return 5;
        else
            return 3;
    }

    void MovePlayer(float horizontal, float speed)
    {
        // horizontal movement
        Vector3 position = transform.position;
        position.x += horizontal * speed * Time.deltaTime;
        transform.position = position;

        // vertical movement
        if(Input.GetKeyDown(KeyCode.Space))
        {
            rb2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        
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

        if(isCrouching)
        {
            boxCollider.size = crouchSize;
            boxCollider.offset = crouchOffset;
        }
        else
        {
            boxCollider.size = standingSize;
            boxCollider.offset = standingOffset;
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

    

}
