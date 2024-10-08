using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class TopDownShooting : MonoBehaviour
{
    private TopDownController controller;
    [SerializeField] private Transform projectileSpawnPosition;
    private Vector2 aimDirection = Vector2.right;


    private void Awake ()
    {
        controller = GetComponent<TopDownController>();
    }

    private void Start ()
    {
        controller.LookEvent += OnAim;
        controller.AttackEvent += OnShoot;
    }

    private void OnAim (Vector2 direction)
    {
        aimDirection = direction;
    }

    private void OnShoot (AttackSO attackSO)
    {
        RangedAttackSO rangedAttackSO = attackSO as RangedAttackSO;
        if (rangedAttackSO == null)
            return;

        int count = rangedAttackSO.numberOfProjectilePerShot;
        float angle = rangedAttackSO.multipleProjectilesAngle;

        float minAngle = -(count / 2f) * angle + 0.5f * angle;

        for (int i = 0; i < count; i++)
        {
            float randomSpread = Random.Range(-rangedAttackSO.spread, rangedAttackSO.spread);
            float nextAngle = minAngle + i * angle + randomSpread;
            CreateProjectile(rangedAttackSO, nextAngle);
        }

    }

    private void CreateProjectile (RangedAttackSO rangedAttackSO, float angle)
    {
        GameObject obj = GameManager.Instance.ObjectPool.SpawnFromPool(rangedAttackSO.bulletNameTag);
        obj.transform.position = projectileSpawnPosition.position;
        var controller = obj.GetComponent<ProjectileController>();
        controller.InitializeAttack(RotateVector2(aimDirection, angle), rangedAttackSO);
    }

    private static Vector2 RotateVector2(Vector2 v, float angle)
    {
        return Quaternion.Euler(0f, 0f, angle) * v;
    }
}
