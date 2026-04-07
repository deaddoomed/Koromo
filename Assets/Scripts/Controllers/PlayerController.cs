using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerInputActions inputActions;
    private Vector2 moveInput;

    private Vector3 movePlayer;

    [SerializeField] private CharacterController player;
    [SerializeField] int playerSpeed = 5;
    float gravity = -60f;
    float fallSpeed;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    private void Start()
    {
        player = GetComponent<CharacterController>();
    }

    private void Update()
    {
        // Leer input del Action Map "Player" → Action "Move"
        moveInput = inputActions.Player.Move.ReadValue<Vector2>();

        Vector3 inputDirection = new Vector3(moveInput.x, 0, moveInput.y);
        inputDirection = Vector3.ClampMagnitude(inputDirection, 1);

        movePlayer.x = inputDirection.x * playerSpeed;
        movePlayer.z = inputDirection.z * playerSpeed;
        SetGravity();

        player.Move(movePlayer * Time.deltaTime);
    }

    void SetGravity()
    {
        if (player.isGrounded && fallSpeed < 0)
        {
            fallSpeed = -2f;
        }
        else
        {
            fallSpeed += gravity * Time.deltaTime;
        }
        movePlayer.y = fallSpeed;
    }
}

