using UnityEngine;

public abstract class Animal : MonoBehaviour
{
    [Header("Base Settings")]
    [SerializeField] protected float stoppingDistance = 0.5f;

    protected Transform currentTarget;
    protected SpriteRenderer spriteRenderer;

    protected virtual void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Убрали RotateTowardsTarget из базового Update
    protected virtual void Update()
    {
        // Только движение, без поворота
    }

    public virtual void SetTarget(Transform newTarget)
    {
        currentTarget = newTarget;
        UpdateSpriteDirection(); // Обновляем направление спрайта при смене цели
    }

    protected void UpdateSpriteDirection()
    {
        if (currentTarget != null)
        {
            // Разворачиваем спрайт по горизонтали в зависимости от направления
            spriteRenderer.flipX = currentTarget.position.x < transform.position.x;
        }
    }
}