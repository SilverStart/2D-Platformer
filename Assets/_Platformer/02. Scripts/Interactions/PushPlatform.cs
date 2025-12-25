using System.Collections;
using Platformer;
using UnityEngine;

public class PushPlatform : MonoBehaviour, IInteractable
{
    private AdventurerMovement movement;
    private Animator anim;

    private Coroutine pushRoutine;

    [SerializeField] private float pushPower = 50f;

    public bool IsInteracting { get; }

    public void Interact(Transform interactor = null)
    {
        pushRoutine = StartCoroutine(PushRoutine(interactor));
    }

    IEnumerator PushRoutine(Transform interactor)
    {
        yield return new WaitForSeconds(1f);

        Rigidbody2D rb = interactor.GetComponent<Rigidbody2D>();
        
        movement = interactor.GetComponent<AdventurerMovement>();
        movement.SetGroundAnimation(true);

        if (rb != null)
            rb.AddForceY(pushPower, ForceMode2D.Impulse);
    }

    public void UnInteract()
    {
        movement.SetGroundAnimation(false);
        StopCoroutine(pushRoutine);
    }
}