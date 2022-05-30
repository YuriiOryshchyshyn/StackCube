using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public class CubeObject : MonoBehaviour
{
    public event UnityAction<CubeObject> CubeEnter;
    public event UnityAction<CubeObject> CubeExit;

    private bool _isInContainer;
    private bool _stackFollower;
    private Rigidbody _rigidbody;
    private Transform _cubesContainer;

    public bool IsInContainer { get => _isInContainer; set => _isInContainer = value; }
    public bool StackFollower { get => _stackFollower; set => _stackFollower = value; }
    public Rigidbody Rigidbody => _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!IsInContainer)
            return;

        if (Physics.BoxCast(transform.position, new Vector3(0.1f, 0.1f, 0.1f), transform.forward, out RaycastHit hitInfo, transform.rotation))
        {
            if (hitInfo.collider.TryGetComponent(out CubeWall wall) && hitInfo.distance < 0.5f)
            {
                ExitFromStack();
                CubeExit?.Invoke(this);
            }
        }
    }

    public void SetContainer(Transform container)
    {
        _cubesContainer = container;
    }

    private void ExitFromStack()
    {
        if (_cubesContainer == null)
            return;

        transform.SetParent(_cubesContainer);
    }

    public void Stack(Vector3 _stackPosition, Transform parent)
    {
        _isInContainer = true;
        transform.SetParent(parent);
        transform.localPosition = _stackPosition;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out CubeObject cube))
        {
            if (cube.IsInContainer)
                return;

            CubeEnter?.Invoke(cube);
        }
    }
}
