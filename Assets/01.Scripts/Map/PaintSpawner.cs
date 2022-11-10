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
    float noSpawnDistance = 10f;

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
        do
        {
            randomPos = Random.insideUnitSphere * distance + center;
            Debug.Log(randomPos);
        } while(Vector3.Distance(randomPos, centerTransform.position) < noSpawnDistance);

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
