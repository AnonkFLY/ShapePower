using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    [SerializeField]
    private Damage _damage;
    public float _delayTimer = 0.3f;
    [SerializeField]
    private float speed = 100;
    [SerializeField]
    private float _offset = 10;
    [SerializeField]
    private float _recoil = 0.1f;
    private Transform _transform;

    public float Recoil { get => _recoil;}

    private void Awake()
    {
        _transform = transform;
    }
    public void Init(float eluer)
    {
        _transform.Rotate(0, 0, eluer, Space.Self);
        _transform.Rotate(Vector3.forward * Random.Range(-_offset, _offset));
        Destroy(gameObject, 3f);
    }
    void Update()
    {
        _transform.Translate(_transform.up * speed * Time.deltaTime, Space.World);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        other.transform.GetComponent<IHurtable>()?.Hurt(_damage);
        other.rigidbody?.AddForce(_transform.up * speed, ForceMode2D.Impulse);
        BreakBullet(other);
    }
    public abstract void BreakBullet(Collision2D other);
}
