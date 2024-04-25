using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    [HideInInspector]
    public Camera main_Cam;
    private Vector3 mouse_Pos;
    private Rigidbody2D rigidbody2d;
    private float force = 5;

    private void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        mouse_Pos = main_Cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mouse_Pos - transform.position;
        Vector3 rotation = transform.position - mouse_Pos;
        rigidbody2d.velocity = new Vector2(direction.x, direction.y).normalized * force;
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);
    }
}
