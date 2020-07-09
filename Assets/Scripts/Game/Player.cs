using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TankController))]
public class Player : MonoBehaviour
{
    public HealthBar healthBarPrefab;
    public int maxHealth = 30;
    public float endMatchDelay = 3f;

    private HealthBar healthBar;
    private int health;
    private TankController tankController;
    private SpriteRenderer spriteRenderer;

    void Awake() {
        tankController = GetComponent<TankController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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
        healthBar.SetVisible(false);
        tankController.Disable();
        spriteRenderer.enabled = false;
        StartCoroutine(EndMatch());
    }

    private void SetHealth(int health) {
        this.health = health;
        healthBar.SetHealthPercentage(((float) health) / maxHealth);
        if (health <= 0) {
            Die();
        }
    }

    private IEnumerator EndMatch() {
        yield return new WaitForSeconds(endMatchDelay);
        GameController.instance.RestartGame();
    }
}
