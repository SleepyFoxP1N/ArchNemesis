using Photon.Pun;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float MovementSpeed = 5f;


    [Header("Components")]
    [SerializeField] private Camera main_Cam;
    [SerializeField] private GameObject weapon_RP; // RotatePoint 
    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource playerAud;
    private Rigidbody2D rb;


    [Header("Scripts")]
    [SerializeField] private PlayerAim playerAim;
    [SerializeField] private PlayerShoot playerShoot;
    [SerializeField] private PlayerStateHandler playerStateHandler;


    // ===== ===== ===== ===== ===== VARIABLES DECLARATION
    private Vector2 moveDirection = new Vector2(0.0f, 0.0f);

    public PlayerInputActions playerControls;
    private InputAction movement;
    private InputAction shoot;


    // ===== ===== ===== ===== ===== EVENTS
    public event Action<bool> onWalk;
    public event Action<int> onWeaponShoot;
    public event Action<int, int> onWeaponReload; // (arrow, arrowbag)



    // ———————————————————————————————————————————————————————————————————— //
    //                            UNITY METHODS                             //
    // ———————————————————————————————————————————————————————————————————— //

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerControls = new PlayerInputActions();
        playerStateHandler.CurrentState = PlayerStateHandler.PlayerStateEnum.Idle;
    }

    private void OnEnable()
    {
        SetUpPlayerControls();
        EnablePlayerControls();
        shoot.performed += OnShoot;

        playerShoot.OnReloadEvent = onWeaponReload;
        playerShoot.OnShootEvent = onWeaponShoot;
    }
    private void OnDisable()
    {
        DisablePlayerControls();
    }

    void Update()
    {
        moveDirection = movement.ReadValue<Vector2>();
        moveDirection = new Vector2(moveDirection.x, moveDirection.y).normalized;

        OnReload();
    }
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + (moveDirection * MovementSpeed * Time.fixedDeltaTime));
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

    private void OnReload()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            GetComponent<PhotonView>().RPC("ReloadWeapon", RpcTarget.All);
        }
    }

    private void OnAnimate()
    {
        if (Mathf.Abs(moveDirection.x) > 0.1 || Mathf.Abs(moveDirection.y) > 0.1)
        {
            animator.SetBool("IsPlayerMoving", true);
            onWalk?.Invoke(true);
        }
        else
        {
            animator.SetBool("IsPlayerMoving", false);
            onWalk?.Invoke(false);
        }
    }

    private AudioClip thisWalkClip;
    public void playSound(AudioClip _Clip)
    {
        thisWalkClip = _Clip;
        if (!playerAud.isPlaying)
        {
            GetComponent<PhotonView>().RPC("playSoundAllUser", RpcTarget.All);
        }
    }

    [PunRPC]
    private void playSoundAllUser()
    {
        playerAud.PlayOneShot(thisWalkClip);
    }
}