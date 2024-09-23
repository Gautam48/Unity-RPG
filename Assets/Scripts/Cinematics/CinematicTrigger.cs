using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class CinematicTrigger : MonoBehaviour
    {
        bool animationTriggered = false;
        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player" && animationTriggered == false)
            {
                animationTriggered = true;
                GetComponent<PlayableDirector>().Play();
            }
        }
    }
}
