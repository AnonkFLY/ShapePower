using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private Color color = Color.white;
    [SerializeField] private List<EnemyGroup> _enemyGroups;
    [SerializeField] private bool isRandom = false;
    [SerializeField] private float delay = 3;
    [SerializeField] private bool isOver = false;
    public Action onOver;
    private Transform _transform;
    private void Awake()
    {
        _transform = transform;
        StartCoroutine(SpawnEnemy());
    }
    public int GetCout()
    {
        int i = 0;
        foreach (var item in _enemyGroups)
        {
            i += item.count;
        }
        return i;
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
                CreateEnemys(r);
            }
            else
            {
                CreateEnemys(0);
            }
        }
        while (_transform.childCount > 0)
        {
            yield return delay;
        }
        onOver?.Invoke();
    }
    private void CreateEnemys(int i)
    {
        if (!isOver)
        {
            _enemyGroups[i].CreateEnemy(_transform);
            if (_enemyGroups[i].count <= 0)
            {
                RemoveListAt<EnemyGroup>(_enemyGroups, i);
            }
        }
        else
        {
            while (_enemyGroups[i].count > 0)
            {
                _enemyGroups[i].CreateEnemy(_transform);
            }
            RemoveListAt<EnemyGroup>(_enemyGroups, i);
        }
    }
    private void RemoveListAt<T>(List<T> list, int index)
    {
        list[index] = list[list.Count - 1];
        list.RemoveAt(list.Count - 1);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = color;
        if (_enemyGroups == null)
            return;
        foreach (var item in _enemyGroups)
        {
            Gizmos.DrawWireSphere(transform.position, item.random);
        }
    }
}
