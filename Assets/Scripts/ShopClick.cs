using UnityEngine;

public class ShopTrigger : MonoBehaviour
{
    public ShopBase shopManager;

    void OnMouseDown()
    {
        Debug.Log("���� �� ������� ��������!");
        shopManager.ToggleShop();
    }
}