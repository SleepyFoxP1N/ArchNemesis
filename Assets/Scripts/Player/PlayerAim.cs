using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class PlayerAim : MonoBehaviour
{
    private Vector3 mousePosition;

    public void OnAim(Camera main_Camera, GameObject weapon_RP)
    {
        mousePosition = main_Camera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = mousePosition - transform.position;
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        weapon_RP.transform.rotation = Quaternion.Euler(0, 0, rotZ);
    }

    // TODO: CHARACTER AND WEAPON DIRECTION FACING
}
