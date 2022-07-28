using System;
using System.Collections.Generic;
using System.Linq;
using Script.Utilities.Extensions;
using Unity.VisualScripting;
using UnityEngine;

namespace Script.Environment.Rooms
{
    public class Doors : MonoBehaviour
    {
        [SerializeField] private List<Vector2> m_Positions;

        private void OnDrawGizmos()
        {
            foreach (var position in m_Positions)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireCube(position.X1Y(), new Vector3(1, 2, 1));
                Gizmos.color = Color.white;
            }
        }

        public bool Has(Vector3 world)
        {
            return m_Positions.Any(pos => Vector3.SqrMagnitude(pos.X0Y() - world) < 0.5f);
        }
    }
}