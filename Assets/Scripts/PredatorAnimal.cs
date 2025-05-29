using UnityEngine;

using UnityEngine;

public class PredatorAnimal : Animal
{
    [Header("Predator Settings")]
    [SerializeField] private float chaseSpeed = 4f;
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private float detectionRadius = 10f;
    [SerializeField] private LayerMask preyLayer;

    private void Start()
    {
        InvokeRepeating(nameof(FindClosestPrey), 0.5f, 1f);
    }

    protected override void Update()
    {
        base.Update(); // Обработка границ

        if (currentTarget != null)
        {
            if (currentTarget.gameObject == null)
            {
                currentTarget = null;
                return;
            }

            ChaseTarget();
        }
    }

    private void FindClosestPrey()
    {
        Collider2D[] preyColliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius, preyLayer);

        Transform closestPrey = null;
        float closestDistance = Mathf.Infinity;

        foreach (var prey in preyColliders)
        {
            if (prey == null) continue;

            float distance = Vector2.Distance(transform.position, prey.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestPrey = prey.transform;
            }
        }

        SetTarget(closestPrey);
    }

    private void ChaseTarget()
    {
        float distance = Vector2.Distance(transform.position, currentTarget.position);

        if (distance > attackRange)
        {
            transform.position = Vector2.MoveTowards(
                transform.position,
                currentTarget.position,
                chaseSpeed * Time.deltaTime
            );
            UpdateSpriteDirection();
        }
        else
        {
            AttackPrey();
        }
    }

    private void AttackPrey()
    {
        Debug.Log($"{name} атакует {currentTarget.name}!");
        Destroy(currentTarget.gameObject);
        currentTarget = null;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}