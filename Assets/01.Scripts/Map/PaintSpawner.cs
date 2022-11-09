using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PaintSpawner : MonoBehaviour
{
    public GameObject paintPrefab;
    public Transform centerTransform;

    int maxCount = 10;
    public float maxDistance = 5f;

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
        Vector3 randomPos = Random.insideUnitSphere * distance + center;

        NavMeshHit hit;
        NavMesh.SamplePosition(randomPos, out hit, distance, NavMesh.AllAreas);

        return hit.position;
    }
}
