using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    // Update is called once per frame
    [SerializeField] Transform target;
    private void LateUpdate()
    {
        GetComponent<Transform>().position = target.position;
    }
}
