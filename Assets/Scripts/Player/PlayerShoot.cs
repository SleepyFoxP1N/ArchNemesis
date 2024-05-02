using UnityEngine;
using Photon.Pun;

public class PlayerShoot : MonoBehaviour
{
    private GameObject weapon;
    private Weapon currentWeapon_Obj;
    private GameObject projectile;
    [SerializeField] private WeaponHandler weaponHandler;

    public bool canShoot = false;
    private float timer;
    public float timeBetweenFiring = 0f;

    public void Update()
    {
        weapon = weaponHandler.CurrentWeapon;
        currentWeapon_Obj = weapon.GetComponent<WeaponBehavior>().currentWeapon_Obj;
        timeBetweenFiring = currentWeapon_Obj.TimeBetweenFiring;
        projectile = weaponHandler.CurrentProjectile;
        projectile.GetComponent<ProjectileBehavior>().weapon = currentWeapon_Obj;
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
    
    [PunRPC]
    public void OnShoot()
    {
        if ( canShoot )
        {
            canShoot = false;
            GameObject spawnProjectile = PhotonNetwork.Instantiate(projectile.name, weapon.transform.position, Quaternion.identity);
            spawnProjectile.GetComponent<ProjectileBehavior>().IsLocalBullet = true;
        }
    }
}
