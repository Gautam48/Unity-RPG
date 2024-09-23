using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using UnityEditor;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float speed = 1;

    Health target = null;

    void Update()
    {
        transform.LookAt(GetAimLocation());
        // transform.LookAt(target.position);
        transform.Translate(speed * Time.deltaTime * Vector3.forward);
    }

    Vector3 GetAimLocation()
    {
        CapsuleCollider capsule = target.GetComponent<CapsuleCollider>();
        if (capsule == null)
        {
            return target.transform.position;
        }
        return target.transform.position + Vector3.up * capsule.height / 2;
    }

    public void SetTarget(Health target)
    {
        this.target = target;
    }
}
