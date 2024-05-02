using UnityEngine;

public class WeaponBehavior : MonoBehaviour
{
    [SerializeField] private Weapon[] weapons;
    public Weapon currentWeapon_Obj;
    public bool IsEquipped; // TODO: Use this to check when spawn it can be grab

    private void Awake()
    {
        RandomizeThisWeapon();
        UpdateSprite();
    }

   public void RandomizeThisWeapon()
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
                currentWeapon_Obj = weapon;
                break;
            }
            else
            {
                randomValue -= weapon.ProbabilitySpawning;
            }
        }

    }

    public void UpdateSprite()
    {
        GetComponent<SpriteRenderer>().sprite = currentWeapon_Obj.WeaponSprite;
    }
}
