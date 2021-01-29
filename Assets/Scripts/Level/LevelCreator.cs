using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class LevelCreator : MonoBehaviour
{
    [Header("PathCreator")]
    [SerializeField] private PathCreator _pathCreator;
    [Header("Tower")]
    [SerializeField] private Tower _towerTemplate;
    [SerializeField] private int _humanTowerCount;
    [Header("Trampoline")]
    [SerializeField] private Trampoline _trampolineTemplate;
    [SerializeField] private Transform[] _trampolineSpawnPositions;
    [Header("Wall")]
    [SerializeField] private Wall _wallTemplate;    
    [SerializeField] private Transform[] _wallSpawnPositions;

    private void Start()
    {
        GenerateLevel();
    }

    private void GenerateLevel()
    {
        float roadLenth = _pathCreator.path.length;

        float distanceBetweenTowers = roadLenth / _humanTowerCount;

        float distanceTravelled = 0;
        Vector3 spawnPoint;

        for (int i = 0; i < _humanTowerCount; i++)
        {
            distanceTravelled += distanceBetweenTowers;
            spawnPoint = _pathCreator.path.GetPointAtDistance(distanceTravelled, EndOfPathInstruction.Stop);

            Instantiate(_towerTemplate, spawnPoint, Quaternion.identity);
        }

        for (int i = 0; i < _trampolineSpawnPositions.Length; i++)
        {
            Instantiate(_trampolineTemplate, _trampolineSpawnPositions[i]);
        }

        for (int i = 0; i < _wallSpawnPositions.Length; i++)
        {
            Instantiate(_wallTemplate, _wallSpawnPositions[i]);
        }
    }
}
