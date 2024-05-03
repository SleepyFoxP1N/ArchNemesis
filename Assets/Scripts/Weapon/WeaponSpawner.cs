using Photon.Pun;
using System.Collections;
using UnityEngine;

public class WeaponSpawner : MonoBehaviour
{
    [SerializeField] float spawnTime = 10;
    [SerializeField] private Transform[] spawnPoints;


    public IEnumerator SpawnWeapons()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnTime);

            ShuffleSpawnPoints();

            Transform emptySpawnPoint = GetEmptySpawnPoint();
            if (emptySpawnPoint != null) SetUpSpawning(emptySpawnPoint);
        }
    }

    private void SetUpSpawning(Transform emptySpawnPoint)
    {
        GameObject weapon = PhotonNetwork.Instantiate("Weapon", (emptySpawnPoint.position + new Vector3(0f, 0.5f, -1f)), Quaternion.identity);
        weapon.transform.SetParent(emptySpawnPoint);
        emptySpawnPoint.transform.Find("circleVFX").gameObject.SetActive(true);
        emptySpawnPoint.transform.Find("Collider").gameObject.SetActive(true);
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
            if (spawnPoint.childCount == 3)
            {
                return spawnPoint; 
            }
        }
        return null; 
    }
}
