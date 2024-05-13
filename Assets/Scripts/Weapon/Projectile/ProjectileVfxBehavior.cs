using Photon.Pun;
using UnityEngine;

public class ProjectileVfxBehavior : MonoBehaviourPunCallbacks
{
    private float timeLeft;

    [System.Obsolete]
    public void Awake()
    {
        ParticleSystem system = GetComponent<ParticleSystem>();
        timeLeft = system.startLifetime;
    }

    public void Update()
    {
        PhotonView photonView = GetComponent<PhotonView>();

        if (photonView != null && (photonView.IsMine))
        { 
            timeLeft -= Time.deltaTime;
            if (timeLeft <= 0)
            {
                PhotonNetwork.Destroy(gameObject);
            }
        }
    }
}
