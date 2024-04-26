using UnityEngine;
using Photon.Pun;
using TMPro;

public class Health : MonoBehaviour
{
    public int health;
    public bool isLocalPlayer;


    [SerializeField] RectTransform healthBar;
    private float originalHealthBarSize;


    [Header("UI")]
    [SerializeField] TextMeshProUGUI healthText;



    private void Start()
    {
        //originalHealthBarSize = healthBar.sizeDelta.x;
    }


    [PunRPC]
    public void TakeDamage(int _damage)
    {
        Debug.Log("GO_Name: " + gameObject.name + "LP: " + PhotonNetwork.LocalPlayer);
        health -= _damage;
        //healthBar.sizeDelta = new Vector2(originalHealthBarSize * health / 100f, healthBar.sizeDelta.y);
        //healthText.text = health.ToString();
        
        if(health <= 0)
        {
            if (isLocalPlayer)
            {
                RoomManager.instance.SpawnPlayer(); 
                
                RoomManager.instance.deaths++;
                RoomManager.instance.SetHashes();
            }
                
            Destroy(gameObject);    
        }
    }
}
