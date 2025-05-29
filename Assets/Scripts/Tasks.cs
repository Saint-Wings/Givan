using UnityEngine;
using TMPro;

public class Tasks : MonoBehaviour
{
    public Player player;
    public TextMeshProUGUI task;
    void Update()
    {
        task.text = "Надо 2 коровы!";
    }
}

