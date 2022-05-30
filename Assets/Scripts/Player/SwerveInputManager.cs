using UnityEngine;

public class SwerveInputManager : MonoBehaviour
{
    private float _lastFingerPositionX;
    private float _moveFactor;

    public float MoveFactor => _moveFactor;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _lastFingerPositionX = Input.mousePosition.x;
        }
        else if (Input.GetMouseButton(0))
        {
            _moveFactor = Input.mousePosition.x - _lastFingerPositionX;
            _lastFingerPositionX = Input.mousePosition.x;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _moveFactor = 0f;
        }
    }
}
