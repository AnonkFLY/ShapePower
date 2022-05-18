using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireVirus : Enemy
{
    [SerializeField]
    GameObject bullet;
    [SerializeField]
    private float timer = 0.5f;

    [SerializeField]
    private float randomTimer = 1;
    [SerializeField]
    private float speedLeft = 0.6f;
    private bool state;
    private IEnumerator Start()
    {
        var _transform = transform;
        var firePos = _transform.Find("FirePos");
        int r;
        while (true)
        {
            Instantiate(bullet, firePos.position, _transform.rotation).GetComponent<Bullet>().Init(2000);
            r = Random.Range(0, 4);
            if (r <= 1)
                state = !state;
            yield return new WaitForSeconds(timer + randomTimer);
        }
    }
    public override void Behavior()
    {
        Debug.DrawRay(_transform.position, _transform.up);
        if (Vector2.Distance(_transform.position, _target.position) > 5)
            MoveToPlayer();
        else
        {
            LookPlayer();
            _transform.Translate((state ? Vector2.right : -Vector2.right) * (Time.fixedDeltaTime * moveSpeed * speedLeft));
        }
    }
    [SerializeField]
    private Damage damage;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.transform.CompareTag("Player"))
            return;
        other.transform.GetComponent<IHurtable>()?.Hurt(damage);
        other.transform.GetComponent<PlayerController>().AddForce(_transform.up);
        Destroy(gameObject);

    }

}
