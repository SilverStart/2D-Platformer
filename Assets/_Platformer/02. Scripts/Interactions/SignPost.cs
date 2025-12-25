using System;
using System.Collections;
using Platformer;
using UnityEngine;

public class SignPost : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject signPostUI;
    
    public bool IsInteracting { get; private set; }

    public void Interact(Transform interactor = null)
    {
        IsInteracting = true;
        signPostUI.SetActive(true);

        StartCoroutine(InteractRoutine());
    }

    IEnumerator InteractRoutine()
    {
        while (IsInteracting)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                UnInteract();
            
            yield return null;
        }
    }

    public void UnInteract()
    {
        IsInteracting = false;
        signPostUI.SetActive(false);
    }
}