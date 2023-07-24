using UnityEngine;

public class Player : MonoBehaviour, IUpdateable
{
    private const float UpVelocity = 6f;

    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private float _transitionSpeed = 2f;

    private IInput _input;

    public void Construct(IInput input)
    {
        _input = input;
    }

    void IUpdateable.Update()
    {
        if (_input.IsMainButtonPressed == true)
        {
            ChangeVelocity();
        }
    }

    private void ChangeVelocity()
    {
        _rigidbody2D.velocity = Vector2.MoveTowards(
            _rigidbody2D.velocity, Vector2.up * UpVelocity, _transitionSpeed * Time.deltaTime);
    }
}
