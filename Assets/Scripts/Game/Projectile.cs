using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Projectile : MonoBehaviour
{
    private int damage;
    private Rigidbody2D rb2D;

    void Awake() {
        rb2D = GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D col) {
        // Damage the player on hit
        Player player;
        if (col.TryGetComponent<Player>(out player)) {
            player.DamageHealth(damage);
        }

        // Destroy self on collision
        Destroy(gameObject);
    }

    public void Initialize(Vector2 velocity, int damage) {
        this.damage = damage;
        rb2D.velocity = velocity;
    }
}
