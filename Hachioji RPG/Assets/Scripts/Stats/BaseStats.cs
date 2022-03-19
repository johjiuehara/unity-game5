using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Attributes;
using UnityEngine;

namespace RPG.Stats
{
    public class BaseStats : MonoBehaviour
    {
        [Range(1,75)]
        [SerializeField] int startLevel = 1;
        [SerializeField] Job characterJob;
        [SerializeField] Progress characterProgress = null;
        [SerializeField] GameObject levelUpFx = null;

        public delegate void levelUpDelegate();
        public event levelUpDelegate onLevelUp;

        int currentLevel = 0;

        Experience experience;

        private void Awake()
        {
            experience = GetComponent<Experience>();
        }

        private void Start()
        {
            currentLevel = CalculateLevel();
        }

        private void OnEnable()
        {
            if (experience) experience.onReceiveXP += UpdateLevel;
        }

        private void OnDisable()
        {
            if (experience) experience.onReceiveXP -= UpdateLevel;
        }

        private void UpdateLevel()
        {
            int newLevel = CalculateLevel();
            if (newLevel > currentLevel)
            {
                currentLevel = newLevel;
                SpawnLevelUpFx();
                onLevelUp();
            }
        }

        private void SpawnLevelUpFx()
        {
            Instantiate(levelUpFx, transform);
        }

        public float GetStat(Stat stat)
        {
            return (GetBaseStat(stat) + GetModifier(stat)) * (1 + GetPercentModifier(stat) / 100);
        }

        private float GetBaseStat(Stat stat)
        {
            return characterProgress.GetStat(stat, characterJob, GetLevel());
        }

        private float GetModifier(Stat stat)
        {
            float total = 0;
            foreach (IModify imodify in GetComponents<IModify>())
            {
                foreach (float modifier in imodify.GetModifier(stat))
                {
                    total += modifier;
                }
            }
            return total;
        }

        private float GetPercentModifier(Stat stat)
        {
            float total = 0;
            foreach (IModify imodify in GetComponents<IModify>())
            {
                foreach (float modifier in imodify.GetPercentModifier(stat))
                {
                    total += modifier;
                }
            }
            return total;
        }

        public int GetLevel()
        {
            if (currentLevel == 0) currentLevel = CalculateLevel();
            return currentLevel;
        }

        private int CalculateLevel()
        {
            Experience experience = GetComponent<Experience>();
            if (!experience) return startLevel;
            float currentXp = experience.GetExperience();
            int maxLevel = characterProgress.GetMaxLevel(characterJob);
            for (int level = 1; level <= maxLevel; level++)
            {
                float expToLevelup = characterProgress.GetStat(Stat.ExpToLevelup, characterJob, level);
                if (expToLevelup > currentXp) return level;
            }
            return maxLevel + 1;
        }
    }
}
