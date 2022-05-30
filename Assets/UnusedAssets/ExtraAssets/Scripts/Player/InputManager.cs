using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    public event UnityAction PointerDown;
    public event UnityAction PointerUp;

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            PointerDown?.Invoke();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            PointerUp?.Invoke();
        }
    }
}
