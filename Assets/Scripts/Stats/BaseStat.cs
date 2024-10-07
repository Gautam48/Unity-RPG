using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Schema;
using UnityEngine;

namespace RPG.Stats
{
    public class BaseStat : MonoBehaviour
    {

        [Range(0, 99)][SerializeField] int startingLevel = 1;
        [SerializeField] CharacterClass characterClass;
        [SerializeField] Progression progression = null;
        [SerializeField] GameObject LevelUpParticleEffect = null;
        [SerializeField] bool shouldUseAdditiveModifiers = false;
        [SerializeField] bool shouldUsePercentageModifiers = false;

        Experience experience;

        int currentLevel = 0;

        public event Action OnLevelUp;

        void Awake()
        {
            experience = GetComponent<Experience>();
        }

        void Start()
        {
            currentLevel = CalculateLevel();
            if (experience != null)
            {
                experience.OnExperienceGained += UpdateLevel;
            }
        }

        void OnEnable()
        {
            if (experience != null)
            {
                experience.OnExperienceGained += UpdateLevel;
            }
        }

        void OnDisable()
        {
            if (experience != null)
            {
                experience.OnExperienceGained -= UpdateLevel;
            }
        }

        void UpdateLevel()
        {
            int newLevel = CalculateLevel();
            if (newLevel > currentLevel)
            {
                currentLevel = newLevel;
                LevelUpEffect();
                OnLevelUp();
            }
        }

        void LevelUpEffect()
        {
            Instantiate(LevelUpParticleEffect, transform);
        }

        public float GetStat(Stat stat)
        {
            return (GetBaseStat(stat) + GetAdditiveModifier(stat)) * (1 + GetPercentageModifier(stat));
        }


        float GetBaseStat(Stat stat)
        {
            return progression.GetStat(stat, characterClass, GetLevel());
        }

        float GetAdditiveModifier(Stat stat)
        {
            if (!shouldUseAdditiveModifiers) return 0;

            float total = 0;

            foreach (IModifierProvider provider in GetComponents<IModifierProvider>())
            {
                foreach (float modifier in provider.GetAdditiveModifier(stat))
                {
                    total += modifier;
                }
            }

            return total;
        }

        float GetPercentageModifier(Stat stat)
        {
            if (!shouldUsePercentageModifiers) return 0;

            float total = 0;

            foreach (IModifierProvider provider in GetComponents<IModifierProvider>())
            {
                foreach (float modifier in provider.GetPercetegeModifier(stat))
                {
                    total += modifier;
                }
            }

            return total;
        }

        public int GetLevel()
        {
            if (currentLevel < 1)
            {
                currentLevel = CalculateLevel();
            }

            return currentLevel;
        }

        public int CalculateLevel()
        {
            Experience experience = GetComponent<Experience>();
            if (experience == null) return startingLevel;

            float currentXP = experience.ExperiencePoints;

            int penultimateLevel = progression.GetLevels(characterClass, Stat.ExperienceToLevelUp);

            for (int level = 1; level <= penultimateLevel; level++)
            {
                float XPToLevelUp = progression.GetStat(Stat.ExperienceToLevelUp, characterClass, level);

                if (XPToLevelUp > currentXP)
                {
                    return level;
                }
            }

            return penultimateLevel + 1;
        }
    }
}
