using UnityEngine;

namespace Platformer
{
    public interface IItem
    {
        ItemManager Inventory { get; }

        GameObject Obj { get; }
        string ItemName { get; }
        Sprite Icon { get; }

        void Get();
        void Use();
    }
}