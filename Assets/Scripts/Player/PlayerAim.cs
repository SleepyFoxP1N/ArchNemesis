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
    [SerializeField] private GameObject weapon;
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
            CheckIfAimingLeft(rotZ);
        }
    }

    private void CheckIfAimingLeft(float rotZ)
    {
        if(rotZ >= 90f || rotZ <= -90f)
        {
            GetComponent<PhotonView>().RPC("FlipObject", RpcTarget.All, true);
        }
        else
        {
            GetComponent<PhotonView>().RPC("FlipObject", RpcTarget.All, false);
        }
    }

    [PunRPC]
    public void FlipObject(bool flip)
    {
        if (flip)
        {
            weapon.transform.localScale = new Vector3(weapon.transform.localScale.x, -1 * Mathf.Abs(weapon.transform.localScale.y), weapon.transform.localScale.z);
        }
        else
        {
            weapon.transform.localScale = new Vector3(weapon.transform.localScale.x, Mathf.Abs(weapon.transform.localScale.y), weapon.transform.localScale.z);
        }
    }

    private void Update()
    {
        animator.SetFloat("MouseAngle", rotZ);
        OnAim();
    }
}
