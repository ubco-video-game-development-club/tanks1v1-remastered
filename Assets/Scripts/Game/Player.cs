using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TankController))]
public class Player : MonoBehaviour
{
    public string playerName = "Player";
    public HealthBar healthBarPrefab;
    public int maxHealth = 30;
    public StatsDisplay statsDisplay;

    private HealthBar healthBar;
    private PlayerStats playerStats;
    private int health;
    private TankController tankController;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider2D;
    private bool isGameOver = false;
    
    void Awake() {
        tankController = GetComponent<TankController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }
    
    void Start() {
        playerStats = new PlayerStats();
        playerStats.playerName = playerName;
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
        boxCollider2D.enabled = false;
        GameController.instance.EndGame();
    }

    private void SetHealth(int health) {
        if (isGameOver) {
            return;
        }

        this.health = health;
        healthBar.SetHealthPercentage(((float) health) / maxHealth);

        if (health <= 0) {
            Die();
        }
    }

    public bool EndGame() {
        isGameOver = true;

        playerStats.healthRemaining = health;
        playerStats.distanceTravelled = tankController.GetDistanceTravelled();
        playerStats.primaryWeaponStats = tankController.GetPrimaryWeaponStats();
        playerStats.secondaryWeaponStats = tankController.GetSecondaryWeaponStats();

        statsDisplay.SetStats(playerStats);
        return health > 0;
    }
}
