using System.Collections.Generic;
using UnityEngine;

public class GameStarter : MonoBehaviour
{
    [SerializeField] private Player _playerPrefab;
    [SerializeField] private Transform _playerSpawnPoint;

    private List<IUpdateable> _updateables;

    private void Awake()
    {
        _updateables = new List<IUpdateable>();
    }

    private void Start()
    {
        IInput input = RegisterUpdateable(CreateInput());
        Player player = RegisterUpdateable(CreatePlayer(input));
    }

    private void Update()
    {
        for (int i = 0; i < _updateables.Count; i++)
        {
            _updateables[i].Update();
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
        player.Construct(input);
        return player;
    }

    private T RegisterUpdateable<T>(T obj)
    {
        if (obj is IUpdateable == false)
        {
            return obj;
        }

        IUpdateable updateable = obj as IUpdateable;

        if (_updateables.Contains(updateable) == false)
        {
            _updateables.Add(updateable);
        }

        return obj;
    }
}
