using System;
using UnityEngine;

namespace Script.Charactr
{
    [Serializable]
    struct MaterialPair
    {
        public Material Alive;
        public Material Dead;

        public Material Get(bool _isAlive)
            => _isAlive ? Alive : Dead;
    }
    
    public class LivingState : MonoBehaviour
    {
        [SerializeField] private bool m_IsAlive = true;

        [Header("Materials")]
        [SerializeField] private MaterialPair m_SkinMaterials;
        [SerializeField] private MaterialPair m_ClothesMaterials;
        [SerializeField] private MaterialPair m_HairMaterials;

        [Header("Renderer")]
        [SerializeField] private Renderer[] m_Skins;
        [SerializeField] private Renderer[] m_Clothes;
        [SerializeField] private Renderer[] m_Hair;

        private void Start()
        {
            AssignToAllRenderer(m_Skins, m_SkinMaterials);
            AssignToAllRenderer(m_Clothes, m_ClothesMaterials);
            AssignToAllRenderer(m_Hair, m_HairMaterials);
        }

        private void AssignToAllRenderer(Renderer[] _renderers, MaterialPair _materials)
        {
            foreach (var renderer in _renderers)
            {
                renderer.material = _materials.Get(m_IsAlive);
            }
        }
    }
}