using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CameraMananger : MonoBehaviour
{
    [SerializeField]
    private float smoothTimer = 10;
    private Transform _lockTrans;
    private Transform _transform;
    private Vector3 _offset;
    private Tweener _shake;
    private void Awake()
    {
        _transform = transform;
        _offset = _transform.position;
        _shake = _transform.DOShakePosition(-1, new Vector3(0.1f, 0.1f, 0));
        _shake.Pause();

    }
    public void SetLockTrans(Transform trans)
    {
        _lockTrans = trans;
    }
    public void StartShakeCamera()
    {
        _shake.Play();

    }
    public void StopShakeCamera()
    {
        _shake.Pause();
    }

    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            _transform.DOShakePosition(100, new Vector3(0.1f, 0.1f, 0));
        }
        if (_lockTrans == null)
            return;
        var over = _lockTrans.position + _offset;
        //_transform.position = Vector3.SmoothDamp(_transform.position, over,ref currentSpeed,smoothTimer,20,Time.deltaTime);
        _transform.position = Vector3.Lerp(_transform.position, over, Time.deltaTime * smoothTimer);
    }
}
