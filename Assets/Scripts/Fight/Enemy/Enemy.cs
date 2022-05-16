using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IHurtable
{
    [SerializeField] protected int maxHealth = 30;
    [SerializeField] protected int currentHealth = 30;
    [SerializeField] protected float moveSpeed = 3;
    protected Transform _target;
    protected Transform _transform;
    protected Rigidbody2D _rig;
    private void Awake()
    {
        _transform = transform;
        _rig = GetComponent<Rigidbody2D>();
        _target = FindObjectOfType<PlayerController>().transform;
    }
    private void FixedUpdate()
    {
        Behavior();
    }
    public abstract void Behavior();
    protected void MoveToPlayer()
    {
        var dir = (_target.position - _transform.position).normalized * moveSpeed;
        _rig.velocity = dir;
        _transform.eulerAngles = new Vector3(0,0,Vector2.Angle(Vector2.up,dir));
        Debug.DrawRay(_transform.position,dir*3);
    }

    public void Hurt(Damage damage)
    {
        print("Be Hurt");
        if (damage.originDamage == OriginDamage.Enemy)
            return;
        currentHealth = Math.Clamp(currentHealth - damage.damageValue, 0, maxHealth);
    }
}
