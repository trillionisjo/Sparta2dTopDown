using System;
using UnityEngine;

public class TopDownShooting : MonoBehaviour
{
    private TopDownController controller;
    [SerializeField] private Transform projectileSpawnPosition;
    private Vector2 aimDirection = Vector2.right;

    public GameObject TestPrefab;

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

    private void OnShoot ()
    {
        CreateProjectile();
    }

    private void CreateProjectile ()
    {
        Instantiate(TestPrefab, projectileSpawnPosition.position, Quaternion.identity);
    }

}
