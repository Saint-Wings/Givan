using UnityEngine;

public class ShopBase : MonoBehaviour
{
    [Header("UI")]
    public GameObject shopPanel;

    [Header("������")]
    public Player player;
    public InventoryManager inventory;

    [Header("�������� (�������)")]
    public Item eggItem;
    public Item woolItem;
    public Item milkItem;
    public Item meatItem;

    [Header("���� �������")]
    public int chickenPrice = 100;
    public int sheepPrice = 1000;
    public int pigPrice = 4000;
    public int cowPrice = 6000;

    [Header("���� �������")]
    public int eggSellPrice = 10;
    public int woolSellPrice = 100;
    public int milkSellPrice = 600;
    public int meatSellPrice = 800;

    void Start()
    {
        shopPanel.SetActive(false); // �� ��������� �����
    }

    public void ToggleShop()
    {
        shopPanel.SetActive(!shopPanel.activeSelf);
    }

    public void CloseShop()
    {
        shopPanel.SetActive(false);
    }

    // -------------------
    // ������� ��������
    // -------------------

    public void BuyChicken()
    {
        TryBuyAnimal(chickenPrice, "������");
        // ����� ����� �������� ��������� ������ �� �����
    }

    public void BuySheep()
    {
        TryBuyAnimal(sheepPrice, "����");
    }

    public void BuyPig()
    {
        TryBuyAnimal(pigPrice, "������");
    }

    public void BuyCow()
    {
        TryBuyAnimal(cowPrice, "������");
    }

    private void TryBuyAnimal(int cost, string name)
    {
        if (player.CanSpendMoney(cost))
        {
            Debug.Log($"{name} �������!");
            // ����� ����� �������� Instantiate(prefab)
        }
        else
        {
            Debug.Log("������������ ������� ��� ������� " + name);
        }
    }

    // -------------------
    // ������� ��������
    // -------------------

    public void SellEgg()
    {
        TrySellItem("����", eggItem, eggSellPrice);
    }

    public void SellWool()
    {
        TrySellItem("������", woolItem, woolSellPrice);
    }

    public void SellMilk()
    {
        TrySellItem("������", milkItem, milkSellPrice);
    }

    public void SellMeat()
    {
        TrySellItem("����", meatItem, meatSellPrice);
    }

    private void TrySellItem(string name, Item item, int price)
    {
        bool removed = inventory.RemoveItem(item.itemName);

        if (removed)
        {
            player.AddMoney(price);
            Debug.Log($"{name} ������� �� {price}");
        }
        else
        {
            Debug.Log($"��� ������� '{name}' ��� �������!");
        }
    }
}
