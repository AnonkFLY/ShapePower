using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyGroup
{
    [SerializeField]
    private GameObject enemy;
    [SerializeField]
    public int count = 1;
    [SerializeField]
    public float random = 4;
    public void CreateEnemy(Transform pointTrans)
    {
        var x = Random.Range(-random, random);
        var y = Random.Range(-random, random);
        GameObject.Instantiate(enemy, pointTrans).transform.Translate(new Vector3(x, y, 0));
        --count;
        DebugLog.Message($"Create {enemy.name}");
    }
}
