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

        void Update()
        {
            if (gameObject.tag == "Player")
            {
                print(GetLevel());
            }
        }

        public float GetStat(Stat stat)
        {
            return progression.GetStat(stat, characterClass, startingLevel);
        }

        public int GetLevel()
        {
            float currentXP = GetComponent<Experience>().ExperiencePoints;

            int penultimateLevel = progression.GetLevels(characterClass, Stat.ExperienceToLovelUp);

            for (int level = 1; level <= penultimateLevel; level++)
            {
                float XPToLevelUp = progression.GetStat(Stat.ExperienceToLovelUp, characterClass, level);

                if (XPToLevelUp > currentXP)
                {
                    return level;
                }
            }

            return penultimateLevel + 1;
        }
    }
}
