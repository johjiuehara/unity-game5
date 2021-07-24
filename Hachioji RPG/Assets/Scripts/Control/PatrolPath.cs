using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    public class PatrolPath : MonoBehaviour
    {
        const float waypointGizmoRadius = 0.3f;

        private void OnDrawGizmos()
        {
            for (int x = 0; x < transform.childCount; x++)
            {
                int z = GetNextIndex(x);
                Gizmos.DrawSphere(GetWaypoint(x), waypointGizmoRadius);
                Gizmos.DrawLine(GetWaypoint(x), GetWaypoint(z));
                
            }
        }

        public int GetNextIndex(int x)
        {
            if (x + 1 == transform.childCount) return 0;
            return x + 1;
        }

        public Vector3 GetWaypoint(int x)
        {
            return transform.GetChild(x).position;
        }
    }
}

