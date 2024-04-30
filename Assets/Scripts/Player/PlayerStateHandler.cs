using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateHandler : MonoBehaviour
{
    public enum PlayerStateEnum
    {
        Idle,
        Running,
        Shooting,
        Death
    }

    [SerializeField] private PlayerStateEnum currentState;
    public PlayerStateEnum CurrentState { get => currentState; set => currentState = value; }
}

