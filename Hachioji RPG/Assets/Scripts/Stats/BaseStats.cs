using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stats
{
    public class BaseStats : MonoBehaviour
    {
        [Range(1,75)]
        [SerializeField] int startLevel = 1;
        [SerializeField] Job characterJob;
        [SerializeField] Progress characterProgress = null;

        public float GetHealth()
        {
            return characterProgress.GetHealth(characterJob, startLevel);
        }
    }
}
