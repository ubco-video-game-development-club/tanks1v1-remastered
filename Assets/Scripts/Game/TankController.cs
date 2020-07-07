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

    [Header("Keybindings")]
    public KeyCode moveForwardKey = KeyCode.W;
    public KeyCode moveBackwardKey = KeyCode.S;
    public KeyCode turnLeftKey = KeyCode.A;
    public KeyCode turnRightKey = KeyCode.D;

    private Vector3 facingDirection;

    void Start() {
        facingDirection = startDirection;
    }

    void Update() {
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
    }

    void FixedUpdate() {
        // Rotate to face current direction
        transform.rotation = Quaternion.FromToRotation(spriteOrientation, facingDirection);
    }
}
