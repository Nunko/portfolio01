using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PaintSpawner : MonoBehaviour
{
    public GameObject paintPrefab;
    public Transform centerTransform;

    int maxCount = 10;
    float maxDistance = 20f;
    float noSpawnDistance = 5f;

    void OnEnable() 
    {
        SpawnToMax();
    }

    void SpawnToMax()
    {
        for (int i = 0; i < maxCount; i++)
        {
            Spawn();
        }
    }

    void Spawn()
    {
        Vector3 spawnPosition = GetRandomPointOnNavMesh(centerTransform.position, maxDistance);
        spawnPosition += Vector3.up * 0.5f;

        int randNumber = Random.Range(0, 360);
        GameObject item = Instantiate(paintPrefab, spawnPosition, Quaternion.Euler(0, randNumber, 90), this.transform);        
    }

    Vector3 GetRandomPointOnNavMesh(Vector3 center, float distance)
    {
        Vector3 randomPos;
        Vector3 randomPosXZ, centerPosXZ;
        do
        {
            randomPos = Random.insideUnitSphere * distance + center;
            Debug.Log(randomPos);

            randomPosXZ = new Vector3(randomPos.x, 0, randomPos.z);
            centerPosXZ = new Vector3(centerTransform.position.x, 0, centerTransform.position.z);
        } while(Vector3.Distance(randomPosXZ, centerPosXZ) < noSpawnDistance);

        NavMeshHit hit;
        NavMesh.SamplePosition(randomPos, out hit, distance, NavMesh.AllAreas);

        return hit.position;
    }

    public void Replace(GameObject item)
    {
        Vector3 spawnPosition = GetRandomPointOnNavMesh(centerTransform.position, maxDistance);
        spawnPosition += Vector3.up * 0.5f;

        item.transform.position = spawnPosition;
        int randNumber = Random.Range(0, 360);        
        item.transform.rotation = Quaternion.Euler(0, randNumber, 90);  
    }
}
