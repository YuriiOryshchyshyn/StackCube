using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Track : MonoBehaviour
{
    [SerializeField] private List<Transform> _walls;
    [SerializeField] private Transform _cubesPool;
    [SerializeField] private List<CubesPosition> _spawnPositions;

    private EmptyCubesContainer _emptyCubesContainer;
    private List<CubeObject> _cubes;
    private Transform _cubesContainer;
    private int _cubesSpawnedCount;

    private void Awake()
    {
        _cubes = GetChildrens<CubeObject>(_cubesPool);
    }

    private List<T> GetChildrens<T>(Transform parent)
    {
        List<T> childrens = new List<T>();
        for (int i = 0; i < parent.childCount; i++)
        {
            childrens.Add(_cubesPool.GetChild(i).gameObject.GetComponent<T>());
        }
        return childrens;
    }

    public void Init(List<CubesPosition> spawnPositions, Transform cubeContainer, EmptyCubesContainer emptyCubesContainer)
    {
        _spawnPositions = spawnPositions;
        _cubesContainer = cubeContainer;
        _emptyCubesContainer = emptyCubesContainer;
    }

    public void SetRandomWall()
    {
        DisableAllWalls();
        _walls[Random.Range(0, _walls.Count)].gameObject.SetActive(true);
    }

    public void SetRandomCubes()
    {
        if (_cubesPool.childCount == 0)
        {
            _emptyCubesContainer.GetCubes(_cubesPool);
        }
        _cubes = GetChildrens<CubeObject>(_cubesPool);

        CubesPosition randomPosition = _spawnPositions[Random.Range(0, _spawnPositions.Count)];
        CubeObject cube;
        int currentSpawnPosition = 0;
        for (int i = 0; i < randomPosition.SpawnPositions.Count; i++)
        {
            cube = _cubes[i];
            cube.transform.SetParent(_cubesPool);
            cube.IsInCubesWaitPool = false;
            cube.gameObject.SetActive(true);
            cube.transform.localPosition = randomPosition.SpawnPositions[currentSpawnPosition];
            cube.transform.SetParent(_cubesContainer);
            _cubesSpawnedCount++;
            currentSpawnPosition++;
        }
    }

    private void DisableAllWalls()
    {
        foreach (var wall in _walls)
        {
            wall.gameObject.SetActive(false);
        }
    }

    public int GetCubesSpawnedCound()
    {
        return _cubesSpawnedCount;
    }

    public void SetCubesSpawnedCound(int count)
    {
        _cubesSpawnedCount = count;
    }
}
