using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankWeapon : MonoBehaviour
{
    public Projectile projectilePrefab;
    public Transform projectileSpawn;
    public Vector2 projectileSpriteOrientation = Vector2.right;
    public Vector2 spriteOffset;
    public float projectileSpeed = 6f;
    public int projectileDamage = 2;
    public float projectileRange = 10f;
    public float cooldown = 1f;
    public int burstCount = 5;
    public float burstInterval = 0.2f;

    private TankController tank;
    private SpriteRenderer spriteRenderer;
    private bool isWeaponEnabled;

    void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start() {
        tank = transform.parent.GetComponent<TankController>();
        transform.position = (Vector2)tank.transform.position + spriteOffset;
        isWeaponEnabled = true;
    }

    public void Fire() {
        if (isWeaponEnabled) {
            StartCoroutine(WeaponCooldown());
            StartCoroutine(FireSequence());
        }
    }

    public void Disable() {
        spriteRenderer.enabled = false;
        isWeaponEnabled = false;
    }

    private IEnumerator WeaponCooldown() {
        isWeaponEnabled = false;
        yield return new WaitForSeconds(cooldown);
        isWeaponEnabled = true;
    }

    private IEnumerator FireSequence() {
        for (int i = 0; i < burstCount; i++) {
            ShootProjectile();
            yield return new WaitForSeconds(burstInterval);
        }
    }

    private void ShootProjectile() {
        Quaternion projectileRotation = Quaternion.FromToRotation(projectileSpriteOrientation, tank.GetFacingDirection());
        Projectile projectile = Instantiate(projectilePrefab, projectileSpawn.position, projectileRotation);
        projectile.Initialize(tank.gameObject.GetInstanceID(), tank.GetFacingDirection() * projectileSpeed, projectileDamage, projectileRange);
    }
}
