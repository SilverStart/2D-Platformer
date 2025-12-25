using UnityEngine;
using UnityEngine.UI;

namespace Platformer
{
    public class Slot : MonoBehaviour
    {
        private IItem item; // 슬롯에 들어올 아이템
        [SerializeField] private Button slotButton; // 먹은 아이템의 Use() 기능이 실행될 버튼
        [SerializeField] private Image slotImage; // 먹은 아이템의 이미지가 들어갈 UI

        // private bool isEmpty = true; // 일반적인 프로퍼티 초기화
        // public bool IsEmpty
        // {
        //     get
        //     {
        //         return isEmpty;
        //     }
        //     private set
        //     {
        //         isEmpty = value;
        //     }
        // }

        public bool IsEmpty { get; set; } = true; // 자동구현 프로퍼티 초기화
        
        void Awake()
        {
            slotButton.onClick.AddListener(UseItem);
        }

        void OnEnable()
        {
            slotImage.gameObject.SetActive(!IsEmpty);
            slotButton.interactable = !IsEmpty;
            
            // if (!IsEmpty)
            // {
            //     slotImage.gameObject.SetActive(true);
            //     slotButton.interactable = true;
            // }
            // else
            // {
            //     slotImage.gameObject.SetActive(false);
            //     slotButton.interactable = false;
            // }
        }

        public void AddItem(IItem item)
        {
            Debug.Log("AddItem");
            this.item = item;
            IsEmpty = false;
            slotImage.sprite = item.Icon;
            slotImage.SetNativeSize();
            
            slotImage.gameObject.SetActive(!IsEmpty);
            slotButton.interactable = !IsEmpty;
        }
        
        private void UseItem()
        {
            if (item == null)
                return;

            item.Use();
            item = null;
            IsEmpty = true;
            slotImage.gameObject.SetActive(false);
            slotButton.interactable = false;
        }
    }
}