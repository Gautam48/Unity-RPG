using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class DestroyAfterEffect : MonoBehaviour
    {
        [SerializeField] GameObject tragetToDestroy = null;
        void Update()
        {
            if (!GetComponent<ParticleSystem>().IsAlive())
            {
                if (tragetToDestroy != null)
                {
                    Destroy(tragetToDestroy);
                }
                else
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
