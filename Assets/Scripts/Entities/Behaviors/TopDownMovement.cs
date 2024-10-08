using System;
using UnityEngine;

public class TopDownMovement : MonoBehaviour
{
    private TopDownController controller;
    private Rigidbody2D movementRigidbody;
    private CharacterStatsHandler characterStatsHandler;

    private Vector2 movementDirection = Vector2.zero;
    private Vector2 knockback = Vector2.zero;
    private float knockbackDuration = 0f;

    private void Awake ()
    {
        controller = GetComponent<TopDownController>();
        movementRigidbody = GetComponent<Rigidbody2D>();
        characterStatsHandler = GetComponent<CharacterStatsHandler>();
    }

    private void Start ()
    {
        controller.MoveEvent += OnCharacterMove;
    }

    private void FixedUpdate ()
    {
        ApplyMovement(movementDirection);

        if (knockbackDuration > 0f)
            knockbackDuration -= Time.deltaTime;
    }

    private void OnCharacterMove (Vector2 direction)
    {
        movementDirection = direction;
    }

    private void ApplyMovement (Vector2 direction)
    {
        direction *= characterStatsHandler.CurrentStat.speed;
        if (knockbackDuration > 0f)
            direction += knockback;
        movementRigidbody.velocity = direction;
    }

    public void ApplyKnockback(Transform other, float power, float duration)
    {
        knockbackDuration = duration;
        knockback = (transform.position - other.position).normalized * power;
    }
}
