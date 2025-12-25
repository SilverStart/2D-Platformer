using UnityEngine;

namespace Platformer
{
    public class Potion : MonoBehaviour, IInteractable, IItem
    {
        public enum PotionType { Red, Blue }
        public PotionType type;

        private AdventurerAttack playerAttack;
        
        public ItemManager Inventory { get; private set; }
        public GameObject Obj { get; private set; }
        public string ItemName { get; private set; }
        
        public Sprite Icon { get; private set; }
        
        public bool IsInteracting { get; }

        void Start()
        {
            playerAttack = FindFirstObjectByType<AdventurerAttack>();
            Inventory = FindFirstObjectByType<ItemManager>();
            Obj = gameObject;
            ItemName = gameObject.name;
            Icon = GetComponent<SpriteRenderer>().sprite;
        }
        
        public void Interact(Transform interactor = null)
        {
            Get();
        }

        public void UnInteract() { }
        
        public void Get()
        {
            Debug.Log($"{type}을 획득하였습니다.");
            
            Inventory.GetItem(this);
            Destroy(gameObject); // Active(false) 또는 Destory
        }

        public void Use()
        {
            if (type == PotionType.Red)
            {
                Debug.Log("체력 50 회복");
                // playerAttack.Hp += 50;
                playerAttack.Heal(5);

            }
            else if (type == PotionType.Blue)
            {
                Debug.Log("마나 50 회복");
            }
        }
    }
}