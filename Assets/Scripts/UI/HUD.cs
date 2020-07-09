using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour
{
    public static HUD instance;

    public RectTransform healthBarParent;

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
}
