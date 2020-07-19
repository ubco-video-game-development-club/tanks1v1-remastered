using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public KeyCode restartGameKey = KeyCode.Space;

    public Player player1;
    public Player player2;
    public PlayerStats stats;
    public WeaponStats wStats;

    public GameObject gameOverUI;
    public TextMeshProUGUI restartTime;
    public TextMeshProUGUI playerWinText;
    public TextMeshProUGUI statsDisplay1;
    public TextMeshProUGUI statsDisplay2;

    void Awake() {
        if (instance != null) {
            Destroy(gameObject);
        } else {
            instance = this;
        }
    }

    void Update() {
        if (Input.GetKeyDown(restartGameKey)) {
            RestartGame();
        }
    }

    public void RestartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private IEnumerator EndMatch() {
        GameOverScreen();
        yield return new WaitForSeconds(player1.endMatchDelay);
        gameOverUI.SetActive(false);
        instance.RestartGame();
    }

    public void EndGame() {
        StartCoroutine(EndMatch());
    }

    private void GameOverScreen() {
        gameOverUI.SetActive(true);

        restartTime.text = "Restarting game in " + player1.endMatchDelay + " seconds";
        playerWinText.text = stats.playerName + " wins!!!";
        string healthRemaining = "Health Remaining: " + stats.healthRemaining.ToString();
        string distanceTravelled = "Distance travelled" + stats.distanceTravelled.ToString();
        string primaryBulletsFired = "Primary Bullets Fired: " + wStats.projectilesFired.ToString();
        string secondaryBulletsFired = "Secondary Bullets Fired: " + wStats.projectilesFired.ToString();

        statsDisplay1.text = "Player1\n" + healthRemaining + "\n" + distanceTravelled + "\n" + primaryBulletsFired + "\n" + secondaryBulletsFired;

        statsDisplay2.text = "Player2\n" + healthRemaining + "\n" + distanceTravelled + "\n" + primaryBulletsFired + "\n" + secondaryBulletsFired; ;
        //player2Stats.text = "Player2:\n" + healthRemaining + health.ToString() + "\n" + bulletsFired;
    }
}
