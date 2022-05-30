using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class WarpEffest : MonoBehaviour
{
    [SerializeField] private InputManager _inputManager;

    private ParticleSystem _wartEffect;

    private void Awake()
    {
        _wartEffect = GetComponent<ParticleSystem>();
    }

    private void OnEnable()
    {
        _inputManager.PointerDown += StartWarpEffect;
        _inputManager.PointerUp += StopWarpEffect;
    }

    private void OnDisable()
    {
        _inputManager.PointerDown -= StartWarpEffect;
        _inputManager.PointerUp -= StopWarpEffect;
    }

    private void StartWarpEffect()
    {
        _wartEffect.Play();
    }

    private void StopWarpEffect()
    {
        _wartEffect.Stop();
    }
}
