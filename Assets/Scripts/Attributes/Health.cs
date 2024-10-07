using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using RPG.Saving;
using RPG.Stats;
using UnityEngine;

namespace RPG.Attributes
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] float regenerationPercent = 70f;

        float healthPoints = -1f;

        bool isDead = false;
        public bool IsDead { get { return isDead; } }

        private void Start()
        {
            if (healthPoints < 0)
            {
                healthPoints = GetComponent<BaseStat>().GetStat(Stat.Health);
            }
        }

        private void OnEnable()
        {
            GetComponent<BaseStat>().OnLevelUp += RegenerateHealth;
        }

        private void OnDisable()
        {
            GetComponent<BaseStat>().OnLevelUp += RegenerateHealth;
        }

        public void TakeDamage(float damage, GameObject instigator)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0);
            if (healthPoints == 0)
            {
                Die();
                AwardExperience(instigator);
            }
        }

        public float GetHealthPoints()
        {
            return healthPoints;
        }

        public float GetMaxHealthPoints()
        {
            return GetComponent<BaseStat>().GetStat(Stat.Health);
        }

        public float GetPercentage()
        {
            return 100 * healthPoints / GetComponent<BaseStat>().GetStat(Stat.Health);
        }

        private void Die()
        {
            if (isDead) return;

            isDead = true;
            GetComponent<Animator>().SetTrigger("die");
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        void AwardExperience(GameObject instigator)
        {
            Experience experience = instigator.GetComponent<Experience>();
            if (experience == null) return;

            experience.GainExperience(GetComponent<BaseStat>().GetStat(Stat.ExperienceReward));
        }

        void RegenerateHealth()
        {
            float regenHealthPoints = GetComponent<BaseStat>().GetStat(Stat.Health) * regenerationPercent / 100;
            healthPoints = Mathf.Max(regenerationPercent, regenHealthPoints);
        }

        public object CaptureState()
        {
            return healthPoints;
        }

        public void RestoreState(object state)
        {
            healthPoints = (float)state;

            if (healthPoints == 0)
            {
                Die();
            }
        }
    }

}
