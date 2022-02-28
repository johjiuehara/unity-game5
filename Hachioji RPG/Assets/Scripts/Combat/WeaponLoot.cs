using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class WeaponLoot : MonoBehaviour
    {
        [SerializeField] Weapon lootableWeapon = null;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                other.GetComponent<Fighter>().EquipWeapon(lootableWeapon);
                Destroy(gameObject);
            }
        }
    }
}