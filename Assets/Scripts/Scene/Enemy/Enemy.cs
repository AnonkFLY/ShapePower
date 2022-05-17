using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IHurtable
{
    [SerializeField] protected int maxHealth = 30;
    [SerializeField] protected int currentHealth = 30;
    [SerializeField] protected float moveSpeed = 3;
    [SerializeField] protected int money = 5;
    protected Transform _target;
    protected Transform _transform;
    protected Rigidbody2D _rig;
    protected SceneBase _scenes;
    private void Awake()
    {
        _transform = transform;
        _rig = GetComponent<Rigidbody2D>();
        _target = FindObjectOfType<PlayerController>().transform;
        _scenes = FindObjectOfType<SceneBase>();
    }
    private void FixedUpdate()
    {
        Behavior();
    }
    public virtual void Dead()
    {
        GameManager.Instance.archiveManager.AddMoney(money);
        _scenes.EnemyDead();
        Destroy(gameObject);
    }
    public abstract void Behavior();
    protected void MoveToPlayer()
    {
        var dir = (_target.position - _transform.position).normalized * moveSpeed;
        _rig.velocity = dir;
        _transform.eulerAngles = new Vector3(0, 0, Vector2.Angle(Vector2.up, dir));
    }

    public void Hurt(Damage damage)
    {
        if (damage.originDamage == OriginDamage.Enemy)
            return;
        print("Be Hurt");
        currentHealth = Math.Clamp(currentHealth - damage.damageValue, 0, maxHealth);
        if(currentHealth<=0)
            Dead();
    }
}
