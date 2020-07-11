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
    private int tankID;

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
        // Ignore collisions with other projectiles from the same tank
        Projectile proj;
        if (col.TryGetComponent<Projectile>(out proj)) {
            if (proj.GetTankID() == tankID) {
                return;
            }
        }

        // Ignore collisions with the original tank
        TankController tank;
        if (col.TryGetComponent<TankController>(out tank)) {
            if (tank.GetTankID() == tankID) {
                return;
            }
        }

        // Damage the player on hit
        Player player;
        if (col.TryGetComponent<Player>(out player)) {
            player.DamageHealth(damage);
        }

        // Destroy self on collision
        Destroy(gameObject);
    }

    public void Initialize(int tankID, Vector2 velocity, int damage, float range) {
        rb2D.velocity = velocity;
        this.tankID = tankID;
        this.damage = damage;
        this.range = range;
    }

    public int GetTankID() {
        return tankID;
    }
}
