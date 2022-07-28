using GMTK.LevelHandling;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;

public class Castle : MonoBehaviour
{
    [Button]
    public void GenerateAll()
    {
        foreach (var room in GetComponentsInChildren<Room>())
        {
            room.Generate();
        }
    }

    [Button]
    public void ClearAll()
    {
        foreach (var room in GetComponentsInChildren<Room>())
        {
            room.RemoveAll();
        }
    }
}