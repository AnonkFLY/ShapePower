using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RectangleBullet : Bullet
{
    [Range(0, 4)]
    [SerializeField]
    private float _addForce;
    [SerializeField]
    private Vector3 _rotation;
    private void Start()
    {
        var child = _transform.GetChild(0);
        onUpdate += () =>
        {
            child.Rotate(_rotation * Time.deltaTime);
        };
    }
    public override void BreakBullet(Collision2D other)
    {
        other.rigidbody.AddForce(_transform.up * _addForce, ForceMode2D.Impulse);
        Destroy(gameObject);
    }
}
