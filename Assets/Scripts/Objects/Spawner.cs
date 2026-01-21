using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private TimeManager _timeManager;
    [SerializeField] private ObjectMovement[] _fallingLines;
    [SerializeField] private GameObject _objectToSpawn;
    [SerializeField] private int _spawnTimer = 0;
    [SerializeField] private int _spawnInterval = 3;

    private void OnEnable()
    {
        _timeManager.OnTimePassed += TimeGestion;
    }

    private void OnDisable()
    {
        _timeManager.OnTimePassed -= TimeGestion;
    }

    private void TimeGestion()
    {
        _spawnTimer++;
        if (_spawnTimer >= _spawnInterval)
        {
            _spawnTimer = 0;
            _fallingLines[0].Init(_objectToSpawn);
        }
    }
}

