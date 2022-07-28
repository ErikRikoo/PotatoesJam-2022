using System;
using System.Collections.Generic;
using GMTK.Utilities;
using UnityEngine;

namespace GMTK.LevelHandling.Generation
{
    [Serializable]
    public struct WallInfo
    {
        public GameObject Wall;
        public GameObject Door;
        
    }
    
    [CreateAssetMenu(fileName = "LevelGenerationInfo", menuName = "Level Handling/Generation/Info", order = 0)]
    public class LevelGenerationInfo : ScriptableObject
    {
        [SerializeField] public List<WallInfo> WallsPrefab;
    }
}