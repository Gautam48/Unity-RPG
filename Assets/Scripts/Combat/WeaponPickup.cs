using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Control;
using UnityEngine;

namespace RPG.Combat
{
    public class WeaponPickup : MonoBehaviour, IRayCastable
    {
        [SerializeField] Weapon weapon;
        [SerializeField] float respawnTime = 5f;

        void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                Pickup(other.GetComponent<Fighter>());
            }
        }

        private void Pickup(Fighter fighter)
        {
            fighter.EquipWeapon(weapon);
            StartCoroutine(HideForSeconds());
        }

        IEnumerator HideForSeconds()
        {
            ShowPickup(false);
            yield return new WaitForSeconds(respawnTime);
            ShowPickup(true);
        }

        void ShowPickup(bool shouldShow)
        {
            GetComponent<Collider>().enabled = shouldShow;
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(shouldShow);
            }
        }

        public bool HandleRaycast(PlayerController player)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Pickup(player.GetComponent<Fighter>());
            }
            return true;
        }
    }
}
