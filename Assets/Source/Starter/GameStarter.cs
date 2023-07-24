using UnityEngine;

public class GameStarter : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private Player _playerPrefab;
    [SerializeField] private Transform _playerSpawnPoint;

    [Header("Obstacles")]
    [SerializeField] private Obstacle[] _obstaclePrefabs;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private float _minSpawnTime;
    [SerializeField] private float _maxSpawnTime;

    private void Start()
    {
        IInput input = CreateInput();
        CreatePlayer(input);
        CreateObstacleSpawner(_minSpawnTime, _maxSpawnTime, _spawnPoint, _obstaclePrefabs);
    }

    private IInput CreateInput()
    {
        if (Application.isMobilePlatform)
        {
            return gameObject.AddComponent<MobileInput>();
        }
        else
        {
            return gameObject.AddComponent<PCInput>();
        }
    }

    private Player CreatePlayer(IInput input)
    {
        PlayerSpawner playerSpawner = gameObject.AddComponent<PlayerSpawner>();
        playerSpawner.Construct(_playerPrefab, _playerSpawnPoint);
        Player player = playerSpawner.CreatePlayer();
        PlayerMove playerMove = player.gameObject.AddComponent<PlayerMove>();
        Health playerHealth = player.gameObject.AddComponent<Health>();
        playerMove.Construct(input);
        return player;
    }

    private ObstacleSpawner CreateObstacleSpawner(float minSpawnTime, float maxSpawnTime, 
        Transform spawnPoint, Obstacle[] obstaclePrefabs)
    {
        ObstacleSpawner obstacleSpawner = gameObject.AddComponent<ObstacleSpawner>();
        obstacleSpawner.Construct(minSpawnTime, maxSpawnTime, spawnPoint, obstaclePrefabs);
        return obstacleSpawner;
    }
}
