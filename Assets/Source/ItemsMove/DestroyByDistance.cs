using UnityEngine;

public class DestroyByDistance : MonoBehaviour
{
    [SerializeField] private float MaxDistance = 15f;

    private Vector2 _startPosition;

    private void Start()
    {
        _startPosition = (Vector2)transform.position;
    }

    private void Update()
    {
        if (Vector2.Distance(_startPosition, transform.position) > MaxDistance)
        {
            Destroy(gameObject);
        }
    }
}
