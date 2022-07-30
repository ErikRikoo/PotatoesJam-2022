using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Lobby
{
    public class LobbyPlayerController : MonoBehaviour
    {
        [SerializeField] private Animator m_Animator;

        [SerializeField] private UnityEvent<bool> m_ReadynessChanged;
        
        
        private Vector3 m_OriginalScale;
        private LobbyManager m_LobbyManager;
        private bool m_IsReady;

        private void Awake()
        {
            m_OriginalScale = transform.localScale;
        }

        public void BindToHandler(LobbyManager lobbyManager)
        {
            m_LobbyManager = lobbyManager;
        }

        public void OnReady(InputAction.CallbackContext _context)
        {
            if (_context.phase != InputActionPhase.Started)
            {
                return;
            }
            
            if (m_IsReady)
            {
                return;
            }
            
            ++m_LobbyManager.PlayerReadyCount;
           // m_Animator.SetTrigger("ReadyChanged");
            m_ReadynessChanged?.Invoke(true);
            //transform.localScale *= 1.1f;
            m_IsReady = true;
        }

        public void OnCancelReady(InputAction.CallbackContext _context)
        {
            if (_context.phase != InputActionPhase.Started)
            {
                return;
            }
            
            if (!m_IsReady)
            {
                return;
            }
            
            --m_LobbyManager.PlayerReadyCount;
            //m_Animator.SetTrigger("ReadyChanged");
            m_ReadynessChanged?.Invoke(false);
            transform.localScale = m_OriginalScale;
            m_IsReady = false;
        }
    }
}