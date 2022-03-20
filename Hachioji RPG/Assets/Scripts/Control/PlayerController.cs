using UnityEngine;
using RPG.Movement;
using RPG.Combat;
using RPG.Attributes;
using System;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        Health health;

        enum CursorIconType
        {
            None,
            Movement,
            Combat
        }

        [System.Serializable]
        struct CursorIconMapping
        {
            public CursorIconType type;
            public Vector2 hotspot;
            public Texture2D texure;
        }

        [SerializeField]
        CursorIconMapping[] cursorIconMappings;

        private void Awake()
        {
            health = GetComponent<Health>();
        }

        private void Update()
        {
            if (health.IsDead()) return;
            if (InteractWithCombat()) return;
            if (InteractWithMovement()) return;
            SetCursonIcon(CursorIconType.None);
        }

        private bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit hit in hits)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                if (target == null) continue;
                if (!GetComponent<Fighter>().CanAttack(target.gameObject)) continue;

                if (Input.GetMouseButton(0))
                {
                    GetComponent<Fighter>().Attack(target.gameObject);
                }
                SetCursonIcon(CursorIconType.Combat);
                return true;
            }
            return false;
        }

        private bool InteractWithMovement()
        {
            RaycastHit hit;
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);
            if (hasHit)
            {
                if (Input.GetMouseButton(0))
                {
                    GetComponent<Mover>().StartMoveAction(hit.point, 1f);
                }
                SetCursonIcon(CursorIconType.Movement);
                return true;
            }
            return false;
        }

        private void SetCursonIcon(CursorIconType type)
        {
            CursorIconMapping cursorIcon = GetCursorIconMapping(type);
            Cursor.SetCursor(cursorIcon.texure, cursorIcon.hotspot, CursorMode.Auto);
        }

        private CursorIconMapping GetCursorIconMapping(CursorIconType type)
        {
            foreach (CursorIconMapping mapping in cursorIconMappings)
            {
                if (mapping.type == type) return mapping;
            }
            return cursorIconMappings[0];
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}