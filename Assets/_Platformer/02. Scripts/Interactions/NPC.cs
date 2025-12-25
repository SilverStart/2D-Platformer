using System.Collections;
using TMPro;
using UnityEngine;

namespace Platformer
{
    public class NPC : MonoBehaviour, IInteractable
    {
        [SerializeField] private NPCUI ui;
        [SerializeField] private NPCDataSO data;
   
        [SerializeField] private float typingSpeed = 0.1f;
        
        private Coroutine typingRoutine;

        public bool IsInteracting { get; private set; }
        
        public void Interact(Transform interactor = null)
        {
            ui.SetData(data.name, data.age, data.gender, data.job, data.description, data.icon);
            ui.DescriptionUI.text = "";
            
            IsInteracting = true;
            // ui.gameObject.SetActive(true);

            typingRoutine = StartCoroutine(InteractRoutine());
        }

        IEnumerator InteractRoutine()
        {
            Debug.Log($"{data.name}과 상호작용하려면 'F' 키를 누르세요.");
            
            while (IsInteracting)
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    ui.gameObject.SetActive(true);
                    break;
                }
                
                yield return null;
            }
            
            int textLength = data.description.Length;
            for (int i = 0; i < textLength; i++)
            {
                ui.DescriptionUI.text += data.description[i];
            
                yield return new WaitForSeconds(typingSpeed);
            }
        }
        
        public void UnInteract()
        {
            if (typingRoutine != null)
                StopCoroutine(typingRoutine);
            
            IsInteracting = false;
            ui.gameObject.SetActive(false);
        }
    }
}