using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    private PlayerInputActions playerControls;

    private Transform projectile_Transform;
    private bool canShoot = false;
    private float timer;
    private float timeBetweenFiring = 0.5f;

    private void Update()
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
            GameObject spawnProjectile = Instantiate(projectile, projectile_Transform.position, Quaternion.identity );
            spawnProjectile.GetComponent<ProjectileBehavior>().main_Cam = main_Cam;
        }
    }
}
