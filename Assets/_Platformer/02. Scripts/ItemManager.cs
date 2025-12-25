using UnityEngine;

namespace Platformer
{
    public class ItemManager : MonoBehaviour
    {
        [SerializeField] private GameObject[] itemPrefabs;

        [SerializeField] private Transform slotGroup;
        [SerializeField] private Slot[] slots;

        void Start()
        {
            slots = slotGroup.GetComponentsInChildren<Slot>();
        }

        public void DropItem(Vector3 dropPos)
        {
            int randomIndex = Random.Range(0, itemPrefabs.Length); // 생성할 랜덤 아이템 번호 결정
            
            GameObject item = Instantiate(itemPrefabs[randomIndex], dropPos, Quaternion.identity); // 아이템 생성
            
            Rigidbody2D itemRb = item.GetComponent<Rigidbody2D>();
            
            // 랜덤한 방향으로 아이템 날리기
            itemRb.AddForceX(Random.Range(-2f, 2f), ForceMode2D.Impulse);
            itemRb.AddForceY(3f, ForceMode2D.Impulse);
            
            // 날아가는 아이템에 회전 적용
            itemRb.AddTorque(Random.Range(-1.5f, 1.5f), ForceMode2D.Impulse);
        }

        public void GetItem(IItem item)
        {
            foreach (var slot in slots)
            {
                if (slot.IsEmpty)
                {
                    slot.AddItem(item);
                    break;
                }
            }
        }
    }
}