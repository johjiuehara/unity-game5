using UnityEngine;
using System.Collections;
using System;

namespace RPG.Core
{
    public class PersistentObjectInitializer : MonoBehaviour
    {
        [SerializeField] GameObject persistentObjectPrefab;
        static bool hasInitialized = false;
        private void Awake()
        {
            if (hasInitialized) return;

            InitializePersistentObjects();

            hasInitialized = true;
        }

        private void InitializePersistentObjects()
        {
            GameObject persistentObject = Instantiate(persistentObjectPrefab);
            DontDestroyOnLoad(persistentObject);
        }
    }
}

