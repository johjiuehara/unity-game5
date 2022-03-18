using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stats
{
    [CreateAssetMenu(fileName = "Progress", menuName = "Stats/Create New Progress", order = 0)]
    public class Progress : ScriptableObject
    {
        [SerializeField] CharacterProgress[] characterProgress = null;

        Dictionary<Job, Dictionary<Stat, float[]>> lookup = null;

        public float GetStat(Stat stat,Job job, int level)
        {
            BuildLookup();

            float[] levels = lookup[job][stat];

            if (levels.Length < level) return 0;

            return levels[level - 1];
        }

        public int GetMaxLevel(Job job)
        {
            BuildLookup();

            float[] levels = lookup[job][Stat.ExpToLevelup];

            return levels.Length;
        }

        private void BuildLookup()
        {
            if (lookup != null) return;

            lookup = new Dictionary<Job, Dictionary<Stat, float[]>>();

            foreach (CharacterProgress cp in characterProgress)
            {
                Dictionary<Stat, float[]> progressStatLookup = new Dictionary<Stat, float[]>();
                foreach (ProgressStats ps in cp.stats)
                {
                    progressStatLookup.Add(ps.stat, ps.levels);
                }
                lookup.Add(cp.job, progressStatLookup);
            }
        }

        [System.Serializable]
        public class ProgressStats
        {
            public Stat stat;
            public float[] levels;
        }

        [System.Serializable]
        public class CharacterProgress
        {
            public Job job;
            public ProgressStats[] stats;
        }
    }
}
