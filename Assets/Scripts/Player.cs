using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float movingSpeed = 10f;
    public int Money;
    private Rigidbody2D rb;
    private void Awake()
    {
        Money = 20; //delete this after tests
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Debug.Log(Money);
        var inputVector = GameInput.Instance.GetMovemmentVector();
        inputVector.Normalize();
        rb.MovePosition(rb.position+inputVector*(movingSpeed * Time.fixedDeltaTime));
    }
    public void AddMoney(int amount) => Money += amount;

    public bool CanSpendMoney(int amount)
    {
        if (Money >= amount)
        {
            Money -= amount;
            return true;
        }
        else
            return false;
    }
}
