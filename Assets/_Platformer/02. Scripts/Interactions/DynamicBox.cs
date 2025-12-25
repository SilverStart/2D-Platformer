using Platformer;
using UnityEngine;

public class DynamicBox : MonoBehaviour, IInteractable
{
    private AdventurerMovement movement;
    
    public bool IsInteracting { get; }
    
    public void Interact(Transform interactor = null)
    {
        if (movement == null)
            movement = interactor.GetComponent<AdventurerMovement>();
        
        movement.SetGroundAnimation(true);
    }

    public void UnInteract()
    {
        
    }
}
