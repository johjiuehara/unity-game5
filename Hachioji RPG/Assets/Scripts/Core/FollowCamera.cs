using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class FollowCamera : MonoBehaviour
    {
        // Update is called once per frame
        [SerializeField] Transform target;
        private void LateUpdate()
        {
            GetComponent<Transform>().position = target.position;
        }
    }
}
