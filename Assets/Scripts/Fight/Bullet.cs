using System.Collections;
using System.Collections.Generic;
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
    private void Awake()
    {
        transform.Rotate(Vector3.forward * Random.Range(-_offset, _offset));
        GetComponent<Rigidbody2D>().AddForce(transform.up * speed, ForceMode2D.Impulse);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        other.transform.GetComponent<IHurtable>()?.Hurt(_damage);
        BreakBullet(other);
    }
    public abstract void BreakBullet(Collision2D other);
}
