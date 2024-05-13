using UnityEngine;
using Photon.Pun;
using System;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private GameObject weapon;
    private Weapon currentWeapon_Obj;
    [SerializeField] private GameObject projectile;
    [SerializeField] private PlayerController playerController;

    [Header("Weapon Status")]
    public int projectileCount;
    public int maxProjectile;
    public int ProjectileBagCount;
    public bool canShoot = false;
    private float timer;
    public float timeBetweenFiring = 0f;


    // ===== ===== ===== ===== ===== EVENTS
    public Action<int> OnShootEvent;
    public Action<int, int> OnReloadEvent;

    public void SetUp(Weapon weaponObj)
    {
        currentWeapon_Obj = weaponObj;
        projectileCount = currentWeapon_Obj.Arrow;
        maxProjectile = currentWeapon_Obj.Arrow;
        ProjectileBagCount = currentWeapon_Obj.ArrowBag;
    }

    public void Update()
    {
        if (currentWeapon_Obj == null) return;

        currentWeapon_Obj = weapon.GetComponent<WeaponBehavior>().CurrentWeapon_Obj;
        timeBetweenFiring = currentWeapon_Obj.TimeBetweenFiring;
        projectile.GetComponent<ProjectileBehavior>().weapon = currentWeapon_Obj;

        if (canShoot) return;

        timer += Time.deltaTime;
        if (timer > timeBetweenFiring)
        {
            canShoot = true;
            timer = 0f;
        }
    }
    
    [PunRPC]
    public void OnShoot()
    {
        if ( canShoot && projectileCount > 0)
        {
            projectileCount--;
            OnShootEvent?.Invoke(projectileCount);
            canShoot = false;
            GameObject spawnProjectile = PhotonNetwork.Instantiate(projectile.name, weapon.transform.position, Quaternion.identity);
            spawnProjectile.GetComponent<ProjectileBehavior>().IsLocalBullet = true;
        }
    }

    [PunRPC]
    public void ReloadWeapon()
    {
        if (ProjectileBagCount > 0)
        {
            ProjectileBagCount--;
            projectileCount = maxProjectile;
            OnReloadEvent?.Invoke(projectileCount, ProjectileBagCount);
        }
    }
}
