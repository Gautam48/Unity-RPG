using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Stats;
using TMPro;
using UnityEngine;

public class LevelDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    BaseStat baseStat;

    void Start()
    {
        baseStat = GameObject.FindWithTag("Player").GetComponent<BaseStat>();
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<TextMeshProUGUI>().text = String.Format("{0:0}", baseStat.GetLevel());
    }
}
