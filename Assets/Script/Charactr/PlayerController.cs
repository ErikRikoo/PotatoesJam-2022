using Script.Utilities.Extensions;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Script.Charactr
{
    public class PlayerController : MonoBehaviour
    {

        [SerializeField] private CharacterController m_Controller;
        
        [SerializeField] private Animator[] m_Skins;

        private Animator CurrentSkin => m_Skins[m_CurrentSkinIndex % m_Skins.Length];
        
        private int m_CurrentSkinIndex = 0;

        private int CurrentSkinIndex
        {
            get => m_CurrentSkinIndex;
            set
            {
                CurrentSkin.gameObject.SetActive(false);
                m_CurrentSkinIndex = value % m_Skins.Length;
                if (m_CurrentSkinIndex < 0)
                {
                    m_CurrentSkinIndex += m_Skins.Length;
                }
                m_Controller.Animator = CurrentSkin;
                CurrentSkin.gameObject.SetActive(true);
            }
        }

        public void ChangeSkin(InputAction.CallbackContext _context)
        {
            float x = _context.ReadValue<Vector2>().x;
            int direction = x == 0? 0:(int) Mathf.Sign(x);
            CurrentSkinIndex += direction;
        }

        public void Movement(InputAction.CallbackContext _context)
        {
            Vector2 movementInput = _context.ReadValue<Vector2>();

            m_Controller.Movement(movementInput);
        }

        public void Action(InputAction.CallbackContext _context)
        {
            if (_context.phase == InputActionPhase.Started)
            {
                m_Controller.Action();
            }
        }
    }
}