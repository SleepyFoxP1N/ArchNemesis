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
    private new Rigidbody2D rigidbody2D;


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
        playerAim.OnAim(main_Cam, weapon_RP);
        moveDirection = movement.ReadValue<Vector2>();
        moveDirection = new Vector2(moveDirection.x, moveDirection.y).normalized;
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
        GetComponent<PhotonView>().RPC("OnShoot", RpcTarget.All);
    }

}