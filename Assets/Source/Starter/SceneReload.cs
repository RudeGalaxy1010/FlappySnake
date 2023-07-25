using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneReload : MonoBehaviour
{
    private Health _playerHealth;

    public void Construct(Health playerHealth)
    {
        _playerHealth = playerHealth;
        _playerHealth.Died += OnPlayerDied;
    }

    private void OnDestroy()
    {
        if (_playerHealth != null)
        {
            _playerHealth.Died -= OnPlayerDied;
        }
    }

    private void OnPlayerDied()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
