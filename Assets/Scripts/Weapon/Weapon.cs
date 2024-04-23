using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon")]
public class Weapon : ScriptableObject
{
    [Header("Item Details")]
    [SerializeField] private new string name;
    [SerializeField] private string description;

    [Header("Prefab")]
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private GameObject projectilePrefab;

    public string Name { get => name; private set => name = value; }
    public string Description { get => description; private set => description = value; }
}
