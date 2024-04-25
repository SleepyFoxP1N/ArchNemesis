using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateHandler : MonoBehaviour
{
    private static PlayerStateHandler instance;
    public static PlayerStateHandler Instance { get { return instance; } }
    private void Awake() => instance = this;

    public enum PlayerStateEnum
    {
        Idle,
        Running,
        Shooting,
        Death
    }

    private PlayerStateEnum currentState;
    public PlayerStateEnum CurrentState { get => currentState; set => currentState = value; }
}

