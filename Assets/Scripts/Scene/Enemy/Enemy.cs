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
    protected bool isDead = false;
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
        if (isDead)
        {
            _rig.velocity = Vector2.Lerp(_rig.velocity, Vector2.zero, mass * Time.fixedDeltaTime);
            Debug.Log(_rig.velocity);
            return;
        }
        Behavior();
    }
    public virtual void Dead()
    {
        GameManager.Instance.archiveManager.AddMoney(money);
        isDead = true;
        _animator.Play(UIManager.close);
        GetComponent<Collider2D>().enabled = false;
    }
    public void DestoryThis()
    {
        Destroy(gameObject);
    }
    private void OnDestroy()
    {
        _scenes.EnemyDead();
    }
    public abstract void Behavior();
    protected void MoveToPlayer()
    {
        if (isDead)
            return;
        var dir = LookPlayer();
        _rig.velocity = Vector2.Lerp(_rig.velocity, dir, mass * Time.fixedDeltaTime);
    }
    protected Vector3 LookPlayer()
    {
        if (isDead)
            return _transform.up;
        var dir = (_target.position - _transform.position).normalized * moveSpeed;
        _transform.up = dir.normalized;
        return dir;
    }

    public void Hurt(Damage damage)
    {
        if (damage.originDamage == OriginDamage.Enemy)
            return;
        if (!isPlay)
        {
            AudioManager.Instance.PlaySoundEffect(7);
            isPlay = true;
            StartCoroutine(Delay());
        }
        _animator.Play(UIManager.open);
        currentHealth = Math.Clamp(currentHealth - damage.damageValue, 0, maxHealth);
        if (currentHealth <= 0)
            Dead();
    }
    private bool isPlay;
    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.3f);
        isPlay = false;
    }
}
