using UnityEngine;

[RequireComponent(typeof(InputManager))]
public class PlayerMover : MonoBehaviour, IMover
{
    [SerializeField] private float _speedMoving;

    private InputManager _inputManager;

    private void Awake()
    {
        _inputManager = GetComponent<InputManager>();
    }

    private void OnEnable()
    {
        _inputManager.PointerDown += Move;
    }

    private void OnDisable()
    {
        _inputManager.PointerDown -= Move;
    }

    public void Move()
    {
        transform.Translate(Vector3.forward * _speedMoving * Time.deltaTime);
    }
}
