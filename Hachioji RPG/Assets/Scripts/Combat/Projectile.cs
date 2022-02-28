using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float speed = 1;
    Health target = null;
    float damage = 0;

    // Update is called once per frame
    void Update()
    {
        Transform transform = GetComponent<Transform>();
        transform.LookAt(GetTargetPosition());
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    public void setTarget(Health targetToSet, float damageToSet)
    {
        target = targetToSet;
        damage = damageToSet;
    }

    private Vector3 GetTargetPosition()
    {
        CapsuleCollider targetCapsule = target.GetComponent<CapsuleCollider>();
        if (targetCapsule == null) return target.transform.position;
        return targetCapsule.transform.position + (Vector3.up * targetCapsule.height / 2);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Health>() != target) return;
        target.TakeDamage(damage);
        Destroy(gameObject);
    }
}
