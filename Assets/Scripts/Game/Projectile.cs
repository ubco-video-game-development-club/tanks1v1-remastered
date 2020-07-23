using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Projectile : MonoBehaviour
{
    public string projectileName = "Projectile";
    public float speed = 6f;
    public int damage = 2;
    public float range = 10f;
    public AudioClip hitSound;

    private Rigidbody2D rb2D;
    private AudioSource audioSource;
    private SpriteRenderer spriteRenderer;
    private Vector2 startPos;
    private int tankID;
    private bool destroyed;

    void Awake() {
        rb2D = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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
        // Ignore collisions if the projectile is already destroyed
        if (destroyed) {
            return;
        }

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

        // Play audio clip on hit
        audioSource.PlayOneShot(hitSound);

        // Hide the object on collision and destroy after 1 second
        spriteRenderer.enabled = false;
        rb2D.velocity = Vector2.zero;
        destroyed = true;
        Destroy(gameObject, 1f);
    }

    public void Initialize(int tankID, Vector2 direction) {
        this.tankID = tankID;
        rb2D.velocity = direction * speed;
    }

    public int GetTankID() {
        return tankID;
    }
}
