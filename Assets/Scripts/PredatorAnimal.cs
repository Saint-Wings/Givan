using UnityEngine;

public class PredatorAnimal : Animal
{
    [Header("Predator Settings")]
    [SerializeField] private float chaseSpeed = 4f;
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private float detectionRadius = 10f;
    [SerializeField] private float targetUpdateInterval = 1f;

    private float lastTargetUpdateTime;
    private WanderingAnimal[] potentialPrey;

    private void Start()
    {
        potentialPrey = FindObjectsOfType<WanderingAnimal>();
        lastTargetUpdateTime = -targetUpdateInterval;

        // Фиксируем вращение по Z
        transform.rotation = Quaternion.identity;
    }

    protected override void Update()
    {
        if (Time.time - lastTargetUpdateTime >= targetUpdateInterval)
        {
            FindClosestPrey();
            lastTargetUpdateTime = Time.time;
        }

        if (currentTarget != null)
        {
            ChaseTarget();
        }
    }

    private void FindClosestPrey()
    {
        WanderingAnimal closestPrey = null;
        float closestDistance = Mathf.Infinity;

        foreach (WanderingAnimal prey in potentialPrey)
        {
            if (prey == null) continue;

            float distance = Vector2.Distance(transform.position, prey.transform.position);
            if (distance < detectionRadius && distance < closestDistance)
            {
                closestDistance = distance;
                closestPrey = prey;
            }
        }

        SetTarget(closestPrey?.transform);
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
            UpdateSpriteDirection(); // Обновляем направление при движении
        }
        else
        {
            AttackPrey();
        }
    }

    private void AttackPrey()
    {
        Debug.Log($"{name} атакует {currentTarget.name}!");
    }
}