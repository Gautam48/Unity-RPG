using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stats
{
    public class BaseStat : MonoBehaviour
    {

        [Range(0, 99)][SerializeField] int baseLevel = 1;
        [SerializeField] CharacterClass characterClass;

    }
}
