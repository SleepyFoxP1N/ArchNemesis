using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerAim : MonoBehaviour
{
    private Vector3 mousePosition;
    private PhotonView photonView;
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject weaponRotatePoint;
    [SerializeField] private Animator animator;
    public bool isLocalPlayer;
    private float rotZ;

    private void Start()
    {
        photonView = GetComponent<PhotonView>(); 
    }
    public void OnAim()
    {
        if (photonView.IsMine && isLocalPlayer)
        {
            mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
            Vector3 rotation = mousePosition - transform.position;
            rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
            weaponRotatePoint.transform.rotation = Quaternion.Euler(0, 0, rotZ);
        }
    }

    private void Update()
    {
        animator.SetFloat("MouseAngle", rotZ);
    }

    // TODO: CHARACTER AND WEAPON DIRECTION FACING
}
