using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMananger : MonoBehaviour
{
    [SerializeField]
    private float smoothTimer = 10;
    private Transform _lockTrans;
    private Transform _transform;
    private Vector3 _offset;
    private void Awake()
    {
        _transform = transform;
        _offset = _transform.position;
    }
    public void SetLockTrans(Transform trans)
    {
        _lockTrans = trans;
    }

    private void LateUpdate()
    {
        if (_lockTrans == null)
            return;
        var over = _lockTrans.position + _offset;
        //_transform.position = Vector3.SmoothDamp(_transform.position, over,ref currentSpeed,smoothTimer,20,Time.deltaTime);
        _transform.position = Vector3.Lerp(_transform.position, over, Time.deltaTime * smoothTimer);
    }
}
