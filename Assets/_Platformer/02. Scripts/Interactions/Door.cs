using System.Collections;
using Platformer;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    private SoundManager sound;

    [SerializeField] private FadeEvent fade;

    [SerializeField] private Vector3 houseInPos;
    [SerializeField] private Vector3 houseOutPos;

    [SerializeField] private GameObject[] insideObjs;
    [SerializeField] private GameObject[] outsideObjs;

    public bool IsInteracting { get; private set; }

    void Start()
    {
        sound = FindFirstObjectByType<SoundManager>();
    }

    public void Interact(Transform interactor = null)
    {
        StartCoroutine(InteractRoutine(interactor));
    }

    IEnumerator InteractRoutine(Transform interactor)
    {
        sound.SoundOneShot("Door Open");
        fade.Fade(3, Color.black, true);
        yield return new WaitForSeconds(3f);

        if (!IsInteracting)
        {
            interactor.position = houseInPos;

            foreach (var obj in insideObjs)
                obj.SetActive(true);

            foreach (var obj in outsideObjs)
                obj.SetActive(false);
        }
        else
        {
            interactor.position = houseOutPos;
            
            foreach (var obj in insideObjs)
                obj.SetActive(false);

            foreach (var obj in outsideObjs)
                obj.SetActive(true);
        }

        IsInteracting = !IsInteracting;

        sound.SoundOneShot("Door Close");
        yield return new WaitForSeconds(1f);

        fade.Fade(3, Color.black, false);
    }

    public void UnInteract()
    {

    }
}