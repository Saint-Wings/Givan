using UnityEngine;

public class ShopTrigger : MonoBehaviour
{
    public ShopBase shopManager;

    void OnMouseDown()
    {
        Debug.Log("Клик по объекту магазина!");
        shopManager.ToggleShop();
    }
}