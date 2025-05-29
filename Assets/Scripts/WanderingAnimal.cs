using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WanderingAnimal : Animal
{
    [Header("Wandering Settings")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float minIdleTime = 1f;
    [SerializeField] private float maxIdleTime = 4f;
    [SerializeField][Range(0, 0.1f)] private float directionChangeChance = 0.01f;
    [SerializeField] private float minMoveTime = 2f;

    [Header("Grass Attraction")]
    [SerializeField] private float grassDetectionRadius = 5f;
    [SerializeField] private float minTimeBetweenGrassVisits = 10f;
    [SerializeField] private float timeToConsumeGrass = 2f;

    private bool isWaiting;
    private Vector2 currentDirection;
    private float lastDirectionChangeTime;
    private float lastGrassVisitTime;
    private GameObject targetGrass;
    private bool isConsumingGrass;

    protected override void Awake()
    {
        base.Awake();
        SetRandomDirection();
        lastGrassVisitTime = -minTimeBetweenGrassVisits; // Чтобы можно было сразу искать траву
    }

    private void Update()
    {
        base.Update();

        if (isConsumingGrass) return;

        if (!isWaiting)
        {
            if (CanSeekGrass() && TryFindNearbyGrass())
            {
                StartCoroutine(ConsumeGrassRoutine());
            }
            else
            {
                Move();
                CheckForDirectionChange();
                CheckForRest();
            }
        }
    }

    private bool CanSeekGrass()
    {
        return Time.time - lastGrassVisitTime >= minTimeBetweenGrassVisits;
    }

    private bool TryFindNearbyGrass()
    {
        Collider2D[] nearbyObjects = Physics2D.OverlapCircleAll(transform.position, grassDetectionRadius);
        List<GameObject> grassList = new List<GameObject>();

        foreach (Collider2D collider in nearbyObjects)
        {
            if (collider.CompareTag("Grass")) // Убедитесь, что ваша трава имеет тег "Grass"
            {
                grassList.Add(collider.gameObject);
            }
        }

        if (grassList.Count > 0)
        {
            targetGrass = grassList[Random.Range(0, grassList.Count)];
            return true;
        }

        return false;
    }

    private IEnumerator ConsumeGrassRoutine()
    {
        isConsumingGrass = true;
        Vector2 startPosition = transform.position;
        float startTime = Time.time;
        float journeyLength = Vector2.Distance(startPosition, targetGrass.transform.position);

        // Движение к траве
        while (Vector2.Distance(transform.position, targetGrass.transform.position) > 0.1f)
        {
            float distCovered = (Time.time - startTime) * moveSpeed;
            float fractionOfJourney = distCovered / journeyLength;
            transform.position = Vector2.Lerp(startPosition, targetGrass.transform.position, fractionOfJourney);
            yield return null;
        }

        // "Поедание" травы
        yield return new WaitForSeconds(timeToConsumeGrass);

        // Уничтожение травы (опционально)
        Destroy(targetGrass);

        lastGrassVisitTime = Time.time;
        isConsumingGrass = false;
        SetRandomDirection();
    }

    private void SetRandomDirection()
    {
        currentDirection = Random.insideUnitCircle.normalized;
    }

    private void Move()
    {
        Vector2 newPosition = (Vector2)transform.position + currentDirection * moveSpeed * Time.deltaTime;
        transform.position = newPosition;
    }

    private void CheckForDirectionChange()
    {
        if (Time.time - lastDirectionChangeTime < minMoveTime) return;

        bool nearBorder =
            transform.position.x <= movementBounds.min.x + 0.5f ||
            transform.position.x >= movementBounds.max.x - 0.5f ||
            transform.position.y <= movementBounds.min.y + 0.5f ||
            transform.position.y >= movementBounds.max.y - 0.5f;

        if (nearBorder || Random.value < directionChangeChance)
        {
            SetRandomDirection();
            lastDirectionChangeTime = Time.time;
        }
    }

    private void CheckForRest()
    {
        if (Random.value < 0.005f)
        {
            StartCoroutine(RestRoutine());
        }
    }

    private IEnumerator RestRoutine()
    {
        isWaiting = true;
        yield return new WaitForSeconds(Random.Range(minIdleTime, maxIdleTime));
        isWaiting = false;
    }

    private void OnDrawGizmosSelected()
    {
        // Визуализация радиуса обнаружения травы
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, grassDetectionRadius);
    }
}