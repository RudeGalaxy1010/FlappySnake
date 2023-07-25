using System;
using UnityEngine;

[RequireComponent(typeof(Player), typeof(Rigidbody2D), typeof(Collider2D))]
public class PlayerMove : MonoBehaviour, IPauseable
{
    private const float VerticalVelocity = 6f;

    private IInput _input;
    private ParticleSystem _trail;
    private Rigidbody2D _rigidbody2D;
    private float _transitionSpeed = 25f;
    private bool _isPause;
    private float _gravityScale;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _gravityScale = _rigidbody2D.gravityScale;
    }

    public void Construct(IInput input, ParticleSystem trail, float? transitionSpeed = null)
    {
        _input = input;
        _trail = trail;

        if (transitionSpeed == null)
        {
            return;
        }

        _transitionSpeed = transitionSpeed.Value;
    }

    private void Update()
    {
        if (_input == null || _isPause == true)
        {
            return;
        }

        if (_input.IsMainButtonPressed == true)
        {
            ChangeVelocity(Vector2.up * VerticalVelocity);
        }
    }

    private void ChangeVelocity(Vector2 velocity)
    {
        _rigidbody2D.velocity = Vector2.MoveTowards(
            _rigidbody2D.velocity, velocity, _transitionSpeed * Time.deltaTime);
    }

    public void Pause()
    {
        _isPause = true;
        _rigidbody2D.gravityScale = 0f;
        _trail.Stop();
    }

    public void Resume()
    {
        _isPause = false;
        _rigidbody2D.gravityScale = _gravityScale;
        _trail.Play();
    }
}
