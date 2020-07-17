using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private bool isPaused = false;

    public GameObject pauseMenuUI;

    [SerializeField] private KeyCode pauseGameKey = KeyCode.Escape;  // maybe a second pause key for the second player?

    void Update()
    {
        if (Input.GetKeyDown(pauseGameKey)) {

            if (isPaused) {
                ResumeGame();
            }

            else {
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
        pauseMenuUI.SetActive(false);
    }

    private void PauseGame() {
        //Pause Game Steps:
        // 1) Pause Gameplay (Usually by setting timescale to 0)
        // 2) Open Pause Menu

        isPaused = true;
        Time.timeScale = 0f;
        pauseMenuUI.SetActive(true);
    }

    public void RestartGame() {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        isPaused = false;
    }

    public void BackToMainMenu() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
        isPaused = false;
    }
}
