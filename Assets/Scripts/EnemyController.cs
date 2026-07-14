using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Animator animator;
    private Rigidbody2D rb;

    [SerializeField] private Transform player;
    [SerializeField] private PlayerController playerController;

    public float distanceX;
    public float distanceY;
    public float speed;

    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        CalcDistance();
        if(ActivateEnemy())
        {
            EnemyAttack();
            EnemyLocomotion();
            //EnemyDie();
            EnemyDirection();
        }
        MoveEnemey();
    }

    void MoveEnemey()
    {
        rb.velocity = new Vector2(speed, rb.velocity.y);
    }

    void CalcDistance()
    {
        distanceX = Mathf.Abs(player.position.x - gameObject.transform.position.x);
        distanceY = Mathf.Abs(player.position.y - gameObject.transform.position.y);
        //Debug.Log("Distance: " + distanceX);
    }

    bool ActivateEnemy()
    {
        if (distanceX < 15f && distanceY < 1f)
        {
            //Debug.Log("Activate Enemy: true");
            //Debug.Log("DistanceX: " + distanceX + ", " + "DistanceY: " + distanceY);
            return true;
        }
        else
        {
            speed = 0.1f;
            animator.SetFloat("PlayerDistance", 0f);
            return false;
        }
    }
    
    void EnemyAttack()
    {
        if(distanceX < 1f && distanceY < 1f)
        {
            animator.SetBool("isAttacking", true);
        }
        else
        {
            animator.SetBool("isAttacking", false);
        }
    }

    void EnemyLocomotion()
    {
        if (distanceX < 4f && distanceY < 1f)
        {
            speed = 6f;
            animator.SetFloat("PlayerDistance", 2f);
        }
        else if(distanceX < 8f && distanceY < 1f)
        {
            speed = 4f;
            animator.SetFloat("PlayerDistance", 1f);
        }
        else
        {
            speed = 0.1f;
            animator.SetFloat("PlayerDistance", 0f);
        }
    }

    void EnemyDirection()
    {
        Vector3 scale = gameObject.transform.localScale;
        if(player.position.x - gameObject.transform.position.x > 0)
        {
            scale.x = Mathf.Abs(scale.x);
            speed = Mathf.Abs(speed);
        }
        else
        {
            scale.x = -1f * Mathf.Abs(scale.x);
            speed = -1f * Mathf.Abs( speed);
        }

        transform.localScale = scale;
    }

    void EnemyDie()
    {
        Debug.Log("Enemy died");
        //animator.SetTrigger("Die");
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.GetComponent<PlayerController>() != null)
        {
            playerController = other.gameObject.GetComponent<PlayerController>();
            playerController.TakeDamage();
        }
    }
}
