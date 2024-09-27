using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stats
{
    public class BaseStat : MonoBehaviour
    {

        [Range(0, 99)][SerializeField] int startingLevel = 1;
        [SerializeField] CharacterClass characterClass;
        [SerializeField] Progression progression = null;

        public float GetHealth()
        {
            return progression.GetHealth(characterClass, startingLevel);
        }
    }
}
