using System;
using RPG.Attributes;
using UnityEngine;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Hachioji RPG/Create New Weapon", order = 0)]
    public class Weapon : ScriptableObject
    {
        [SerializeField] AnimatorOverrideController swordAnimatorOR = null;
        [SerializeField] GameObject weaponPrefab = null;
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float weaponDamage = 5f;
        [SerializeField] float percentModifier = 5f;
        [SerializeField] bool isRightHand = true;
        [SerializeField] Projectile projectile = null;

        const string prefabStringRef = "Weapon";

        public float GetDamage() { return weaponDamage; }

        public float GetRange() { return weaponRange; }

        public bool IsRanged() { return projectile != null; }

        public float GetPercentModifier() { return percentModifier; }

        public void Create(Transform rightHand, Transform leftHand, Animator animator)
        {
            UnequipWeapon(rightHand, leftHand);
            if (weaponPrefab)
            {
                Transform handTransform = GetHandTransform(rightHand, leftHand);
                GameObject weapon = Instantiate(weaponPrefab, handTransform);
                weapon.name = prefabStringRef;
            }

            var overrideController = animator.runtimeAnimatorController as AnimatorOverrideController;
            if (swordAnimatorOR)
            {
                animator.runtimeAnimatorController = swordAnimatorOR;
            }
            else if (overrideController)
            {
                animator.runtimeAnimatorController = overrideController.runtimeAnimatorController;
            }
        }

        private static void UnequipWeapon(Transform rightHand, Transform leftHand)
        {
            Transform currentWeapon = null;
            currentWeapon = leftHand.Find(prefabStringRef);
            if (!currentWeapon) currentWeapon = rightHand.Find(prefabStringRef);
            if (!currentWeapon) return;
            Destroy(currentWeapon.gameObject);
        }

        private Transform GetHandTransform(Transform rightHand, Transform leftHand)
        {
            Transform handTransform;
            if (isRightHand) handTransform = rightHand;
            else handTransform = leftHand;
            return handTransform;
        }

        public void ShootProjectile(Transform rightHand, Transform leftHand, Health target, GameObject initiator)
        {
            Projectile projectileInstance = Instantiate(projectile, GetHandTransform(rightHand, leftHand).position, Quaternion.identity);
            projectileInstance.setTarget(target, initiator, weaponDamage);
        }
    }
}