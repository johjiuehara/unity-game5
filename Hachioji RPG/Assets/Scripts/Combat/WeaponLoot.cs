using System.Collections;
using UnityEngine;

namespace RPG.Combat
{
    public class WeaponLoot : MonoBehaviour
    {
        [SerializeField] Weapon lootableWeapon = null;
        [SerializeField] float hideTimeout = 3f;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                other.GetComponent<Fighter>().EquipWeapon(lootableWeapon);
                StartCoroutine(Respawn(hideTimeout));
            }
        }

        private IEnumerator Respawn(float timeout)
        {
            ShowLoot(false);
            yield return new WaitForSeconds(hideTimeout);
            ShowLoot(true);
        }

        private void ShowLoot(bool flag)
        {
            GetComponent<Collider>().enabled = flag;
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(flag);
            }
        }
    }
}