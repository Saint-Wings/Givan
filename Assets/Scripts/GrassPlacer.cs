using UnityEngine;
using UnityEngine.UI; // Для работы с UI

public class GrassPlacer : MonoBehaviour
{
    [Header("Настройки травы")]
    public GameObject grassPrefab;       // Префаб травы
    public KeyCode placeKey = KeyCode.G; // Клавиша для размещения
    public LayerMask groundLayer;        // Слой, на котором можно размещать траву
    public bool canPlaceGrass = false;   // Флаг, разрешающий размещение

    [Header("Лимит травы")]
    public int maxGrass = 10;            // Максимальное количество травы
    private int currentGrass = 0;        // Текущее количество травы

    [Header("UI Элементы")]
    public Text grassCounterText;        // Текст для отображения счетчика

    void Start()
    {
        UpdateGrassCounterUI(); // Обновляем UI при старте
    }

    void Update()
    {
        if (canPlaceGrass && Input.GetKeyDown(placeKey))
        {
            TryPlaceGrass();
        }
    }

    void TryPlaceGrass()
    {
        // Если достигнут лимит - выходим
        if (currentGrass >= maxGrass)
        {
            Debug.Log("Достигнут лимит травы!");
            return;
        }

        // Получаем все объекты с тегом "Ground_0"
        GameObject[] groundTiles = GameObject.FindGameObjectsWithTag("Ground_0");

        foreach (GameObject groundTile in groundTiles)
        {
            // Проверяем расстояние до игрока (чтобы не ставить траву слишком далеко)
            if (Vector2.Distance(transform.position, groundTile.transform.position) < 5f)
            {
                // Размещаем траву как дочерний объект
                Instantiate(grassPrefab, groundTile.transform.position, Quaternion.identity, groundTile.transform);
                currentGrass++; // Увеличиваем счетчик
                UpdateGrassCounterUI(); // Обновляем UI
                break; // Размещаем только одну траву за нажатие
            }
        }
    }

    // Обновляет UI счетчика травы
    void UpdateGrassCounterUI()
    {
        if (grassCounterText != null)
        {
            grassCounterText.text = $"Трава: {currentGrass}/{maxGrass}";
        }
    }

    // Метод для изменения флага (можно вызывать из других скриптов)
    public void SetCanPlaceGrass(bool value)
    {
        canPlaceGrass = value;
    }

    // Метод для сброса счетчика (если нужно)
    public void ResetGrassCounter()
    {
        currentGrass = 0;
        UpdateGrassCounterUI();
    }
}