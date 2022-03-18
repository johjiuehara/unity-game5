using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class DestroyParticle : MonoBehaviour
    {
        [SerializeField] GameObject objectToDestroy = null;

        void Update()
        {
            if (!GetComponent<ParticleSystem>().IsAlive())
            {
                if (objectToDestroy) Destroy(objectToDestroy);
                else Destroy(gameObject);
            }
        }
    }
}
