using UnityEngine;

public class PCInput : MonoBehaviour, IInput
{
    private readonly KeyCode _mainButtonKey = KeyCode.Space;

    private bool _isMainButtonPressed;

    public bool IsMainButtonPressed => _isMainButtonPressed;

    private void Update()
    {
        _isMainButtonPressed = Input.GetKey(_mainButtonKey);
    }
}
