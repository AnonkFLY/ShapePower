using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankVirus : VirusEnemy
{
    [SerializeField]
    GameObject bullet;
    [SerializeField]
    private float timer = 2;
    [SerializeField]
    private float randomTimer = 3;
    private IEnumerator Start()
    {
        var _transform = transform;
        var firePos = _transform.Find("FirePos");
        while (true)
        {
            if(isDead)
                break;
            Instantiate(bullet, firePos.position, _transform.rotation).GetComponent<Bullet>().Init(2000);
            yield return new WaitForSeconds(timer + randomTimer);
        }
    }
}
