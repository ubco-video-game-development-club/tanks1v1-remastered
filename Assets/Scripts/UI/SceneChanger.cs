﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuInstructionsUI;
    [SerializeField] private GameObject creditsUI;

    public void LoadMainGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame() {
        Application.Quit();
    }

    public void Instructions() {
        mainMenuInstructionsUI.SetActive(true);
    }

    public void Credits() {
        creditsUI.SetActive(true);
    }

    public void ClickToClose() {
        //Click on the instruction panel to close it

        mainMenuInstructionsUI.SetActive(false);
        creditsUI.SetActive(false);
    }
}
