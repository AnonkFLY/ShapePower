using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StarBullet2 : Bullet
{
    private Animator _animator;
    private void Start()
    {
        _animator = GetComponent<Animator>();
    }
    public override void BreakBullet(Collision2D collision, Collider2D trigger)
    {
        Boom();
        _animator.Play(UIManager.open);
    }

    private void Boom()
    {
        speed = 0;
        // var hitObjs = Physics2D.CircleCastAll(_transform.position, boomRadius, Vector2.zero, 0, _layer);
        // foreach (var item in hitObjs)
        // {
        //     var dir = item.transform.position - _transform.position;
        //     var force = boomRadius / (boomRadius - dir.sqrMagnitude) * dir.normalized;
        //     item.rigidbody.AddForce(force * _addForce * speed *boomForce, ForceMode2D.Impulse);
        //     item.transform.GetComponent<IHurtable>().Hurt(boomDamage);
        //     DebugLog.Message($"对{item.transform.name}进行爆炸");
        // }
    }
    public void DestroyThis()
    {
        Destroy(gameObject);
    }
}
