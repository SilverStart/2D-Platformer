using System.Collections;
using Platformer;
using UnityEngine;

public class MovingPlatform : MonoBehaviour, IInteractable
{
    public enum MoveType { Horizontal, Vertical }
    public MoveType moveType;

    private AdventurerMovement movement;

    private Vector3 initPos;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float power;
    private float theta;
    
    public bool IsInteracting { get; }

    IEnumerator Start()
    {
        initPos = transform.position;

        while (true)
        {
            theta += Time.deltaTime * moveSpeed;
            float pos = power * Mathf.Sin(theta);

            if (moveType == MoveType.Horizontal)
                transform.position = initPos + new Vector3(pos, 0, 0);
            else if (moveType == MoveType.Vertical)
                transform.position = initPos + new Vector3(0, pos, 0);

            yield return null;
        }
    }

    public void Interact(Transform interactor = null)
    {
        if (movement == null)
            movement = interactor.GetComponent<AdventurerMovement>();

        movement.transform.SetParent(transform);
        movement.SetGroundAnimation(true);
    }

    public void UnInteract()
    {
        movement.transform.SetParent(null);
        movement.SetGroundAnimation(false);
    }
}