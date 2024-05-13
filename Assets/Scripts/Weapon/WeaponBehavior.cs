using UnityEngine;
using Photon.Pun;
using System;

public class WeaponBehavior : MonoBehaviourPunCallbacks, IPunObservable
{
    public Weapon[] weapons;
    public Weapon CurrentWeapon_Obj;
    public PlayerShoot PlayerShoot;
    public WeaponUI WeaponUI;

    [Header("Others")]
    [SerializeField] private AudioSource weaponAUD;

    private void Start()
    {
        if (GetComponent<PhotonView>().IsMine)
        {
            RandomizeThisWeapon();
        }
    }

    public override void OnEnable()
    {
        base.OnEnable();
        if (GetComponent<PhotonView>().IsMine)
        {
            RandomizeThisWeapon();
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(Array.IndexOf(weapons, CurrentWeapon_Obj));
        }
        else
        {
            int weaponIndex = (int)stream.ReceiveNext();
            CurrentWeapon_Obj = weapons[weaponIndex];
            UpdateSprite();
        }
    }

    private void RandomizeThisWeapon()
    {
        float totalProbability = 0f;
        foreach (Weapon weapon in weapons)
        {
            totalProbability += weapon.ProbabilitySpawning;
        }

        float randomValue = UnityEngine.Random.Range(0f, totalProbability);
        foreach (Weapon weapon in weapons)
        {
            if (randomValue < weapon.ProbabilitySpawning)
            {
                CurrentWeapon_Obj = weapon;
                break;
            }
            else
            {
                randomValue -= weapon.ProbabilitySpawning;
            }
        }

        UpdateSprite();

        GetComponent<PhotonView>().RPC("UpdateWeaponRPC", RpcTarget.AllBuffered, Array.IndexOf(weapons, CurrentWeapon_Obj));
    }

    [PunRPC]
    private void UpdateWeaponRPC(int weaponIndex)
    {
        CurrentWeapon_Obj = weapons[weaponIndex];
        UpdateSprite();
        SetUp();
    }

    private void UpdateSprite()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.sprite = CurrentWeapon_Obj.WeaponSprite;
        sr.color = CurrentWeapon_Obj.SpriteColor;
        gameObject.transform.localScale = CurrentWeapon_Obj.SpriteScale;
    }
    private void SetUp()
    {
        if (WeaponUI != null)
        {
            WeaponUI.InitializeScript(CurrentWeapon_Obj);
        }
        if (PlayerShoot != null)
        {
            PlayerShoot.SetUp(CurrentWeapon_Obj);
        }
    }

    public void playSound(AudioClip _Clip)
    {
        weaponAUD.PlayOneShot(_Clip);
    }
}
