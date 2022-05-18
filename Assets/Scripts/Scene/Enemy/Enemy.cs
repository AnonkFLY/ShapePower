using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IHurtable
{
    [SerializeField] protected int maxHealth = 30;
    [SerializeField] protected int currentHealth = 30;
    [SerializeField] protected float moveSpeed = 3;
    [SerializeField] protected int money = 5;
    [SerializeField] protected float mass = 3;
    protected Transform _target;
    protected Transform _transform;
    protected Rigidbody2D _rig;
    protected SceneBase _scenes;
    protected Animator _animator;
    private void Awake()
    {
        _transform = transform;
        _rig = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
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
        var dir = LookPlayer();
        _rig.velocity = Vector2.Lerp(_rig.velocity, dir, mass * Time.fixedDeltaTime);
    }
    protected Vector3 LookPlayer()
    {
        var dir = (_target.position - _transform.position).normalized * moveSpeed;
        _transform.up = dir.normalized;
        return dir;
    }

    public void Hurt(Damage damage)
    {
        if (damage.originDamage == OriginDamage.Enemy)
            return;
        _animator.Play(UIManager.open);
        currentHealth = Math.Clamp(currentHealth - damage.damageValue, 0, maxHealth);
        if (currentHealth <= 0)
            Dead();
    }
}
