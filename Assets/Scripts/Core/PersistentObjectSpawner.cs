using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class PersistentObjectSpawner : MonoBehaviour
    {
        // Start is called before the first frame update
        [SerializeField] GameObject persistentObjectPrefab;

        static bool hasSpawned;

        private void Awake()
        {
            if (hasSpawned) return;
            SpawnPersistentObjects();
            hasSpawned = true;
        }

        private void SpawnPersistentObjects()
        {
            GameObject persistentObject = Instantiate(persistentObjectPrefab);
            DontDestroyOnLoad(persistentObject);
        }
    }

}