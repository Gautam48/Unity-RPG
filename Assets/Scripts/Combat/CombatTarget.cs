using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;
using RPG.Attributes;
using RPG.Control;

namespace RPG.Combat
{
    [RequireComponent(typeof(Health))]
    public class CombatTarget : MonoBehaviour, IRayCastable
    {
        public bool HandleRaycast(PlayerController player)
        {

            if (!player.GetComponent<Fighter>().CanAttack(gameObject))
            {
                return false;
            }
            if (Input.GetMouseButton(0))
            {
                player.GetComponent<Fighter>().Attack(gameObject);
            }

            return true;
        }

    }
}
