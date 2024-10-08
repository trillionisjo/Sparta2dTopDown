using System;
using UnityEngine;

public class CharacterAnimationController : AnimationController
{
    private static readonly int isWalking = Animator.StringToHash("isWalking");
    private static readonly int isHit = Animator.StringToHash("isHit");
    private static readonly int attack = Animator.StringToHash("attack");

    private readonly float magnitudeThreshold = 0.5f;

    protected override void Awake ()
    {
        base.Awake();
    }

    private void Start ()
    {
        controller.MoveEvent += OnMoving;
        controller.AttackEvent += OnAttack;

    }
    private void OnMoving (Vector2 vector)
    {
        animator.SetBool(isWalking, vector.magnitude > magnitudeThreshold);
    }

    private void OnAttack (AttackSO attackSO)
    {
        animator.SetTrigger(attack);
    }

    private void OnHit ()
    {
        animator.SetBool(isHit, true);
    }

    private void InvincibilityEnd ()
    {
        animator.SetBool(isHit, false);
    }
}
