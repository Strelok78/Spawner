using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy[] _enemyTemplates;
    [SerializeField] private Spawner[] _spawnPoints;

    private Queue<GameObject> _clones;
    private WaitForSeconds _spawnPause;
    private WaitForSeconds _removePause;


    private void Awake()
    {
        _clones = new Queue<GameObject>();
        _spawnPause = new WaitForSeconds(2);
        _removePause = new WaitForSeconds(7);
    }

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
        StartCoroutine(RemoveEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            int i = 0;

            foreach (var point in _spawnPoints)
            {                
                var clone = Instantiate(_enemyTemplates[i++].gameObject, point.gameObject.transform);
                _clones.Enqueue(clone);
                yield return _spawnPause;
            }
        }
    }

    private IEnumerator RemoveEnemies()
    {
        yield return _removePause;

        while (true)
        {
                Destroy(_clones.Dequeue());
                yield return _spawnPause;
        }
    }
}
