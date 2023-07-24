using UnityEngine;

[RequireComponent(typeof(Player), typeof(Rigidbody2D), typeof(Collider2D))]
public class PlayerMove : MonoBehaviour
{
    private const float VerticalVelocity = 6f;

    private IInput _input;
    private Rigidbody2D _rigidbody2D;
    private float _transitionSpeed = 25f;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void Construct(IInput input, float? transitionSpeed = null)
    {
        _input = input;

        if (transitionSpeed == null)
        {
            return;
        }

        _transitionSpeed = transitionSpeed.Value;
    }

    private void Update()
    {
        if (_input == null)
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
}
