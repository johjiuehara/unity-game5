using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using RPG.Saving;

namespace RPG.Cinematics
{
    public class CinematicTrigger : MonoBehaviour, ISaveable
    {
        bool isAlreadyTriggered = false;

        private void OnTriggerEnter(Collider other)
        {
            if (!isAlreadyTriggered && other.gameObject.tag == "Player")
            {
                isAlreadyTriggered = true;
                GetComponent<PlayableDirector>().Play();
            }
        }

        public object CaptureState()
        {
            return isAlreadyTriggered;
        }

        public void RestoreState(object state)
        {
            isAlreadyTriggered = (bool)state;
        }
    }
}