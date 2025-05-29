using UnityEngine;
using System.Collections.Generic;

public class GrassPlacer : MonoBehaviour
{
    [Header("Настройки травы")]
    public GameObject grassPrefab;
    public KeyCode placeKey = KeyCode.G;
    public bool canPlaceGrass = false;

    [Header("Лимиты")]
    [SerializeField] private int maxGrass = 10;
    private int currentGrass = 0;
    private List<GameObject> activeGrass = new List<GameObject>();

    [Header("Ссылки")]
    public Well well;

    [Header("Звуки")]
    [SerializeField] private AudioClip placeSound;
    [SerializeField] private AudioClip limitSound;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    void Update()
    {
        if (canPlaceGrass && Input.GetKeyDown(placeKey))
        {
            TryPlaceGrass();
        }
    }

    public void TryPlaceGrass()
    {
        if (well == null || !well.playerDrewWater)
        {
            Debug.Log("Сначала наберите воду из колодца!");
            return;
        }

        if (currentGrass >= maxGrass)
        {
            Debug.Log("Достигнут лимит травы!");
            PlaySound(limitSound);
            return;
        }

        GameObject[] groundTiles = GameObject.FindGameObjectsWithTag("Ground_0");
        bool placed = false;

        foreach (GameObject groundTile in groundTiles)
        {
            if (Vector2.Distance(transform.position, groundTile.transform.position) < 5f)
            {
                GameObject newGrass = Instantiate(
                    grassPrefab,
                    groundTile.transform.position,
                    Quaternion.identity,
                    groundTile.transform
                );

                // Добавляем компонент для отслеживания травы
                Grass grassComponent = newGrass.AddComponent<Grass>();
                grassComponent.Initialize(this);

                activeGrass.Add(newGrass);
                currentGrass++;
                well.playerDrewWater = false;
                placed = true;
                PlaySound(placeSound);
                break;
            }
        }

        if (!placed) Debug.Log("Рядом нет подходящего места для травы!");
    }

    // Вызывается при уничтожении травы
    public void GrassEaten(GameObject grass)
    {
        if (activeGrass.Contains(grass))
        {
            activeGrass.Remove(grass);
            currentGrass--;
        }
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    public void ResetGrassCounter()
    {
        currentGrass = 0;
        foreach (var grass in activeGrass)
        {
            if (grass != null) Destroy(grass);
        }
        activeGrass.Clear();
    }
}