using System;
using UnityEngine;

public class TopDownRangeEnemyController : TopDownEnemyController
{
    [SerializeField][Range(0f, 100f)] private float followRange = 15f;
    [SerializeField][Range(0f, 100f)] private float shootRange = 10f;

    private int layerMaskTarget;

    protected override void Start ()
    {
        base.Start();
        layerMaskTarget = stats.CurrentStat.attackSO.target;
    }

    protected override void FixedUpdate ()
    {
        base.FixedUpdate();

        float distanceToTarget = DistanceToTarget();
        Vector2 directionToTarget = DirectionToTarget();
        UpdateEnemyState(distanceToTarget, directionToTarget);
    }

    private void UpdateEnemyState (float distanceToTarget, Vector2 directionToTarget)
    {
        IsAttacking = false;

        if (distanceToTarget <= followRange)
            CheckIfNear(distanceToTarget, directionToTarget);
    }

    private void CheckIfNear(float distance, Vector2 directionToTarget)
    {
        if (distance <= shootRange)
            TryShootAtTarget(directionToTarget);
        else
            CallMoveEvent(directionToTarget);
    }

    private void TryShootAtTarget(Vector2 directionToTarget)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToTarget, shootRange, layerMaskTarget);

        if (hit.collider != null)
            PerfromAttackAction(directionToTarget);
        else
            CallMoveEvent(directionToTarget);
    }

    private void PerfromAttackAction (Vector2 direction)
    {
        CallLookEvent(direction);
        CallMoveEvent(Vector2.zero);
        IsAttacking = true;
    }
}
