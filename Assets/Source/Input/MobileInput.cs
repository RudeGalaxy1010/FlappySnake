using UnityEngine;

public class MobileInput : MonoBehaviour, IInput
{
    private bool _isMainButtonPressed;

    public bool IsMainButtonPressed => _isMainButtonPressed;

    private void Update()
    {
        _isMainButtonPressed = Input.touchCount > 0;
    }
}
