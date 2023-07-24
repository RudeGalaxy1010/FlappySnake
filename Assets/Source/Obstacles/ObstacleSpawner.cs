using System.Collections;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public void Construct(float minSpawnTime, float maxSpawnTime, Transform spawnPoint, Obstacle[] obstaclePrefabs)
    {
        StartCoroutine(Spawn(minSpawnTime, maxSpawnTime, spawnPoint, obstaclePrefabs));
    }

    private IEnumerator Spawn(float minTime, float maxTime, Transform spawnPoint, Obstacle[] obstaclePrefabs)
    {
        while (true)
        {
            float waitTime = Random.Range(minTime, maxTime);
            yield return new WaitForSeconds(waitTime);
            CreateObstacle(obstaclePrefabs, spawnPoint);
        }
    }

    private Obstacle CreateObstacle(Obstacle[] obstaclePrefabs, Transform spawnPoint)
    {
        Obstacle obstaclePrefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];
        Obstacle obstacle = Instantiate(obstaclePrefab, spawnPoint.position, Quaternion.identity);
        return obstacle;
    }
}
