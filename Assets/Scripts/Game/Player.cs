using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TankController))]
public class Player : MonoBehaviour
{
    public int maxHealth;

    private int health;
    private TankController tankController;

    void Awake() {
        tankController = GetComponent<TankController>();
    }
    
    void Start() {
        health = maxHealth;
    }

    public void DamageHealth(int damage) {
        health -= damage;
        if (health <= 0) {
            Die();
        }
    }

    public void Die() {
        tankController.Disable();
    }
}
