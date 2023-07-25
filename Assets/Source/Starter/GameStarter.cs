using System;
using TMPro;
using UnityEngine;

public class GameStarter : MonoBehaviour
{
    [SerializeField] private GameObject _startText;

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
    [SerializeField] private TMP_Text _coinsValueText;

    private IInput _input;
    private bool _isPause;
    private PlayerMove _playerMove;
    private ObstacleSpawner _obstacleSpawner;

    private void Start()
    {
        _input = CreateInput();
        Player player = CreatePlayer(_input);
        _playerMove = player.GetComponent<PlayerMove>();
        _obstacleSpawner = CreateObstacleSpawner(_obstacleSpawnChance, _spawnPoint, _obstaclePrefabs);
        CoinsCollector coinsCollector = player.GetComponent<CoinsCollector>();
        CreateCoinsSpawner(_obstacleSpawner, _coinPrefab, coinsCollector, _coinSpawnChance, _attractTime);
        CreateCoinsDisplayer(coinsCollector, _coinsValueText);
        Pause(_playerMove, _obstacleSpawner);
    }

    private void Pause(params IPauseable[] pauseables)
    {
        for (int i = 0; i < pauseables.Length; i++)
        {
            pauseables[i].Pause();
        }

        _isPause = true;
        _startText.SetActive(true);
    }

    private void Resume(params IPauseable[] pauseables)
    {
        for (int i = 0; i < pauseables.Length; i++)
        {
            pauseables[i].Resume();
        }

        _isPause = false;
        _startText.SetActive(false);
    }

    private void Update()
    {
        if (_isPause == false)
        {
            return;
        }

        if (_input.IsMainButtonPressed == true)
        {
            Resume(_playerMove, _obstacleSpawner);
            _isPause = false;
        }
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
        ParticleSystem trail = player.GetComponentInChildren<ParticleSystem>();
        playerMove.Construct(input, trail);
        Health playerHealth = player.gameObject.AddComponent<Health>();
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

    private CoinsValueDisplayer CreateCoinsDisplayer(CoinsCollector coinsCollector, TMP_Text coinsValueText)
    {
        CoinsValueDisplayer coinsValueDisplayer = gameObject.AddComponent<CoinsValueDisplayer>();
        coinsValueDisplayer.Construct(coinsCollector, coinsValueText);
        return coinsValueDisplayer;
    }
}
