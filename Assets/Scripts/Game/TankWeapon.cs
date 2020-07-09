using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankWeapon : MonoBehaviour
{
    public Projectile projectilePrefab;
    public Vector2 spawnOffset;
    public float projectileSpeed = 6f;
    public int projectileDamage = 2;
    public float projectileRange = 10f;
    public float cooldown = 1f;
    public int burstCount = 5;
    public float burstInterval = 0.2f;

    private TankController tank;
    private bool isWeaponEnabled;

    void Start() {
        tank = transform.parent.GetComponent<TankController>();
        transform.position = (Vector2)tank.transform.position + spawnOffset;
        isWeaponEnabled = true;
    }

    public void Fire() {
        if (isWeaponEnabled) {
            StartCoroutine(WeaponCooldown());
            StartCoroutine(FireSequence());
        }
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
        Projectile projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        projectile.Initialize(tank.GetFacingDirection() * projectileSpeed, projectileDamage, projectileRange);
    }
}
