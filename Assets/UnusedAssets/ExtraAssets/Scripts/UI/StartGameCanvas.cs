using UnityEngine;

public class StartGameCanvas : MonoBehaviour
{
    [SerializeField] private InputManager _inputManager;


    private void OnEnable()
    {
        _inputManager.PointerDown += DisableStartCanvas;
    }

    private void OnDisable()
    {
        _inputManager.PointerDown -= DisableStartCanvas;
    }


    private void DisableStartCanvas()
    {
        gameObject.SetActive(false);
    }
}
