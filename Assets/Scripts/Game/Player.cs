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
    public float endMatchDelay = 3f;

    private HealthBar healthBar;
    private PlayerStats playerStats;
    private int health;
    private TankController tankController;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider2D;

    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private TextMeshProUGUI restartTime;
    [SerializeField] private TextMeshProUGUI playerWinText;
    [SerializeField] private TextMeshProUGUI statsDisplay;

    void Awake() {
        tankController = GetComponent<TankController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }
    
    void Start() {
        playerStats.playerName = playerName;
        healthBar = Instantiate(healthBarPrefab, HUD.instance.GetHealthBarParent());
        healthBar.BindToTarget(transform);
        SetHealth(maxHealth);
    }

    public void DamageHealth(int damage) {
        SetHealth(health - damage);
    }

    public void Die() {
        playerStats.healthRemaining = health;
        playerStats.distanceTravelled = tankController.GetDistanceTravelled();
        playerStats.primaryWeaponStats = tankController.GetPrimaryWeaponStats();
        playerStats.secondaryWeaponStats = tankController.GetSecondaryWeaponStats();
        healthBar.SetVisible(false);
        tankController.Disable();
        spriteRenderer.enabled = false;
        boxCollider2D.enabled = false;
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
        GameOverScreen();
        yield return new WaitForSeconds(endMatchDelay);
        gameOverUI.SetActive(false);
        GameController.instance.RestartGame();
    }

    //Game Over code
    private void GameOverScreen() {
        gameOverUI.SetActive(true);
        
        restartTime.text = "Restarting game in " + endMatchDelay + " seconds";
        playerWinText.text = "Player " + "#" + " wins!!!";
        string healthRemaining = "Health Remaining: ";
        string bulletsFired = "Bullets Fired: ";
        statsDisplay.text = "Player1:\n" + healthRemaining + health.ToString() + "\n" + bulletsFired;
        //player2Stats.text = "Player2:\n" + healthRemaining + health.ToString() + "\n" + bulletsFired;
    }
}
