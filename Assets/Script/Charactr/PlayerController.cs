using System;
using NaughtyAttributes;
using Script.Utilities.Extensions;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Min(0)]
    [SerializeField] private float m_Damping = 1;
    
    [SerializeField] private float m_Speed;

    [SerializeField] private Transform m_Visual;
    [SerializeField] private Animator m_Animator;
    [AnimatorParam("m_Animator")]
    [SerializeField] private int m_SpeedAnimParameter;
    
    
    private Vector3 Target;
    private Vector3 RealSpeed;
    private Rigidbody m_Rigidbody;
    private float m_InverseSpeed;

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_InverseSpeed = 1 / m_Speed;
    }

    private void FixedUpdate()
    {
        RealSpeed = Vector3.LerpUnclamped(RealSpeed, Target, m_Damping * Time.deltaTime);
        m_Rigidbody.velocity = RealSpeed;
        
        m_Animator.SetFloat(m_SpeedAnimParameter, RealSpeed.magnitude * m_InverseSpeed);
        m_Visual.LookAt(m_Visual.position + RealSpeed);
    }

    /**
     * Tout est configuré sur le player, tu as:
     * - Capsule COllider pour les collisions, je l'ai mis par défaut mais fais ce que tu veux ;)
     * - Un rigidbody tu vas en avoir besoin pour le controller, j'explique ça ci-dessous :)
     *
     * Première chose à faire c'est récupérer le Rigidbody qui se trouve sur l'objet grâce au
     * GetComponent<Rigidbody>
     * Le mieux est de le faire dans le Awake et de le stocker c'est moins gourmand
     *
     * Ensuite il faut, s'en servir dans le Update pour changer la vitesse de joueur
     * Il faut utiliser Rigidbody.velocity
     * https://docs.unity3d.com/ScriptReference/Rigidbody-velocity.html
     * Qui va donc changer la vitesse au niveau physique ce qui permet d'avoir un bon comportement,
     * de pas passer à travers les murs etc.
     * Je crois que c'est déjà /seconde donc pas besoin du Time.deltaTime
     */
    
    public void Movement(InputAction.CallbackContext _context)
    {
        Vector2 movementInput = _context.ReadValue<Vector2>();

        Target = (movementInput * m_Speed).X0Y();
    }

    public void Action(InputAction.CallbackContext _context)
    {
        if (_context.phase == InputActionPhase.Started)
        {
            // On vérifie qu'on vient d'appuyer sur le bouton
            // SInon l'event est déclenché 3 fois, à la pression, juste après et quand on le relache
            Debug.Log("Action vient d'être pressé");
        }
    }
}
