using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Transform))]
public class PositionCorrection : MonoBehaviour
{
    [Header("Position Adjustment")]
    public Vector3 positionOffset;

    [Header("Collider Offset")]
    public Collider2D[] colliders;
    public Vector2 colliderOffset;

    private void Awake()
    {
        Vector3 currentPosition = transform.position;
        transform.position = currentPosition + positionOffset;

        foreach (Collider2D collider in colliders)
        {
            collider.offset = colliderOffset;
        }
    }
}
