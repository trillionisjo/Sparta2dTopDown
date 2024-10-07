using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatsHandler : MonoBehaviour
{
    [SerializeField] private CharacterStat baseStat;
    public CharacterStat CurrentStat { get; set; }
    public List<CharacterStat> statModifiers = new List<CharacterStat>();

    private void Awake ()
    {
        UpdateCharacterStat();
    }

    private void UpdateCharacterStat ()
    {
        AttackSO attackSO = Instantiate(baseStat.attackSO);
        CurrentStat = new CharacterStat { attackSO = attackSO };
        CurrentStat.statChangeType = baseStat.statChangeType;
        CurrentStat.maxHealth = baseStat.maxHealth;
        CurrentStat.speed = baseStat.speed;
    }
}
