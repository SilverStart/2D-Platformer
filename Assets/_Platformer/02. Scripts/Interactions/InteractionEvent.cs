using System;
using UnityEngine;

public class InteractionEvent : MonoBehaviour
{
    public enum InteractionType { SignPost, Door, NPC, Portal }
    public InteractionType interactionType;

    public GameObject signPostUI;
    public GameObject house;
    public GameObject houseRoof;
    public GameObject env;
    public GameObject npcDialogueUI;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            switch (interactionType)
            {
                case InteractionType.SignPost:
                    signPostUI.SetActive(true);
                    break;
                case InteractionType.Door:
                    Debug.Log("건물 안으로 이동");
                    break;
                case InteractionType.NPC:
                    Debug.Log("대화창 켜짐");
                    break;
                case InteractionType.Portal:
                    Debug.Log("사냥터 씬으로 전환");
                    break;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            switch (interactionType)
            {
                case InteractionType.SignPost:
                    signPostUI.SetActive(false);
                    break;
                case InteractionType.Door:
                    Debug.Log("건물 밖으로 이동");
                    break;
                case InteractionType.NPC:
                    Debug.Log("대화창 꺼짐");
                    break;
                case InteractionType.Portal:
                    break;
            }
        }
    }
}