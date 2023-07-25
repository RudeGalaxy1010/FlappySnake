using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObstacleSpawner : MonoBehaviour
{
    private const float Second = 1f;

    public event Action<Obstacle> ObstacleCreated;

    public void Construct(float spawnChance, Transform spawnPoint, Obstacle[] obstaclePrefabs)
    {
        StartCoroutine(Spawn(spawnChance, spawnPoint, obstaclePrefabs));
    }

    private IEnumerator Spawn(float spawnChance, Transform spawnPoint, Obstacle[] obstaclePrefabs)
    {
        var waitForSeconds = new WaitForSeconds(Second);

        while (true)
        {
            yield return waitForSeconds;

            if (Random.value <= spawnChance)
            {
                CreateObstacle(obstaclePrefabs, spawnPoint);
            }
        }
    }

    private Obstacle CreateObstacle(Obstacle[] obstaclePrefabs, Transform spawnPoint)
    {
        Obstacle obstaclePrefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];
        Obstacle obstacle = Instantiate(obstaclePrefab, spawnPoint.position, Quaternion.identity);
        ObstacleCreated?.Invoke(obstacle);
        return obstacle;
    }
}
