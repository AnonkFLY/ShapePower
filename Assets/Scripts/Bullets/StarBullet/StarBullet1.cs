using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarBullet1 : Bullet
{
    [SerializeField]
    private Transform[] subPoints;
    [SerializeField]
    private GameObject subBullet;
    [SerializeField]
    private Transform _target;
    [SerializeField]
    private float _rotationSpeed = 0.1f;

    private void Start()
    {
        LockTarget();
        onUpdate += Track;
        onTrigger += GenerateSubBullet;
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
            return;
        }
        OnRotate();
    }
    private void OnRotate()
    {
        _transform.up = Vector3.Lerp(_transform.up, _target.position - _transform.position, _rotationSpeed * Time.deltaTime);
        
    }
    private void GenerateSubBullet()
    {
        for (int i = 0; i < subPoints.Length; ++i)
        {
            Instantiate(subBullet, subPoints[i].position, subPoints[i].rotation);
        }
    }
    public override void BreakBullet(Collision2D collision, Collider2D trigger)
    {

        Destroy(gameObject);
    }
}
