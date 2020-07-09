using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TankController))]
public class Player : MonoBehaviour
{
    public HealthBar healthBarPrefab;
    public int maxHealth;

    private HealthBar healthBar;
    private int health;
    private TankController tankController;

    void Awake() {
        tankController = GetComponent<TankController>();
    }
    
    void Start() {
        healthBar = Instantiate(healthBarPrefab, HUD.instance.GetHealthBarParent());
        healthBar.BindToTarget(transform);

        SetHealth(maxHealth);
    }

    public void DamageHealth(int damage) {
        SetHealth(health - damage);
    }

    public void Die() {
        Destroy(healthBar.gameObject);
        Destroy(gameObject);
    }

    private void SetHealth(int health) {
        this.health = health;
        healthBar.SetHealthPercentage(((float) health) / maxHealth);
        if (health <= 0) {
            Die();
        }
    }
}
