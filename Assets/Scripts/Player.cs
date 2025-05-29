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
        var inputVector = GameInput.Instance.GetMovemmentVector();
        inputVector.Normalize();
        rb.MovePosition(rb.position+inputVector*(movingSpeed * Time.fixedDeltaTime));
    }
}
