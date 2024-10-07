using System;
using UnityEngine;

public class TopDownMovement : MonoBehaviour
{
    private TopDownController controller;
    private Rigidbody2D movementRigidbody;
    private CharacterStatsHandler characterStatsHandler;

    private Vector2 movementDirection = Vector2.zero;

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
    }

    private void OnCharacterMove (Vector2 direction)
    {
        movementDirection = direction;
    }

    private void ApplyMovement (Vector2 direction)
    {
        direction *= characterStatsHandler.CurrentStat.speed;
        movementRigidbody.velocity = direction;
    }
}
