using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputButton : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private Transform _transform;
    private Vector3 _offset;
    private Vector3 _originPos;
    [Range(50, 200)]
    [SerializeField]
    private float maxDistance = 100;
    public Action<Vector2> onDrag;
    private void Awake()
    {
        _transform = transform;
        _originPos = _transform.position;
    }

    void FixedUpdate()
    {
        onDrag?.Invoke(_offset/maxDistance);
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        //_offset = _transform.position - Input.mousePosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (Time.timeScale <= 0)
            return;
        _offset = Input.mousePosition - _originPos;
        if (Vector3.Distance(Input.mousePosition, _originPos) > maxDistance)
        {
            _offset = _offset.normalized * maxDistance;
        }
        _transform.position = _originPos + _offset;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _offset = Vector3.zero;
        _transform.DOMove(_originPos, 0.1f);
    }
}
