using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    [SerializeField] private Camera main_Camera;
    [SerializeField] private GameObject bullet;

    private Vector3 mousePosition;

    void Update()
    {
        mousePosition = main_Camera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = mousePosition - transform.position;
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotZ);
    }
}
