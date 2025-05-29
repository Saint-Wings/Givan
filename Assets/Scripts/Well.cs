using UnityEngine;

public class Well : MonoBehaviour
{
    [Header("��������� �������")]
    public bool playerDrewWater = false;
    public KeyCode interactKey = KeyCode.E;
    public float interactRadius = 2f;

    [Header("�����")]
    public AudioClip drawWaterSound;
    private AudioSource audioSource;

    private void Start()
    {
        // ��������� � ����������� ��������� AudioSource
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 1f; // ��� 2D ���� ����� ��������� 0
    }

    private void Update()
    {
        // ��������� ������� ������� ����� � ��������
        if (Input.GetKeyDown(interactKey) && IsPlayerNear())
        {
            DrawWater();
        }
    }

    private bool IsPlayerNear()
    {
        // ���� ������ �� ���� (��������� ��� � ������ ������ ���� ��� "Player")
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) return false;

        // ��������� ����������
        return Vector2.Distance(transform.position, player.transform.position) <= interactRadius;
    }

    public void DrawWater()
    {
        playerDrewWater = true;
        PlaySound(drawWaterSound);
        Debug.Log("�� ������� ���� �� �������!");
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
        // ������������ ������� �������������� � ���������
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, interactRadius);
    }
}