using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
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
    
    public void Movement(InputValue _value)
    {
        Vector2 movementInput = _value.Get<Vector2>();
        Debug.Log("Déplacement demandé: " + movementInput);
        // Cet événement n'est pas appelé tout le temps
        // Seulement quand "_value.Get<Vector2>()" change
        // Il faut donc stocker cette valeur et opérer les changements dans le Update
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
