using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    public Sprite destroyedSprite;
    public AudioClip destroySound;

    private bool destroyed;
    private AudioSource audioSource;
    private SpriteRenderer spriteRenderer;
    private CircleCollider2D circleCollider;

    void Awake() {
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        circleCollider = GetComponent<CircleCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (destroyed) {
            return;
        }

        Projectile projectile;
        if (col.TryGetComponent<Projectile>(out projectile)) {
            DestroyBarrel();
        }
    }

    private void DestroyBarrel() {
        destroyed = true;
        audioSource.PlayOneShot(destroySound);
        spriteRenderer.sprite = destroyedSprite;
        circleCollider.enabled = false;
    }
}
