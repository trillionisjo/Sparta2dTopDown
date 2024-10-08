using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private float healthChangeDelay = 0.5f;
    private CharacterStatsHandler characterStats;
    private float timeSinceLastChange = float.MaxValue;
    private bool isAttacked = false;

    public event Action Damaged;
    public event Action Healed;
    public event Action Died;
    public event Action InvincibilityEnded;

    public float CurrentHealth { get; private set; }
    private float MaxHealth => characterStats.CurrentStat.maxHealth;

    private void Awake ()
    {
        characterStats = GetComponent<CharacterStatsHandler>();
    }

    private void Start ()
    {
        CurrentHealth = MaxHealth;
    }

    private void Update ()
    {
        if (isAttacked && timeSinceLastChange < healthChangeDelay)
        {
            timeSinceLastChange += Time.deltaTime;
            if (timeSinceLastChange >= healthChangeDelay)
            {
                InvincibilityEnded.Invoke();
                isAttacked = false;
            }
        }
    }

    public bool ChangeHealth(float amount)
    {
        if (timeSinceLastChange < healthChangeDelay)
            return false;

        timeSinceLastChange = 0f;
        CurrentHealth += amount;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0f, MaxHealth);

        if (CurrentHealth == 0f)
        {
            Died?.Invoke();
            return true;
        }

        if (amount > 0f)
            Healed?.Invoke();
        else
        {
            Damaged?.Invoke();
            isAttacked = true;
        }

        return true;
    }
}
