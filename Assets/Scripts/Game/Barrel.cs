using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    public Sprite destroyedSprite;

    private bool destroyed;
    private SpriteRenderer spriteRenderer;
    private CircleCollider2D circleCollider;

    void Awake() {
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
        spriteRenderer.sprite = destroyedSprite;
        circleCollider.enabled = false;
    }
}
