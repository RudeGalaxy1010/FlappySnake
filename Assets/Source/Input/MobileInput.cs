using UnityEngine;

public class MobileInput : MonoBehaviour, IInput
{
    private bool _isMainButtonPressed;

    public bool IsMainButtonPressed => _isMainButtonPressed;

    void IUpdateable.Update()
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            if (Input.touches[i].phase == TouchPhase.Began)
            {
                _isMainButtonPressed = true;
                return;
            }
        }

        _isMainButtonPressed = false;
    }
}
