using UnityEngine;
using System.Collections;

public class WanderingAnimal : Animal
{
    [Header("Wandering Settings")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float minIdleTime = 1f;
    [SerializeField] private float maxIdleTime = 4f;
    [SerializeField][Range(0, 0.1f)] private float directionChangeChance = 0.01f;
    [SerializeField] private float minMoveTime = 2f;

    private bool isWaiting;
    private Vector2 currentDirection;
    private float lastDirectionChangeTime;

    protected override void Awake()
    {
        base.Awake();
        SetRandomDirection();
        Debug.Log("Initial Bounds: " + movementBounds);
    }

    private void SetRandomDirection()
    {
        currentDirection = Random.insideUnitCircle.normalized;
        Debug.Log("New Direction: " + currentDirection);
    }

    private void Update()
    {
        base.Update();

        if (!isWaiting)
        {
            Move();
            CheckForDirectionChange();
            CheckForRest();
        }
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
}