using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    [SerializeField] private Transform _trackPool;
    [SerializeField] private float _roadLenght;
    [SerializeField] private Player _player;
    [SerializeField] private List<CubesPosition> _spawnPositions;
    [SerializeField] private Transform _spawnPointsContainer;
    [SerializeField] private Transform _cubesContainer;
    [SerializeField] private Transform _emptyCubesContainer;
    [SerializeField] private CameraShake _cameraShake;

    private Track[] _tracks;
    private Vector3 _currentTrackPosition;
    private int _firstRoadIndex;
    private int _cubesSpawnedCount;

    private void Awake()
    {
        _tracks = GetChildrens<Track>(_trackPool);
        _currentTrackPosition = _tracks[0].transform.localPosition - new Vector3(0f, 0f, 50f);
        _firstRoadIndex = 0;

        StartBuildRoad();
    }

    private void OnEnable()
    {
        _player.RoadTrigger.RoadInTrigger += SetNewFirstTrackPositin;
        _player.RoadTrigger.RoadInTrigger += CameraShake;
    }

    private void OnDisable()
    {
        _player.RoadTrigger.RoadInTrigger -= SetNewFirstTrackPositin;
        _player.RoadTrigger.RoadInTrigger -= CameraShake;
    }

    [ContextMenu("SetCubesPositions")]
    public void SetCubesPositions()
    {
        CubesPosition cubePositions = new CubesPosition();

        for (int i = 0; i < _spawnPointsContainer.childCount; i++)
        {
            Transform childTransform = _spawnPointsContainer.GetChild(i);
            cubePositions.SpawnPositions.Add(childTransform.localPosition);
        }

        _spawnPositions.Add(cubePositions);
    }

    private void StartBuildRoad()
    {
        List<Track> tracks = new List<Track>(_tracks);
        tracks[0].transform.localPosition = _currentTrackPosition - new Vector3(0, 0, _roadLenght);

        for (int i = 0; i < _tracks.Length; i++)
        {
            Track track = tracks[i];
            SetTrackPotision(track, _currentTrackPosition);
            track.SetRandomCubes();
            _currentTrackPosition = GetNextRoadPosition();
        }
    }

    private void SetNewFirstTrackPositin()
    {
        Track track = _tracks[_firstRoadIndex];
        Vector3 newTrackPosition = new Vector3(_currentTrackPosition.x,
            _currentTrackPosition.y - 50f,
            _currentTrackPosition.z);
        SetTrackPotision(track, newTrackPosition);
        StartCoroutine(MoveTrackToPositionCoroutine(_tracks[_firstRoadIndex]));
        _emptyCubesContainer.GetComponent<EmptyCubesContainer>().DisableCubes();
    }

    private void SetTrackPotision(Track track, Vector3 position)
    {
        track.Init(_spawnPositions, _cubesContainer, _emptyCubesContainer.GetComponent<EmptyCubesContainer>());
        track.SetRandomWall();
        track.SetCubesSpawnedCound(_cubesSpawnedCount);
        _cubesSpawnedCount = track.GetCubesSpawnedCound();
        track.transform.localPosition = position;
    }

    private Vector3 GetNextRoadPosition()
    {
        return new Vector3(_currentTrackPosition.x,
                _currentTrackPosition.y,
                _currentTrackPosition.z + _roadLenght);
    }

    private IEnumerator MoveTrackToPositionCoroutine(Track track)
    {
        while (track.transform.localPosition != _currentTrackPosition)
        {
            track.transform.localPosition = Vector3.MoveTowards(track.transform.localPosition, _currentTrackPosition, 1);
            yield return null;
        }

        _currentTrackPosition = GetNextRoadPosition();
        if (_firstRoadIndex >= _tracks.Length - 1)
            _firstRoadIndex = 0;
        else
            _firstRoadIndex++;

        track.SetRandomCubes();
    }

    private T[] GetChildrens<T>(Transform trackPool)
    {
        T[] childrens = new T[trackPool.childCount];

        for (int i = 0; i < childrens.Length; i++)
        {
            trackPool.GetChild(i).TryGetComponent(out T track);
            childrens[i] = track;
        }

        return childrens;
    }

    private void CameraShake()
    {
        StartCoroutine(_cameraShake.Shake(0.3f, 0.1f));
    }
}
