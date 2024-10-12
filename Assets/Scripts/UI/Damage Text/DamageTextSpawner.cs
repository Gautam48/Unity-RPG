using System;
using System.Collections;
using System.Collections.Generic;
using RPG.UI.DamageText;
using UnityEngine;

namespace RPG.UI.DamageText
{
    public class DamageTextSpawner : MonoBehaviour
    {
        [SerializeField] DamageText damageText = null;

        public void Spawn(float damage)
        {
            DamageText instance = Instantiate(damageText, transform);
            instance.SetValue(damage);
        }
    }
}
