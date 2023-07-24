using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    private Player _playerPrefab;
    private Transform _spawnPoint;

    public void Construct(Player playerPrefab, Transform spawnPoint)
    {
        _playerPrefab = playerPrefab;
        _spawnPoint = spawnPoint;
    }

    public Player CreatePlayer()
    {
        Player player = Instantiate(_playerPrefab, _spawnPoint.position, Quaternion.identity);
        return player;
    }
}
