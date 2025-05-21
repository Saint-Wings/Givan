using UnityEngine;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }
    private PlayerAction playerInputAction;
    private void Awake()
    {
        Instance = this;
        playerInputAction = new PlayerAction();
        playerInputAction.Enable();
    }

    public Vector2 GetMovemmentVector() =>
        playerInputAction.Player.Moving.ReadValue<Vector2>();
}
