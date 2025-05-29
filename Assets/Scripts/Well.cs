using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Well : MonoBehaviour
{
    public int maxFullness = 5;
    private int currentFullness;
    private bool isRefilling = false;

    public bool playerDrewWater { get; private set; } = false;

    public Text fullnessText;
    public SpriteRenderer spriteRenderer;

    public Sprite fullSprite;  // Спрайт, когда колодец наполнен
    public Sprite emptySprite; // Спрайт, когда колодец пуст

    void Start()
    {
        currentFullness = maxFullness;
        UpdateVisual();
    }

    void OnMouseDown()
    {
        playerDrewWater = false;

        if (isRefilling || currentFullness <= 0)
            return;

        currentFullness--;
        playerDrewWater = true;
        UpdateVisual();

        if (currentFullness == 0)
        {
            StartCoroutine(RefillAfterDelay(10f));
        }
    }

    void UpdateVisual()
    {
        if (fullnessText != null)
            fullnessText.text = "Наполненность: " + currentFullness;

        if (spriteRenderer != null)
        {
            if (currentFullness == 0 && emptySprite != null)
                spriteRenderer.sprite = emptySprite;
            else if (currentFullness == maxFullness && fullSprite != null)
                spriteRenderer.sprite = fullSprite;
        }
    }

    IEnumerator RefillAfterDelay(float delay)
    {
        isRefilling = true;
        yield return new WaitForSeconds(delay);

        currentFullness = maxFullness;
        UpdateVisual();
        isRefilling = false;
    }
}
