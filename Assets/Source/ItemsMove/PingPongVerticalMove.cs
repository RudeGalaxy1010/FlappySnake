using System.Collections;
using UnityEngine;

public class PingPongVerticalMove : MonoBehaviour
{
    private const float MinOffsetY = -3.5f;
    private const float MaxOffsetY = 3.5f;
    private const float MinDistance = 0.1f;
    private const float ForwardDirectionChance = 0.49f;

    [SerializeField] private float _minDelay;
    [SerializeField] private float _maxDelay;
    [SerializeField] private float _speed;

    private bool _isForwardDirection;
    private bool _changeDirectionCorutineStarted;
    private float _targetY;
    private float _centerY;

    private void Start()
    {
        _targetY = transform.position.y;
        _centerY = (MaxOffsetY - MinOffsetY) / 2f;
        _isForwardDirection = Random.value <= ForwardDirectionChance;
        StartCoroutine(ChangeTargetY());
        _changeDirectionCorutineStarted = true;
    }

    private void Update()
    {
        if (Mathf.Abs(transform.position.y - _targetY) < MinDistance)
        {
            if (_changeDirectionCorutineStarted == true)
            {
                return;
            }

            StartCoroutine(ChangeTargetY());
            _changeDirectionCorutineStarted = true;
            return;
        }

        Vector2 targetPosition = new Vector2(transform.position.x, _targetY);
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, _speed * Time.deltaTime);
    }

    private IEnumerator ChangeTargetY()
    {
        float delay = Random.Range(_minDelay, _maxDelay);
        yield return new WaitForSeconds(delay);
        _targetY = _isForwardDirection ? Random.Range(MinOffsetY, _centerY) : Random.Range(_centerY, MaxOffsetY);
        _isForwardDirection = !_isForwardDirection;
        _changeDirectionCorutineStarted = false;
    }
}
