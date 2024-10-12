using System.Collections;
using System.Collections.Generic;
using RPG.Attributes;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Health healthCoponent = null;
    [SerializeField] RectTransform foreground = null;
    void Update()
    {
        float health = healthCoponent.GetPercentage() / 100;
        foreground.localScale = new Vector3(health, 1, 1);
    }
}
