﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public RectTransform barFillImage;
    public Vector2 offset;

    private Transform target;
    private CanvasGroup canvasGroup;

    void Awake() {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    void Update() {
        if (target != null) {
            transform.position = (Vector2)target.position + offset;
        }
    }

    public void BindToTarget(Transform target) {
        this.target = target;
    }

    public void SetHealthPercentage(float healthPercentage) {
        barFillImage.anchorMax = new Vector2(healthPercentage, barFillImage.anchorMax.y);
    }

    public void SetVisible(bool visible) {
        canvasGroup.alpha = visible ? 1 : 0;
    }
}
