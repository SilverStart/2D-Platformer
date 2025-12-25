using UnityEngine;

namespace Platformer
{
    public interface IInteractable
    {
        bool IsInteracting { get; }

        void Interact(Transform interactor = null);
        void UnInteract();
    }
}