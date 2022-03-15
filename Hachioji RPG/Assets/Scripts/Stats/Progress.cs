using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stats
{
    [CreateAssetMenu(fileName = "Progress", menuName = "Stats/Create New Progress", order = 0)]
    public class Progress : ScriptableObject
    {
        [SerializeField] CharacterProgress[] characterProgress = null;

        public float GetHealth(Job job, int level)
        {
            foreach (CharacterProgress cp in characterProgress)
            {
                if (cp.job == job) return cp.health[level - 1];
            }
            return 0;
        }

        [System.Serializable]
        public class CharacterProgress
        {
            public Job job;
            public float[] health;
        }
    }
}
