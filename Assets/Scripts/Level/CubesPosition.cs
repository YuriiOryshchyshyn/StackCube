using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class CubesPosition
{
    private List<Transform> _spawnPositions = new List<Transform>();
    [SerializeField]
    private List<Vector3> _positions;

    public List<Vector3> SpawnPositions { get => _positions; set => _positions = value; }

    public CubesPosition()
    {
        _positions = _spawnPositions.Select(item => item.localPosition).ToList();
    }
}