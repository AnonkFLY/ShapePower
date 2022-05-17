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

    private void Awake()
    {
        // _win = transform.Find("Win").gameObject;
        // _lose = transform.Find("Lose").gameObject;
        // _win.SetActive(false);
        // _lose.SetActive(false);
        var list = GetComponentsInChildren<SpawnPoint>();
        _spawnPoints = list.Length;
        for (int i = 0; i < list.Length; ++i)
        {
            _enemyCout += list[i].GetCout();
            list[i].onOver += () =>
            {
                --_spawnPoints;
                if (_spawnPoints <= 0)
                {
                    Win();
                }
            };
        }
    }
    public void EnemyDead()
    {
        --_enemyCout;
        onEnemyDead?.Invoke(_enemyCout);
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
    public void Lose()
    {
        GameManager.Instance.uiManager.OpenUI(UIType.GameLose, true);
    }
}
