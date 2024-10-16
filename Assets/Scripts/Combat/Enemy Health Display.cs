using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Attributes;
using TMPro;
using UnityEngine;

namespace RPG.Combat
{
    public class EnemyHealthDisplay : MonoBehaviour
    {
        Fighter fighter;

        void Awake()
        {
            fighter = GameObject.FindWithTag("Player").GetComponent<Fighter>();
        }

        void Update()
        {
            if (fighter.GetTarget() == null)
            {
                GetComponent<TextMeshProUGUI>().text = "N/A";
            }
            else
            {
                Health health = fighter.GetTarget();
                GetComponent<TextMeshProUGUI>().text = String.Format("{0:0}/{1:0}", health.GetHealthPoints(), health.GetMaxHealthPoints());
            }


        }
    }
}
