using System.Collections;
using UnityEngine;

public class WanderingAnimal : Animal
{
    [Header("Wandering Settings")]
    [SerializeField] private float moveSpeed = 1.5f;
    [SerializeField] private float minIdleTime = 1f;
    [SerializeField] private float maxIdleTime = 4f;
    [SerializeField] private float wanderRadius = 5f;

    private Vector2 wanderCenter;
    private bool isWaiting;

    protected override void Awake()
    {
        base.Awake();
        wanderCenter = transform.position;
        SetRandomTarget();

        // Фиксируем вращение по Z
        transform.rotation = Quaternion.identity;
    }

    protected override void Update()
    {
        if (!isWaiting)
        {
            MoveToTarget();
            CheckDistance();
        }
    }

    private void MoveToTarget()
    {
        transform.position = Vector2.MoveTowards(
            transform.position,
            currentTarget.position,
            moveSpeed * Time.deltaTime
        );
    }

    private void CheckDistance()
    {
        if (Vector2.Distance(transform.position, currentTarget.position) <= stoppingDistance)
        {
            StartCoroutine(WaitAndMove());
        }
    }

    private IEnumerator WaitAndMove()
    {
        isWaiting = true;
        yield return new WaitForSeconds(Random.Range(minIdleTime, maxIdleTime));
        SetRandomTarget();
        isWaiting = false;
    }

    private void SetRandomTarget()
    {
        Vector2 randomPoint = wanderCenter + Random.insideUnitCircle * wanderRadius;
        currentTarget = new GameObject("WanderTarget").transform;
        currentTarget.position = randomPoint;
        UpdateSpriteDirection(); // Обновляем направление спрайта
    }
}