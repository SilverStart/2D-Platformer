using Platformer;
using UnityEngine;

public class Ladder : MonoBehaviour, IInteractable
{
    private AdventurerMovement movement;
    
    public bool IsInteracting { get; }
    
    public void Interact(Transform interactor = null)
    {
        if (movement == null)
            movement = interactor.GetComponent<AdventurerMovement>();

        movement.IsLadder = true;
    }

    public void UnInteract()
    {
        movement.IsLadder = false;
    }
}