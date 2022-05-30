using UnityEngine;

[RequireComponent(typeof(SwerveInputManager))]
public class SwerveMovement : MonoBehaviour
{
    [SerializeField] private float _swerveSpeed;
    [SerializeField] private float _maxPositionX;

    private SwerveInputManager _swerveInput;
    private float _swerveAmount;

    private void Awake()
    {
        _swerveInput = GetComponent<SwerveInputManager>();
    }

    private void Update()
    {
        _swerveAmount = _swerveSpeed * _swerveInput.MoveFactor * Time.deltaTime;
        transform.Translate(_swerveAmount, 0f, 0f);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -_maxPositionX, _maxPositionX),
            transform.position.y,
            transform.position.z);
    }
}
