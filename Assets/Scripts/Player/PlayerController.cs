using Photon.Pun;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;

public class PlayerController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float MovementSpeed = 5f;


    [Header("Components")]
    [SerializeField] private Camera main_Cam;
    [SerializeField] private GameObject weapon_RP; // RotatePoint 
    [SerializeField] private Weapon weapon_SO; //ScriptableObject
    [SerializeField] private GameObject bullet;
    [SerializeField] private Animator animator;
    private Rigidbody2D rigidbody2D;


    [Header("Scripts")]
    [SerializeField] private PlayerAim playerAim;
    [SerializeField] private PlayerShoot playerShoot;
    [SerializeField] private PlayerStateHandler playerStateHandler;


    // ===== ===== ===== ===== ===== VARIABLES DECLARATION
    private Vector2 moveDirection = new Vector2(0.0f, 0.0f);

    public PlayerInputActions playerControls;
    private InputAction movement;
    private InputAction shoot;


    // ———————————————————————————————————————————————————————————————————— //
    //                            UNITY METHODS                             //
    // ———————————————————————————————————————————————————————————————————— //

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        playerControls = new PlayerInputActions();
        playerStateHandler.CurrentState = PlayerStateHandler.PlayerStateEnum.Idle;
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
        moveDirection = new Vector2(moveDirection.x, moveDirection.y).normalized;
    }
    void FixedUpdate()
    {
        playerAim.OnAim();
        rigidbody2D.MovePosition(rigidbody2D.position + (moveDirection * MovementSpeed * Time.fixedDeltaTime));
        OnAnimate();
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
        GetComponent<PhotonView>().RPC("OnShoot", RpcTarget.All);
    }

    private void OnAnimate()
    {
        if (Mathf.Abs(moveDirection.x) > 0.1 || Mathf.Abs(moveDirection.y) > 0.1)
            animator.SetBool("IsPlayerMoving", true);
        else
            animator.SetBool("IsPlayerMoving", false);
    }
}