using UnityEngine;
using Photon.Pun;

public class PlayerShoot : MonoBehaviour
{
    private Transform projectile_Transform;
    public bool canShoot = false;
    private float timer;
    public float timeBetweenFiring = 0f;

    public void Update()
    {
        if(!canShoot)
        {
            timer += Time.deltaTime;
            if (timer > timeBetweenFiring)
            {
                canShoot = true;
                timer = 0f;
            }
        }
    }

    public void OnShoot(Camera main_Cam, GameObject weapon, GameObject projectile)
    {
        projectile_Transform = weapon.transform;
        if ( canShoot )
        {
            canShoot = false;
            GameObject spawnProjectile = PhotonNetwork.Instantiate(projectile.name, projectile_Transform.position, Quaternion.identity );
            spawnProjectile.GetComponent<ProjectileBehavior>().main_Cam = main_Cam;
        }
    }
}
