using UnityEngine;

public abstract class Animal : MonoBehaviour
{
    [Header("Base Settings")]
    [SerializeField] protected float stoppingDistance = 0.5f;
    [SerializeField] protected GameObject[] boundarySprites;

    protected Transform currentTarget;
    protected SpriteRenderer spriteRenderer;
    protected Bounds movementBounds;
    protected Vector2 velocity;

    protected virtual void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (boundarySprites != null && boundarySprites.Length > 0)
        {
            movementBounds = AreaBoundary.CalculateTotalBounds(boundarySprites);
        }
    }

    protected virtual void Update()
    {
        HandleBoundaries();
    }

    public virtual void SetTarget(Transform newTarget)
    {
        currentTarget = newTarget;
        UpdateSpriteDirection();
    }

    protected void UpdateSpriteDirection()
    {
        if (currentTarget != null)
        {
            spriteRenderer.flipX = currentTarget.position.x < transform.position.x;
        }
    }

    protected void HandleBoundaries()
    {
        if (boundarySprites == null || boundarySprites.Length == 0) return;

        // Отталкивание от границ
        float repelForce = 0.1f;
        float repelDistance = 0.5f;

        if (transform.position.x <= movementBounds.min.x)
            velocity += Vector2.right * repelForce;
        else if (transform.position.x >= movementBounds.max.x)
            velocity += Vector2.left * repelForce;

        if (transform.position.y <= movementBounds.min.y)
            velocity += Vector2.up * repelForce;
        else if (transform.position.y >= movementBounds.max.y)
            velocity += Vector2.down * repelForce;

        // Применяем скорость
        transform.position += (Vector3)velocity * Time.deltaTime;
        velocity *= 0.9f; // Постепенное замедление

        // Ограничение позиции
        Vector3 clampedPosition = new Vector3(
            Mathf.Clamp(transform.position.x, movementBounds.min.x, movementBounds.max.x),
            Mathf.Clamp(transform.position.y, movementBounds.min.y, movementBounds.max.y),
            transform.position.z
        );
        transform.position = clampedPosition;
    }
}