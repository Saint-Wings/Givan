using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float movingSpeed = 10f;
    private Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        var inputVector = GameInput.Instance.GetMovemmentVector();
        inputVector.Normalize();
        rb.MovePosition(rb.position+inputVector*(movingSpeed * Time.fixedDeltaTime));
    }
}
