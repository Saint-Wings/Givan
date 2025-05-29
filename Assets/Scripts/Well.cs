using UnityEngine;

public class Well : MonoBehaviour
{
    [Header("Настройки колодца")]
    public bool playerDrewWater = false;
    public KeyCode interactKey = KeyCode.E;
    public float interactRadius = 2f;

    [Header("Звуки")]
    public AudioClip drawWaterSound;
    private AudioSource audioSource;

    private void Start()
    {
        // Добавляем и настраиваем компонент AudioSource
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 1f; // Для 2D игры можно поставить 0
    }

    private void Update()
    {
        // Проверяем нажатие клавиши рядом с колодцем
        if (Input.GetKeyDown(interactKey) && IsPlayerNear())
        {
            DrawWater();
        }
    }

    private bool IsPlayerNear()
    {
        // Ищем игрока по тегу (убедитесь что у вашего игрока есть тег "Player")
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) return false;

        // Проверяем расстояние
        return Vector2.Distance(transform.position, player.transform.position) <= interactRadius;
    }

    public void DrawWater()
    {
        playerDrewWater = true;
        PlaySound(drawWaterSound);
        Debug.Log("Вы набрали воды из колодца!");
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Визуализация радиуса взаимодействия в редакторе
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, interactRadius);
    }
}