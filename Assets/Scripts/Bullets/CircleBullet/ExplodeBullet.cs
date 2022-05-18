using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeBullet : MonoBehaviour
{
    [SerializeField]
    private Damage boomDamage;
    [SerializeField]
    private float boomRadius = 5;
    [SerializeField]
    private float boomForce = 10;
    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
        _transform = transform;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        var dir = other.transform.position - _transform.position;
        var force = boomRadius / (boomRadius - dir.sqrMagnitude) * dir.normalized;
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().AddForce(force * boomForce);
        }
        other.GetComponent<Rigidbody2D>()?.AddForce(force * boomForce, ForceMode2D.Impulse);
        other.GetComponent<IHurtable>().Hurt(boomDamage);
        Debug.DrawRay(_transform.position, dir);
    }
}
