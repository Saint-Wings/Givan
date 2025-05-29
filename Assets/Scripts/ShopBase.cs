using UnityEngine;

public class ShopBase : MonoBehaviour
{
    [Header("UI")]
    public GameObject shopPanel;

    [Header("Ссылки")]
    public Player player;
    public InventoryManager inventory;

    [Header("Предметы (ресурсы)")]
    public Item eggItem;
    public Item woolItem;
    public Item milkItem;
    public Item meatItem;

    [Header("Цены покупки")]
    public int chickenPrice = 100;
    public int sheepPrice = 1000;
    public int pigPrice = 4000;
    public int cowPrice = 6000;

    [Header("Цены продажи")]
    public int eggSellPrice = 10;
    public int woolSellPrice = 100;
    public int milkSellPrice = 600;
    public int meatSellPrice = 800;

    void Start()
    {
        shopPanel.SetActive(false); // по умолчанию скрыт
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
    // Покупка животных
    // -------------------

    public void BuyChicken()
    {
        TryBuyAnimal(chickenPrice, "Курица");
        // Можно здесь вызывать появление курицы на сцене
    }

    public void BuySheep()
    {
        TryBuyAnimal(sheepPrice, "Овца");
    }

    public void BuyPig()
    {
        TryBuyAnimal(pigPrice, "Свинья");
    }

    public void BuyCow()
    {
        TryBuyAnimal(cowPrice, "Корова");
    }

    private void TryBuyAnimal(int cost, string name)
    {
        if (player.CanSpendMoney(cost))
        {
            Debug.Log($"{name} куплена!");
            // Здесь можно вызывать Instantiate(prefab)
        }
        else
        {
            Debug.Log("Недостаточно средств для покупки " + name);
        }
    }

    // -------------------
    // Продажа ресурсов
    // -------------------

    public void SellEgg()
    {
        TrySellItem("Яйцо", eggItem, eggSellPrice);
    }

    public void SellWool()
    {
        TrySellItem("Шерсть", woolItem, woolSellPrice);
    }

    public void SellMilk()
    {
        TrySellItem("Молоко", milkItem, milkSellPrice);
    }

    public void SellMeat()
    {
        TrySellItem("Мясо", meatItem, meatSellPrice);
    }

    private void TrySellItem(string name, Item item, int price)
    {
        bool removed = inventory.RemoveItem(item.itemName);

        if (removed)
        {
            player.AddMoney(price);
            Debug.Log($"{name} продано за {price}");
        }
        else
        {
            Debug.Log($"Нет ресурса '{name}' для продажи!");
        }
    }
}
