using System;
using UnityEngine;

public class TopDownController : MonoBehaviour
{
    public event Action<Vector2> MoveEvent;
    public event Action<Vector2> LookEvent;
    public event Action<AttackSO> AttackEvent;

    protected bool IsAttacking { get; set; }
    private float timeSinceLastAttack = float.MaxValue;

    protected CharacterStatsHandler stats { get; private set; }

    protected virtual void Awake ()
    {
        stats = GetComponent<CharacterStatsHandler>();
    }

    private void Update ()
    {
        HandleAttackDelay();
    }

    private void HandleAttackDelay ()
    {
        if (timeSinceLastAttack < stats.CurrentStat.attackSO.delay) 
            timeSinceLastAttack += Time.deltaTime;
        else if (IsAttacking)
        {
            timeSinceLastAttack = 0;
            CallAttackEvent(stats.CurrentStat.attackSO);
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

    private void CallAttackEvent (AttackSO attackSO)
    {
        AttackEvent?.Invoke(attackSO);
    }
}