using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Attributes;
using UnityEngine;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float speed = 1;
        [SerializeField] bool isHoming = false;
        [SerializeField] GameObject hitEffect = null;
        [SerializeField] float maxLifetime = 10;
        [SerializeField] GameObject[] destroyOnHit = null;
        [SerializeField] float lifeAfterImpact = 1;
        Health target = null;
        float damage = 0;

        private void Start()
        {
            transform.LookAt(GetTargetPosition());
        }

        // Update is called once per frame
        void Update()
        {
            Transform transform = GetComponent<Transform>();
            if (isHoming && !target.IsDead()) transform.LookAt(GetTargetPosition());
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        public void setTarget(Health targetToSet, float damageToSet)
        {
            target = targetToSet;
            damage = damageToSet;

            Destroy(gameObject, maxLifetime);
        }

        private Vector3 GetTargetPosition()
        {
            CapsuleCollider targetCapsule = target.GetComponent<CapsuleCollider>();
            if (targetCapsule == null) return target.transform.position;
            return targetCapsule.transform.position + (Vector3.up * targetCapsule.height / 2);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Health>() != target || target.IsDead()) return;
            target.TakeDamage(damage);
            speed = 0;
            if (hitEffect) Instantiate(hitEffect, GetTargetPosition(), transform.rotation);

            foreach(GameObject toDestroy in destroyOnHit)
            {
                Destroy(toDestroy);
            }

            Destroy(gameObject, lifeAfterImpact);
        }
    }
}
