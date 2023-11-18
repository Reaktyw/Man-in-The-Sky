using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private const string IS_WALKING = "IsWalking";
    private const string IS_JUMPING = "IsJumping";
    private const string IS_CLIMBING = "IsClimbing";
    private const string IS_DEAD = "IsDead";
    private const string IS_FALLING = "IsFalling";

    [SerializeField] private Player player;
    [SerializeField] private Animator animator;

    void Update()
    {
        animator.SetBool(IS_WALKING, player.IsWalking());
        animator.SetBool(IS_JUMPING, player.IsJumping());
        animator.SetBool(IS_CLIMBING, player.IsClimbing());
        animator.SetBool(IS_FALLING, player.IsFalling());
        animator.SetBool(IS_DEAD, player.IsDead());
    }
}
