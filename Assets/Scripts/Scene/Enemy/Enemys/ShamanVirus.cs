using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShamanVirus : Enemy
{
    [SerializeField] Damage damage;
    [SerializeField] private int count = 3;
    [SerializeField] private float delay = 10;
    [SerializeField] private Transform[] summonPoint;
    [SerializeField] private GameObject summonObject;
    private List<GameObject> _objs;
    private SceneBase _scene;
    private IEnumerator Start()
    {
        _scene = FindObjectOfType<SceneBase>();
        var wait = new WaitForSeconds(delay);
        while (true)
        {
            yield return wait;
            CreateChilds(count);
            if (isDead)
                break;
        }
    }
    private void CreateChilds(int i)
    {
        if (summonObject == null)
            return;
        StartCoroutine(CreateDelay(i));
    }
    private IEnumerator CreateDelay(int i)
    {
        while (i > 0)
        {
            CreateChild();
            --i;
            yield return new WaitForSeconds(0.3f);
        }
    }
    private void CreateChild()
    {
        if (summonObject == null)
            return;
        var r = UnityEngine.Random.Range(0, summonPoint.Length);
        var obj = Instantiate(summonObject, _transform);
        obj.transform.position = summonPoint[r].position;
        _scene.AddEnemyCount();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.transform.CompareTag("Player"))
            return;
        other.transform.GetComponent<IHurtable>()?.Hurt(damage);
        other.transform.GetComponent<PlayerController>().AddForce(_transform.up);
        Destroy(gameObject);
    }
    private bool state;
    public override void Behavior()
    {
        if (Vector2.Distance(_transform.position, _target.position) > 5)
            MoveToPlayer();
        else
        {
            LookPlayer();
            _transform.Translate(Vector2.right * (Time.fixedDeltaTime * moveSpeed));
        }
    }
}
