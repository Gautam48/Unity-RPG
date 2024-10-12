using System.Collections;
using System.Collections.Generic;
using RPG.Attributes;
using UnityEditor.Rendering;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Health healthCoponent = null;
    [SerializeField] RectTransform foreground = null;
    [SerializeField] Canvas canvas = null;
    void Update()
    {
        float health = healthCoponent.GetPercentage() / 100;

        if (Mathf.Approximately(health, 1) || Mathf.Approximately(health, 0))
        {
            canvas.enabled = false;
            return;
        }
        canvas.enabled = true;
        foreground.localScale = new Vector3(health, 1, 1);
    }
}
