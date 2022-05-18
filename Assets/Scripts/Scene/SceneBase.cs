using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class SceneBase : MonoBehaviour
{
    private int _spawnPoints;
    private int _enemyCout;
    private event Action<int> onEnemyDead;
    private GameObject _spawns;

    private void Awake()
    {
        // _win = transform.Find("Win").gameObject;
        // _lose = transform.Find("Lose").gameObject;
        // _win.SetActive(false);
        // _lose.SetActive(false);
        _spawns = transform.Find("SpawnPoints").gameObject;
        var list = GetComponentsInChildren<SpawnPoint>();
        _spawnPoints = list.Length;
        for (int i = 0; i < list.Length; ++i)
        {
            _enemyCout += list[i].GetCout();
            // list[i].onOver += () =>
            // {
            //     --_spawnPoints;
            //     if (_spawnPoints <= 0)
            //     {
            //         Win();
            //     }
            // };
        }
    }
    public void EnemyDead()
    {
        --_enemyCout;
        _enemyCout = Math.Clamp(_enemyCout, 0, 9999);
        onEnemyDead?.Invoke(_enemyCout);
        if (_enemyCout <= 0)
        {
            Destroy(_spawns);
            Win();
        }
    }
    public void RegisterEnemyCount(Action<int> eventCall)
    {
        onEnemyDead += eventCall;
        onEnemyDead?.Invoke(_enemyCout);
    }

    private void Win()
    {
        GameManager.Instance.uiManager.OpenUI(UIType.GameWin, true);
    }

}
