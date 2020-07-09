using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Projectile : MonoBehaviour
{
    private int damage;
    private float range;
    private Rigidbody2D rb2D;
    private Vector2 startPos;

    void Awake() {
        rb2D = GetComponent<Rigidbody2D>();
    }

    void Start() {
        startPos = transform.position;
    }

    void Update() {
        // Destroy self after travelling max range
        if (Vector2.Distance(startPos, transform.position) > range) {
            Destroy(gameObject);
        }
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

    public void Initialize(Vector2 velocity, int damage, float range) {
        rb2D.velocity = velocity;
        this.damage = damage;
        this.range = range;
    }
}
