using System.Collections.Generic;
using UnityEngine;

public class RoadGenerate : MonoBehaviour
{
    [SerializeField] private Transform _player;
    private List<GameObject> _activeRoads = new List<GameObject>();
    public GameObject[] _roadPrefabs;
    private float _spawnPos = 0f;
    private float _roadLength = 100f;
    private int _startRoads = 6;

    void Start()
    {
        for (int i = 0; i < _startRoads; i++)
        {
            _spawnInfiniteRoads(Random.Range(0, _roadPrefabs.Length));
        }
    }
    void Update()
    {
        if (_player.position.z - 60 > _spawnPos - (_startRoads * _roadLength))
        {
            _spawnInfiniteRoads(Random.Range(0, _roadPrefabs.Length));
            _destroyRoads();
        }
    }
    private void _spawnInfiniteRoads(int roadsIndex)
    {
        GameObject nextRoad = Instantiate(_roadPrefabs[roadsIndex], transform.forward * _spawnPos, transform.rotation);
        _activeRoads.Add(nextRoad);
        _spawnPos += _roadLength;
    }
    private void _destroyRoads()
    {
        Destroy(_activeRoads[0]);
        _activeRoads.RemoveAt(0);
    }
}
