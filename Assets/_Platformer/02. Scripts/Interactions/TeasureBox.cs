using System.Collections;
using Platformer;
using UnityEngine;

namespace Platformer
{
    public class TeasureBox : MonoBehaviour, IInteractable
    {
        private ItemManager itemManager;

        private Animator anim;

        public bool IsInteracting { get; private set; }
        private bool isOpen = false;

        void Start()
        {
            itemManager = FindFirstObjectByType<ItemManager>();
            anim = GetComponent<Animator>();
        }

        public void Interact(Transform interactor = null)
        {
            IsInteracting = true;
            StartCoroutine(InteractRoutine());
        }

        IEnumerator InteractRoutine()
        {
            while (IsInteracting && !isOpen)
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    isOpen = true;
                    anim.SetTrigger("Open");

                    int ranCount = Random.Range(10, 20);

                    for (int i = 0; i < ranCount; i++)
                    {
                        itemManager.DropItem(transform.position);
                        yield return new WaitForSeconds(0.1f);
                    }
                    
                    Debug.Log("상자를 열었습니다.");
                    break;
                }

                yield return null;
            }
        }

        public void UnInteract()
        {
            IsInteracting = false;
        }
    }
}