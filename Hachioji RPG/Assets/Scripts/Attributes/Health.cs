using UnityEngine;
using System.Collections;
using RPG.Saving;
using RPG.Core;
using RPG.Stats;
using System;

namespace RPG.Attributes
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] float healthPoints = 100f;
        [SerializeField] float recoveryPercentage = 50f;
        bool isDead = false;
        BaseStats baseStats;

        private void Awake()
        {
            baseStats = GetComponent<BaseStats>();
            healthPoints = baseStats.GetStat(Stat.Health);
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
            healthPoints = Mathf.Max(healthPoints, recoverValue);
        }

        public bool IsDead()
        {
            return isDead;
        }   

        public void TakeDamage(GameObject initiator, float damage)
        {
            print("took damage " + damage);
            healthPoints = Mathf.Max(healthPoints - damage, 0);
            if (healthPoints == 0)
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
            return healthPoints;
        }

        public float GetMaxHp()
        {
            return baseStats.GetStat(Stat.Health);
        }

        public float GetPercentage()
        {
            return 100 * (healthPoints / baseStats.GetStat(Stat.Health));
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
            healthPoints = (float)state;
            if (healthPoints == 0) Die();
            else isDead = false;
        }

    }
}

