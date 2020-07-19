using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{
    public static HUD instance;

    public RectTransform healthBarParent;

    public CanvasGroup pauseMenu;
    public CanvasGroup gameOverMenu;

    public TextMeshProUGUI playerWinText;

    void Awake() {
        if (instance != null) {
            Destroy(gameObject);
        } else {
            instance = this;
        }
    }

    public RectTransform GetHealthBarParent() {
        return healthBarParent;
    }

    private void SetMenuActive(CanvasGroup menu, bool isActive) {
        menu.alpha = isActive ? 1 : 0;
        menu.interactable = isActive;
        menu.blocksRaycasts = isActive;
    }

    public void SetPauseMenuActive(bool isActive) {
        SetMenuActive(pauseMenu, isActive);
    }

    public void SetGameOverMenuActive(bool isActive) {
        SetMenuActive(gameOverMenu, isActive);
    }

    public void SetWinner(string playerName) {
        playerWinText.text = playerName + " won!!!";
    }
}
