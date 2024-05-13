using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUI : MonoBehaviour
{
    public Weapon weapon;
    [SerializeField] private PlayerController playerControler;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI bagTXT;
    [SerializeField] private TextMeshProUGUI projectileTXT;
    [SerializeField] private Image projectileCircleIMG;

    private int projectile, maxProjectile, arrowBag;

    public void InitializeScript(Weapon _weapon)
    {
        weapon = _weapon;
        projectile = weapon.Arrow;
        maxProjectile = weapon.Arrow;
        arrowBag = weapon.ArrowBag;
        setProjectile();
        playerControler.onWeaponShoot += OnFire;
        playerControler.onWeaponReload += OnReload;
    }

    private void setProjectile()
    {
        projectileCircleIMG.fillAmount = (float) projectile / maxProjectile;
        bagTXT.text = arrowBag.ToString();
        projectileTXT.text = projectile + "/" + maxProjectile;
    }

    public void OnFire(int projectile)
    {
        if (weapon == null) return;
        projectileCircleIMG.fillAmount = (float) projectile / maxProjectile;
        projectileTXT.text = projectile + "/" + maxProjectile;
    }

    public void OnReload(int projectile, int arrowBag)
    {
        bagTXT.text = arrowBag.ToString();
        projectileTXT.text = projectile + "/" + maxProjectile;
    }
}
