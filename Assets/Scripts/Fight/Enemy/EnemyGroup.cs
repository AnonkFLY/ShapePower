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
    public void CreateEnemy(Transform pointTrans)
    {
        var x = Random.Range(-5,5);
        var y = Random.Range(-5,5);
        GameObject.Instantiate(enemy,pointTrans).transform.Translate(new Vector3(x,y,0));
        DebugLog.Message($"Create {enemy.name}");
    }
}
