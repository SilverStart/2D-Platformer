using System;
using UnityEngine;

namespace Platformer
{
    public class Coin : MonoBehaviour, IInteractable, IItem
    {
        public enum CoinType { Red, Green, Blue, Purple, Gold }
        public CoinType type;
        
        public ItemManager Inventory { get; private set; }
        public GameObject Obj { get; private set; }
        public string ItemName { get; private set; }
        
        public Sprite Icon { get; private set; }
        
        public bool IsInteracting { get; }

        private int cost;

        void Start()
        {
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
            switch (type)
            {
                case CoinType.Red:
                    cost = 10;
                    break;
                case CoinType.Green:
                    cost = 20;
                    break;
                case CoinType.Blue:
                    cost = 30;
                    break;
                case CoinType.Purple:
                    cost = 40;
                    break;
                case CoinType.Gold:
                    cost = 100;
                    break;
            }

            // playerCash += cost;
            Debug.Log($"금화 {cost}개를 획득하였습니다.");
        }
    }
}