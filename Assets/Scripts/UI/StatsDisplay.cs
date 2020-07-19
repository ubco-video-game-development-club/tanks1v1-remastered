﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatsDisplay : MonoBehaviour
{
    private TextMeshProUGUI statTextbox;

    private void Start() {
        statTextbox = GetComponent<TextMeshProUGUI>();
    }

    public void SetStats(PlayerStats playerStats) {

        string healthRemaining = "Health Remaining: " + playerStats.healthRemaining;
        string distanceTravelled = "Distance travelled" + playerStats.distanceTravelled;
        string primaryBulletsFired = playerStats.primaryWeaponStats.projectileName + "s Fired: " + playerStats.primaryWeaponStats.projectilesFired;
        string secondaryBulletsFired = playerStats.secondaryWeaponStats.projectileName + "s Fired: " + playerStats.secondaryWeaponStats.projectilesFired;

        statTextbox.text = playerStats.playerName + "\n" + healthRemaining + "\n" + distanceTravelled + "\n" + primaryBulletsFired + "\n" + secondaryBulletsFired;

    }
}