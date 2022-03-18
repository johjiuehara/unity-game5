using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Attributes;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Combat
{
    public class EnemyHealthDisplay : MonoBehaviour
    {
        Fighter fighter;

        void Awake()
        {
            fighter = GameObject.FindWithTag("Player").GetComponent<Fighter>();
        }

        private void Update()
        {
            Health enemyHealth = fighter.GetTarget();
            if (!enemyHealth || enemyHealth.IsDead()) GetComponent<Text>().text = "NA";
            else GetComponent<Text>().text = String.Format("{0:0}/{1:0}", enemyHealth.GetHp(), enemyHealth.GetMaxHp());
        }
    }
}
