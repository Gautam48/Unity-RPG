using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] Transform target = null;
    [SerializeField] float speed = 1;

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
            return target.position;
        }
        return target.position + Vector3.up * capsule.height / 2;
    }
}
