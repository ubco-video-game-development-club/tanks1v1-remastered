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
    public AudioClip deathSound;
    public float deathSoundVolume = 0.15f;

    private HealthBar healthBar;
    private PlayerStats playerStats;
    private int health;
    private TankController tankController;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider2D;
    private Animator animator;
    private AudioSource audioSource;
    private bool isGameOver = false;
    
    void Awake() {
        tankController = GetComponent<TankController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
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
        Disable();
        animator.SetTrigger("Die");
        audioSource.volume = deathSoundVolume;
        audioSource.PlayOneShot(deathSound);
        GameController.instance.EndGame();
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

    public void Disable() {
        healthBar.SetVisible(false);
        tankController.Disable();
        boxCollider2D.enabled = false;
    }

    private void SetHealth(int health) {
        if (isGameOver) {
            return;
        }

        this.health = Mathf.Max(0, health);
        healthBar.SetHealthPercentage(((float) this.health) / maxHealth);

        if (this.health <= 0) {
            this.health = 0;
            Die();
        }
    }
}
