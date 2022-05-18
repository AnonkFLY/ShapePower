using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarSubBullet : Bullet
{
    [SerializeField]
    private Transform _target;
    [SerializeField]
    private float _rotationSpeed = 3f;
    private void Start()
    {
        LockTarget();
        onUpdate += Track;
        Destroy(gameObject, 2f);
    }
    private void LockTarget()
    {
        _target = RayUtil.FindObjOfLate(_transform.position, 5, 1 << 6);
    }
    private void Track()
    {
        if (_target == null)
        {
            LockTarget();
            if (_target == null)
                onUpdate -= Track;
            return;
        }
        OnRotate();
    }
    private void OnRotate()
    {
        _transform.up = Vector3.Lerp(_transform.up, _target.position - _transform.position, _rotationSpeed * Time.deltaTime);
    }
    public override void BreakBullet(Collision2D collision, Collider2D trigger)
    {
        Destroy(gameObject);
    }
}
