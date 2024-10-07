using System;
using UnityEngine;

public class TopDownController : MonoBehaviour
{
    public event Action<Vector2> MoveEvent;
    public event Action<Vector2> LookEvent;
    public event Action AttackEvent;

    protected bool IsAttacking { get; set; }
    private float timeSinceLastAttack = float.MaxValue;

    private void Update ()
    {
        HandleAttackDelay();
    }

    private void HandleAttackDelay ()
    {
        if (timeSinceLastAttack < 0.2f)
            timeSinceLastAttack += Time.deltaTime;
        else if (IsAttacking)
        {
            timeSinceLastAttack = 0;
            CallAttackEvent();
        }
    }

    public void CallMoveEvent(Vector2 direction)
    {
        MoveEvent?.Invoke(direction);
    }

    public void CallLookEvent(Vector2 direction)
    {
        LookEvent?.Invoke(direction);
    }

    private void CallAttackEvent ()
    {
        AttackEvent?.Invoke();
    }
}
