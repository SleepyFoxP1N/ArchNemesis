using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon")]
public class Weapon : ScriptableObject
{
    [Header("Item Details")]
    [SerializeField] private new string name;
    [TextArea(15, 17)]
    [SerializeField] private string description;
    [SerializeField] private int damage;
    [SerializeField] private float force;
    [SerializeField] private float timeBetweenFiring;
    [SerializeField] private float probabilitySpawning;

    [Header("Prefab")]
    [SerializeField] private Sprite weaponSprite;

    public string Name { get => name; private set => name = value; }
    public string Description { get => description; private set => description = value; }
    public int Damage { get => damage; private set => damage = value; }
    public float Force { get => force; private set => force = value; }
    public float TimeBetweenFiring { get => timeBetweenFiring; private set => timeBetweenFiring = value; }
    public float ProbabilitySpawning { get => probabilitySpawning; private set => probabilitySpawning = value; }
    public Sprite WeaponSprite { get => weaponSprite; private set => weaponSprite = value; }
}
