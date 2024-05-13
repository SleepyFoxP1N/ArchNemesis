using Photon.Pun;
using System.Collections;
using UnityEngine;

public class WeaponSpawner : MonoBehaviourPunCallbacks
{
    [SerializeField] private float spawnTime = 10;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject weaponPrefab;
    [SerializeField] private GameObject weaponSpawned;
    [SerializeField] private Transform emptySpawnPoint;

    public IEnumerator SpawnWeapons()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnTime);

            ShuffleSpawnPoints();

            emptySpawnPoint = GetEmptySpawnPoint();
            if (emptySpawnPoint != null)
            {
                weaponSpawned = PhotonNetwork.Instantiate(weaponPrefab.name, emptySpawnPoint.position + new Vector3(0f, 0.5f, -1f), Quaternion.identity);
                GetComponent<PhotonView>().RPC("SetUpWeaponSpawn", RpcTarget.AllBuffered, weaponSpawned.GetPhotonView().ViewID, emptySpawnPoint.transform.Find("WeaponCollider").gameObject.GetPhotonView().ViewID);
            }
        }
    }

    [PunRPC]
    private void SetUpWeaponSpawn(int weaponViewID, int weaponSpawnCollisionID)
    {
        GameObject weaponObject = PhotonView.Find(weaponViewID).gameObject;
        GameObject WeaponCollider = PhotonView.Find(weaponSpawnCollisionID).gameObject;
        Transform emptySpawnPoint = WeaponCollider.transform.parent.transform;

        weaponObject.transform.SetParent(emptySpawnPoint);
        WeaponCollider.GetComponent<WeaponSpawnCollider>().WeaponSpawned = weaponObject;
        emptySpawnPoint.Find("circleVFX").gameObject.SetActive(true);
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
            if (spawnPoint.childCount <= 3)
            {
                return spawnPoint;
            }
        }
        return null;
    }
}
