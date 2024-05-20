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
    [SerializeField] RectTransform healthBarUI; 
    private float originalHealthBarUIWidth;
    [SerializeField] TextMeshProUGUI healthText;

    private void Start()
    {
        originalHealthBarSize = healthBar.localScale.x;
        originalHealthBarUIWidth = healthBarUI.sizeDelta.x;
    }

    [PunRPC]
    public void TakeDamage(int _damage)
    {
        health -= _damage;

        healthBar.localScale = new Vector3(originalHealthBarSize * health / 100f, healthBar.localScale.y, healthBar.localScale.z);
        healthBarUI.sizeDelta = new Vector2(originalHealthBarUIWidth * health / 100f, healthBarUI.sizeDelta.y);

        healthText.text = "HP:" + health.ToString();

        if (health <= 0)
        {
            if (isLocalPlayer)
            {
                RoomManager.instance.SpawnPlayer();
                RoomManager.instance.deaths++;
                RoomManager.instance.SetHashes();
            }
            PhotonNetwork.Instantiate("Dead", gameObject.transform.position, Quaternion.identity);
            PhotonView.Destroy(gameObject);
        }
    }
}
