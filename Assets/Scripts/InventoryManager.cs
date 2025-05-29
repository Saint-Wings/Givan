using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
    public InventorySlot[] slots;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public bool AddItem(Item item)
    {
        foreach (InventorySlot slot in slots)
        {
            if (slot.IsEmpty())
            {
                slot.AddItem(item);
                return true;
            }
        }

        Debug.Log("Инвентарь заполнен!");
        return false;
    }

    public bool RemoveItem(string itemName)
    {
        foreach (InventorySlot slot in slots)
        {
            Item item = slot.GetItem();
            if (item != null && item.itemName == itemName)
            {
                slot.ClearSlot();
                return true;
            }
        }

        Debug.Log($"Нет предмета '{itemName}' в инвентаре!");
        return false;
    }
}
