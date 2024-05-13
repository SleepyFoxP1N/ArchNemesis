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
    [SerializeField] Transform healthBarUI;
    private float originalHealthBarUISize;
    [SerializeField] TextMeshProUGUI healthText;



    private void Start()
    {
        originalHealthBarSize = healthBar.localScale.x;
        originalHealthBarUISize = healthBar.localScale.x;
    }


    [PunRPC]
    public void TakeDamage(int _damage)
    {
        health -= _damage;

        healthBar.localScale = new Vector3(originalHealthBarSize * health / 100f, healthBar.localScale.y, healthBar.localScale.z);

        healthBarUI.localScale = new Vector3(originalHealthBarUISize * health / 100f, healthBarUI.localScale.y, healthBarUI.localScale.z);
        healthText.text = "HP:"+health.ToString();

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
