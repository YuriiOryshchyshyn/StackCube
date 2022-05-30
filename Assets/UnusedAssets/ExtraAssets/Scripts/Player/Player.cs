using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _StackJumpForce;
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _stickman;
    [SerializeField] private GameObject _skeletic;
    [SerializeField] private RoadTrigger _roadTrigger;
    [SerializeField] private CubesContainer _cubesContainer;
    [SerializeField] private InputManager _input;

    public Animator Animator => _animator;
    public RoadTrigger RoadTrigger => _roadTrigger;

    private void OnEnable()
    {
        _cubesContainer.PlayerLoose += Lose;
    }

    private void OnDisable()
    {
        _cubesContainer.PlayerLoose -= Lose;
    }

    private void Lose()
    {
        _input.enabled = false;
        transform.SetParent(null);
        _animator.enabled = false;
        _skeletic.gameObject.SetActive(true);
    }

    public void Jump()
    {
        _animator.SetTrigger("Jump");
    }
}
