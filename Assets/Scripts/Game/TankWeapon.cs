using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankWeapon : MonoBehaviour
{
    public string weaponName = "Weapon";
    public Vector2 spriteOffset;
    public Projectile projectilePrefab;
    public Transform projectileSpawn;
    public Vector2 projectileSpriteOrientation = Vector2.right;
    public string projectileName = "Projectile";
    public float projectileSpeed = 6f;
    public int projectileDamage = 2;
    public float projectileRange = 10f;
    public float cooldown = 1f;
    public int burstCount = 5;
    public float burstInterval = 0.2f;

    private TankController tank;
    private SpriteRenderer spriteRenderer;
    private bool isWeaponEnabled;
    private WeaponStats weaponStats;

    void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start() {
        // Initialize weapon stats
        weaponStats = new WeaponStats();
        weaponStats.weaponName = weaponName;
        weaponStats.projectileName = projectileName;
        weaponStats.projectilesFired = 0;

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

    public WeaponStats GetWeaponStats() {
        return weaponStats;
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
        weaponStats.projectilesFired++;
    }
}
