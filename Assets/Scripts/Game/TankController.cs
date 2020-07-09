using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : MonoBehaviour
{
    [Header("Movement")]
    public Vector2 spriteOrientation = Vector2.right;
    public Vector2 startDirection = Vector2.right;
    public float forwardSpeed = 4f;
    public float backwardSpeed = 4f;
    public float turnSpeed = 90f;

    [Header("Weapons")]
    public Projectile primaryWeaponPrefab;
    public Transform primaryWeaponSpawn;
    public float primaryWeaponSpeed = 7f;
    public int primaryWeaponDamage = 5;
    public float primaryWeaponCooldown = 0.8f;
    public Projectile secondaryWeaponPrefab;
    public Transform secondaryWeaponSpawn;
    public float secondaryWeaponSpeed = 7f;
    public int secondaryWeaponDamage = 2;
    public float secondaryWeaponCooldown = 0.2f;

    [Header("Keybindings")]
    public KeyCode moveForwardKey = KeyCode.W;
    public KeyCode moveBackwardKey = KeyCode.S;
    public KeyCode turnLeftKey = KeyCode.A;
    public KeyCode turnRightKey = KeyCode.D;
    public KeyCode primaryFireKey = KeyCode.Alpha7;
    public KeyCode secondaryFireKey = KeyCode.Alpha8;

    private Vector3 facingDirection;
    private bool isPrimaryWeaponEnabled;
    private bool isSecondaryWeaponEnabled;
    private bool isControllerEnabled;

    void Start() {
        facingDirection = startDirection.normalized;
        isPrimaryWeaponEnabled = true;
        isSecondaryWeaponEnabled = true;
        isControllerEnabled = true;
    }

    void Update() {
        // Don't allow inputs if disabled
        if (!isControllerEnabled) {
            return;
        }

        // Get turning inputs
        float turnInput = 0f;
        if (Input.GetKey(turnLeftKey)) {
            turnInput += turnSpeed;
        }
        if (Input.GetKey(turnRightKey)) {
            turnInput -= turnSpeed;
        }

        // Apply turning
        facingDirection = Quaternion.AngleAxis(turnInput * Time.deltaTime, Vector3.forward) * facingDirection;

        // Get movement inputs
        float moveInput = 0f;
        if (Input.GetKey(moveForwardKey)) {
            moveInput += forwardSpeed;
        }
        if (Input.GetKey(moveBackwardKey)) {
            moveInput -= backwardSpeed;
        }

        // Apply movement
        transform.position += facingDirection * moveInput * Time.deltaTime;

        // Get weapon inputs
        if (Input.GetKey(primaryFireKey) && isPrimaryWeaponEnabled) {
            StartCoroutine(PrimaryWeaponCooldown());
            Projectile primaryWeapon = Instantiate(primaryWeaponPrefab, primaryWeaponSpawn.position, Quaternion.identity);
            primaryWeapon.Initialize(facingDirection * primaryWeaponSpeed, primaryWeaponDamage);
        }
        if (Input.GetKey(secondaryFireKey) && isSecondaryWeaponEnabled) {
            StartCoroutine(SecondaryWeaponCooldown());
            Projectile secondaryWeapon = Instantiate(secondaryWeaponPrefab, secondaryWeaponSpawn.position, Quaternion.identity);
            secondaryWeapon.Initialize(facingDirection * secondaryWeaponSpeed, secondaryWeaponDamage);
        }
    }

    void FixedUpdate() {
        // Rotate to face current direction
        transform.rotation = Quaternion.FromToRotation(spriteOrientation, facingDirection);
    }

    public void Disable() {
        isControllerEnabled = false;
    }

    private IEnumerator PrimaryWeaponCooldown() {
        isPrimaryWeaponEnabled = false;
        yield return new WaitForSeconds(primaryWeaponCooldown);
        isPrimaryWeaponEnabled = true;
    }

    private IEnumerator SecondaryWeaponCooldown() {
        isSecondaryWeaponEnabled = false;
        yield return new WaitForSeconds(secondaryWeaponCooldown);
        isSecondaryWeaponEnabled = true;
    }
}
