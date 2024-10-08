using System;
using UnityEngine;

public class TopDownContactEnemyController : TopDownEnemyController
{
    [SerializeField][Range(0f, 100f)] private float followRange;
    [SerializeField] private string targetTag = "Player";
    private bool isColldingWithTarget;

    [SerializeField] private SpriteRenderer characterRenderer;

    private HealthSystem healthSystem;
    private HealthSystem collidingTargetHealthSystem;
    private TopDownMovement collidingMovement;

    protected override void Awake ()
    {
        base.Awake();
        healthSystem = GetComponent<HealthSystem>();
        healthSystem.Damaged += OnDamaged;
    }

    protected override void Start ()
    {
        base.Start();
    }

    protected override void FixedUpdate ()
    {
        base.FixedUpdate();

        if (isColldingWithTarget)
            ApplyHealthChange();

        Vector2 direction = Vector2.zero;
        if (DistanceToTarget() < followRange)
            direction = DirectionToTarget();

        Rotate(direction);
        CallMoveEvent(direction);
    }

    private void ApplyHealthChange ()
    {
        var attackSO = stats.CurrentStat.attackSO;
        bool isAttackable = collidingTargetHealthSystem.ChangeHealth(-attackSO.power);

        if (isAttackable
            && attackSO.isOnKnockback
            && collidingMovement != null)
            collidingMovement.ApplyKnockback(transform, attackSO.power, attackSO.knockbackPower);
    }

    private void Rotate (Vector2 direction)
    {
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        characterRenderer.flipX = Mathf.Abs(rotZ) > 90f;
    }

    private void OnDamaged ()
    {
        followRange = 100f;
    }

    //private void OnTriggerEnter2D (Collider2D collision)
    //{
    //    var obj = collision.gameObject;
    //    if (!obj.CompareTag(targetTag))
    //        return;

    //    collidingTargetHealthSystem = obj.GetComponent<HealthSystem>();
    //    if (collidingTargetHealthSystem != null)
    //        isColldingWithTarget = true;

    //    collidingMovement = obj.GetComponent<TopDownMovement>();
    //}

    //private void OnTriggerExit2D (Collider2D collision)
    //{
    //    if (!collision.CompareTag(targetTag))
    //        return;

    //    isColldingWithTarget = false;
    //}

    private void OnCollisionEnter2D (Collision2D collision)
    {
        var obj = collision.gameObject;
        if (!obj.CompareTag(targetTag))
            return;

        collidingTargetHealthSystem = obj.GetComponent<HealthSystem>();
        if (collidingTargetHealthSystem != null)
            isColldingWithTarget = true;

        collidingMovement = obj.GetComponent<TopDownMovement>();
    }

    private void OnCollisionExit2D (Collision2D collision)
    {
        var obj = collision.gameObject;
        if (!obj.CompareTag(targetTag))
            return;

        isColldingWithTarget = false;
    }
}
