using UnityEngine;
using TMPro;

public class MoneyDisplay : MonoBehaviour
{
    public Player player; 
    public TextMeshProUGUI moneyText; 
    void Update()
    {
        moneyText.text = player.Money.ToString();
    }
}
