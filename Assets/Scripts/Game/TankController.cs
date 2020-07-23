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
    public AudioClip idleSound;
    public AudioClip moveSound;

    [Header("Weapons")]
    public TankWeapon primaryWeaponPrefab;
    public TankWeapon secondaryWeaponPrefab;

    [Header("Keybindings")]
    public KeyCode moveForwardKey = KeyCode.W;
    public KeyCode moveBackwardKey = KeyCode.S;
    public KeyCode turnLeftKey = KeyCode.A;
    public KeyCode turnRightKey = KeyCode.D;
    public KeyCode primaryFireKey = KeyCode.Alpha7;
    public KeyCode secondaryFireKey = KeyCode.Alpha8;

    private Vector3 facingDirection;
    private TankWeapon primaryWeapon;
    private TankWeapon secondaryWeapon;
    private bool isControllerEnabled;
    private float distanceTravelled;
    private AudioSource audioSource;
    private bool isMoving;

    void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    void Start() {
        facingDirection = startDirection.normalized;
        primaryWeapon = Instantiate(primaryWeaponPrefab, transform);
        secondaryWeapon = Instantiate(secondaryWeaponPrefab, transform);
        isControllerEnabled = true;
        isMoving = false;
        SetActiveSound(idleSound);
    }

    void Update() {
        // Don't allow inputs if disabled
        if (!isControllerEnabled || Time.timeScale == 0) {
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
        distanceTravelled += moveInput * Time.deltaTime;

        // Play movement sounds
        if (!isMoving && moveInput != 0) {
            isMoving = true;
            SetActiveSound(moveSound);
        } else if (isMoving && moveInput == 0) {
            isMoving = false;
            SetActiveSound(idleSound);
        }

        // Get weapon inputs
        if (Input.GetKey(primaryFireKey)) {
            primaryWeapon.Fire();
        }
        if (Input.GetKey(secondaryFireKey)) {
            secondaryWeapon.Fire();
        }
    }

    void FixedUpdate() {
        // Rotate to face current direction
        float facingAngle = Vector2.SignedAngle(spriteOrientation, facingDirection);
        transform.rotation = Quaternion.AngleAxis(facingAngle, Vector3.forward);
    }

    public Vector3 GetFacingDirection() {
        return facingDirection;
    }

    public int GetTankID() {
        return gameObject.GetInstanceID();
    }

    public void Disable() {
        isControllerEnabled = false;
        primaryWeapon.Disable();
        secondaryWeapon.Disable();
    }

    public float GetDistanceTravelled() {
        return distanceTravelled;
    }

    public WeaponStats GetPrimaryWeaponStats() {
        return primaryWeapon.GetWeaponStats();
    }

    public WeaponStats GetSecondaryWeaponStats() {
        return secondaryWeapon.GetWeaponStats();
    }

    private void SetActiveSound(AudioClip clip) {
        audioSource.clip = clip;
        audioSource.Play();
    }
}
