using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using RPG.Stats;
using UnityEngine;

namespace RPG.Stats
{
    [CreateAssetMenu(fileName = "Progression", menuName = "Stats/ New Progression", order = 0)]
    public class Progression : ScriptableObject
    {
        [SerializeField] ProgressionCharacterClass[] characterClasses = null;

        Dictionary<CharacterClass, Dictionary<Stat, float[]>> lookupTable = null;

        [System.Serializable]
        class ProgressionCharacterClass
        {
            public CharacterClass characterClass;
            public ProgressionStat[] stats;

        }

        [System.Serializable]
        class ProgressionStat
        {
            public Stat stat;
            public float[] level;
        }

        public float GetStat(Stat stat, CharacterClass characterClass, int level)
        {
            BuildLookup();
            if (lookupTable[characterClass][stat].Length < level) return 0;

            return lookupTable[characterClass][stat][level - 1];
        }

        void BuildLookup()
        {
            if (lookupTable != null) return;
            lookupTable = new Dictionary<CharacterClass, Dictionary<Stat, float[]>>();

            foreach (ProgressionCharacterClass progressionClass in characterClasses)
            {
                var statLookup = new Dictionary<Stat, float[]>();

                foreach (ProgressionStat progressionStat in progressionClass.stats)
                {
                    statLookup[progressionStat.stat] = progressionStat.level;
                }

                lookupTable[progressionClass.characterClass] = statLookup;
            }
        }

        public int GetLevels(CharacterClass characterClass, Stat stat)
        {
            BuildLookup();
            float[] levels = lookupTable[characterClass][stat];
            return levels.Length;
        }
    }
}
