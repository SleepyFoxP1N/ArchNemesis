using UnityEngine;
using Photon.Pun;
using TMPro;

public class Health : MonoBehaviour
{
    public int health;
    public bool isLocalPlayer;


    [SerializeField] Transform healthBar;
    private float originalHealthBarSize;


    [Header("UI")]
    [SerializeField] TextMeshProUGUI healthText;



    private void Start()
    {
        originalHealthBarSize = healthBar.localScale.x;
    }


    [PunRPC]
    public void TakeDamage(int _damage)
    {
        health -= _damage;

        healthBar.localScale = new Vector3(originalHealthBarSize * health / 100f, healthBar.localScale.y, healthBar.localScale.z);
        //healthText.text = health.ToString();

        if (health <= 0)
        {
            if (isLocalPlayer)
            {
                RoomManager.instance.SpawnPlayer(); 
                
                RoomManager.instance.deaths++;
                RoomManager.instance.SetHashes();
            }

            PhotonView.Destroy(gameObject);
        }
    }
}
