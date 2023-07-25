using UnityEngine;
using Random = UnityEngine.Random;

public class CoinSpawner : MonoBehaviour
{
    private const float OffsetX = 2f;
    private const float MinOffsetY = -4.5f;
    private const float MaxOffsetY = 4.5f;

    private ObstacleSpawner _obstacleSpawner;
    private Coin _coinPrefab;
    private CoinsCollector _coinsCollector;
    private float _spawnChance;
    private float _coinAttractTime;

    public void Construct(ObstacleSpawner obstacleSpawner, Coin coinPrefab, CoinsCollector coinsCollector,
        float spawnChance, float coinAttractTime)
    {
        _obstacleSpawner = obstacleSpawner;
        _obstacleSpawner.ObstacleCreated += OnObstacleCreated;
        _coinPrefab = coinPrefab;
        _coinsCollector = coinsCollector;
        _spawnChance = spawnChance;
        _coinAttractTime = coinAttractTime;
    }

    private void OnDestroy()
    {
        if (_obstacleSpawner != null)
        {
            _obstacleSpawner.ObstacleCreated -= OnObstacleCreated;
        }
    }

    private void OnObstacleCreated(Obstacle obstacle)
    {
        if (Random.value <= _spawnChance)
        {
            CreateCoin(obstacle);
        }
    }

    private void CreateCoin(Obstacle obstacle)
    {
        float offsetY = Random.Range(MinOffsetY, MaxOffsetY);
        Vector2 position = (Vector2)obstacle.transform.position + Vector2.right * OffsetX + Vector2.up * offsetY;
        Coin coin = Instantiate(_coinPrefab, position, Quaternion.identity);
        coin.Construct(_coinsCollector, _coinAttractTime);
    }
}
