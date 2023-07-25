using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int _value;
    [SerializeField] private float _attractRadius;
    [SerializeField] private float _collectRadius;

    private float _attractTime;
    private CoinsCollector _coinsCollector;

    public void Construct(CoinsCollector coinsCollector, float attractTime)
    {
        _coinsCollector = coinsCollector;
        _attractTime = attractTime;
    }

    public int Value => _value;

    private void Update()
    {
        if (_coinsCollector == null)
        {
            return;
        }

        float distanceToCoinsCollector = Vector2.Distance(transform.position, _coinsCollector.transform.position);

        if (distanceToCoinsCollector <= _attractRadius)
        {
            Attract(_coinsCollector.transform);
        }

        if (distanceToCoinsCollector <= _collectRadius)
        {
            _coinsCollector.Collect(this);
            Destroy(gameObject);
        }
    }

    private void Attract(Transform to)
    {
        transform.position = Vector2.MoveTowards(transform.position, to.position, _attractTime * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _attractRadius);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _collectRadius);
    }
}
