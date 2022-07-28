using UnityEngine;

namespace Utilities.Extensions
{
    public static class TransformExtension
    {
        public static void DestroyChildren(this Transform _transform)
        {
            for (int i = 0; i < _transform.transform.childCount; i++)
            {
                Object.DestroyImmediate(_transform.GetChild(i).gameObject);
            }      
        }
    }
}