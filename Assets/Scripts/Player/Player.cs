using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;
using static UnityEditor.Searcher.SearcherWindow.Alignment;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    [SerializeField] private float range;
    [SerializeField] private float colliderDistance;
    [SerializeField] private int damage;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private Health enemyHealth;
    [SerializeField] private Collider2D enemy;

    private bool isWalking = false;
    private bool isJumping = false;
    private bool isClimbing = false;
    private bool isFalling = false;
    private bool isFacingRight = true;
    public bool isDead = false;

    private float horizontal;
    private Vector2 groundCheckSize = new Vector2(1f, 0.1f);
    private readonly float wallCheckRadius = 0.01f;
    private readonly float moveSpeed = 4f;
    private readonly float climbingSpeed = 3f;
    private readonly float jumpingPower = 8f;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private Animator anim;


    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (!isDead)
        {
            rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);

            Walk();
            Climb();
            Jump();
            anim.SetBool("Hit", false);

            if (Input.GetKey(KeyCode.J))
            {
                anim.SetBool("Hit", true);
                if (EnemyInSight())
                {
                    DamageEnemy();
                }
            }
        }
        else
        {
            rb.velocity = new Vector2(0f, 0f);
        }
    }

    private void Walk()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }

        if (horizontal != 0f && !IsJumping())
        {
            isWalking = true;
        }
        else if (horizontal == 0f && !IsJumping())
        {
            isWalking = false;
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0, groundLayer)
            || Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0, wallLayer);
    }

    private void Jump()
    {
        if (!IsGrounded() && !IsClimbing())
        {
            if (rb.velocity.y > 0)
            {
                isJumping = true;
            }
            else if (rb.velocity.y < 0)
            {
                isFalling = true;
                isJumping = false;
            }
        }
        else
        {
            isJumping = false;
            isFalling = false;
        }

        if (Input.GetKeyDown(KeyCode.W) && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }
        else if (Input.GetKeyUp(KeyCode.W) && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
    }

    private void Climb()
    {
        if (Physics2D.OverlapCircle(wallCheck.position, wallCheckRadius, wallLayer) && horizontal != 0f && Input.GetKey(KeyCode.W))
        {
            isClimbing = true;
            isWalking = false;
            isJumping = false;
            isFalling = false;
            rb.velocity = new Vector2(0f, climbingSpeed);
        }
        else
        {
            isClimbing = false;
        }
    }

    public bool IsWalking()
    {
        return isWalking;
    }

    public bool IsJumping()
    {
        return isJumping;
    }

    public bool IsClimbing()
    {
        return isClimbing;
    }

    public bool IsDead()
    {
        return isDead;
    }

    public bool IsFalling()
    {
        return isFalling;
    }




    private bool EnemyInSight()
    {
        RaycastHit2D hit =
            Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0, enemyLayer);
        if (hit.collider != null)
        {
            enemyHealth = hit.transform.GetComponent<Health>();
        }
        return hit.collider != null;
    }

    private void DamageEnemy()
    {
        if (EnemyInSight())
        {
            enemyHealth.TakeDamage(damage, enemy);
        }
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
