using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(GameObject))]
public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private GameObject[] _enemies;
    [SerializeField] private Transform[] _spawnPoints;

    private Queue<GameObject> _clones;

    private void Awake()
    {
        _clones = new Queue<GameObject>();
    }

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
        StartCoroutine(RemoveEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            int i = 0;

            foreach (Transform t in _spawnPoints)
            {
                Debug.Log($"Spawn {_enemies[i].name} at {t.position}");
                _clones.Enqueue(Instantiate(_enemies[i++], t));
                yield return new WaitForSeconds(2);
            }
        }
    }

    IEnumerator RemoveEnemies()
    {
        yield return new WaitForSeconds(7);

        while (true)
        {
                Destroy(_clones.Dequeue());
                yield return new WaitForSeconds(2);
        }
    }
}
