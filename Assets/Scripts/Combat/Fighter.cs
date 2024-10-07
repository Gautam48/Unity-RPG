using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using Unity.VisualScripting;
using RPG.Core;
using RPG.Saving;
using JetBrains.Annotations;
using RPG.Attributes;
using RPG.Stats;
using GameDevTV.Utils;
using System;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction, ISaveable, IModifierProvider
    {
        [SerializeField] float timeBetweenAttacks = 1f;
        [Range(0, 1)][SerializeField] float patrolSpeedFraction = 0.2f;
        [SerializeField] Transform rightHandTransform = null;
        [SerializeField] Transform leftHandTransform = null;
        [SerializeField] Weapon defaultWeapon = null;

        Health target;
        LazyValue<Weapon> currentWeapon;

        float timeAfterAttack = Mathf.Infinity;

        private void Awake()
        {
            currentWeapon = new LazyValue<Weapon>(SetDefaultWeapon);
        }

        void Start()
        {
            currentWeapon.ForceInit();
        }


        void Update()
        {
            timeAfterAttack += Time.deltaTime;

            if (target == null) return;

            if (!GetIsInRange())
            {
                GetComponent<Mover>().MoveTo(target.transform.position, 1f);
            }

            // if (GetIsInRange())
            else
            {
                if (target.IsDead) return;
                GetComponent<Mover>().Cancel();
                AttackBehaviour();
            }
        }

        private Weapon SetDefaultWeapon()
        {
            AttachWeapon(defaultWeapon);
            return defaultWeapon;
        }

        public Health GetTarget()
        {
            return target;
        }

        public void EquipWeapon(Weapon weapon)
        {
            currentWeapon.value = weapon;
            AttachWeapon(weapon);
        }

        void AttachWeapon(Weapon weapon)
        {
            Animator animator = GetComponent<Animator>();
            weapon.Spawn(rightHandTransform, leftHandTransform, animator);
        }

        void AttackBehaviour()
        {
            transform.LookAt(target.transform);
            if (timeAfterAttack > timeBetweenAttacks)
            {
                TriggerAttack();
                timeAfterAttack = 0;
            }

        }

        void TriggerAttack()
        {
            GetComponent<Animator>().ResetTrigger("stopAttack");
            GetComponent<Animator>().SetTrigger("attack");
        }

        bool GetIsInRange()
        {

            return Vector3.Distance(transform.position, target.transform.position) <= currentWeapon.value.WeaponRange;
        }

        public void Attack(GameObject combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.GetComponent<Health>();
            // GetComponent<Mover>().MoveTo(target.transform.position);
        }

        public bool CanAttack(GameObject combatTarget)
        {
            if (combatTarget == null) return false;
            Health targetToTest = combatTarget.GetComponent<Health>();
            return targetToTest != null && !targetToTest.IsDead;
        }

        public void Cancel()
        {
            StopAttack();
            GetComponent<Mover>().Cancel();
            target = null;
        }

        void StopAttack()
        {
            GetComponent<Animator>().ResetTrigger("attack");
            GetComponent<Animator>().SetTrigger("stopAttack");
        }

        public IEnumerable<float> GetAdditiveModifier(Stat stat)
        {
            if (stat == Stat.Damage)
            {
                yield return currentWeapon.value.WeaponDamage;
            }
        }

        public IEnumerable<float> GetPercetegeModifier(Stat stat)
        {
            if (stat == Stat.Damage)
            {
                yield return currentWeapon.value.PercentageBonus;
            }
        }
        //Animation event
        void Hit()
        {
            if (target == null) return;

            float damage = GetComponent<BaseStat>().GetStat(Stat.Damage);

            if (currentWeapon.value.HasProjectile())
            {
                currentWeapon.value.LaunchProjectile(rightHandTransform, leftHandTransform, target, gameObject, damage);
            }
            else
            {

                target.TakeDamage(damage, gameObject);
            }

        }

        void Shoot()
        {
            Hit();
        }

        public object CaptureState()
        {
            return currentWeapon.value.name;
        }

        public void RestoreState(object state)
        {
            string weaponName = (string)state;
            Weapon weapon = Resources.Load<Weapon>(weaponName);
            EquipWeapon(weapon);

        }

    }
}