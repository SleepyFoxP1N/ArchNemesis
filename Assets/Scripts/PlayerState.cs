using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    private static PlayerState instance;
    public static PlayerState Instance { get { return instance; } }
    private void Awake() => instance = this;

    public enum PlayerStateEnum
    {
        Idle,
        Running,
        Shooting,
        Death
    }

    // ===== ===== ===== ===== ===== Current player state
    private PlayerStateEnum currentState;
    public PlayerStateEnum CurrentState { get => currentState; set => currentState = value; }
}

