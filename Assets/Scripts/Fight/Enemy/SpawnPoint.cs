using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private List<EnemyGroup> _enemyGroups;
    [SerializeField] private bool isRandom = false;
    [SerializeField] private float delay = 3;
    public bool over;
    private Transform _transform;
    private void Awake()
    {
        _transform = transform;
        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        var wait = new WaitForSeconds(delay);
        while (_enemyGroups.Count > 0)
        {
            yield return wait;
            if (isRandom)
            {
                var r = UnityEngine.Random.Range(0, _enemyGroups.Count);
                _enemyGroups[r].CreateEnemy(_transform);
                if (_enemyGroups[r].count <= 0)
                {
                    RemoveListAt<EnemyGroup>(_enemyGroups, r);
                }
            }
            else
            {
                _enemyGroups[0].CreateEnemy(_transform);
                if (_enemyGroups[0].count <= 0)
                {
                    RemoveListAt<EnemyGroup>(_enemyGroups, 0);
                }
            }
        }
    }
    private void RemoveListAt<T>(List<T> list, int index)
    {
        list[index] = list[list.Count - 1];
        list.RemoveAt(list.Count - 1);
    }
}
