using System;
using NaughtyAttributes;
using UnityEngine;

namespace Script.Utilities
{
    public class AnimatorBoolOnAciveState : MonoBehaviour
    {
        [SerializeField] private Animator m_Animator;
        
        [AnimatorParam("m_Animator")]
        [SerializeField] private int m_Param;

        private void OnEnable()
        {
            m_Animator.SetBool(m_Param, true);
        }

        private void OnDisable()
        {
            m_Animator.SetBool(m_Param, false);
        }
    }
}