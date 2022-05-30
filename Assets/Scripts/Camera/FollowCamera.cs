using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _lerpSpeed;
    private Vector3 _position;

    private void Start()
    {
        _position = _target.InverseTransformPoint(transform.position);
    }

    private void Update()
    {
        Vector3 currentPosition = _target.TransformPoint(_position);
        transform.position = Vector3.Lerp(transform.position,
            new Vector3(transform.position.x, transform.position.y, currentPosition.z),
            _lerpSpeed * Time.deltaTime);
    }
}
