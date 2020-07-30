using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public float endDelay = 3f;
    public KeyCode restartGameKey = KeyCode.Space;
    public KeyCode pauseGameKey = KeyCode.Escape;
    public Player player1;
    public Player player2;

    private bool isPaused = false;

    void Awake() {
        if (instance != null) {
            Destroy(gameObject);
        } else {
            instance = this;
        }
    }

    void Start() {
        MusicPlayer.instance.SetGameVolume();
    }

    void Update() {
        if (Input.GetKeyDown(restartGameKey)) {
            RestartGame();
        }

        if (Input.GetKeyDown(pauseGameKey)) {
            if (isPaused) {
                ResumeGame();
            } else {
                PauseGame();
            }
        }
    }

    public void ResumeGame() {
        //Resume Game Steps:
        // 1) Close Pause Menu
        // 2) Start/Continue Gameplay (Usually by setting timescale to 1)

        isPaused = false;
        Time.timeScale = 1f;
        MusicPlayer.instance.SetGameVolume();
        HUD.instance.SetPauseMenuActive(false);
    }

    public void PauseGame() {
        //Pause Game Steps:
        // 1) Pause Gameplay (Usually by setting timescale to 0)
        // 2) Open Pause Menu

        isPaused = true;
        Time.timeScale = 0f;
        MusicPlayer.instance.SetMenuVolume();
        HUD.instance.SetPauseMenuActive(true);
    }

    public void RestartGame() {
        Time.timeScale = 1f;
        MusicPlayer.instance.SetGameVolume();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        isPaused = false;
    }

    public void BackToMainMenu() {
        Time.timeScale = 1f;
        MusicPlayer.instance.SetMenuVolume();
        SceneManager.LoadScene("MainMenu");
        isPaused = false;
    }

    //Delay end screen for x amount of seconds and shows player stats
    private IEnumerator DelayEndScreen() {
        yield return new WaitForSeconds(endDelay);
        player1.Disable();
        player2.Disable();
        MusicPlayer.instance.SetMenuVolume();
        HUD.instance.SetGameOverMenuActive(true);
    }

    public void EndGame() {
        StartCoroutine(DelayEndScreen());

        bool isPlayer1Alive = player1.EndGame();
        bool isPlayer2Alive = player2.EndGame();
        if (isPlayer1Alive) {
            HUD.instance.SetWinner(player1.playerName);
        } else if(isPlayer2Alive) {
            HUD.instance.SetWinner(player2.playerName);
        }
    }
}
