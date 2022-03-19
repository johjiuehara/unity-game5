using UnityEngine;
using System.Collections;
using RPG.Saving;
using RPG.Core;
using RPG.Stats;
using Utilities;
using System;

namespace RPG.Attributes
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] float recoveryPercentage = 50f;
        bool isDead = false;
        ResettableLazy<float> healthPoints;
        BaseStats baseStats;

        private void Awake()
        {
            baseStats = GetComponent<BaseStats>();
            healthPoints = new ResettableLazy<float>(GetInitHealthPoints);
        }

        private float GetInitHealthPoints()
        {
            return baseStats.GetStat(Stat.Health);
        }

        private void OnEnable()
        {
            baseStats.onLevelUp += RecoverHealth;
        }

        private void OnDisable()
        {
            baseStats.onLevelUp -= RecoverHealth;
        }

        private void RecoverHealth()
        {
            float recoverValue = baseStats.GetStat(Stat.Health) * (recoveryPercentage / 100);
            healthPoints.value = Mathf.Max(healthPoints.value, recoverValue);
        }

        public bool IsDead()
        {
            return isDead;
        }   

        public void TakeDamage(GameObject initiator, float damage)
        {
            healthPoints.value = Mathf.Max(healthPoints.value - damage, 0);
            if (healthPoints.value == 0)
            {
                Die();
                AwardExperience(initiator);
            }
        }

        private void AwardExperience(GameObject initiator)
        {
            float xpToAward = baseStats.GetStat(Stat.ExperienceReward);
            Experience initiatorXp = initiator.GetComponent<Experience>();
            if (!initiatorXp) return;
            initiatorXp.ReceiveXP(xpToAward);
        }

        public float GetHp()
        {
            return healthPoints.value;
        }

        public float GetMaxHp()
        {
            return baseStats.GetStat(Stat.Health);
        }

        public float GetPercentage()
        {
            return 100 * (healthPoints.value / baseStats.GetStat(Stat.Health));
        }

        private void Die()
        {
            if (isDead) return;
            isDead = true;
            GetComponent<Animator>().SetTrigger("die");
            GetComponent<CommandManager>().CancelCurrentAction();
        }

        public object CaptureState()
        {
            return healthPoints;
        }

        public void RestoreState(object state)
        {
            healthPoints.value = (float)state;
            if (healthPoints.value == 0) Die();
            else isDead = false;
        }

    }
}

