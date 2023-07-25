using UnityEngine;

public class HorizontalMove : MonoBehaviour
{
    private float _speed = 5f;

    public void Construct(float? speed = null)
    {
        if (speed == null)
        {
            return;
        }

        _speed = speed.Value;
    }

    private void Update()
    {
        transform.position += Vector3.left * _speed * Time.deltaTime;
    }
}
