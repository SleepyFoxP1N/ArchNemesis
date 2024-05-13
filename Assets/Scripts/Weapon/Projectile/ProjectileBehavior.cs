using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using System;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    public bool IsLocalBullet;
    public Weapon weapon;
    private GameObject[] mainCameras;
    private Camera activeMainCamera;
    private Rigidbody2D rb;

    private int damage;
    private float force;

    private void Awake()
    {
        damage = weapon.Damage;
        force = weapon.Force;
        SetUpProjectile();
        InitializeComponents();
        SetInitialVelocity();
    }

    private void SetUpProjectile()
    {
        GetComponent<SpriteRenderer>().sprite = weapon.ProjectleSprite;
        GetComponent<SpriteRenderer>().color = weapon.SpriteColor;
        gameObject.transform.localScale = weapon.SpriteScale * 3;
    }

    private void InitializeComponents()
    {
        mainCameras = GameObject.FindGameObjectsWithTag("MainCamera");
        rb = GetComponent<Rigidbody2D>();

        if (mainCameras.Length == 0)
        {
            Debug.LogError("No main cameras found!");
        }
        else
        {
            foreach (GameObject cam in mainCameras)
            {
                if (cam.gameObject.activeSelf)
                {
                    activeMainCamera = cam.GetComponent<Camera>();
                    break;
                }
            }

            if (activeMainCamera == null)
            {
                Debug.LogError("No active main camera found!");
            }
        }

        if (rb == null)
        {
            Debug.LogError("Rigidbody2D component not found!");
        }
    }


    private void SetInitialVelocity()
    {
        if (activeMainCamera == null || rb == null)
        {
            return; 
        }

        Vector3 mousePos = activeMainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePos - transform.position).normalized;
        rb.velocity = direction * force;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        Health health = other.GetComponent<Health>();

        if (health == null)
        {
            PhotonNetwork.Instantiate(weapon.ImpactVFX.name, transform.position, Quaternion.identity);
            DestroyProjectile();
            return;
        }

        if (!health.isLocalPlayer && IsLocalBullet)
        {
            PhotonNetwork.Instantiate(weapon.ImpactVFX.name, transform.position, Quaternion.identity);
            ApplyDamage(other.gameObject, health);
            DestroyProjectile();
        }
    }

    private void DestroyProjectile()
    {
        if (GetComponent<PhotonView>().IsMine)
        {
            PhotonNetwork.Destroy(gameObject);
        }
    }


    private void ApplyDamage(GameObject otherGameObject, Health health)
    {
        PhotonView photonView = otherGameObject.GetComponent<PhotonView>();

        if (photonView != null)
        {
            photonView.RPC("TakeDamage", RpcTarget.All, damage);
        }

        CheckAndUpdatePlayerStatus(health);
    }

    private void CheckAndUpdatePlayerStatus(Health health)
    {
        if (damage >= health.health) // Kill
        {
            HandlePlayerKill();
            UpdatePlayerScore();
        }
    }

    private void HandlePlayerKill()
    {
        RoomManager.instance.kills++;
        RoomManager.instance.SetHashes();
    }

    private void UpdatePlayerScore()
    {
        PhotonNetwork.LocalPlayer.AddScore(100);
    }
}
