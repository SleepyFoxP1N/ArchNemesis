using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;

public class PlayerController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Vector2 MovementSpeed = new Vector2(100.0f, 100.0f);


    // ===== ===== ===== ===== ===== COMPONENTS
    private new Rigidbody2D rigidbody2D;


    public PlayerInputActions playerControls;
    private InputAction movement;
    private InputAction shoot;


    // ===== ===== ===== ===== ===== VARIABLES DECLARATION
    private Vector2 moveDirection = new Vector2(0.0f, 0.0f);


    // ———————————————————————————————————————————————————————————————————— //
    //                            UNITY METHODS                             //
    // ———————————————————————————————————————————————————————————————————— //

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        playerControls = new PlayerInputActions();
        //PlayerStateHandler.Instance.CurrentState = PlayerStateHandler.PlayerStateEnum.Idle;
    }

    private void OnEnable()
    {
        SetUpPlayerControls();
        EnablePlayerControls();
        shoot.performed += OnShoot;
    }
    private void OnDisable()
    {
        DisablePlayerControls();
    }

    void Update()
    {
        moveDirection = movement.ReadValue<Vector2>();
    }
    void FixedUpdate()
    {
        rigidbody2D.MovePosition(rigidbody2D.position + (moveDirection * MovementSpeed * Time.fixedDeltaTime));
    }

    // ———————————————————————————————————————————————————————————————————— //
    //                          PERSONAL METHODS                            //
    // ———————————————————————————————————————————————————————————————————— //

    private void SetUpPlayerControls()
    {
        movement = playerControls.Player.Move;
        shoot = playerControls.Player.Fire;
    }
    private void EnablePlayerControls()
    {
        movement.Enable();
        shoot.Enable();
    }
    private void DisablePlayerControls()
    {
        movement.Disable();
        shoot.Disable();
    }

    private void OnShoot(InputAction.CallbackContext context)
    {
        Debug.Log(moveDirection);
    }
}