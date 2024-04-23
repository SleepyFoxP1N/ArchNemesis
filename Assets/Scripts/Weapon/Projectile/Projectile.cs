using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Projectile", menuName = "Projectile")]
public class Projectile : ScriptableObject
{
    [Header("Item Details")]
    [SerializeField] private new string name;
    [SerializeField] private string description;
    [SerializeField] private int damage;

    public string Name { get => name; private set => name = value; }
    public string Description { get => description; private set => description = value; }
    public int Damage { get => damage; private set => damage = value; }
}
