using System;
using System.Collections.Generic;
using System.Linq;
using GMTK.LevelHandling.Generation;
using Script.Environment.Rooms;
using Script.Utilities.Extensions;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using Utilities.Extensions;

namespace GMTK.LevelHandling
{
    public class Room : MonoBehaviour
    {
        #region Unity Editor Generation

        [ValueDropdown("m_WallsInfos")]
        [SerializeField] private WallInfo m_WallType;


        private IEnumerable<ValueDropdownItem<WallInfo>> m_WallsInfos
            => m_GenerationInfo.WallsPrefab.Select(
                value => new ValueDropdownItem<WallInfo>
                {
                    Text = value.Wall.name
                        .Replace("Wall_", "")
                        .Replace("Variant", ""),
                    Value = value
                });

        [SerializeField] private Vector2Int m_Size;

        private int SizeX => m_Size.x - 1;

        private Vector3 GetExtremity(float x, float side = 1)
            => transform.TransformPoint(
                new Vector3(x - m_Size.x * 0.5f + 0.5f, 0, side * (m_Size.y) * 0.5f)
            );

        private float StartX => -m_Size.x * 0.5f;
        private float StartY => -m_Size.y * 0.5f;
        

        [SerializeField] private LevelGenerationInfo m_GenerationInfo;
        [SerializeField] private Transform m_Floor;
        [SerializeField] private Transform m_WallsSouth;
        [SerializeField] private Transform m_WallsNorth;
        [SerializeField] private Transform m_WallsWest;
        [SerializeField] private Transform m_WallsEast;
        [SerializeField] private Doors m_Doors;

        [Button]
        public void Generate()
        {
            RemoveAll();
            m_Floor.gameObject.SetActive(true);
            UpdatePositions();
            GenerateItems();
        }

        [Button]
        public void RemoveAll()
        {
            m_Floor.gameObject.SetActive(false);

            m_WallsWest.DestroyChildren();
            m_WallsNorth.DestroyChildren();
            m_WallsEast.DestroyChildren();
            m_WallsSouth.DestroyChildren();
        }

        private void GenerateItems()
        {
            for (int x = 0; x < m_Size.x; ++x)
            {
                InstantiateWall(x, m_WallsSouth);
                InstantiateWall(x, m_WallsNorth);
            }
            
            for (int z = 0; z < m_Size.y; ++z)
            {
                InstantiateWall(z, m_WallsWest);
                InstantiateWall(z, m_WallsEast);
            }
        }

        public void InstantiateWall(int position, Transform holder)
        {
            Vector2Int gridPos = new Vector2Int(position, 0);
            Vector3 world = gridPos.ToFloat().X0Y() + new Vector3(0.5f, 0, 0);
            world = holder.TransformPoint(world);
            if (m_Doors.Has(world))
            {
                InstantiateElement(m_WallType.Door, gridPos, holder);
                return;
            }
            InstantiateElement(m_WallType.Wall, gridPos, holder);
        }
        
        private void InstantiateElement(GameObject element, Vector2Int _pos, Transform _parent)
        {
            #if UNITY_EDITOR
            GameObject instantiated = (GameObject) PrefabUtility.InstantiatePrefab(element);
            instantiated.transform.SetParent(_parent, false);
            instantiated.transform.localPosition = _pos.ToFloat().X0Y();
            #endif
        }

        private void UpdatePositions()
        {

            m_Floor.localScale = m_Size.ToFloat().X1Y();

            m_WallsSouth.localPosition = new Vector2(-StartX, StartY).X0Y();
            m_WallsNorth.localPosition = new Vector2(StartX, -StartY).X0Y();

            m_WallsWest.localPosition = new Vector2(StartX, StartY).X0Y();
            m_WallsEast.localPosition = new Vector2(-StartX, StartY).X0Y();
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireCube(transform.position + new Vector3(0, 1, 0), m_Size.ToFloat().X1Y());
        }

        #endregion
    }
}