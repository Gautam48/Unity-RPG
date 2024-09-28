using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Attributes;
using TMPro;
using UnityEngine;

public class ExperienceDisplay : MonoBehaviour
{
    Experience experience;
    void Start()
    {
        experience = GameObject.FindWithTag("Player").GetComponent<Experience>();
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<TextMeshProUGUI>().text = String.Format("{0:0}", experience.ExperiencePoints);
    }
}
