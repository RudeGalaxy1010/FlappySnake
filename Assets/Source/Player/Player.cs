using UnityEngine;

public class Player : MonoBehaviour, IUpdateable
{
    private const float UpVelocity = 10f;

    [SerializeField] private Rigidbody2D _rigidbody2D;

    private IInput _input;

    public void Construct(IInput input)
    {
        _input = input;
    }

    void IUpdateable.Update()
    {
        if (_input.IsMainButtonPressed == true)
        {
            _rigidbody2D.velocity = Vector2.up * UpVelocity;
        }
    }
}
