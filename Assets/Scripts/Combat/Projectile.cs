using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using UnityEditor;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float speed = 1;
    [SerializeField] bool isHoming = false;

    Health target = null;

    float damage = 0;

    void Start()
    {
        transform.LookAt(GetAimLocation());
    }

    void Update()
    {
        if (target == null) return;

        if (isHoming && !target.IsDead)
        {
            transform.LookAt(GetAimLocation());
        }

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

    public void SetTarget(Health target, float damage)
    {
        this.target = target;
        this.damage = damage;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Health>() != target) return;
        if (target.IsDead) return;
        target.TakeDamage(damage);
        Destroy(gameObject);
    }
}
