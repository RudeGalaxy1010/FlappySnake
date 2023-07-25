using UnityEngine;

public class GameStarter : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private Player _playerPrefab;
    [SerializeField] private Transform _playerSpawnPoint;

    [Header("Obstacles")]
    [SerializeField] private Obstacle[] _obstaclePrefabs;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] [Range(0, 1)] private float _obstacleSpawnChance;

    [Header("Coins")]
    [SerializeField] private Coin _coinPrefab;
    [SerializeField] private float _attractTime;
    [SerializeField] [Range(0, 1)] private float _coinSpawnChance;

    private void Start()
    {
        IInput input = CreateInput();
        Player player = CreatePlayer(input);
        ObstacleSpawner obstacleSpawner = CreateObstacleSpawner(_obstacleSpawnChance, _spawnPoint, _obstaclePrefabs);
        CoinsCollector coinsCollector = player.GetComponent<CoinsCollector>();
        CreateCoinsSpawner(obstacleSpawner, _coinPrefab, coinsCollector, _coinSpawnChance, _attractTime);
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

    private ObstacleSpawner CreateObstacleSpawner(float obstacleSpawnChance, Transform spawnPoint, 
        Obstacle[] obstaclePrefabs)
    {
        ObstacleSpawner obstacleSpawner = gameObject.AddComponent<ObstacleSpawner>();
        obstacleSpawner.Construct(obstacleSpawnChance, spawnPoint, obstaclePrefabs);
        return obstacleSpawner;
    }

    private CoinSpawner CreateCoinsSpawner(ObstacleSpawner obstacleSpawner, Coin coinPrefab,
        CoinsCollector coinsCollector, float spawnChance, float attractTime)
    {
        CoinSpawner coinSpawner = gameObject.AddComponent<CoinSpawner>();
        coinSpawner.Construct(obstacleSpawner, coinPrefab, coinsCollector, spawnChance, attractTime);
        return coinSpawner;
    }
}
