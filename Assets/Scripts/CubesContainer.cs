using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CubesContainer : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Transform _stickman;
    [SerializeField] private Transform _EmptyCubesContainer;
    [SerializeField] private GameObject _gameOverCanvas;
    [SerializeField] private ParticleSystem _cubeExplosion;
    [SerializeField] private GameObject _collectCubeText;

    private List<CubeObject> _cubes;
    private CubeObject _currentCube;
    private CubeObject _triggerCube;
    private Vector3 _stackPosition;

    public event UnityAction PlayerLoose;
    public event UnityAction<Vector3> CubeStacked;

    private void Awake()
    {
        _cubes = new List<CubeObject>();
        for (int i = 0; i < transform.childCount; i++)
        {
            CubeObject childCube = transform.GetChild(i).gameObject.GetComponent<CubeObject>();
            _cubes.Add(childCube);
            childCube.SetContainer(_EmptyCubesContainer);
            childCube.Stack(childCube.transform.localPosition, transform);
        }
        _currentCube = _cubes[_cubes.Count - 1];
        SetTriggerCube(_cubes[0]);

        FollowCubeOnEvents(_triggerCube);
    }

    private void SetTriggerCube(CubeObject cube)
    {
        _triggerCube = cube;
    }

    private void OnCubeEnter(CubeObject cube)
    {
        StackCube(cube);
        CubeStacked?.Invoke(_currentCube.transform.position);
        _cubeExplosion.transform.position = _stickman.position;
        _cubeExplosion.Play();

        _stickman.SetParent(_currentCube.transform);
        _stickman.localPosition = new Vector3(_stickman.transform.localPosition.x, _stickman.transform.localPosition.y + _currentCube.transform.localScale.y, _stickman.transform.localPosition.z);

        _player.Jump();
    }

    private void StackCube(CubeObject cube)
    {
        _stackPosition = GetNextStackPosition(cube);
        cube.Stack(_stackPosition, transform);
        cube.SetContainer(_EmptyCubesContainer);
        _cubes.Add(cube);
        _currentCube = cube;
        FollowCubeOnEvents(cube);
    }

    private void FollowCubeOnEvents(CubeObject cube)
    {
        if (cube.StackFollower)
            return;
        cube.CubeEnter += OnCubeEnter;
        cube.CubeExit += OnCubeExit;
        cube.StackFollower = true;
    }

    private Vector3 GetNextStackPosition(CubeObject cube)
    {
        return new Vector3(_currentCube.transform.localPosition.x,
            _currentCube.transform.localPosition.y + cube.transform.localScale.y,
            _currentCube.transform.localPosition.z);
    }

    private void OnCubeExit(CubeObject cube)
    {
        _cubes.Remove(cube);
        if (_currentCube == cube)
        {
            PlayerLoose?.Invoke();
            _gameOverCanvas.SetActive(true);
            return;
        }

        cube.CubeEnter -= OnCubeEnter;
        cube.CubeExit -= OnCubeExit;
        cube.StackFollower = false;
        SetTriggerCube(_cubes[0]);
        _currentCube = _cubes[_cubes.Count - 1];
    }
}
