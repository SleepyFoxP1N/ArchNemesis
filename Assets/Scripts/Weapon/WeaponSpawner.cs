using Photon.Pun;
using System.Collections;
using UnityEngine;

public class WeaponSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;

    private void Start()
    {
        StartCoroutine(SpawnWeapons());
    }

    private IEnumerator SpawnWeapons()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);

            ShuffleSpawnPoints();

            Transform emptySpawnPoint = GetEmptySpawnPoint();
            if (emptySpawnPoint != null)
            {
                GameObject weapon = PhotonNetwork.Instantiate("Weapon", emptySpawnPoint.position, Quaternion.identity);
                weapon.transform.SetParent(emptySpawnPoint);
                Debug.Log("Weapon Spawned at " + emptySpawnPoint.gameObject.name);
            }
        }
    }

    private void ShuffleSpawnPoints()
    {
        // Fisher-Yates shuffle algorithm
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            int randomIndex = Random.Range(i, spawnPoints.Length);
            Transform temp = spawnPoints[randomIndex];
            spawnPoints[randomIndex] = spawnPoints[i];
            spawnPoints[i] = temp;
        }
    }

    private Transform GetEmptySpawnPoint()
    {
        foreach (Transform spawnPoint in spawnPoints)
        {
            if (spawnPoint.childCount == 1)
            {
                return spawnPoint; 
            }
        }
        return null; 
    }
}
