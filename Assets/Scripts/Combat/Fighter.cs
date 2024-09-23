using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using Unity.VisualScripting;
using RPG.Core;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float timeBetweenAttacks = 1f;
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float weaponDamage = 10f;
        [Range(0, 1)][SerializeField] float patrolSpeedFraction = 0.2f;
        [SerializeField] GameObject weaponPrefab = null;
        [SerializeField] Transform handTransform = null;
        [SerializeField] AnimatorOverrideController weaponOverride = null;

        Health target;

        float timeAfterAttack = Mathf.Infinity;

        void Start()
        {
            SpawnWeapon();

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

        void SpawnWeapon()
        {
            Instantiate(weaponPrefab, handTransform);
            Animator animator = GetComponent<Animator>();
            animator.runtimeAnimatorController = weaponOverride;
        }

        private void AttackBehaviour()
        {
            transform.LookAt(target.transform);
            if (timeAfterAttack > timeBetweenAttacks)
            {
                TriggerAttack();
                timeAfterAttack = 0;
            }

        }

        private void TriggerAttack()
        {
            GetComponent<Animator>().ResetTrigger("stopAttack");
            GetComponent<Animator>().SetTrigger("attack");
        }

        bool GetIsInRange()
        {

            return Vector3.Distance(transform.position, target.transform.position) <= weaponRange;
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

        private void StopAttack()
        {
            GetComponent<Animator>().ResetTrigger("attack");
            GetComponent<Animator>().SetTrigger("stopAttack");
        }

        //Animation event
        void Hit()
        {
            if (target == null) return;
            target.TakeDamage(weaponDamage);
        }

    }
}
