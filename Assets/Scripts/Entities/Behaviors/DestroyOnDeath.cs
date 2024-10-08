using System;
using UnityEngine;

public class DestroyOnDeath : MonoBehaviour
{
    private HealthSystem healthSystem;
    private Rigidbody2D rigidbody2d;

    private void Awake ()
    {
        healthSystem = GetComponent<HealthSystem>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        healthSystem.Died += OnDeath;
    }

    private void OnDeath ()
    {
        rigidbody2d.velocity = Vector2.zero;

        foreach (var renderer in GetComponentsInChildren<SpriteRenderer>())
        {
            Color color = renderer.color;
            color.a = 0.3f;
            renderer.color = color;
        }

        foreach (Behaviour behaviour in GetComponentsInChildren<Behaviour>())
            behaviour.enabled = false;

        Destroy(gameObject, 2f);
    }
}
