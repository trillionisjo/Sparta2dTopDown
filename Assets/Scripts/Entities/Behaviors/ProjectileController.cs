using System;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] private LayerMask levelCollisionLayer;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigidbody2d;
    private TrailRenderer trailRenderer;

    private RangedAttackSO rangedAttackSO;
    private float currentDuration;
    private Vector2 direction;
    private bool isReady;
    private bool fxOnDestroy = true;

    private void Awake ()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        trailRenderer = GetComponent<TrailRenderer>();
    }

    private void Update ()
    {
        if (!isReady)
            return;

        currentDuration += Time.deltaTime;
        if (currentDuration >= rangedAttackSO.duration)
            DestroyProjectile(transform.position, false);
    }

    private void FixedUpdate ()
    {
        rigidbody2d.velocity = direction * rangedAttackSO.speed;
    }

    public void InitializeAttack(Vector2 direction, RangedAttackSO rangedAttackSO)
    {
        this.rangedAttackSO = rangedAttackSO;
        this.direction = direction;
        UpdateProjectileSprite();
        trailRenderer.Clear();
        currentDuration = 0;
        spriteRenderer.color = rangedAttackSO.projectileColor;
        transform.right = direction;
        isReady = true;
    }

    private void DestroyProjectile (Vector3 position, bool createFx)
    {
        if (createFx)
        {
        }
        gameObject.SetActive(false);
    }

    private void UpdateProjectileSprite()
    {
        transform.localScale = Vector3.one * rangedAttackSO.size;
    }

    private void OnTriggerEnter2D (Collider2D collision)
    {
        if (IsLayerMatched(levelCollisionLayer.value, collision.gameObject.layer))
        {
            Vector2 destroyPosition = collision.ClosestPoint(transform.position) - direction * 0.2f;
            DestroyProjectile(destroyPosition, fxOnDestroy);
        }
        else if (IsLayerMatched(rangedAttackSO.target.value, collision.gameObject.layer))
        {
            var hs = collision.GetComponent<HealthSystem>();
            if (hs != null)
            {
                bool isAttackApplied = hs.ChangeHealth(-rangedAttackSO.power);
                if (isAttackApplied && rangedAttackSO.isOnKnockback)
                    ApplyKnockback(collision);
            }
            DestroyProjectile(collision.ClosestPoint(transform.position), fxOnDestroy);
        }
    }

    private bool IsLayerMatched (int value, int layer)
    {
        return value == (value | 1 << layer);
    }

    private void ApplyKnockback (Collider2D collision)
    {
        var movement = collision.GetComponent<TopDownMovement>();
        if (movement == null)
            return;

        movement.ApplyKnockback(transform, rangedAttackSO.knockbackPower, rangedAttackSO.knockbackTime);
    }
}